using GherkinWebAPI.Core.StoresMasterDetails;
using GherkinWebAPI.Models.StoresMasterDetails;
using GherkinWebAPI.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using GherkinWebAPI.Utilities;
using System;

namespace GherkinWebAPI.Repository.StoresMasterDetails
{
	public class GSMaterialDetailRepository : RepositoryBase<GSMaterialDetail>, IGSMaterialDetailRepository
	{
		private RepositoryContext _context;

		public GSMaterialDetailRepository(RepositoryContext repositoryContext) : base(repositoryContext)
		{
			_context = repositoryContext;
		}

		public async Task<ApiResponse<List<GSMaterialDetail>>> GetStoresMasterMaterialByName(string materialName, int groupCode, int subGroupCode)
		{
			ApiResponse<List<GSMaterialDetail>> apiResponse = new ApiResponse<List<GSMaterialDetail>>();
			try
			{
				apiResponse.Data = await _context.GSMaterialDetail.Where(x => x.GSMaterialName.StartsWith(materialName) && x.GSGroupCode == groupCode && x.GSSubGroupCode == subGroupCode).ToListAsync();
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
			catch(Exception ex)
			{
				apiResponse.IsSucceed = false;
				apiResponse.Exception = ex;
				return apiResponse;
			}
		}

		public async Task<ApiResponse<bool>> IsStoresMasterMaterialExists(string materialName)
		{
			ApiResponse<bool> apiResponse = new ApiResponse<bool>();
			try
			{
				apiResponse.Data = await _context.GSMaterialDetail.Where(x => x.GSMaterialName.ToLower() == materialName.ToLower()).CountAsync() > 0;
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

		public async Task<ApiResponse<GSMaterialDetail>> SaveStoresMasterMaterial(GSMaterialDetail materialDetail)
		{
			ApiResponse<GSMaterialDetail> apiResponse = new ApiResponse<GSMaterialDetail>();
			try
			{
				_context.GSMaterialDetail.AddOrUpdate(materialDetail);
				await _context.SaveChangesAsync();
				apiResponse.Data = materialDetail;
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
	}
}