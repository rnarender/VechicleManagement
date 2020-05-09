using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using vega.Core;
using vega.Models;
using Vega.Core.Models;
using Vega.Extensions;

namespace vega.Persistence
{
	public class VehicleRepository : IVehicleRepository
	{
		private readonly VegaDbContext vegaDbContext;

		public VehicleRepository(VegaDbContext vegaDbContext)
		{
			this.vegaDbContext = vegaDbContext;
		}
		public async Task<Vehicle> GetVechicle(int Id, bool IncludeRelated = true)
		{
			if (!IncludeRelated)
				return await vegaDbContext.Vehicles.FindAsync(Id);

			return await vegaDbContext.Vehicles
						.Include(v => v.Features)
							.ThenInclude(vf => vf.Feature)
						.Include(v => v.Model)
							.ThenInclude(m => m.Make)
						.SingleOrDefaultAsync(v => v.Id == Id);
		}

		public void Add(Vehicle vehicle)
		{
			vegaDbContext.Add(vehicle);
		}

		public void Remove(Vehicle vehicle)
		{
			vegaDbContext.Remove(vehicle);
		}

		public async Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery queryObj)
		{
			var result = new QueryResult<Vehicle>();

			var query = vegaDbContext.Vehicles
			  .Include(v => v.Model)
				.ThenInclude(m => m.Make)
			  .Include(v => v.Features)
				.ThenInclude(vf => vf.Feature)
				.AsQueryable();

			if (queryObj.MakeId.HasValue)
				query = query.Where(v => v.Model.MakeId == queryObj.MakeId.Value);

			if (queryObj.ModelId.HasValue)
				query = query.Where(v => v.ModelId == queryObj.ModelId.Value);


			var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
			{
				["make"] = v =>v.Model.Make.Name,
				["model"] = v =>v.Model.Name,
				["contactName"] = v =>v.ContactName
			};

			query = query.ApplyOrdering(queryObj, columnsMap);

			result.TotalItems = await query.CountAsync();

			query = query.ApplyPaging(queryObj);

			result.Items =  await query.ToListAsync();

			return result;
		}

		//private IQueryable<Vehicle> ApplyOrdering( VehicleQuery queryObj, IQueryable<Vehicle> query,  Dictionary<string, Expression<Func<Vehicle, object>>> columnsMap)
		//{
		//	if (queryObj.IsSortAscending)
		//		return	query = query.OrderBy(columnsMap[queryObj.SortBy]);
		//	else
		//		return query = query.OrderByDescending(columnsMap[queryObj.SortBy]);

		//}
		//public async Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery queryObj)
		//{
		//	var result = new QueryResult<Vehicle>();

		//	var query = vegaDbContext.Vehicles
		//	  .Include(v => v.Model)
		//		.ThenInclude(m => m.Make)
		//	  .Include(v => v.Features)
		//		.ThenInclude(vf => vf.Feature)
		//	  .AsQueryable();

		//	if (queryObj.MakeId.HasValue)
		//		query = query.Where(v => v.Model.MakeId == queryObj.MakeId.Value);

		//	if (queryObj.ModelId.HasValue)
		//		query = query.Where(v => v.ModelId == queryObj.ModelId.Value);

		//	var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
		//	{
		//		["make"] = v => v.Model.Make.Name,
		//		["model"] = v => v.Model.Name,
		//		["contactName"] = v => v.ContactName
		//	};

		//	query = query.ApplyOrdering(queryObj, columnsMap);

		//	result.TotalItems = await query.CountAsync();

		//	query = query.ApplyPaging(queryObj);

		//	result.Items = await query.ToListAsync();

		//	return result;
		//}
	}
}
