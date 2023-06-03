using GherkinWebAPI.Core.SowingFarming;
using GherkinWebAPI.Request.SowingFarming;
using GherkinWebAPI.Response.SowingFarming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GherkinWebAPI.Controllers.Sowing
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/V1/[Controller]")]
    public class SowingFarmingController : ApiController
    {
        private readonly ISowingFarmingService _service;

        public SowingFarmingController(ISowingFarmingService service)
        {
            this._service = service;
        }

        [HttpGet]
        [Route("GetSowingFarmingDataForFormRequiredForGrid")]
        public async Task<HttpResponseMessage> GetSowingFarmingDataForFormRequiredForGrid(DateTime sowingDate, string cropNameCode, string psNumber)
        {

            SowingFarmingDataForFormRequiredForGrid data = new SowingFarmingDataForFormRequiredForGrid();
            try
            {
                data = await _service.GetSowingFarmingDataForFormRequiredForGrid(sowingDate, cropNameCode, psNumber);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetSowingFarmingDataForFormByAreaId")]
        public async Task<HttpResponseMessage> GetSowingFarmingDataForFormByAreaId(string areaId)
        {

            SowingFarmingFormDataResponse data = new SowingFarmingFormDataResponse();
            try
            {
                data = await _service.GetSowingFarmingDataForFormByAreaId(areaId);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }



        [HttpPost]
        [Route("sowingFarming/insert")]
        public async Task<HttpResponseMessage> InsertSowingFamring(SowingFarmingInsert sowingFarmingInsert)
        {

            SowingFarmingInsert data = new SowingFarmingInsert();
            try
            {
                data = await _service.InsertSowingFamring(sowingFarmingInsert);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }
    }
}
