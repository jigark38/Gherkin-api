using GherkinWebAPI.Core;
using GherkinWebAPI.Core.MaterialIndentByDepartment;
using GherkinWebAPI.Models.MaterialIndentByDepartment;
using GherkinWebAPI.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Service.MaterialIndentByDepartment
{
	public class StoreInternalIndentDetailService : IStoreInternalIndentDetailService
	{
		private IRepositoryWrapper repositoryWrapper;

		public StoreInternalIndentDetailService(IRepositoryWrapper _repositoryWrapper)
		{
			repositoryWrapper = _repositoryWrapper;
		}

		public async Task<ApiResponse<List<StoreInternalIndentDetail>>> GetMaterialIndentDetailByIndentNo(string indentNo)
		{
			return await repositoryWrapper.StoreInternalIndentDetailRepository.GetMaterialIndentDetailByIndentNo(indentNo);
		}

		public async Task<ApiResponse<StoreInternalIndentDetail>> SaveMaterialIndentDetail(StoreInternalIndentDetail storeInternalIndentDetail)
		{
			return await repositoryWrapper.StoreInternalIndentDetailRepository.SaveMaterialIndentDetail(storeInternalIndentDetail);
		}
	}
}