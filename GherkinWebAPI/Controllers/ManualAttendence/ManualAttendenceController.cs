using GherkinWebAPI.Core.ManualAttendence;
using GherkinWebAPI.Models.ManualAttendence;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GherkinWebAPI.Controllers.ManualAttendence
{
    [Route("api/V1/[Controller]")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ManualAttendenceController : ApiController
    {
        private IManualAttendenceService _manualAttendenceService;
        public ManualAttendenceController(IManualAttendenceService manualAttendenceService)
        {
            _manualAttendenceService = manualAttendenceService;
        }

        [HttpGet]
        [Route("GetManualAttendenceDetails")]
        public async Task<HttpResponseMessage> GetManualAttendenceDetailsList(string areaId, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize)
        {
            try
            {
                var response = new ApiResponse<ManualAttendenceData>();
                var data = await _manualAttendenceService.GetManualAttendenceDetailsList(areaId, fromDate, toDate, pageIndex, pageSize);
                response.Data = data;
                response.IsSucceed = true;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<List<ManualAttendenceDto>>();
                response.IsSucceed = false;
                response.ErrorMessages = new List<string>();
                response.ErrorMessages.Add(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost]
        [Route("SaveManualAttendence")]
        public async Task<HttpResponseMessage> SaveManualAttendence(List<ManualAttendenceDto> manualAttendenceDtos)
        {
            try
            {
                var response = new ApiResponse<bool>();
                var data = await _manualAttendenceService.SaveManualAttendence(manualAttendenceDtos);
                response.Data = data;
                response.IsSucceed = true;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<bool>();
                response.IsSucceed = false;
                response.ErrorMessages = new List<string>();
                response.ErrorMessages.Add(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, response);
            }
        }
    }
}
