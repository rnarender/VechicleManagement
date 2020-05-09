using System.Collections.Generic;
using System.Threading.Tasks;
using vega.Models;
using Vega.Core.Models;

namespace vega.Core
{
	public interface IVehicleRepository
	{
		Task<Vehicle> GetVechicle(int Id, bool IncluceRelated = true);
		void Add(Vehicle vehicle);
		void Remove(Vehicle vehicle);
		Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery filter);
		//Task<IEnumerable<Vehicle>> GetVehicles(VehicleQuery filter);

	}
}