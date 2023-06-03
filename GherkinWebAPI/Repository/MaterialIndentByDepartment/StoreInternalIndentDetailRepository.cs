using GherkinWebAPI.Core.MaterialIndentByDepartment;
using GherkinWebAPI.Models.MaterialIndentByDepartment;
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
	public class StoreInternalIndentDetailRepository : RepositoryBase<StoreInternalIndentDetail>, IStoreInternalIndentDetailRepository
	{
		private RepositoryContext _context;

		public StoreInternalIndentDetailRepository(RepositoryContext repositoryContext) : base(repositoryContext)
		{
			_context = repositoryContext;
		}

		public async Task<ApiResponse<StoreInternalIndentDetail>> SaveMaterialIndentDetail(StoreInternalIndentDetail storeInternalIndentDetail)
		{
			ApiResponse<StoreInternalIndentDetail> apiResponse = new ApiResponse<StoreInternalIndentDetail>();
			using (DbContextTransaction transaction = _context.Database.BeginTransaction())
			{
				try
				{
					var detailId = await this._context.Database.SqlQuery<int>(
						"USP_SaveMaterialIndentDetail @Id, @Store_Internal_Indent_No, @Store_Material_Group_Code, @Store_Material_SubGroup_Code, @Store_Material_Item_Code, @Store_Department_Indent_Quantity, @Store_Department_Indent_Required_Date",
						new SqlParameter("Id", storeInternalIndentDetail.Id),
						new SqlParameter("Store_Internal_Indent_No", storeInternalIndentDetail.StoreInternalIndentNo),
						new SqlParameter("Store_Material_Group_Code", storeInternalIndentDetail.StoreMaterialGroupCode),
						new SqlParameter("Store_Material_SubGroup_Code", storeInternalIndentDetail.StoreMaterialSubGroupCode),
						new SqlParameter("Store_Material_Item_Code", storeInternalIndentDetail.StoreMaterialItemCode),
						new SqlParameter("Store_Department_Indent_Quantity", storeInternalIndentDetail.StoreDeptIndentQty),
						new SqlParameter("Store_Department_Indent_Required_Date", storeInternalIndentDetail.StoreDeptIndentReqDate)
					).FirstAsync();

					transaction.Commit();

					apiResponse.Data = await _context.StoreInternalIndentDetail.Where(x => x.Id == detailId).FirstOrDefaultAsync();
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
		}

		public async Task<ApiResponse<List<StoreInternalIndentDetail>>> GetMaterialIndentDetailByIndentNo(string indentNo)
		{
			ApiResponse<List<StoreInternalIndentDetail>> apiResponse = new ApiResponse<List<StoreInternalIndentDetail>>();
			try
			{
				apiResponse.Data = await _context.StoreInternalIndentDetail.Where(x => x.StoreInternalIndentNo == indentNo).ToListAsync();
				var groupCodes = apiResponse.Data.Select(x => x.StoreMaterialGroupCode).ToList();
				var groupDetails = await _context.GSGroupDetail.Where(x => groupCodes.Contains(x.GSGroupCode)).ToListAsync();

				var subGroupCodes = apiResponse.Data.Select(x => x.StoreMaterialSubGroupCode).ToList();
				var subGroupDetails = await _context.GSSubGroupDetail.Where(x => subGroupCodes.Contains(x.GSSubGroupCode)).ToListAsync();

				var materialCodes = apiResponse.Data.Select(x => x.StoreMaterialItemCode).ToList();
				var materialDetails = await _context.GSMaterialDetail.Where(x => materialCodes.Contains(x.GSMaterialCode)).ToListAsync();

				foreach (var indentDetail in apiResponse.Data)
				{
					indentDetail.StoreMaterialGroupName = groupDetails.Where(x => x.GSGroupCode == indentDetail.StoreMaterialGroupCode).FirstOrDefault().GSGroupName;
					indentDetail.StoreMaterialSubGroupName = subGroupDetails.Where(x => x.GSSubGroupCode == indentDetail.StoreMaterialSubGroupCode).FirstOrDefault().GSSubGroupName;
					indentDetail.MaterialDetailName = materialDetails.Where(x => x.GSMaterialCode == indentDetail.StoreMaterialItemCode).FirstOrDefault().GSMaterialName;
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

	}
}