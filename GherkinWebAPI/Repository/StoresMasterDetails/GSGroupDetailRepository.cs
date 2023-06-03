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
	public class GSGroupDetailRepository : RepositoryBase<GSGroupDetail>, IGSGroupDetailRepository
	{
		private RepositoryContext _context;

		public GSGroupDetailRepository(RepositoryContext repositoryContext) : base(repositoryContext)
		{
			_context = repositoryContext;
		}

		public async Task<ApiResponse<List<GSGroupDetail>>> GetStoresMasterGroupByName(string groupName)
		{
			ApiResponse<List<GSGroupDetail>> apiResponse = new ApiResponse<List<GSGroupDetail>>();
			try
			{
				apiResponse.Data = await _context.GSGroupDetail.Where(x => x.GSGroupName.StartsWith(groupName)).ToListAsync();
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

		public async Task<ApiResponse<bool>> IsStoresMasterGroupExists(string groupName)
		{
			ApiResponse<bool> apiResponse = new ApiResponse<bool>();
			try
			{
				apiResponse.Data = await _context.GSGroupDetail.Where(x => x.GSGroupName.ToLower() == groupName.ToLower()).CountAsync() > 0;
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

		public async Task<ApiResponse<GSGroupDetail>> SaveStoresMasterGroup(GSGroupDetail groupDetail)
		{
			ApiResponse<GSGroupDetail> apiResponse = new ApiResponse<GSGroupDetail>();
			try
			{
				_context.GSGroupDetail.AddOrUpdate(groupDetail);
				await _context.SaveChangesAsync();
				apiResponse.Data = groupDetail;
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