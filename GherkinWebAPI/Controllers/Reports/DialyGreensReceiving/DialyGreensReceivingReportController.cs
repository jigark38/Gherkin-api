using GherkinWebAPI.Core.Reports.DialyGreensReceiving;
using GherkinWebAPI.DTO.Reports.DialyGreensReceiving;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.Reports.DialyGreensReceiving
{
    [RoutePrefix("DialyGreensReceivingReport")]
    public class DialyGreensReceivingReportController : ApiController
    {
        private readonly IDialyGreensReceivingRepository _repository;
        public DialyGreensReceivingReportController(IDialyGreensReceivingRepository repository)
        {
            _repository = repository;
        }

        [Route("GetBuyers")]
        [HttpGet]
        public async Task<IHttpActionResult> GetBuyers(string date, string areaCode)
        {
            return Ok(await _repository.GetBuyers(date.DateTimeTryParse(), areaCode));
        }

        [Route("GetDailyBuyerWiseReport")]
        [HttpPost]
        public async Task<IHttpActionResult> GetDailyBuyerWiseReport(DialyGreensReceivingForBuyersDto data)
        {
            return Ok(await _repository.GetDailyBuyerWiseReport(data));
        }
    }
}
