using GherkinWebAPI.Models.StoresMasterDetails;
using System.Collections.Generic;
using System.Threading.Tasks;
using GherkinWebAPI.Utilities;

namespace GherkinWebAPI.Core.StoresMasterDetails
{
	public interface IGSMaterialDetailService
	{
		Task<ApiResponse<List<GSMaterialDetail>>> GetStoresMasterMaterialByName(string materialName, int groupCode, int subGroupCode);

		Task<ApiResponse<GSMaterialDetail>> SaveStoresMasterMaterial(GSMaterialDetail materialDetail);

		Task<ApiResponse<bool>> IsStoresMasterMaterialExists(string materialName);
	}
}