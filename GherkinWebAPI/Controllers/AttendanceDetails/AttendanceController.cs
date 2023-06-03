using GherkinWebAPI.Core.AttendanceDetails;
using GherkinWebAPI.DTO.AttendanceDetail;
using GherkinWebAPI.Models.AttendanceDetails;
using GherkinWebAPI.Request.HumanResource;
using GherkinWebAPI.Utilities;
using GherkinWebAPI.ValidateModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.AccountsMaster
{
    [RoutePrefix("api/v1/Attendance")]
    public class AttendanceController : ApiController
    {
        private readonly IAttendanceService _service;
        public readonly string controller = nameof(AttendanceController);

        public AttendanceController(IAttendanceService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetAttendanceDetail")]
        public async Task<IHttpActionResult> GetAttendanceDetails(string date, int orgOfficeNo, string category, string deptCode, string gender,
            string division, int biometricNo, string filter)
        {
            try
            {
                var result = await _service.GetAttendanceDetail(date, orgOfficeNo, category, deptCode, gender, division, biometricNo, filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(AttendanceController.GetAttendanceDetails)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetEmployeeByDivision")]
        public async Task<IHttpActionResult> GetEmployeeByDivision(string division)
        {
            try
            {
                var result = await _service.GetEmployeeByDivision(division);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(AttendanceController.GetAttendanceDetails)}");
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("GetAttendanceDetail")]
        public async Task<IHttpActionResult> GetAttendanceDetails(AttendanceDetailSearchDTO searchModel)
        {
            try
            {
                var result = await _service.GetAttendanceDetail(searchModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(AttendanceController.GetAttendanceDetails)}");
                return InternalServerError();
            }
        }


        [HttpPost]
        [Route("GetEmployeeById")]
        public async Task<IHttpActionResult> GetEmployeeById(AttendanceDetailSearchDTO searchModel)
        {
            try
            {
                var result = await _service.GetEmployeeById(searchModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(AttendanceController.GetEmployeeById)}");
                return InternalServerError();
            }
        }


        [HttpPost]
        [Route("updateAttendanceDetail")]
        public async Task<IHttpActionResult> UpdateAttendanceDetail(AttendanceRequestmodel attendanceModel)
        {
            try
            {
                var result = await _service.UpdateAttendanceDetail(attendanceModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(AttendanceController.UpdateAttendanceDetail)}");
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("GetAttendanceDetailsForSalaryCalculation")]
        public async Task<IHttpActionResult> GetAttendanceDetailsForSalaryCalculation([FromBody] AttendanceRequestDetails attendanceRequestDetails)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var data = await _service.GetAttendanceDetailsForSalaryCalculation(attendanceRequestDetails);
                if (data != null)
                {
                    apiResponse.IsSucceed = true;
                    apiResponse.Data = data;
                }
                else
                {
                    apiResponse.IsSucceed = false;
                    apiResponse.Data = null;
                }

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.IsSucceed = false;
                apiResponse.Exception = ex;
                return Ok(apiResponse);
            }
        }


        [HttpPost]
        [Route("SaveAttendanceDetailsForSalary")]
        public async Task<IHttpActionResult> SaveAttendanceDetailsForSalary([FromBody] AttendanceSalaryDetailsRequest obj)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var data = await _service.SaveAttendanceSalaryDetail(obj);
                if (data != null)
                {
                    apiResponse.IsSucceed = true;
                    apiResponse.Data = data;
                }
                else
                {
                    apiResponse.IsSucceed = false;
                    apiResponse.Data = null;
                }

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.IsSucceed = false;
                apiResponse.Exception = ex;
                return Ok(apiResponse);
            }
        }

    }
}
