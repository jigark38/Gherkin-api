using GherkinWebAPI.Core.BuyingStaffDetails;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GherkinWebAPI.Controllers.BuyingStaffDetails
{
    [RoutePrefix("api/V1/BuyingStaffDetails")]
    public class BuyingStaffDetailsController : ApiController
    {
        private readonly IBuyingStaffDetailsService _service;

        public BuyingStaffDetailsController(IBuyingStaffDetailsService service)
        {
            this._service = service;
        }

        [HttpPost, Route("Add")]
        public async Task<IHttpActionResult> AddBuyingStaffDetails(List<HarvestAreaBuyingStaffDetails> harvestAreaBuyingStaffDetailsList)
        {

            return Ok(await _service.AddBuyingStaffDetails(harvestAreaBuyingStaffDetailsList));

        }

        [HttpPut, Route("Update")]
        public async Task<IHttpActionResult> UpdateBuyingStaffDetails(List<HarvestAreaBuyingStaffDetails> harvestAreaBuyingStaffDetailsList)
        {

            return Ok(await _service.UpdateBuyingStaffDetails(harvestAreaBuyingStaffDetailsList));

        }

        [HttpGet, Route("Get")]
        public async Task<IHttpActionResult> GetBuyingStaffDetailsByEmployee(string employeId)
        {

            return Ok(await _service.getBuyingStaffDetailsByEmployee(employeId));
        }

        [HttpGet, Route("Delete")]
        public async Task<IHttpActionResult> DeleteBuyingStaffDetailsByEmployee(string employeId, string areaId)
        {

            return Ok(await _service.DeleteBuyingStaffDetailsByEmployee(employeId, areaId));
        }
    }
}
