using GherkinWebAPI.Core.DailyInputAndFeedingDetails;
using GherkinWebAPI.Models.DailyInputAndFeedingDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.DailyInputAndFeedingDetails
{
    [Route("api/V1/[Controller]")]
    public class InputIssuesController : ApiController
    {
        private readonly IInputIssuesService _service;
        private readonly string controller = nameof(InputIssuesController);
        public InputIssuesController(IInputIssuesService dailyInputService)
        {
            _service = dailyInputService;
        }
        [HttpPost]
        [Route("GetMaterialInputConsumed")]
        public async Task<IHttpActionResult> GetMaterialInputConsumed(DailyInputModel dailyInputModel)
        {
            try
            {
                var res = await _service.MaterialInputConsumed(dailyInputModel);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(InputIssuesController.GetMaterialInputConsumed)}");
                return InternalServerError();
            }
        }
        [HttpPost]
        [Route("SaveFarmerInputDatails")]
        public async Task<IHttpActionResult> SaveFarmerInputDatails(FarmerInputConAndMatIssueModel ListInputModel)
        {
            try
            {
                var res = await _service.SaveFarmerInputDatails(ListInputModel);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(InputIssuesController.SaveFarmerInputDatails)}");
                return InternalServerError();
            }
        }
    }
}
