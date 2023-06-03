using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Extensions.Logging;
using GherkinWebAPI.Request;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Response.SupplierDetails;

namespace GherkinWebAPI.Controllers
{
    public class SupplierDetailsController : ApiController
    {
        private readonly ISupplierDetailsService _service;
        private readonly ILogger<SupplierDetails> _logger;

        public SupplierDetailsController(ISupplierDetailsService supplierDetailsService)
        {
            _service = supplierDetailsService;
        }

        [HttpGet]
        [Route("getAllSupplierOrgs")]
        public async Task<List<SupplierDetails>> GetAllSupplierOrgs()
        {
            try
            {
                var res = await _service.GetAllSupplierOrgs();
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        [Route("addSupplierDetails"), HttpPost]
        public async Task<IHttpActionResult> AddSupplierDetails([FromBody]SupplierDetailsRequest supplierDetailsreq)
        {
            try
            {
                var supplierDet = await _service.AddSupplierDetails(supplierDetailsreq);
                return Ok(supplierDet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in EmployeeController / {nameof(SupplierDetailsController.AddSupplierDetails)}");
                return InternalServerError();
            }
        }

        [HttpPost, Route("updateSupplierDetails")]
        public async Task<HttpResponseMessage> UpdateSupplierDetails([FromBody] SupplierDetailsRequest supplierDetailsReq)
        {
            try
            {
                if (supplierDetailsReq == null || !ModelState.IsValid)
                {
                    _logger.LogError("supplierDetails object sent from client is not valid");
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"supplierDetails object sent from client is not valid");
                }

                var details = await _service.UpdateSupplierDetails(supplierDetailsReq);
                return Request.CreateResponse(HttpStatusCode.OK, details);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdatesupplierDetails action: {ex.Message}");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet, Route("getSupplierDetailsByID")]
        public async Task<SupplierDetailsResponse> GetSupplierDetailsByID(string SupplierOrgID)
        {
            return await _service.GetSupplierDetailsByID(SupplierOrgID);
        }
    }
}
