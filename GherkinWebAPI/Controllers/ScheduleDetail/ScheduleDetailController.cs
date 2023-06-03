using GherkinWebAPI.Core;
using GherkinWebAPI.Core.ScheduleDetail;
using GherkinWebAPI.DTO;
using GherkinWebAPI.DTO.ScheduleDetail;
using GherkinWebAPI.Entities;
using GherkinWebAPI.Filter;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GherkinWebAPI.Controllers.ScheduleDetail
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("ScheduleDetail")]
    public class ScheduleDetailController : ApiController
    {
        private IScheduleDetailService _scheduleDetailService;

        public ScheduleDetailController(IScheduleDetailService scheduleDetailService)
        {
            _scheduleDetailService = scheduleDetailService;
           
        }

        [HttpGet]
        [Route("GetPendingOrderScheduleDetails")]

        public async Task<IHttpActionResult> GetPendingOrderScheduleDetails()
        {
            //int officeNo,string processCode
            List<ScheduleDetailDTO> result = new List<ScheduleDetailDTO>();
            try
            {
                result = await _scheduleDetailService.GetPendingOrderScheduleDetails();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in ScheduleDetailController / {nameof(ScheduleDetailController.GetPendingOrderScheduleDetails)}");
                return InternalServerError(ex);
            }

            return Ok(result);

        }
    }
}
