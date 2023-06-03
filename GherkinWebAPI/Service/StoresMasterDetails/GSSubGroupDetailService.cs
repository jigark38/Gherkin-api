using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GherkinWebAPI.Core;
using GherkinWebAPI.Core.StoresMasterDetails;
using GherkinWebAPI.Models.StoresMasterDetails;
using GherkinWebAPI.Utilities;

namespace GherkinWebAPI.Service.StoresMasterDetails
{
	public class GSSubGroupDetailService : IGSSubGroupDetailService
	{
		private IRepositoryWrapper repositoryWrapper;

		public GSSubGroupDetailService(IRepositoryWrapper _repositoryWrapper)
		{
			repositoryWrapper = _repositoryWrapper;
		}

		public async Task<ApiResponse<List<GSSubGroupDetail>>> GetStoresMasterSubGroupByName(string subGroupName, int groupCode)
		{
			return await repositoryWrapper.GSSubGroupDetailRepository.GetStoresMasterSubGroupByName(subGroupName, groupCode);
		}

		public async Task<ApiResponse<bool>> IsStoresMasterSubGroupExists(string subGroupName, int groupCode)
		{
			return await repositoryWrapper.GSSubGroupDetailRepository.IsStoresMasterSubGroupExists(subGroupName, groupCode);
		}

		public async Task<ApiResponse<GSSubGroupDetail>> SaveStoresMasterSubGroup(GSSubGroupDetail subGroupDetail)
		{
			return await repositoryWrapper.GSSubGroupDetailRepository.SaveStoresMasterSubGroup(subGroupDetail);
		}
	}
}