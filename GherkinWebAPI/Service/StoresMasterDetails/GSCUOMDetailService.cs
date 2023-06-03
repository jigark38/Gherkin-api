using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GherkinWebAPI.Core;
using GherkinWebAPI.Core.StoresMasterDetails;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.StoresMasterDetails;
using GherkinWebAPI.Utilities;

namespace GherkinWebAPI.Service.StoresMasterDetails
{
	public class GSCUOMDetailService : IGSCUOMDetailService
	{
		private IRepositoryWrapper repositoryWrapper;

		public GSCUOMDetailService(IRepositoryWrapper _repositoryWrapper)
		{
			repositoryWrapper = _repositoryWrapper;
		}

		public async Task<ApiResponse<List<GSCUomDetails>>> GetStoresMasterUOMByName(string uomName)
		{
			return await repositoryWrapper.GSCUOMDetailRepository.GetStoresMasterUOMByName(uomName);
		}

		public async Task<ApiResponse<bool>> IsStoresMasterUOMExists(string uomName)
		{
			return await repositoryWrapper.GSCUOMDetailRepository.IsStoresMasterUOMExists(uomName);
		}

		public async Task<ApiResponse<GSCUomDetails>> SaveStoresMasterUOM(GSCUomDetails uomDetail)
		{
			return await repositoryWrapper.GSCUOMDetailRepository.SaveStoresMasterUOM(uomDetail);
		}
	}
}