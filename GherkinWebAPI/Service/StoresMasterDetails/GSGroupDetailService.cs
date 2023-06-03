using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GherkinWebAPI.Core;
using GherkinWebAPI.Core.StoresMasterDetails;
using GherkinWebAPI.Models.StoresMasterDetails;
using GherkinWebAPI.Utilities;

namespace GherkinWebAPI.Service.StoresMasterDetails
{
	public class GSGroupDetailService : IGSGroupDetailService
	{
		private IRepositoryWrapper repositoryWrapper;

		public GSGroupDetailService(IRepositoryWrapper _repositoryWrapper)
		{
			repositoryWrapper = _repositoryWrapper;
		}

		public async Task<ApiResponse<List<GSGroupDetail>>> GetStoresMasterGroupByName(string groupName)
		{
			return await repositoryWrapper.GSGroupDetailRepository.GetStoresMasterGroupByName(groupName);
		}

		public async Task<ApiResponse<bool>> IsStoresMasterGroupExists(string groupName)
		{
			return await repositoryWrapper.GSGroupDetailRepository.IsStoresMasterGroupExists(groupName);
		}

		public async Task<ApiResponse<GSGroupDetail>> SaveStoresMasterGroup(GSGroupDetail groupDetail)
		{
			return await repositoryWrapper.GSGroupDetailRepository.SaveStoresMasterGroup(groupDetail);
		}
	}
}