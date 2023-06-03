using GherkinWebAPI.Core;
using GherkinWebAPI.Core.BuyingStaffDetails;
using GherkinWebAPI.Models;
using GherkinWebAPI.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GherkinWebAPI.Controllers.AdvanceCashIssuedToFarmers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/V1/AdvanceCashIssuedToFarmers")]
    public class AdvanceCashIssuedToFarmersController : ApiController
    {
        private readonly IAdvCashIssuedToFarmrService _service;

        public AdvanceCashIssuedToFarmersController(IAdvCashIssuedToFarmrService service)
        {
            this._service = service;
        }

        [HttpGet, Route("GetFieldSupervisorList")]
        public async Task<IHttpActionResult> GetFieldSupervisorList(string areaId, DateTime aggrementDate)
        {
            return Ok(await _service.GetFieldSupervisorList(areaId, aggrementDate));
        }

        [HttpGet, Route("GetFieldStaffList")]
        public async Task<IHttpActionResult> GetFieldStaffList(string areaId, DateTime aggrementDate)
        {
            return Ok(await _service.GetFieldStaffList(areaId, aggrementDate));
        }

        [HttpPost, Route("GetFarmerDetails")]
        public async Task<IHttpActionResult> GetFarmerDetails(FarmerDetailsFilterModel farmerDetailsFilterModel)
        {
            return Ok(await _service.GetFarmerDetails(farmerDetailsFilterModel));
        }

        [HttpPost, Route("AddAdvanceCashToFarmer")]
        public async Task<IHttpActionResult> AddAdvanceCashToFarmer(List<AdvanceCashIssuedToFarmersModel> advanceCashIssuedToFarmersList)
        {
            return Ok(await _service.AddAdvanceCashToFarmer(advanceCashIssuedToFarmersList));
        }

    }
}