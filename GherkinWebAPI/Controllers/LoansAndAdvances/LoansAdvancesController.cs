using System;
using System.Threading.Tasks;
using System.Web.Http;
using GherkinWebAPI.ValidateModel;
using GherkinWebAPI.Core.LoansAndAdvancesDetails;
using GherkinWebAPI.Models.LoansAndAdvancesDetails;

namespace GherkinWebAPI.Controllers.TransportVehicleManagement
{
    [RoutePrefix("api/v1/LoansAdvances")]
    public class LoansAdvancesController : ApiController
    {
        private readonly ILoansAdvancesService _loansAdvancesService;
        public readonly string controller = nameof(LoansAdvancesController);

        public LoansAdvancesController(ILoansAdvancesService service)
        {
            _loansAdvancesService = service;
        }

        [CheckModelForNull]
        [ValidateModelState]
        [Route("CreateLoansAdvances")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateLoansAdvances(LoansAdvancesDTO loansAdvances)
        {
            try
            {
                var lADetail = await _loansAdvancesService.CreateLoansAdvances(loansAdvances);
                return Ok(lADetail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(LoansAdvancesController.CreateLoansAdvances)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetLoansAdvancesNo")]
        public async Task<IHttpActionResult> GetLoansAdvancesNo()
        {
            try
            {
                var lANo = await _loansAdvancesService.GetLoansAdvanceNo();
                return Ok(lANo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(LoansAdvancesController.GetLoansAdvancesNo)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("SeachLoansAdvances")]
        public async Task<IHttpActionResult> SeachLoansAdvances(int orgOfficeNo, string employeeId)
        {
            try
            {
                var lADetails = await _loansAdvancesService.SearchLoansAdvances(orgOfficeNo, employeeId);
                return Ok(lADetails);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(LoansAdvancesController.SeachLoansAdvances)}");
                return InternalServerError();
            }
        }

        [CheckModelForNull]
        [ValidateModelState]
        [Route("UpdateLoansAdvances")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateLoansAdvances(LoansAdvancesDTO loansAdvances)
        {
            try
            {
                var lADetail = await _loansAdvancesService.UpdateLoansAdvances(loansAdvances);
                return Ok(lADetail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller} / {nameof(LoansAdvancesController.UpdateLoansAdvances)}");
                return InternalServerError();
            }
        }

    }
}
