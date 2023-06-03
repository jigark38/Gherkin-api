using GherkinWebAPI.Core;
using GherkinWebAPI.Core.MaterialIndentByDepartment;
using GherkinWebAPI.Models.MaterialIndentByDepartment;
using GherkinWebAPI.Models.StoresMasterDetails;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Service.MaterialIndentByDepartment
{
	public class StoreInternalIndentMasterService : IStoreInternalIndentMasterService
	{
		private IRepositoryWrapper repositoryWrapper;

		public StoreInternalIndentMasterService(IRepositoryWrapper _repositoryWrapper)
		{
			repositoryWrapper = _repositoryWrapper;
		}

		public async Task<ApiResponse<List<GSGroupDetail>>> GetGroupNameList()
		{
			return await repositoryWrapper.StoreInternalIndentMasterRepository.GetGroupNameList();
		}

		public async Task<ApiResponse<List<StoreInternalIndentMaster>>> GetMaterialIndentListByIndentDate(DateTime indentDate)
		{
			return await repositoryWrapper.StoreInternalIndentMasterRepository.GetMaterialIndentListByIndentDate(indentDate);
		}

		public async Task<ApiResponse<List<GSMaterialDetail>>> GetMaterialListByGroupSubGroupCode(int groupCode, int subGroupCode)
		{
			return await repositoryWrapper.StoreInternalIndentMasterRepository.GetMaterialListByGroupSubGroupCode(groupCode, subGroupCode);
		}

		public async Task<ApiResponse<List<GSSubGroupDetail>>> GetSubGroupNameListByGroupCode(int groupCode)
		{
			return await repositoryWrapper.StoreInternalIndentMasterRepository.GetSubGroupNameListByGroupCode(groupCode);
		}

		public async Task<ApiResponse<StoreInternalIndentMaster>> SaveMaterialIndentMaster(StoreInternalIndentMaster storeInternalIndentMaster)
		{
			return await repositoryWrapper.StoreInternalIndentMasterRepository.SaveMaterialIndentMaster(storeInternalIndentMaster);
		}
	}
}