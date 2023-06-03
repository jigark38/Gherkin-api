using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;

namespace GherkinWebAPI.Controllers.HarvestDetailsWeightment
{
    [Route("api/V1/[Controller]")]
    public class HarvestGRNWeightmentController : ApiController
    {
        private IHarvestGRNWeightmentDetailsService _harvestGRNWeightmentDetailsService;
        public HarvestGRNWeightmentController(IHarvestGRNWeightmentDetailsService harvestGRNWeightment)
        {
            _harvestGRNWeightmentDetailsService = harvestGRNWeightment;
        }
        [HttpGet, Route("GetInwardDetailsByOrgId")]
        public async Task<HttpResponseMessage> GetInwardDetails(int orgId)
        {
            try
            {
                var _details = await _harvestGRNWeightmentDetailsService.GetInwardDetails(orgId);
                if (_details.Count() > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,_details);
                }
                else
                {
                    
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
              //  Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(HarvestGRNController.GetGreensReceivedDetailAsync)}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet, Route("GreensReceptionDetailsByOrgId")]
        public async Task<HttpResponseMessage> GreensReceptionDetailsByOrgID(int orgId)
        {
            try
            {
                var _details = await _harvestGRNWeightmentDetailsService.GetGreenReceptionDetails(orgId);
                if (_details.Count() > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _details);
                }
                else
                {
                    
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost, Route("AddHarvestGRNDetails")]
        public async Task<HttpResponseMessage> AddHarvestGRNDetails(HarvestGRNWeighmentDetailsDTO details)
        {
            try
            {
                var _details = await _harvestGRNWeightmentDetailsService.AddHarvestGRNDetails(details);
                   return Request.CreateResponse(HttpStatusCode.OK, _details);               
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}
