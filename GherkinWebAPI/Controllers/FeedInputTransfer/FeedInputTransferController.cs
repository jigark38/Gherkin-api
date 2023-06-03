using GherkinWebAPI.Core.FeedInputTransfer;
using GherkinWebAPI.Core.InputToFieldStaff;
using GherkinWebAPI.DTO.FeedInputTransfer;
using GherkinWebAPI.Models.InputTransferDetails;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.FeedInputTransfer
{
    public class FeedInputTransferController : ApiController
    {
        private readonly IFeedInputTransferService _service;
        private readonly IInputToFieldStaffService _inputFieldStaffService;

        public FeedInputTransferController(IFeedInputTransferService service, IInputToFieldStaffService inputToField)
        {
            _service = service;
            _inputFieldStaffService = inputToField;
        }

        [HttpGet]
        [Route("GetInputTranferDetails")]
        public async Task<IHttpActionResult> GetInputTranferDetails(string cropNameCode, string cropSchemeCode, string psNumber, string areaId)
        {
            List<HBOMDetailsDto> hBOMs = new List<HBOMDetailsDto>();
            try
            {
                hBOMs = await _service.GetTranferDetails(cropNameCode, cropSchemeCode, psNumber, areaId);
                return Ok(hBOMs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in FeedInputTransferController / {nameof(FeedInputTransferController.GetInputTranferDetails)}");
                return InternalServerError();
            }
        }

        [HttpGet, Route("GetFarmersNoOfAcres")]
        public async Task<IHttpActionResult> GetFarmersNoOfAcres(string cropNameCode, string psNumber)
        {
            decimal? famNoOfAcres;
            try
            {
                famNoOfAcres = await _service.GetFarmersNoOfAcres(cropNameCode, psNumber);
                if (famNoOfAcres.HasValue)
                    return Ok(famNoOfAcres);
                else
                    return Ok("Not Found");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in FeedInputTransferController / {nameof(FeedInputTransferController.GetFarmersNoOfAcres)}");
                return InternalServerError();
            }
        }

        [HttpGet, Route("GetHBOMPracticePerAcreage")]
        public async Task<IHttpActionResult> GetHBOMPracticePerAcreage(string cropNameCode, string psNumber)
        {
            List<string> hBomPracPerAcre = null;
            try
            {
                hBomPracPerAcre = await _service.GetHBOMPracticePerAcreage(cropNameCode, psNumber);
                if (hBomPracPerAcre.Count > 0)
                    return Ok(hBomPracPerAcre);
                else
                    return Ok("Not Found");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in FeedInputTransferController / {nameof(FeedInputTransferController.GetHBOMPracticePerAcreage)}");
                return InternalServerError();
            }
        }

        [HttpGet, Route("GetHBOMPracticeNo")]
        public async Task<IHttpActionResult> GetHBOMPracticeNo(string cropNameCode, string psNumber)
        {
            List<string> hBomPracNo = null;
            try
            {
                hBomPracNo = await _service.GetHBOMPracticeNo(cropNameCode, psNumber);
                if (hBomPracNo.Count > 0)

                    return Ok(hBomPracNo);
                else
                    return Ok("Not Found");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in FeedInputTransferController / {nameof(FeedInputTransferController.GetHBOMPracticeNo)}");
                return InternalServerError();
            }
        }

        [HttpGet, Route("GetAllStockAndBatchDetails")]
        public async Task<IHttpActionResult> GetAllStockAndBatchDetails(string groupCode, string detailsCode, DateTime transferDate)
        {         
            try
            {
                var sbDetails = await _service.GetAllStockAndBatchDetails(groupCode, detailsCode, transferDate);
                return Ok(sbDetails);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in FeedInputTransferController / {nameof(FeedInputTransferController.GetAllStockAndBatchDetails)}");
                return InternalServerError();
            }
        }

        [HttpPost, Route("SaveStockAndBatchDetails")]
        public async Task<IHttpActionResult> SaveStockAndBatchDetails([FromBody] SaveStockAndBatchDetail saveStockAndBatchDetails)
        {
            try
            {
                var sbDetails = await _service.SaveStockAndBatchDetails(saveStockAndBatchDetails);
                return Ok(sbDetails);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in FeedInputTransferController / {nameof(FeedInputTransferController.SaveStockAndBatchDetails)}");
                return InternalServerError();
            }
        }

        [HttpGet, Route("GenerateTransferNo")]
        public async Task<IHttpActionResult> GenerateTransferNoPK()
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var res = await _service.GenerateTransferNoPK();
                if(res != null)
                {
                    apiResponse.IsSucceed = true;
                    apiResponse.Data = res;
                }
                else
                {
                    apiResponse.IsSucceed = true;
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

        [HttpGet, Route("GenerateOutwardGatePassNo")]
        public async Task<IHttpActionResult> GetOutwardGatePassNo()
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var res = await _service.GetOutwardGatePassNo();
                if (res != null)
                {
                    apiResponse.IsSucceed = true;
                    apiResponse.Data = res;
                }
                else
                {
                    apiResponse.IsSucceed = true;
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

        [HttpGet, Route("GetDetailsByCropPsNumber")]
        public async Task<IHttpActionResult> GetDetailsByCropPsNumber(string cropNameCode, string psNumber)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var res = await _service.GetDetailsByCropAndPsNumber(cropNameCode, psNumber);
                if (res != null)
                {
                    apiResponse.IsSucceed = true;
                    apiResponse.Data = res;
                }
                else
                {
                    apiResponse.IsSucceed = true;
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

        [HttpGet, Route("GetOrgOfficeDetailsList")]
        public async Task<IHttpActionResult> GetAllOrgOfficeDetails()
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var res = await _service.GetOrgofficelocationDetails();
                if (res != null)
                {
                    apiResponse.IsSucceed = true;
                    apiResponse.Data = res;
                }
                else
                {
                    apiResponse.IsSucceed = true;
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

        [HttpGet, Route("GetCropDetailsByCropGroupCode")]
        public async Task<IHttpActionResult> GetCropDetailsByCropGroupCode(string cropGroupCode)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var res = await _service.GetCropDetailsByCode(cropGroupCode);
                if (res != null)
                {
                    apiResponse.IsSucceed = true;
                    apiResponse.Data = res;
                }
                else
                {
                    apiResponse.IsSucceed = true;
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

        [HttpGet, Route("GetAllRawMaterialGroups")]
        public async Task<IHttpActionResult> GetAllRawMaterialGroups()
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var res = await _inputFieldStaffService.GetAllRawMaterialGroups();
                if (res != null)
                {
                    apiResponse.IsSucceed = true;
                    apiResponse.Data = res;
                }
                else
                {
                    apiResponse.IsSucceed = true;
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

        [HttpGet, Route("GetAllRawMaterialDetails")]
        public async Task<IHttpActionResult> GetAllRawMaterialDetails()
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var res = await _inputFieldStaffService.GetAllRawMaterialDetails();
                if (res != null)
                {
                    apiResponse.IsSucceed = true;
                    apiResponse.Data = res;
                }
                else
                {
                    apiResponse.IsSucceed = true;
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
