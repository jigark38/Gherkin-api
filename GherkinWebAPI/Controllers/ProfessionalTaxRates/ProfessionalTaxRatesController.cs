using GherkinWebAPI.Core.ProfessionalTaxRates;
using GherkinWebAPI.Models.ProfessionalTaxRates;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers
{
    [Route("api/v1/[Contoller]")]
    public class ProfessionalTaxRatesController : ApiController
    {
        private readonly IProfessionalTaxMasterService _service;
        private readonly IProfessionalTaxSlabService _slabService;
        private readonly ILogger<ProfessionalTaxRatesController> _logger;

        public readonly string controller = nameof(FarmersAgreementController);

        public ProfessionalTaxRatesController(IProfessionalTaxMasterService service, IProfessionalTaxSlabService slabService)
        {
            _service = service;
            _slabService = slabService;
        }

        [HttpGet]
        [Route("GetProfessionalTaxRates")]
        public async Task<IHttpActionResult> GetProfessionalTaxRates()
        {
            try
            {
                var professionalTaxRates = await _service.GetProfessionalTaxRates();
                return Ok(professionalTaxRates);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(ProfessionalTaxRatesController.GetProfessionalTaxRates)}");
                return InternalServerError();
            }

        }

        [HttpPost]
        [Route("CreateProfessionalTaxRates")]
        public async Task<IHttpActionResult> CreateProfessionalTaxRates(ProfessionalTax professionalTax)
        {
            try
            {
                if (professionalTax == null || !ModelState.IsValid)
                {
                    _logger.LogError("professionalTax object sent from client is not valid");
                    return BadRequest($"professionalTax object sent from client is not valid");
                }

                var professionalTaxMaster = new ProfessionalTaxMaster
                {
                    PTPassingNo = professionalTax.PTPasssingNo,
                    EntryDate = professionalTax.DateofEntry,
                    EnteredEmpID = professionalTax.LoginUserName,
                    PTEffectiveDate = professionalTax.EffectiveDate,
                    PTChallanPaymentDate = professionalTax.MonthlyChallanDate,
                    PTDirectorsPayable = professionalTax.DirectorsPayable,
                    PTExemptedTillSalary = professionalTax.TaxAmountExemptedSalary,
                };

                var detail = await _service.CreateProfessionalTaxMaster(professionalTaxMaster);

                foreach (var fs in professionalTax.ProfessionalTaxSlabs)
                {
                    var professionalTaxSlab = new ProfessionalTaxSlabsDetail
                    {
                        PTSalarySlabID = fs.PTPassingSlabID,
                        PTPassingNo = detail.PTPassingNo,
                        PTSalaryFrom = fs.SalaryFrom,
                        PTSalaryTo = fs.SalaryTo == 0 ? int.MaxValue : fs.SalaryTo,
                        PTEmployeesAmountPayable = fs.ProfessionalTaxAmount == 0 ? 200 : fs.ProfessionalTaxAmount
                    };

                    var slabDetail = await _slabService.CreateProfessionalSlab(professionalTaxSlab);
                }


                return Ok($"ProfessionalTaxRate with Code : {detail.PTPassingNo} Created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(ProfessionalTaxRatesController.CreateProfessionalTaxRates)}");
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("UpdateProfessionalTaxRates/{ptPassingNo}")]
        public async Task<IHttpActionResult> UpdateProfessionalTaxRates(long ptPassingNo, [FromBody] ProfessionalTax professionalTax)
        {
            try
            {
                if (professionalTax == null || !ModelState.IsValid)
                {
                    _logger.LogError("professionalTax object sent from client is not valid");
                    return BadRequest($"professionalTax object sent from client is not valid");
                }

                var professionalTaxMasterDetails = new ProfessionalTaxMaster
                {
                    EntryDate = professionalTax.DateofEntry,
                    EnteredEmpID = professionalTax.LoginUserName,
                    PTEffectiveDate = professionalTax.EffectiveDate,
                    PTChallanPaymentDate = professionalTax.MonthlyChallanDate,
                    PTDirectorsPayable = professionalTax.DirectorsPayable,
                    PTExemptedTillSalary = professionalTax.TaxAmountExemptedSalary
                };

                var detail = await _service.UpdateProfessionalTaxMaster(ptPassingNo, professionalTaxMasterDetails);

                foreach (var fs in professionalTax.ProfessionalTaxSlabs)
                {
                    var professionalTaxSlabDetails = new ProfessionalTaxSlabsDetail
                    {
                        PTSalarySlabID = fs.PTPassingSlabID,
                        PTSalaryFrom = fs.SalaryFrom,
                        PTSalaryTo = fs.SalaryTo,
                        PTEmployeesAmountPayable = fs.ProfessionalTaxAmount
                    };

                    var sizeDetail = await _slabService.UpdateProfessionalSlab(ptPassingNo, professionalTaxSlabDetails);
                }

                if (detail == null)
                {
                    return NotFound();
                }

                return Ok($"ProfessionalTaxRate with No : {ptPassingNo} Updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(ProfessionalTaxRatesController.UpdateProfessionalTaxRates)}");
                return InternalServerError();
            }
        }
    }
}
