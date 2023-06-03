using GherkinWebAPI.Core.HarvestStage;
using GherkinWebAPI.Request.HarvestStage;
using GherkinWebAPI.Response;
using GherkinWebAPI.Response.HarvestStage;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GherkinWebAPI.Controllers.PlantationHarvest
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HarvestStageController : ApiController
    {
        private readonly IHarvestStageService _service;

        public HarvestStageController(IHarvestStageService service)
        {
            this._service = service;
        }

        [Route("harveststage/formdetail")]
        public async Task<HttpResponseMessage> GetRMStockDataForForm()
        {

            HarvestStageShowResponse data = new HarvestStageShowResponse();
            try
            {
                data = await _service.GetHarvestStageFormDataAsync();
                if (data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("harveststage/add")]
        public async Task<HttpResponseMessage> InsertHarvestStages(HarvestStageInsertRequest harvestStageInsertRequest)
        {

            HarvestStageResponse data = new HarvestStageResponse();
            try
            {
                data = await _service.InsertHarvestStages(harvestStageInsertRequest);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }

        [HttpGet]
        [Route("harveststage/getEffectiveDateList/{cropNameCode}")]
        public async Task<HttpResponseMessage> GetEffectiveDateList(string cropNameCode)
        {
            try
            {
                var data = await _service.GetEffectiveDateList(cropNameCode);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }

        [HttpGet]
        [Route("harveststage/getHarvestStageDetails/{hsTransactionCode}")]
        public async Task<HttpResponseMessage> GetHarvestStageDetails(string hsTransactionCode)
        {
            try
            {
                var data = await _service.GetHarvestStageDetails(hsTransactionCode);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }

    }
}