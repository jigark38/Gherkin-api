using GherkinWebAPI.Core.PurchaseMgmt;
using GherkinWebAPI.DTO.PurchageMgmt;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.GoodsReceiptNote;
using GherkinWebAPI.Models.PurchageMgmt;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.PurchageMgmt
{
    [Route("api/V1/[Controller]")]
    public class PurchageManagementController : ApiController
    {
        private readonly IPurchageManagementService _pmService;

        public PurchageManagementController(IPurchageManagementService pmService)
        {
            _pmService = pmService;
        }

        [Route("GetAllPurchaseOrder")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllPurchaseOrder()
        {
            List<PurchaseOrderDetail> purchageOrders = new List<PurchaseOrderDetail>();
            try
            {
                purchageOrders = await _pmService.GetAllPurchaseOrder();
                if (purchageOrders.Count > 0)
                    return Ok(purchageOrders);
                else
                    return Ok("Purchage order details are not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in PurchageManagementController / {nameof(PurchageManagementController.GetAllPurchaseOrder)}");
                return InternalServerError();
            }
        }

        [Route("GetAllPendingPurchaseOrder")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllPendingPurchaseOrder()
        {
            try
            {
                var pendingPurchageOrders = await _pmService.GetPendingPurchaseOrdersAsync();
                    return Ok(pendingPurchageOrders);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in PurchageManagementController / {nameof(PurchageManagementController.GetAllPendingPurchaseOrder)}");
                return InternalServerError();
            }
        }
        [Route("CreatePurchageOrder")]
        [HttpPost]
        public async Task<IHttpActionResult> CreatePurchageOrder([FromBody] CreatePOWithMaterialAndCondition poDetail)
        {
            CreatePOWithMaterialAndCondition purchageOrder = new CreatePOWithMaterialAndCondition();
            try
            {
                purchageOrder = await _pmService.CreatePurchaseOrder(poDetail);
                return Ok(purchageOrder);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in PurchageManagementController / {nameof(PurchageManagementController.CreatePurchageOrder)}");
                return InternalServerError();
            }
        }

        [Route("GetIndentDetails")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllIndentDetails()
        {
            List<IndentDetail> indentDetails = new List<IndentDetail>();
            try
            {
                indentDetails = await _pmService.GetAllIndentDetails();
                if (indentDetails.Count > 0)
                    return Ok(indentDetails);
                else
                    return Ok("Indents details are not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in PurchageManagementController / {nameof(PurchageManagementController.GetAllIndentDetails)}");
                return InternalServerError();
            }
        }

        [Route("GetNextPurchageOrderId")]
        [HttpGet]
        public async Task<IHttpActionResult> GetNextPurchageOrderId()
        {
            try
            {
                int maxId = await _pmService.GetNextPurchageOrderId();
                if (maxId > 0)
                    return Ok(maxId + 1);
                else
                    return Ok(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in PurchageManagementController / {nameof(PurchageManagementController.GetNextPurchageOrderId)}");
                return InternalServerError();
            }
        }

        [Route("GetPlacesBySuppOrgId")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPlacesBySuppOrgId([FromUri] string suppOrgId)
        {
            List<Place> places = new List<Place>();
            try
            {
                places = await _pmService.GetPlacesBySuppOrgId(suppOrgId);
                if (places != null)
                    return Ok(places);
                else
                    return Ok($"Place details not available for supplier organisation Id: { suppOrgId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in PurchageManagementController / {nameof(PurchageManagementController.GetPlacesBySuppOrgId)}");
                return InternalServerError();
            }
        }

        [Route("GetStatesBySuppOrgId")]
        [HttpGet]
        public async Task<IHttpActionResult> GetStatesBySuppOrgId([FromUri] string suppOrgId)
        {
            List<State> states = new List<State>();
            try
            {
                states = await _pmService.GetStatesBySuppOrgId(suppOrgId);
                if (states != null)
                    return Ok(states);
                else
                    return Ok($"State setails not available for supplier organisation Id : {suppOrgId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in PurchageManagementController / {nameof(PurchageManagementController.GetStatesBySuppOrgId)}");
                return InternalServerError();
            }
        }

        [Route("GetCountryBySppOrgId")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCountryBySppOrgId([FromUri] string suppOrgId)
        {
            List<Country> contries = new List<Country>();
            try
            {
                contries = await _pmService.GetCountryBySppOrgId(suppOrgId);
                if (contries != null)
                    return Ok(contries);
                else
                    return Ok($"Contry details not available for supplier organisation Id : {suppOrgId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in PurchageManagementController / {nameof(PurchageManagementController.GetCountryBySppOrgId)}");
                return InternalServerError();
            }
        }

        [Route("CreateOrderMaterial")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateOrderMaterial([FromBody] RMPOMaterialDetail poDetail)
        {
            RMPOMaterialDetail rmPOMaterialDetail = new RMPOMaterialDetail();
            try
            {
                if (poDetail != null)
                    rmPOMaterialDetail = await _pmService.CreateOrderMaterial(poDetail);
                return Ok(rmPOMaterialDetail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in PurchageManagementController / {nameof(PurchageManagementController.CreateOrderMaterial)}");
                return InternalServerError();
            }
        }
        [Route("GetAllRMPOMaterial")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllRMPOMaterial()
        {
            List<RMPOMaterialDetail> rmPOMaterialDetail = new List<RMPOMaterialDetail>();
            try
            {
                rmPOMaterialDetail = await _pmService.GetAllRMPOMaterial();
                return Ok(rmPOMaterialDetail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in PurchageManagementController / {nameof(PurchageManagementController.GetAllRMPOMaterial)}");
                return InternalServerError();
            }
        }
        [Route("GetTaxPercentByGSTType")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTaxPercentByGSTType([FromUri] string detailCode, [FromUri] string gstType)
        {
            TaxPercentageRate tpr = new TaxPercentageRate();
            try
            {
                tpr = await _pmService.GetTaxPercentByGSTType(detailCode, gstType);
                return Ok(tpr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in PurchageManagementController / {nameof(PurchageManagementController.GetTaxPercentByGSTType)}");
                return InternalServerError();
            }
        }

        [Route("GetAllSuppliers")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllSuppliers()
        {
            List<SupplierDetails> supDetails = new List<SupplierDetails>();
            try
            {
                supDetails = await _pmService.GetAllSuppliers();
                if (supDetails.Count > 0)
                    return Ok(supDetails);
                else
                    return Ok($"Suppliers details are not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in PurchageManagementController / {nameof(PurchageManagementController.GetAllSuppliers)}");
                return InternalServerError();
            }
        }

        [Route("GetSupplierOrgNameByRMGRNNo")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSupplierOrgNameByRMGRNNo([FromUri] string rmGrnNo)
        {
            SupplierDetails sd = new SupplierDetails();
            try
            {
                sd = await _pmService.GetSupplierOrgNameByRMGRNNo(rmGrnNo);
                return Ok(sd);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in PurchageManagementController / {nameof(PurchageManagementController.GetSupplierOrgNameByRMGRNNo)}");
                return InternalServerError();
            }
        }

        [Route("GetPlaceNameBySuppOrgId")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPlaceNameBySuppOrgId([FromUri] string supOrgId)
        {
            Place place = new Place();
            try
            {
                place = await _pmService.GetPlaceNameBySuppOrgId(supOrgId);
                return Ok(place);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in PurchageManagementController / {nameof(PurchageManagementController.GetPlaceNameBySuppOrgId)}");
                return InternalServerError();
            }
        }

        [Route("GetRMGrnDetailsByRMGrnNo")]
        [HttpGet]
        public async Task<IHttpActionResult> GetRMGrnDetailsByRMGrnNo([FromUri] string rmGrnNo)
        {
            RMGRNDetail rgd = new RMGRNDetail();
            try
            {
                rgd = await _pmService.GetRMGrnDetailsByRMGrnNo(rmGrnNo);
                return Ok(rgd);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in PurchageManagementController / {nameof(PurchageManagementController.GetRMGrnDetailsByRMGrnNo)}");
                return InternalServerError();
            }
        }

        [Route("GetPurchageRecievedDetails")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPurchageRecievedDetails([FromUri] string suppOrgId)
        {
            List<PurchageReceivedDetail> prd = new List<PurchageReceivedDetail>();
            try
            {
                prd = await _pmService.GetPurchageRecievedDetails(suppOrgId);
                if (prd.Count > 0)
                    return Ok(prd);
                else
                    return Ok("No Records Exist");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in PurchageManagementController / {nameof(PurchageManagementController.GetPurchageRecievedDetails)}");
                return InternalServerError();
            }
        }
        [Route("GetMaterialRecievedDetails")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMaterialRecievedDetails([FromUri] string rmGrnNo)
        {
            List<PurchaseReceivedMaterialDetail> prmd = new List<PurchaseReceivedMaterialDetail>();
            try
            {
                prmd = await _pmService.GetMaterialRecievedDetails(rmGrnNo);
                if (prmd.Count > 0)
                    return Ok(prmd);
                else
                    return Ok("No Records Exist");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in PurchageManagementController / {nameof(PurchageManagementController.GetMaterialRecievedDetails)}");
                return InternalServerError();
            }
        }

        [Route("CreatePurchaseReturn")]
        [HttpPost]
        public async Task<IHttpActionResult> CreatePurchaseReturn([FromBody] CreatePurchageReturn pReturnDetail)
        {
            CreatePurchageReturn purchageReturn = new CreatePurchageReturn();
            try
            {
                purchageReturn = await _pmService.CreatePurchaseReturn(pReturnDetail);
                return Ok(purchageReturn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in PurchageManagementController / {nameof(PurchageManagementController.CreatePurchaseReturn)}");
                return InternalServerError();
            }
        }

        [Route("GetOrderIdsBySuppOrgId")]
        [HttpGet]
        public async Task<IHttpActionResult> GetOrderIdsBySuppOrgId(string SuppOrgId)
        {
            //List<int> OrderIds = new List<int>();
            try
            {
                List<string> OrderIds = await _pmService.GetOrderIdsBySuppOrgId(SuppOrgId);
                if (OrderIds.Count > 0)
                {
                    return Ok(OrderIds);
                }
                else
                {
                    return Ok("Orders for this supplier are not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in PurchageManagementController / {nameof(PurchageManagementController.GetOrderIdsBySuppOrgId)}");
                return InternalServerError();
            }
        }

        [Route("GetPurchaseOrderByID")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPurchaseOrderByID(string RmPoNo)
        {
            CreatePOWithMaterialAndCondition purchageOrder = new CreatePOWithMaterialAndCondition();
            try
            {
                purchageOrder = await _pmService.GetPurchaseOrderByID(RmPoNo);
                return Ok(purchageOrder);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in PurchageManagementController / {nameof(PurchageManagementController.GetPurchaseOrderByID)}");
                return InternalServerError();
            }
        }

        [Route("ModifyPurchaseOrder")]
        [HttpPost]
        public async Task<IHttpActionResult> ModifyPurchaseOrder([FromBody] CreatePOWithMaterialAndCondition poDetail)
        {
            CreatePOWithMaterialAndCondition purchageOrder = new CreatePOWithMaterialAndCondition();
            try
            {
                purchageOrder = await _pmService.ModifyPurchaseOrder(poDetail);
                return Ok(purchageOrder);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in PurchageManagementController / {nameof(PurchageManagementController.ModifyPurchaseOrder)}");
                return InternalServerError();
            }
        }

        [Route("GetPurchaseReturnIdsBySuppOrgId")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPurchaseReturnIdsBySuppOrgId(string SuppOrgId)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            //List<int> OrderIds = new List<int>();
            try
            {
                List<string> OrderIds = await _pmService.GetPurchaseReturnIdsBySuppOrgId(SuppOrgId);
                if (OrderIds.Count > 0)
                {
                    apiResponse.IsSucceed = true;
                    apiResponse.Data = OrderIds;
                    return Ok(apiResponse);
                }
                else
                {
                    apiResponse.IsSucceed = true;
                    return Ok(apiResponse);
                }
            }
            catch (Exception ex)
            {
                apiResponse.IsSucceed = false;
                apiResponse.Exception = ex;
                return Ok(apiResponse);
            }
        }

        [Route("ModifyPurchaseReturn")]
        [HttpPost]
        public async Task<IHttpActionResult> ModifyPurchaseReturn([FromBody] CreatePurchageReturn poDetail)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                CreatePurchageReturn purchaseReturn = new CreatePurchageReturn();
                purchaseReturn = await _pmService.ModifyPurchaseReturn(poDetail);
                apiResponse.IsSucceed = true;
                apiResponse.Data = purchaseReturn;
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.IsSucceed = false;
                apiResponse.Exception = ex;
                return Ok(apiResponse);
            }
        }

        [Route("FindPurchaseReturnById")]
        [HttpGet]
        public async Task<IHttpActionResult> FindPurchaseReturnById(string purchaseReturnID)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                CreatePurchageReturn purchaseReturn = new CreatePurchageReturn();
                purchaseReturn = await _pmService.FindPurchaseReturnById(purchaseReturnID);
                apiResponse.IsSucceed = true;
                apiResponse.Data = purchaseReturn;
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
