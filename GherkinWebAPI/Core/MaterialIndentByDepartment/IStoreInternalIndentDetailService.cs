using System.Collections.Generic;
using System.Threading.Tasks;
using GherkinWebAPI.Utilities;
using GherkinWebAPI.Models.MaterialIndentByDepartment;

namespace GherkinWebAPI.Core.MaterialIndentByDepartment
{
	public interface IStoreInternalIndentDetailService
	{
		Task<ApiResponse<StoreInternalIndentDetail>> SaveMaterialIndentDetail(StoreInternalIndentDetail storeInternalIndentDetail);

		Task<ApiResponse<List<StoreInternalIndentDetail>>> GetMaterialIndentDetailByIndentNo(string indentNo);
	}
}