using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GherkinWebAPI.Core;
using GherkinWebAPI.Core.StoresMasterDetails;
using GherkinWebAPI.Models.StoresMasterDetails;
using GherkinWebAPI.Utilities;

namespace GherkinWebAPI.Service.StoresMasterDetails
{
	public class GSMaterialDetailService : IGSMaterialDetailService
	{
		private IRepositoryWrapper repositoryWrapper;

		public GSMaterialDetailService(IRepositoryWrapper _repositoryWrapper)
		{
			repositoryWrapper = _repositoryWrapper;
		}

		public async Task<ApiResponse<List<GSMaterialDetail>>> GetStoresMasterMaterialByName(string materialName, int groupCode, int subGroupCode)
		{
			return await repositoryWrapper.GSMaterialDetailRepository.GetStoresMasterMaterialByName(materialName, groupCode, subGroupCode);
		}

		public async Task<ApiResponse<bool>> IsStoresMasterMaterialExists(string materialName)
		{
			return await repositoryWrapper.GSMaterialDetailRepository.IsStoresMasterMaterialExists(materialName);
		}

		public async Task<ApiResponse<GSMaterialDetail>> SaveStoresMasterMaterial(GSMaterialDetail materialDetail)
		{
			return await repositoryWrapper.GSMaterialDetailRepository.SaveStoresMasterMaterial(materialDetail);
		}
	}
}