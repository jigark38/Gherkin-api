using GherkinWebAPI.Core.BranchIndent;
using GherkinWebAPI.Models.Branch_Indent;
using GherkinWebAPI.Request.BranchIndent;
using GherkinWebAPI.Response.BranchIndent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GherkinWebAPI.Controllers.BranchIndent
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BranchIndentController : ApiController
    {
        public BranchIndentController()
        {

        }
        private readonly IBranchIndentService _service;

        public BranchIndentController(IBranchIndentService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("branchIndent/insert")]
        public async Task<HttpResponseMessage> InsertBranchIndentDetails([FromBody]BranchIndentInsertRequest branchIndentInsertRequest)
        {

            BranchIndentResponse data = new BranchIndentResponse();
            try
            {
                data = await _service.InsertBranchIndentDetails(branchIndentInsertRequest);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }
        [HttpGet]
        [Route("branchIndent/GetAllIndentRequest")]
        public async Task<HttpResponseMessage> GetAllIndentRequest()
        {
            try
            {
                List<BranchIndentResponse> data = new List<BranchIndentResponse>();

                data = await _service.GetAllIndentRequest();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("branchIndent/UpdateIndentMaterial")]
        public async Task<HttpResponseMessage> UpdateIndentMaterial([FromBody]BranchIndentMaterialDetailsModel branchIndentMaterialDetailsModel)
        {
            try
            {
                var data = await _service.UpdateIndentMaterial(branchIndentMaterialDetailsModel);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("branchIndent/GetRMUOM")]
        public async Task<IHttpActionResult> GetRMUOM()
        {
            try
            {
                var res = await _service.GetRMUOM();
                if (res != null)
                    return Ok(res);
                else
                    return Ok("This RM UOM is not found");

            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}
