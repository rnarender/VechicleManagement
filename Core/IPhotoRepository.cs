using System.Collections.Generic;
using System.Threading.Tasks;
using vega.Models;
using Vega.Core.Models;

namespace vega.Core
{
	public interface IPhotoRepository
	{
		Task<IEnumerable<Photo>> GetPhotos(int vehicleId);

	}
}