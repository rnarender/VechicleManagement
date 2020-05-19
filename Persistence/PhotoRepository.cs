using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vega.Core;
using Microsoft.EntityFrameworkCore;
using vega.Models;
using vega.Persistence;
using Vega.Core.Models;
using Vega.Extensions;

namespace Vega.Persistence
{
	public class PhotoRepository : IPhotoRepository
	{
		private readonly VegaDbContext context;
		public PhotoRepository(VegaDbContext context)
		{
			this.context = context;
		}
		public async Task<IEnumerable<Photo>> GetPhotos(int vehicleId)
		{
			return await context.Photos
			  .Where(p => p.VehicleId == vehicleId)
			  .ToListAsync();
		}
	}
}
