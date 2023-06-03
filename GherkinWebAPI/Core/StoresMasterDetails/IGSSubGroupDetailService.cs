using GherkinWebAPI.Models.StoresMasterDetails;
using System.Collections.Generic;
using System.Threading.Tasks;
using GherkinWebAPI.Utilities;

namespace GherkinWebAPI.Core.StoresMasterDetails
{
	public interface IGSSubGroupDetailService
	{
		Task<ApiResponse<List<GSSubGroupDetail>>> GetStoresMasterSubGroupByName(string subGroupName, int groupCode);

		Task<ApiResponse<GSSubGroupDetail>> SaveStoresMasterSubGroup(GSSubGroupDetail subGroupDetail);

		Task<ApiResponse<bool>> IsStoresMasterSubGroupExists(string subGroupName, int groupCode);
	}
}