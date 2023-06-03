using GherkinWebAPI.Core.MaterialIndentByDepartment;
using GherkinWebAPI.Models.MaterialIndentByDepartment;
using GherkinWebAPI.Models.StoresMasterDetails;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GherkinWebAPI.Repository.MaterialIndentByDepartment
{
	public class StoreInternalIndentMasterRepository : RepositoryBase<StoreInternalIndentMaster>, IStoreInternalIndentMasterRepository
	{
		private RepositoryContext _context;

		public StoreInternalIndentMasterRepository(RepositoryContext repositoryContext) : base(repositoryContext)
		{
			_context = repositoryContext;
		}

		public async Task<ApiResponse<List<GSGroupDetail>>> GetGroupNameList()
		{
			ApiResponse<List<GSGroupDetail>> apiResponse = new ApiResponse<List<GSGroupDetail>>();
			try
			{
				apiResponse.Data = await _context.GSGroupDetail.ToListAsync();
				apiResponse.IsSucceed = true;
				return apiResponse;
			}
			catch(Exception ex)
			{
				apiResponse.IsSucceed = false;
				apiResponse.Exception = ex;
				return apiResponse;
			}
		}

		public async Task<ApiResponse<List<GSSubGroupDetail>>> GetSubGroupNameListByGroupCode(int groupCode)
		{
			ApiResponse<List<GSSubGroupDetail>> apiResponse = new ApiResponse<List<GSSubGroupDetail>>();
			try
			{
				apiResponse.Data = await _context.GSSubGroupDetail.Where(x => x.GSGroupCode == groupCode).ToListAsync();
				apiResponse.IsSucceed = true;
				return apiResponse;
			}
			catch (Exception ex)
			{
				apiResponse.IsSucceed = false;
				apiResponse.Exception = ex;
				return apiResponse;
			}
		}

		public async Task<ApiResponse<List<GSMaterialDetail>>> GetMaterialListByGroupSubGroupCode(int groupCode, int subGroupCode)
		{
			ApiResponse<List<GSMaterialDetail>> apiResponse = new ApiResponse<List<GSMaterialDetail>>();
			try
			{
				apiResponse.Data = await _context.GSMaterialDetail.Where(x => x.GSGroupCode == groupCode && x.GSSubGroupCode == subGroupCode).ToListAsync();
				var groupCodes = apiResponse.Data.Select(x => x.GSGroupCode).ToList();
				var groupDetails = await _context.GSGroupDetail.Where(x => groupCodes.Contains(x.GSGroupCode)).ToListAsync();

				var subGroupCodes = apiResponse.Data.Select(x => x.GSSubGroupCode).ToList();
				var subGroupDetails = await _context.GSSubGroupDetail.Where(x => subGroupCodes.Contains(x.GSSubGroupCode)).ToListAsync();

				var uomDetailCodes = apiResponse.Data.Select(x => x.GSCUOMCode).ToList();
				var uomDetails = await _context.GSCUomDetails.Where(x => uomDetailCodes.Contains(x.GSC_UOM_Code)).ToListAsync();

				foreach (var material in apiResponse.Data)
				{
					material.GSGroupName = groupDetails.Where(x => x.GSGroupCode == material.GSGroupCode).FirstOrDefault().GSGroupName;
					material.GSSubGroupName = subGroupDetails.Where(x => x.GSSubGroupCode == material.GSSubGroupCode).FirstOrDefault().GSSubGroupName;
					material.GSCUOMName = uomDetails.Where(x => x.GSC_UOM_Code == material.GSCUOMCode).FirstOrDefault().GSC_UOM_Name;
				}
				apiResponse.IsSucceed = true;
				return apiResponse;
			}
			catch (Exception ex)
			{
				apiResponse.IsSucceed = false;
				apiResponse.Exception = ex;
				return apiResponse;
			}
		}

		public async Task<ApiResponse<List<StoreInternalIndentMaster>>> GetMaterialIndentListByIndentDate(DateTime indentDate)
		{
			ApiResponse<List<StoreInternalIndentMaster>> apiResponse = new ApiResponse<List<StoreInternalIndentMaster>>();
			try
			{
				apiResponse.Data = await _context.StoreInternalIndentMaster.Where(x => x.StoreInternalIndentDate.Year == indentDate.Year &&
				x.StoreInternalIndentDate.Month == indentDate.Month &&
				x.StoreInternalIndentDate.Day == indentDate.Day).ToListAsync();
				apiResponse.IsSucceed = true;
				return apiResponse;
			}
			catch (Exception ex)
			{
				apiResponse.IsSucceed = false;
				apiResponse.Exception = ex;
				return apiResponse;
			}
		}

		public async Task<ApiResponse<StoreInternalIndentMaster>> SaveMaterialIndentMaster(StoreInternalIndentMaster storeInternalIndentMaster)
		{
			ApiResponse<StoreInternalIndentMaster> apiResponse = new ApiResponse<StoreInternalIndentMaster>();
			using (DbContextTransaction transaction = _context.Database.BeginTransaction())
			{
				try
				{
					var masterId = await this._context.Database.SqlQuery<int>(
						"USP_SaveMaterialIndentMaster @Store_Internal_Indent_Date, @Emp_Id",
						new SqlParameter("Store_Internal_Indent_Date", storeInternalIndentMaster.StoreInternalIndentDate),
						new SqlParameter("Emp_Id", storeInternalIndentMaster.EmpId)
					).FirstAsync();

					transaction.Commit();

					apiResponse.Data = await _context.StoreInternalIndentMaster.Where(x => x.Id == masterId).FirstOrDefaultAsync();
					apiResponse.IsSucceed = true;
					return apiResponse;
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					apiResponse.IsSucceed = false;
					apiResponse.Exception = ex;
					return apiResponse;
				}
			}
		}
	}
}