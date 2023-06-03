using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.Response;
using GherkinWebAPI.Request;
using GherkinWebAPI.CustomExceptions;
using GherkinWebAPI.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Net.Http;
using System.Net;
using GherkinWebAPI.Entities;

namespace GherkinWebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RawMaterialController : ApiController
    {
        private readonly IRawMaterialService _service;

        public RawMaterialController(IRawMaterialService service)
        {
            this._service = service;
        }

        [Route("rawmaterial/master")]
        public async Task<HttpResponseMessage> GetRawmaterialMaster()
        {

            List<RawMaterialMasterDto> groupCodes = new List<RawMaterialMasterDto>();
            try
            {
                groupCodes = await _service.GetRawmaterialMaster();
                return Request.CreateResponse(HttpStatusCode.OK, groupCodes);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }

        [Route("rawmaterial/details")]
        public async Task<HttpResponseMessage> GetRawmaterialDetails()
        {
            List<RawMaterialDetailsDto> details = new List<RawMaterialDetailsDto>();
            try
            {
                details = await _service.GetRawmaterialDetails();
                return Request.CreateResponse(HttpStatusCode.OK, details);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }

        [HttpPost]
        [Route("rawmaterial/master/create")]
        public async Task<HttpResponseMessage> CreateRawMaterialGroup([FromBody] RawMaterialMasterCreateRequest rawMaterialGroupMasterReq)
        {
            try
            {
                if (rawMaterialGroupMasterReq != null)
                {
                    RawMaterialMaster rawMaterialGroupMaster = new RawMaterialMaster
                    {
                        Raw_Material_Group = rawMaterialGroupMasterReq.Raw_Material_Group,
                        Material_Purchases = rawMaterialGroupMasterReq.Material_Purchases
                    };
                    await _service.CreateRawMaterialMaster(rawMaterialGroupMaster);
                    return Request.CreateResponse(HttpStatusCode.OK, new BoolResponse { Result = true });
                }
            }
            catch (CustomException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { ResponseMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [Route("rawmaterial/master/update")]
        public async Task<HttpResponseMessage> UpdateRawMaterialGroup([FromBody] RawMaterialMasterDto rawMaterialMasterDto)
        {
            try
            {
                if (rawMaterialMasterDto != null)
                {
                    RawMaterialMaster rawMaterialGroupMaster = new RawMaterialMaster
                    {
                        Raw_Material_Group = rawMaterialMasterDto.Raw_Material_Group,
                        ID = rawMaterialMasterDto.ID,
                        Raw_Material_Group_Code = rawMaterialMasterDto.Raw_Material_Group_Code,
                        Material_Purchases = rawMaterialMasterDto.Material_Purchases
                    };
                    await _service.UpdateRawMaterialMaster(rawMaterialGroupMaster);
                    return Request.CreateResponse(HttpStatusCode.OK, new BoolResponse { Result = true });
                }
            }
            catch (CustomException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { ResponseMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [Route("rawmaterial/uom/create")]
        public async Task<HttpResponseMessage> CreateRawMaterialUOM([FromBody] RMUom RMUomDto)
        {
            try
            {
                if (RMUomDto != null)
                {
                    RMUom inserted = await _service.CreateRawMaterialUOM(RMUomDto);
                    return Request.CreateResponse(HttpStatusCode.OK, inserted);
                }
            }
            catch (CustomException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { ResponseMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [Route("rawmaterial/details/create")]
        public async Task<HttpResponseMessage> CreateRawMaterialDetails([FromBody] RawMaterialDetailsRequest rawMaterialDetailsReq)
        {

            try
            {
                if (rawMaterialDetailsReq != null)
                {
                    RawMaterialDetails rawMaterialDetails = new RawMaterialDetails
                    {
                        Raw_Material_Cess_Rate = rawMaterialDetailsReq.Raw_Material_Cess_Rate,
                        Raw_Material_CGST_Rate = rawMaterialDetailsReq.Raw_Material_CGST_Rate,
                        Raw_Material_Details_Name = rawMaterialDetailsReq.Raw_Material_Details_Name,
                        Raw_Material_Group_Code = rawMaterialDetailsReq.Raw_Material_Group_Code,
                        Raw_Material_HSN_CODE_No = rawMaterialDetailsReq.Raw_Material_HSN_CODE_No,
                        Raw_Material_IGST_Rate = rawMaterialDetailsReq.Raw_Material_IGST_Rate,
                        Raw_Material_QC_Norms = rawMaterialDetailsReq.Raw_Material_QC_Norms,
                        Raw_Material_Reorder_Stock = rawMaterialDetailsReq.Raw_Material_Reorder_Stock,
                        Raw_Material_SGST_Rate = rawMaterialDetailsReq.Raw_Material_SGST_Rate,
                        Raw_Material_UOM = rawMaterialDetailsReq.Raw_Material_UOM
                    };
                    var details = await _service.CreateRawMaterialDetails(rawMaterialDetails);
                    return Request.CreateResponse(HttpStatusCode.OK, details);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [HttpPut]
        [Route("rawmaterial/details/update/{id}")]
        public async Task<HttpResponseMessage> UpdateRawmaterial(string id, [FromBody] RawMaterialDetailsRequest rawMaterialDetailsReq)
        {
            try
            {
                if (rawMaterialDetailsReq != null)
                {
                    var details = await _service.UpdateRawMaterialDetails(id, rawMaterialDetailsReq);
                    return Request.CreateResponse(HttpStatusCode.OK, details);
                }
            }
            catch (CustomException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { ResponseMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("rawmaterial/GetRMDeatilsCodeNameByGroupCode")]
        public async Task<HttpResponseMessage> GetRMDeatilsCodeNameByGroupCode(string rawMaterialGroupCode)
        {
            List<RawMaterialDetailsDto> rawMaterialDeatils = new List<RawMaterialDetailsDto>();
            try
            {
                rawMaterialDeatils = await _service.GetRMDeatilsCodeNameByGroupCode(rawMaterialGroupCode);
                return Request.CreateResponse(HttpStatusCode.OK, rawMaterialDeatils);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [Route("rawmaterial/GetAllRMUom")]
        public async Task<HttpResponseMessage> GetAllUom()
        {
            try
            {
                List<RMUom> uomList= await _service.GetAllUom();
                return Request.CreateResponse(HttpStatusCode.OK, uomList);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}



