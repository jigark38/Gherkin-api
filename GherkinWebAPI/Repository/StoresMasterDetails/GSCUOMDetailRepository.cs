using GherkinWebAPI.Core.StoresMasterDetails;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.StoresMasterDetails;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

namespace GherkinWebAPI.Repository.StoresMasterDetails
{
	public class GSCUOMDetailRepository : RepositoryBase<GSCUomDetails>, IGSCUOMDetailRepository
	{
		private RepositoryContext _context;

		public GSCUOMDetailRepository(RepositoryContext repositoryContext) : base(repositoryContext)
		{
			_context = repositoryContext;
		}

		public async Task<ApiResponse<List<GSCUomDetails>>> GetStoresMasterUOMByName(string uomName)
		{
			ApiResponse<List<GSCUomDetails>> apiResponse = new ApiResponse<List<GSCUomDetails>>();
			try
			{
				apiResponse.Data = await _context.GSCUomDetails.Where(x => x.GSC_UOM_Name.StartsWith(uomName)).ToListAsync();
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

		public async Task<ApiResponse<bool>> IsStoresMasterUOMExists(string uomName)
		{
			ApiResponse<bool> apiResponse = new ApiResponse<bool>();
			try
			{
				apiResponse.Data = await _context.GSCUomDetails.Where(x => x.GSC_UOM_Name.ToLower() == uomName.ToLower()).CountAsync() > 0;
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

		public async Task<ApiResponse<GSCUomDetails>> SaveStoresMasterUOM(GSCUomDetails uomDetail)
		{
			ApiResponse<GSCUomDetails> apiResponse = new ApiResponse<GSCUomDetails>();
			try
			{
				_context.GSCUomDetails.AddOrUpdate(uomDetail);
				await _context.SaveChangesAsync();
				apiResponse.Data = uomDetail;
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