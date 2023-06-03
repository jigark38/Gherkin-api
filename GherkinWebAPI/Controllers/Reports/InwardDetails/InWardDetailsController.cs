using GherkinWebAPI.Core.Reports.InWardDailyReport;
using GherkinWebAPI.DTO.Reports.InWardDailyReport;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.Reports.InwardDetails
{
    [RoutePrefix("InWardDetails")]
    public class InWardDetailsController : ApiController
    {
        private readonly IHarvestGRNReportRepository _repository;

        public InWardDetailsController(IHarvestGRNReportRepository repository)
        {
            this._repository = repository;
        }

        [HttpPost]
        [Route("ReportData")]
        public async Task<IHttpActionResult> Test(InwardDetailRequest request)
        {
            ApiResponse<object> data = new ApiResponse<object>();
            try
            {
                var dataFinal = await _repository.GetReportData(request);
                if (dataFinal != null)
                {
                    data.Data = dataFinal;
                    data.IsSucceed = true;
                }
                else
                {
                    data.IsSucceed = false;
                }

            }
            catch (Exception ex)
            {
                data.Exception = ex;
                data.IsSucceed = false;
            }
            return Ok(data);

        }

    }
}
