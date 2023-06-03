using GherkinWebAPI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using GherkinWebAPI.Models;
using GherkinWebAPI.Utilities;

namespace GherkinWebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class ProformaInvoiceDetailsController : ApiController
    {

        private IProformaInvoiceDetailsRepository _proformaService;

        public ProformaInvoiceDetailsController(IProformaInvoiceDetailsRepository proformaService)
        {
            _proformaService = proformaService;

        }

        [HttpGet]
        [Route("GetProformaInvoiceId")]
        public async Task<IHttpActionResult> GetProformaInvoiceId()
        {
            try
            {
                string Id = await _proformaService.GetProformaInvoiceId();
                return Ok(Id);

            }
            catch (Exception ex)
            {
                ExceptonLog.LogError(ex, "ProformaInvoiceDetailsController", "GetProformaInvoiceId");
                //Console.WriteLine(ex.Message + " " + $"Exception in ProformaInvoiceDetailsController / {nameof(ProformaInvoiceDetailsController.GetProformaInvoiceId)}");
                return InternalServerError(ex);
            }
        }


        [HttpPost]
        [Route("AddProfromaInvoiceDetails")]
        public async Task<IHttpActionResult> AddProfromaInvoiceDetails([FromBody] ProformaInvoice proformaInvoice)
        {

            try
            {
                if (proformaInvoice == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState.Values.First().Errors[0].ErrorMessage);
                }

                var detail = await _proformaService.AddProfromaDetails(proformaInvoice);

                return Ok($"ProformaInvoice with Code : {detail.Prof_Inv_No} Created");
            }
            catch (Exception ex)
            {
                ExceptonLog.LogError(ex, "ProformaInvoiceDetailsController", "AddProfromaInvoiceDetails");
                //Console.WriteLine(ex.Message + " " + $"Exception in ProformaInvoiceDetailsController/{nameof(ProformaInvoiceDetailsController.AddProfromaInvoiceDetails)}");
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetProductionDetails")]
        public async Task<IHttpActionResult> GetProductionDetails()
        {
            try
            {

                return Ok(await _proformaService.GetProductionDetails());

            }
            catch (Exception ex)
            {
                ExceptonLog.LogError(ex, "ProformaInvoiceDetailsController", "GetProductionDetails");
                //Console.WriteLine(ex.Message + " " + $"Exception in ProformaInvoiceDetailsController / {nameof(ProformaInvoiceDetailsController.GetProductionDetails)}");
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("GetProductionScheduleId")]
        public async Task<IHttpActionResult> GetProductionScheduleId()
        {
            try
            {

                return Ok(await _proformaService.GetProductionScheduleId());

            }
            catch (Exception ex)
            {
                ExceptonLog.LogError(ex, "ProformaInvoiceDetailsController", "GetProductionScheduleId");
                //Console.WriteLine(ex.Message + " " + $"Exception in ProformaInvoiceDetailsController / {nameof(ProformaInvoiceDetailsController.GetProductionScheduleId)}");
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("AddProductionScheduleDetails")]
        public async Task<IHttpActionResult> AddProductionScheduleDetails([FromBody] ProductionSchedule productionSchedule)
        {

            try
            {
                if (productionSchedule == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState.Values.First().Errors[0].ErrorMessage);
                }
                var prodSchedule = await _proformaService.AddProductionScheduleDetails(productionSchedule);
                return Ok($"ProductionScheduleNo : {prodSchedule.Production_Schedule_No} Created");

            }
            catch (Exception ex)
            {
                ExceptonLog.LogError(ex, "ProformaInvoiceDetailsController", "AddProductionScheduleDetails");
                //Console.WriteLine(ex.Message + " " + $"Exception in ProformaInvoiceDetailsController/{nameof(ProformaInvoiceDetailsController.AddProductionScheduleDetails)}");
                return InternalServerError();
            }
        }
    }
}
