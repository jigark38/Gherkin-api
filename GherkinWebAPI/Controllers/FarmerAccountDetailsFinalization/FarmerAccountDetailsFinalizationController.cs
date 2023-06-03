using GherkinWebAPI.Core.FarmerAccountDetailsFinalization;
using GherkinWebAPI.DTO.FarmersAccountSettlementDetail;
using GherkinWebAPI.Models.FarmerAccountDetailsFinalization;
using GherkinWebAPI.Utilities;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GherkinWebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/v1/[Contoller]")]
    public class FarmerAccountDetailsFinalizationController : ApiController
    {
        private readonly IFarmerAccountDetailsFinalizationService _service;
        public readonly string controller = nameof(FarmerAccountDetailsFinalizationController);
        public FarmerAccountDetailsFinalizationController(IFarmerAccountDetailsFinalizationService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("SearchSettlement")]
        public async Task<IHttpActionResult> Search([FromBody] FarmerAccountSettlementSearchDTO settlementSearchDTO)
        {
            try
            {
                var searchList = await _service.SearchAgreement(settlementSearchDTO);
                return searchList != null ? Ok(searchList) : (IHttpActionResult)NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(FarmersAgreementController.Search)}");
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("GetFarmerInputIssues")]
        public async Task<IHttpActionResult> GetFarmerInputIssues([FromBody] FarmerAccountSettlementSearchDTO settlementSearchDTO)
        {
            try
            {
                var searchList = await _service.GetFarmerInputIssues(settlementSearchDTO);
                return searchList != null ? Ok(searchList) : (IHttpActionResult)NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(FarmersAgreementController.Search)}");
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("GetFarmerInputReturns")]
        public async Task<IHttpActionResult> GetFarmerInputReturns([FromBody] FarmerAccountSettlementSearchDTO settlementSearchDTO)
        {
            try
            {
                var searchList = await _service.GetFarmerInputReturns(settlementSearchDTO);
                return searchList != null ? Ok(searchList) : (IHttpActionResult)NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(FarmersAgreementController.Search)}");
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("GetSettlementDetails")]
        public async Task<IHttpActionResult> GetSettlementDetails([FromBody] FarmerAccountSettlementSearchDTO settlementSearchDTO)
        {
            try
            {
                var searchList = await _service.GetSettlementDetails(settlementSearchDTO);
                return searchList != null ? Ok(searchList) : (IHttpActionResult)NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(FarmersAgreementController.Search)}");
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("CreateSettlementAgreement")]
        public async Task<IHttpActionResult> CreateSettlementAgreement([FromBody]FarmersAccountSettlementDetail farmersAccountSettlementDetail)
        {
            try
            {
                if (farmersAccountSettlementDetail == null || !ModelState.IsValid)
                {
                    return BadRequest($"farmersAgreement object sent from client is not valid");
                }
                return Ok(await _service.CreateAgreement(farmersAccountSettlementDetail));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(FarmerAccountDetailsFinalizationController.CreateSettlementAgreement)}");
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("GetFarmerAdvanceDetails")]
        public async Task<IHttpActionResult> GetFarmerAdvanceDetails([FromBody] FarmerAccountSettlementSearchDTO settlementSearchDTO)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var data = await _service.GetFarmerAdvanceDetails(settlementSearchDTO);
                if (data != null)
                {

                    apiResponse.IsSucceed = true;
                    apiResponse.Data = data;
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

        [HttpPost]
        [Route("GetFarmerInputsReturnDetails")]
        public async Task<IHttpActionResult> GetFarmerInputsReturnDetails([FromBody] FarmerAccountSettlementSearchDTO settlementSearchDTO)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var data = await _service.GetFarmerInputsReturnDetails(settlementSearchDTO);
                if (data != null)
                {

                    apiResponse.IsSucceed = true;
                    apiResponse.Data = data;
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



        [HttpPost]
        [Route("GetFarmerInputsIssuedDetails")]
        public async Task<IHttpActionResult> GetFarmerInputsIssuedDetails([FromBody] FarmerAccountSettlementSearchDTO settlementSearchDTO)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var data = await _service.GetFarmerInputsIssuedDetails(settlementSearchDTO);
                if (data != null)
                {

                    apiResponse.IsSucceed = true;
                    apiResponse.Data = data;
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

        [HttpPost]
        [Route("GetFarmerAgreementAndSettlementInfo")]
        public async Task<IHttpActionResult> GetFarmerAgreementAndSettlementInfo([FromBody] FarmerAccountSettlementSearchDTO settlementSearchDTO)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var data = await _service.GetFarmerAgreementAndSettlementInfo(settlementSearchDTO);
                if (data != null)
                {

                    apiResponse.IsSucceed = true;
                    apiResponse.Data = data;
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

        [HttpPost]
        [Route("GetFarmerGreensReveivingDetails")]
        public async Task<IHttpActionResult> GetFarmerGreensReveivingDetails([FromBody] FarmerAccountSettlementSearchDTO settlementSearchDTO)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var data = await _service.GetFarmerGreensReveivingDetails(settlementSearchDTO);
                if (data != null)
                {

                    apiResponse.IsSucceed = true;
                    apiResponse.Data = data;
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
