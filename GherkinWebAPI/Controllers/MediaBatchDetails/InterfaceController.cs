using GherkinWebAPI.Core.MediaBatchDetails;
using GherkinWebAPI.Models.MediaBatchDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.MediaBatchDetails
{
    [Route("api/V1/[Controller]")]
    public class InterfaceController : ApiController
    {
        private readonly IInterfaceService _service;
        private readonly string controller = nameof(InterfaceController);
        public InterfaceController(IInterfaceService interfaceService)
        {
            _service = interfaceService;
        }
        [HttpGet]
        [Route("GetOrgOfficeNameLists")]
        public async Task<IHttpActionResult> GetOrgOfficeNameLists()
        {
            try
            {
                var res = await _service.GetOrgOfficeNameLists();
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(InterfaceController.GetOrgOfficeNameLists)}");
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("GetMediaProcessNameList")]
        public async Task<IHttpActionResult> GetMediaProcessNameList()
        {
            try
            {
                var res = await _service.GetMediaProcessNameList();
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(InterfaceController.GetMediaProcessNameList)}");
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("GetPendingOrderScheduleGrid/{orgOfficeNo}")]
        public async Task<IHttpActionResult> GetPendingOrderScheduleGrid(int orgOfficeNo)
        {
            try
            {
                var res = await _service.GetPendingOrderScheduleGrid(orgOfficeNo);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(InterfaceController.GetPendingOrderScheduleGrid)}");
                return InternalServerError();
            }
        }
        [HttpPost]
        [Route("GetFilteredPendingOrderScheduleGrid")]
        public async Task<IHttpActionResult> GetFilteredPendingOrderScheduleGrid(UnitAndMediaFilter UAMF)
        {
            try
            {
                var res = await _service.GetFilteredPendingOrderScheduleGrid(UAMF.OrgOfficeNo,UAMF.MediaProcessCode);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(InterfaceController.GetFilteredPendingOrderScheduleGrid)}");
                return InternalServerError();
            }
        }
        [HttpPost]
        [Route("SelectPendingOrderSchedule")]
        public async Task<IHttpActionResult> SelectPendingOrderSchedule(SelectedPendingOrder Pendobj)
        {
            try
            {
                var res = await _service.SelectPendingOrderSchedule(Pendobj);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(InterfaceController.SelectPendingOrderSchedule)}");
                return InternalServerError();
            }
        }

    }
}
