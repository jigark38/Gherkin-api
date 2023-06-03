using GherkinWebAPI.Core.GoodsReceiptNote;
using GherkinWebAPI.Models.GoodsReceiptNote;
using GherkinWebAPI.Utilities;
using GherkinWebAPI.ValidateModel;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GherkinWebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/v1/[Contoller]")]
    public class GoodsReceiptNoteController : ApiController
    {
        private readonly IGRNService _service;
        private readonly IGRNMaterialService _materialService;
        private readonly IMaterialTotalCostService _materialTotalCostService;

        public readonly string controller = nameof(GoodsReceiptNoteController);

        public GoodsReceiptNoteController(IGRNService service, IGRNMaterialService materialService, IMaterialTotalCostService materialTotalCostService)
        {
            _service = service;
            _materialService = materialService;
            _materialTotalCostService = materialTotalCostService;
        }

        [HttpGet]
        [Route("GetGRNCode")]
        public async Task<IHttpActionResult> GetGRNCode()
        {
            try
            {
                var grnCode = await _service.GetGRNCode();
                if (grnCode > 0)
                {
                    return Ok(grnCode);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(GoodsReceiptNoteController.GetGRNCode)}");
                return InternalServerError();
            }

        }

        [HttpPost]
        [Route("CreateGoodsReceiptNote")]
        [ValidateModelState]
        public async Task<IHttpActionResult> CreateGoodsReceipNote([FromBody] OrderDetail orderDetail)
        {
            try
            {
                var rmGRNDetail = await _service.CreateGRNDetail(orderDetail);

                var rmGRNMaterialDetail = await _materialService.CreateGRNMaterial(orderDetail);

                var rmMaterialCostDetail = await _materialTotalCostService.CreateMaterialTotalCost(orderDetail);

                if (rmGRNDetail != null && rmGRNMaterialDetail != null && rmMaterialCostDetail != null)
                {
                    return Ok();
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(GoodsReceiptNoteController.CreateGoodsReceipNote)}");
                return InternalServerError();
            }

        }

        [HttpGet]
        [Route("GetGoodsReceiptNoteByGRNCode")]
        [ValidateModelState]
        public async Task<IHttpActionResult> GetGoodsReceiptNoteByGRNCode(string GRNCode)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var rmGRNDetail = await _service.GetGRNDetailByGRNCode(GRNCode);

                var rmGRNMaterialDetail = await _materialService.GetGRNMaterialByGRNCode(GRNCode);

                var rmMaterialCostDetail = await _materialTotalCostService.GetMaterialTotalCostByGRNCode(GRNCode);

                if (rmGRNDetail != null && (rmGRNMaterialDetail != null || rmMaterialCostDetail != null))
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail = rmGRNDetail;
                    orderDetail.OrderMaterialDetails = rmGRNMaterialDetail;
                    orderDetail.OrderMaterialTotalCostDetails = rmMaterialCostDetail;
                    apiResponse.IsSucceed = true;
                    apiResponse.Data = orderDetail;
                    return Ok(apiResponse);
                }
                else
                    apiResponse.IsSucceed = true;
                    apiResponse.Data = null;
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
        [Route("GetGRNCodeBySupOrgId")]
        public async Task<IHttpActionResult> GetGRNCodeBySupOrgId(string SupOrgId)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var grnCodes = await _service.GetGRNCodeBySupOrgId(SupOrgId);
                apiResponse.IsSucceed = true;
                apiResponse.Data = grnCodes;
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
        [Route("UpdateGoodsReceiptNote")]
        [ValidateModelState]
        public async Task<IHttpActionResult> UpdateGoodsReceiptNote([FromBody] OrderDetail orderDetail)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var rmGRNDetail = await _service.UpdateGRNDetail(orderDetail);

                var rmGRNMaterialDetail = await _materialService.UpdateGRNMaterial(orderDetail);

                var rmMaterialCostDetail = await _materialTotalCostService.UpdateMaterialTotalCost(orderDetail);

                if (rmGRNDetail != null && rmGRNMaterialDetail != null && rmMaterialCostDetail != null)
                {
                    apiResponse.IsSucceed = true;
                }
                else
                    apiResponse.IsSucceed = false;
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
        [Route("UpdateBatchMaterialDetails")]
        [ValidateModelState]
        public async Task<IHttpActionResult> UpdateBatchMaterialDetails([FromBody] BatchMaterialDetails batchMaterialDetails)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();

            try
            {
                var rmGRNDetail = await _service.UpdateBatchMaterialDetails(batchMaterialDetails);
                if (rmGRNDetail != null)
                {
                    apiResponse.Data = rmGRNDetail;
                    apiResponse.IsSucceed = true;
                }
                else
                    apiResponse.IsSucceed = false;
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
        [Route("GetBatchMaterialDetailsByBatchNo")]
        [ValidateModelState]
        public async Task<IHttpActionResult> GetBatchMaterialDetailsByBatchNo(string batchNo)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();

            try
            {
                var rmGRNDetail = await _service.GetBatchMaterialDetailsByBatchNo(batchNo);
                if (rmGRNDetail != null)
                {
                    apiResponse.Data = rmGRNDetail;
                    apiResponse.IsSucceed = true;
                }
                else
                    apiResponse.IsSucceed = false;
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
