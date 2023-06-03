using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GherkinWebAPI.Utilities;
using GherkinWebAPI.Response;
using GherkinWebAPI.Request;
using GherkinWebAPI.Models;

namespace GherkinWebAPI.Core
{
    public interface IAdvCashIssuedToFarmrRepository
    {
        Task<ApiResponse<List<FieldStaffDetailsResponse>>> GetFieldSupervisorList(string areaId, DateTime aggrementDate);
        Task<ApiResponse<List<FieldStaffDetailsResponse>>> GetFieldStaffList(string areaId, DateTime aggrementDate);
        Task<ApiResponse<List<AdvCasIssuedToFarnrResponse>>> GetFarmerDetails(FarmerDetailsFilterModel farmerDetailsFilterModel);
        Task<ApiResponse<object>> AddAdvanceCashToFarmer(List<AdvanceCashIssuedToFarmersModel> advanceCashIssuedToFarmersList);
    }
}
