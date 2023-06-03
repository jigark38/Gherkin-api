using GherkinWebAPI.Core;
using GherkinWebAPI.Core.ProvidentFund;
using GherkinWebAPI.DTO.ProvidentFund;
using GherkinWebAPI.Models.ProvidentFund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.ProvidentFund
{
    [Route("api/V1/[Controller]")]
    public class ProvidentFundRateDetailsController : ApiController
    {
        private IProvidentFundRateDetailsService _providentFundRateDetailsService;
        public ProvidentFundRateDetailsController(IProvidentFundRateDetailsService providentFundRateDetailsService)
        {
            _providentFundRateDetailsService = providentFundRateDetailsService;
        }

        [HttpPost, Route("AddProvidentFundRateDetails")]
        public async Task<HttpResponseMessage> AddProvidentFundRateDetails([FromBody] ProvidentFundRateDetails addProvidentFundRateDetails)
        {
            ProvidentFundRateDetails providentFundRateDetails = new ProvidentFundRateDetails();
            try
            {
                if (addProvidentFundRateDetails != null || ModelState.IsValid)
                {
                    providentFundRateDetails = await _providentFundRateDetailsService.AddProvidentFundRateDetail(addProvidentFundRateDetails);
                    return Request.CreateResponse(HttpStatusCode.OK, providentFundRateDetails);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPost, Route("UpdateProvidentFundRateDetails")]
        public async Task<HttpResponseMessage> UpdateProvidentFundRateDetails([FromBody] ProvidentFundRateDetails providentFundRateDetail)
        {
            ProvidentFundRateDetails providentFundRateDetails = new ProvidentFundRateDetails();
            try
            {
                if (providentFundRateDetail != null || ModelState.IsValid)
                {
                    providentFundRateDetails = await _providentFundRateDetailsService.UpdateProvidentFundRateDetail(providentFundRateDetail);
                    return Request.CreateResponse(HttpStatusCode.OK, providentFundRateDetails);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        [Route("GetProvidentFundRateDetails")]
        public async Task<IHttpActionResult> GetProvidentFundRateDetails()
        {
            try
            {
                List<ProvidentFundRateDetails> providentFundRateDetails = await _providentFundRateDetailsService.GetProvidentFundRateDetails();
                return Ok(providentFundRateDetails);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
           
        }
    }
}
