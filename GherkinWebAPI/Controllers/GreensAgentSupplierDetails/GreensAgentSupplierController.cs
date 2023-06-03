using GherkinWebAPI.Core.GreensAgentSupplierDetails;
using GherkinWebAPI.Models.GreensAgentSupplierDetails;
using GherkinWebAPI.Request.FinishedSFOpeningStockDetails;
using GherkinWebAPI.Request.GreensAgentSupplierDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.GreensAgentSupplierDetails
{
    [RoutePrefix("api/V1/GreensAgentSupplier")]
    public class GreensAgentSupplierController : ApiController
    {
        
        private readonly IGreensAgentSupplierDetailsService _service;
        public GreensAgentSupplierController(IGreensAgentSupplierDetailsService service)
        {
            _service = service;
        }

        [HttpPost, Route("SaveGreensAgentSupplierDetails")]
        public async Task<IHttpActionResult> SaveGreensAgentSupplierDetails(SupplierInformationDetailsRequest supplierInformationDetail)
        {
            return Ok(await _service.SaveGreensAgentSupplierDetails(supplierInformationDetail));
        }

        [HttpGet, Route("GetAgentOrganisationDetails")]
        public async Task<IHttpActionResult> GetAgentOrganisationDetails()
        {
            return Ok(await _service.GetAgentOrganisationDetails());
        }

        [HttpGet, Route("GetSupplierInformationDetail")]
        public async Task<IHttpActionResult> GetSupplierInformationDetail(int agentOrgID)
        {
            return Ok(await _service.GetSupplierInformationDetail(agentOrgID));
        }

        [HttpPost, Route("SaveBankAccountDetails")]
        public async Task<IHttpActionResult> SaveBankAccountDetails(List<AgentBankDetailsRequest> agntBankDetailList)
        {
            return Ok(await _service.SaveBankAccountDetails(agntBankDetailList));
        }

        [HttpGet, Route("GetDocumentByDocId")]
        public async Task<IHttpActionResult> GetDocumentByDocId(int docId)
        {
            return Ok(await _service.GetDocumentByDocId(docId));
        }


        [HttpDelete, Route("DeleteDocumentByDocId")]
        public async Task<IHttpActionResult> DeleteDocumentByDocId(int docId)
        {
            return Ok(await _service.DeleteDocumentByDocId(docId));
        }

        [HttpPost, Route("ModifyGreensAgentSupplierDetails")]
        public async Task<IHttpActionResult> ModifyGreensAgentSupplierDetails(SupplierInformationDetailsRequest suppInfoReq)
        {
            return Ok(await _service.ModifyGreensAgentSupplierDetails(suppInfoReq));
        }
    }
}