using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.DTO.RMStock;
using GherkinWebAPI.Request.RMStock;
using GherkinWebAPI.Response.RMStock;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GherkinWebAPI.Controllers.RMStock
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RMStockController : ApiController
    {
        private readonly IRMStockService _service;

        public RMStockController(IRMStockService service)
        {
            this._service = service;
        }

        [Route("rmstock/formdetail")]
        public async Task<HttpResponseMessage> GetRMStockDataForForm()
        {

            RMStockFormShowResponse data = new RMStockFormShowResponse();
            try
            {
                data = await _service.GetRMStockFormData();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }

        [HttpPost]
        [Route("rmstock/add")]
        public async Task<IHttpActionResult> InsertRMStockDetails(RMStockInsertRequest rMStockInsertRequest)
        {

           
                return Ok(await _service.InsertRMStockDetails(rMStockInsertRequest));
            

        }

        [Route("rmbranch/formdetail")]
        public async Task<HttpResponseMessage> GetRMBranchDataForForm()
        {

            RMBranchShowFormResponse data = new RMBranchShowFormResponse();
            try
            {
                data = await _service.GetRMBranchDataForForm();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("rmstockbranch/add")]
        public async Task<HttpResponseMessage> InsertRMStockBranchDetails(RMStockBranchInsertRequest rMStockBranchInsertRequest)
        {

            RMStockBranchInsertResponse data = new RMStockBranchInsertResponse();
            try
            {
                data = await _service.InsertRMStockBranchDetails(rMStockBranchInsertRequest);
                if (string.IsNullOrEmpty(data.ResponseMessage))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, data);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("rmstockbranch/find")]
        public async Task<HttpResponseMessage> FindRMStockBranchDetails(string areaId)
        {
            if (string.IsNullOrEmpty(areaId))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            List<RMStockBranchQuantityDetailDTO> data = new List<RMStockBranchQuantityDetailDTO>();
            try
            {
                data = await _service.FindRMStockBranchDetails(areaId);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("rmstockbranch/update")]
        public async Task<HttpResponseMessage> UpdateRMStockBranchDetails(List<RMStockBranchQuantityDetailDTO> RMBranchUpdateDetailsModel)
        {
            if (RMBranchUpdateDetailsModel == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                await _service.UpdateRMStockBranchDetails(RMBranchUpdateDetailsModel);
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    result = "success"
                });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("rmstockbranch/findStockDetail")]
        public async Task<IHttpActionResult> FIndRMStockDetail(string orgOfficeNo, string rawMaterialGroupCode, string rawMaterialDetailsCode)
        {
            if (string.IsNullOrEmpty(orgOfficeNo) || string.IsNullOrEmpty(rawMaterialGroupCode) || string.IsNullOrEmpty(rawMaterialDetailsCode))
            {
                return BadRequest();
            }
            return Ok(await _service.FIndRMStockDetail(orgOfficeNo, rawMaterialGroupCode, rawMaterialDetailsCode));
        }

        [HttpPost]
        [Route("rmstockbranch/updateStockDetail")]
        public async Task<IHttpActionResult> UpdateRMStockDetail(RMStockDetailsDto newRMStockDetailsDtoItem)
        {
            if(newRMStockDetailsDtoItem == null)
            {
                return BadRequest();
            }            
            return Ok(await _service.UpdateRMStockDetail(newRMStockDetailsDtoItem));
        }
    }
            
}
