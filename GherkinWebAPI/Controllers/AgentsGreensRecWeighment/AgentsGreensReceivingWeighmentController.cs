using GherkinWebAPI.Core.AgentsGreensRecWeighment;
using GherkinWebAPI.DTO.AgentsGreensRecWeighment;
using GherkinWebAPI.Models.AgentsGreensRecWeighment;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GherkinWebAPI.Controllers.AgentsGreensRecWeighment
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/V1/AgentsGreensReceivingWeighment")]
    public class AgentsGreensReceivingWeighmentController : ApiController
    {
        // GET: AgentsGreensReceivingWeighment
        private readonly IAgentsGreensReceivingWeighmentService _service;
        private readonly ILogger<GreensAgentReceivedDetails> _logger;

        public readonly string controller = nameof(AgentsGreensReceivingWeighmentController);

        public AgentsGreensReceivingWeighmentController(IAgentsGreensReceivingWeighmentService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetOrgOfficeLocation")]
        public async Task<IHttpActionResult> GetFarmersAgreementCode()
        {
            try
            {
                return Ok(await _service.GetOrgOfficeLocation());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(AgentsGreensReceivingWeighmentController.GetFarmersAgreementCode)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetInwardDetails")]
        public async Task<IHttpActionResult> GetInwardDetails(int officeOrgNumber)
        {
            try
            {
                return Ok(await _service.GetInwardDetails(officeOrgNumber));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(AgentsGreensReceivingWeighmentController.GetInwardDetails)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetSupplierInformationDetail")]
        public async Task<IHttpActionResult> GetSupplierInformationDetail()
        {
            try
            {
                return Ok(await _service.GetSupplierInformationDetail());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(AgentsGreensReceivingWeighmentController.GetSupplierInformationDetail)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetCropSchemeDetails")]
        public async Task<IHttpActionResult> GetCropSchemeDetails(string cropNameCode)
        {
            try
            {
                return Ok(await _service.GetCropSchemeDetails(cropNameCode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(AgentsGreensReceivingWeighmentController.GetCropSchemeDetails)}");
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("PartialSaveGreensRecvDetails")]
        public async Task<IHttpActionResult> PartialSaveGreensRecvDetails(GreensAgentReceivedDetails recvDetail)
        {
            try
            {
                return Ok(await _service.PartialSaveGreensRecvDetails(recvDetail));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(AgentsGreensReceivingWeighmentController.PartialSaveGreensRecvDetails)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetGreensRecvDetails")]
        public async Task<IHttpActionResult> GetGreensRecvDetails(string inwardGatepassNo)
        {
            try
            {
                return Ok(await _service.GetGreensRecvDetails( inwardGatepassNo));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(AgentsGreensReceivingWeighmentController.GetGreensRecvDetails)}");
                return InternalServerError();
            }
        }
              

        [HttpGet]
        [Route("ChangeInGoingStatus")]
        public async Task<IHttpActionResult> ChangeInGoingStatus(int GRNNo)
        {
            try
            {
                return Ok(await _service.ChangeInGoingStatus(GRNNo));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(AgentsGreensReceivingWeighmentController.ChangeInGoingStatus)}");
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("SaveRecvWeighmentDetails")]
        public async Task<IHttpActionResult> SaveRecvWeighmentDetails(GreensAgentReceivedDetails agentsGrnRecivWeigmtDetail)
        {
            try
            {
                return Ok(await _service.SaveRecvWeighmentDetails(agentsGrnRecivWeigmtDetail));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(AgentsGreensReceivingWeighmentController.SaveRecvWeighmentDetails)}");
                return InternalServerError();
            }
        }
    }
}