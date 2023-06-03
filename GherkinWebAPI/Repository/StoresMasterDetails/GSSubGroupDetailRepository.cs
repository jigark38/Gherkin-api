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
	public class GSSubGroupDetailRepository : RepositoryBase<GSSubGroupDetail>, IGSSubGroupDetailRepository
	{
		private RepositoryContext _context;

		public GSSubGroupDetailRepository(RepositoryContext repositoryContext) : base(repositoryContext)
		{
			_context = repositoryContext;
		}

		public async Task<ApiResponse<List<GSSubGroupDetail>>> GetStoresMasterSubGroupByName(string subGroupName, int groupCode)
		{
			ApiResponse<List<GSSubGroupDetail>> apiResponse = new ApiResponse<List<GSSubGroupDetail>>();
			try
			{
				apiResponse.Data = await _context.GSSubGroupDetail.Where(x => x.GSSubGroupName.StartsWith(subGroupName) && x.GSGroupCode == groupCode).ToListAsync();
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

		public async Task<ApiResponse<bool>> IsStoresMasterSubGroupExists(string subGroupName, int groupCode)
		{
			ApiResponse<bool> apiResponse = new ApiResponse<bool>();
			try
			{
				apiResponse.Data = await _context.GSSubGroupDetail.Where(x => x.GSSubGroupName.ToLower() == subGroupName.ToLower() && x.GSGroupCode == groupCode).CountAsync() > 0;
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

		public async Task<ApiResponse<GSSubGroupDetail>> SaveStoresMasterSubGroup(GSSubGroupDetail subGroupDetail)
		{
			ApiResponse<GSSubGroupDetail> apiResponse = new ApiResponse<GSSubGroupDetail>();
			try
			{
				_context.GSSubGroupDetail.AddOrUpdate(subGroupDetail);
				await _context.SaveChangesAsync();
				apiResponse.Data = subGroupDetail;
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