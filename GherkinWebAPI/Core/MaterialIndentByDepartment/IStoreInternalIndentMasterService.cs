using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GherkinWebAPI.Utilities;
using GherkinWebAPI.Models.StoresMasterDetails;
using GherkinWebAPI.Models.MaterialIndentByDepartment;

namespace GherkinWebAPI.Core.MaterialIndentByDepartment
{
	public interface IStoreInternalIndentMasterService
	{
		Task<ApiResponse<List<GSGroupDetail>>> GetGroupNameList();

		Task<ApiResponse<List<GSSubGroupDetail>>> GetSubGroupNameListByGroupCode(int groupCode);

		Task<ApiResponse<List<GSMaterialDetail>>> GetMaterialListByGroupSubGroupCode(int groupCode, int subGroupCode);

		Task<ApiResponse<List<StoreInternalIndentMaster>>> GetMaterialIndentListByIndentDate(DateTime indentDate);

		Task<ApiResponse<StoreInternalIndentMaster>> SaveMaterialIndentMaster(StoreInternalIndentMaster storeInternalIndentMaster);

	}
}