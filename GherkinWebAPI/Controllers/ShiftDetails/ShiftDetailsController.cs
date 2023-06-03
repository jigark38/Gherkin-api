using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using GherkinWebAPI.Core.ShiftDetails;
using GherkinWebAPI.DTO.ShiftDetails;
using GherkinWebAPI.Models.ShiftDetail;
using System.Web.Http.Cors;

namespace GherkinWebAPI.Controllers.ShiftDetails
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ShiftDetailsController : ApiController
    {
        private readonly IShiftDetailsService _service;

        public ShiftDetailsController(IShiftDetailsService service)
        {
            this._service = service;
        }
        // GET: ShiftDetails
        [HttpGet]
        [Route("GetAllShiftDetails")]
        public async Task<IHttpActionResult> GetAllShiftDetails()
        {
            try
            {
                var result = await _service.GetShiftDetails();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [Route("AddShiftDetails")]
        public async Task<IHttpActionResult> AddShiftDetails(ShiftDetailsDto shiftDetails)
        {
            try
            {
                var shiftDetail = new ShiftDetailMaster
                {
                    ShiftNo = shiftDetails.ShiftNo,
                    EntryDate = shiftDetails.EntryDate,
                    EnteredEmpID ="test",// shiftDetails.EnteredEmpID,
                    ShiftEffectiveDate = shiftDetails.ShiftEffectiveDate,
                    ShiftName = shiftDetails.ShiftName,
                    ShiftTimeFrom = shiftDetails.ShiftTimeFrom,
                    ShiftTimeTo = shiftDetails.ShiftTimeTo,
                    ShiftDuration = shiftDetails.ShiftDuration,
                    ShiftBreakTimeFrom = shiftDetails.ShiftBreakTimeFrom,
                    ShiftBreakTimeTo = shiftDetails.ShiftBreakTimeTo,
                    ShiftBreakDuration = shiftDetails.ShiftBreakDuration,
                    ShiftRotation = shiftDetails.ShiftRotation,
                    ShiftRotationDays = shiftDetails.ShiftRotationDays
                };

                var result = await _service.AddShiftDetails(shiftDetail);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("AddShiftStatus")]
        public async Task<IHttpActionResult> AddShiftStatus(ShiftDetailsDto shiftDetails)
        {
            try
            {
                var shift = await _service.GetShiftStatus(shiftDetails.ShiftNo);
                if (shift != null)
                {
                    shift.ShiftStatus = shiftDetails.ShiftStatus;
                    shift.ShiftCancellationEffectiveFromDate = shiftDetails.ShiftCancellationEffectiveFromDate.Value;
                    var result = await _service.UpdateShiftStatus(shift);
                    return Ok(result);
                }
                else
                {
                    var shiftStatus = new ShiftStatusDetail
                    {
                        ShiftStatus = shiftDetails.ShiftStatus,
                        ShiftCancellationEffectiveFromDate = shiftDetails.ShiftCancellationEffectiveFromDate.Value,
                        ShiftNo = shiftDetails.ShiftNo
                    };
                    var result = await _service.AddShiftStatus(shiftStatus);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}