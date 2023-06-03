using GherkinWebAPI.Models.StoresMasterDetails;
using System.Collections.Generic;
using System.Threading.Tasks;
using GherkinWebAPI.Utilities;

namespace GherkinWebAPI.Core.StoresMasterDetails
{
	public interface IGSGroupDetailService
	{
		Task<ApiResponse<List<GSGroupDetail>>> GetStoresMasterGroupByName(string groupName);

		Task<ApiResponse<GSGroupDetail>> SaveStoresMasterGroup(GSGroupDetail groupDetail);

		Task<ApiResponse<bool>> IsStoresMasterGroupExists(string groupName);
	}
}