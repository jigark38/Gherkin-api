using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using GherkinWebAPI.Request;
using GherkinWebAPI.Response;
using GherkinWebAPI.Utilities;

namespace GherkinWebAPI.Service
{
    public class AdvanceCashIssuedToFarmersService : IAdvCashIssuedToFarmrService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public AdvanceCashIssuedToFarmersService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<ApiResponse<List<FieldStaffDetailsResponse>>> GetFieldSupervisorList(string areaId, DateTime aggrementDate)
        {
            return await _repositoryWrapper.AdvCashIssuedToFarmrRepository.GetFieldSupervisorList(areaId, aggrementDate);
        }
        public async Task<ApiResponse<List<FieldStaffDetailsResponse>>> GetFieldStaffList(string areaId, DateTime aggrementDate)
        {
            return await _repositoryWrapper.AdvCashIssuedToFarmrRepository.GetFieldStaffList(areaId, aggrementDate);
        }

        public async Task<ApiResponse<List<AdvCasIssuedToFarnrResponse>>> GetFarmerDetails(FarmerDetailsFilterModel farmerDetailsFilterModel)
        {
            return await _repositoryWrapper.AdvCashIssuedToFarmrRepository.GetFarmerDetails(farmerDetailsFilterModel);
        }

        public async Task<ApiResponse<object>> AddAdvanceCashToFarmer(List<AdvanceCashIssuedToFarmersModel> advanceCashIssuedToFarmersList)
        {
            return await _repositoryWrapper.AdvCashIssuedToFarmrRepository.AddAdvanceCashToFarmer(advanceCashIssuedToFarmersList);
        }
    }
}