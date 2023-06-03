using GherkinWebAPI.Models;
using GherkinWebAPI.Request;
using GherkinWebAPI.Response;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
    public interface IAdvCashIssuedToFarmrService
    {
        Task<ApiResponse<List<FieldStaffDetailsResponse>>> GetFieldSupervisorList(string areaId, DateTime aggrementDate);
        Task<ApiResponse<List<FieldStaffDetailsResponse>>> GetFieldStaffList(string areaId, DateTime aggrementDate);
        Task<ApiResponse<List<AdvCasIssuedToFarnrResponse>>> GetFarmerDetails(FarmerDetailsFilterModel farmerDetailsFilterModel);

        Task<ApiResponse<object>> AddAdvanceCashToFarmer(List<AdvanceCashIssuedToFarmersModel> advanceCashIssuedToFarmersList);
    }
}
