using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.AreaMaterialReceivedDetails
{
    [RoutePrefix("api/V1/AreaMRInward")]
    public class AreaMRInwardDetailsController : ApiController
    {
        private IAreaMRInwardService _areaMRInwardService;
        public AreaMRInwardDetailsController(IAreaMRInwardService areaMRInwardService)
        {
            _areaMRInwardService = areaMRInwardService;
        }

        [HttpGet]
        [Route("GetAreaMRInward")]
        public IHttpActionResult GetAreaMRInwardDetails()
        {
            try
            {
                var data = _areaMRInwardService.GetAllAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                ExceptonLog.LogError(ex, "AreaMRInwardDetailsController", "GetAreaMRInward");
            }
            return NotFound();
        }

        [HttpGet]
        [Route("GridData/{areaid}")]
        public async Task<IHttpActionResult> GetAreaMRInwardDetailsbyAreaId(string areaid)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var data = await _areaMRInwardService.GetAMRbyAreaId(areaid);
                if (data != null)
                {
                    apiResponse.IsSucceed = true;
                    apiResponse.Data = data;
                }
                else
                {
                    apiResponse.IsSucceed = false;
                    apiResponse.Data = null;
                }

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.IsSucceed = false;
                apiResponse.Exception = ex;
                return Ok(apiResponse);
            }
        }


        [HttpGet]
        [Route("Note1")]
        public IHttpActionResult Note1(string areaid, string RMTransferNo)
        {
            try
            {
                var data = _areaMRInwardService.Getnote1(areaid, RMTransferNo);
                return Ok(data);
            }
            catch (Exception ex)
            {
                ExceptonLog.LogError(ex, "AreaMRInwardDetailsController", "Note1");
            }
            return NotFound();
        }

        [HttpGet]
        [Route("Note2")]
        public IHttpActionResult Note2(string areaid, string RMTransferNo)
        {
            try
            {
                var data = _areaMRInwardService.Getnote2(areaid, RMTransferNo);
                return Ok(data);
            }
            catch (Exception ex)
            {
                ExceptonLog.LogError(ex, "AreaMRInwardDetailsController", "Note2");
            }
            return NotFound();
        }

        [HttpGet]
        [Route("GetAreaMRNo")]
        public IHttpActionResult GetAreaMRNo()
        {
            try
            {
                var data = _areaMRInwardService.GetAreaMRNo();
                return Ok(data);
            }
            catch (Exception ex)
            {
                ExceptonLog.LogError(ex, "AreaMRInwardDetailsController", "GetAreaMRNo");
            }
            return NotFound();
        }



        [HttpPost]
        [Route("SaveAreaMRDetails")]
        public async Task<IHttpActionResult> SaveAreaMRDetails([FromBody] AreaMRInwardPostDetails areaMRInwardDetails)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var data = await _areaMRInwardService.CreateAreaDetails(areaMRInwardDetails);
                if (data)
                {
                    apiResponse.IsSucceed = true;
                    apiResponse.Data = data;
                }
                else
                {
                    apiResponse.IsSucceed = false;
                    apiResponse.Data = null;
                }

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.IsSucceed = false;
                apiResponse.Exception = ex;
                return Ok(apiResponse);
            }
        }

    }
}
