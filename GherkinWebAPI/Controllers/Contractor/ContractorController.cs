using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using contractor = GherkinWebAPI.Models.Contractor;
using GherkinWebAPI.Core.Contractor;
using GherkinWebAPI.Models;

namespace GherkinWebAPI.Controllers.Contractor
{
    [Route("api/V1/[Controller]")]
    public class ContractorController : ApiController
    {
        private IContratorService _contractorService;
        public ContractorController(IContratorService contractorService)
        {
            _contractorService = contractorService;
        }

        [Route("GetAllContractor")]
        public async Task<IHttpActionResult> GetAllContractor()
        {
            try
            {
                List<contractor> contractors = new List<contractor>();
                contractors = await _contractorService.GetAllContractor();
                return Ok(contractors);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in ContractorController / {nameof(ContractorController.GetAllContractor)}");
                return InternalServerError();
            }
        }

        [Route("GetContractor/{contractorCode}")]
        public async Task<IHttpActionResult> GetContractorByContracorCode(string contractorCode)
        {
            var contractor = new contractor();
            try
            {
                contractor = await _contractorService.GetEmployeeByContractorCode(contractorCode);
                return Ok(contractor);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in ContractorController / {nameof(ContractorController.GetContractorByContracorCode)}");
                return InternalServerError();
            }
        }

        [Route("createContractor")]
        public async Task<IHttpActionResult> createContractor([FromBody] contractor ctor)
        {
            try
            {
                var con = await _contractorService.CreateContractor(ctor);
                return Ok(con);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in EmployeeController / {nameof(EmployeeController.CreateEmployee)}");
                return InternalServerError();
            }
        }
    }
}
