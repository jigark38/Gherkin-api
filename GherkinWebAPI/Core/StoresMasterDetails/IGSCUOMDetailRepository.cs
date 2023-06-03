using GherkinWebAPI.Models.StoresMasterDetails;
using System.Collections.Generic;
using System.Threading.Tasks;
using GherkinWebAPI.Utilities;
using GherkinWebAPI.Models;

namespace GherkinWebAPI.Core.StoresMasterDetails
{
	public interface IGSCUOMDetailRepository
	{
		Task<ApiResponse<List<GSCUomDetails>>> GetStoresMasterUOMByName(string uomName);

		Task<ApiResponse<GSCUomDetails>> SaveStoresMasterUOM(GSCUomDetails uomDetail);

		Task<ApiResponse<bool>> IsStoresMasterUOMExists(string uomName);
	}
}