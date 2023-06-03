using GherkinWebAPI.Core.Reports.GreensReceiptsSummary;
using GherkinWebAPI.DTO.Reports.GreensReceiptsSummary;
using GherkinWebAPI.Models;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.Reports.GreensReceiptsSummary
{
    [RoutePrefix("GreensReceiptsSummaryReport")]
    public class GreensReceiptsSummaryController : ApiController
    {
        private readonly IGreensReceiptsSummaryRepository _repository;

        public GreensReceiptsSummaryController(IGreensReceiptsSummaryRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        [Route("GetSeasonFromTo")]
        public async Task<IHttpActionResult> GetSeasonFromToDllData()
        {
            ApiResponse<List<PlantationSchedule>> data = new ApiResponse<List<PlantationSchedule>>();
            try
            {
                data.Data = await _repository.GetSeasonFromToDllData();
                data.IsSucceed = true;
            }
            catch (Exception ex)
            {
                data.Exception = ex;
                data.IsSucceed = false;
            }
            return Ok(data);
        }


        [HttpGet]
        [Route("GetMaterialGroup")]
        public async Task<IHttpActionResult> GetMaterialGroupDllData()
        {
            ApiResponse<List<CropGroup>> data = new ApiResponse<List<CropGroup>>();
            try
            {
                data.Data = await _repository.GetMaterialGroupDllData();
                data.IsSucceed = true;
            }
            catch (Exception ex)
            {
                data.Exception = ex;
                data.IsSucceed = false;
            }
            return Ok(data);
        }

        [HttpGet]
        [Route("GetMaterialName")]
        public async Task<IHttpActionResult> GetMaterialNameFromGroupDllData(string cropGroupName)
        {
            ApiResponse<List<CropName>> data = new ApiResponse<List<CropName>>();
            try
            {
                data.Data = await _repository.GetMaterialNameFromGroupDllData(cropGroupName);
                data.IsSucceed = true;
            }
            catch (Exception ex)
            {
                data.Exception = ex;
                data.IsSucceed = false;
            }
            return Ok(data);
        }



        [Route("GetReport")]
        [HttpPost]
        public async Task<IHttpActionResult> GetReport(GreensReceiptsSummaryReportDataDto data)
        {
            return Ok(await _repository.GetReportData(data));
        }
    }
}