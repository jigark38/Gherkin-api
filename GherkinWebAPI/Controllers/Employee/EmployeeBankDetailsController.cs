using GherkinWebAPI.Core;
using GherkinWebAPI.Core.EmployeeBankDetails;
using GherkinWebAPI.DTO.BankAccount;
using GherkinWebAPI.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace GherkinWebAPI.Controllers
{
	[RoutePrefix("BankDetails")]
	public class EmployeeBankDetailsController : ApiController
	{
		private readonly IEmployeeBankDetailsMasterService _employeeBankDetailsMasterService;

		public EmployeeBankDetailsController(IEmployeeBankDetailsMasterService employeeBankDetailsMasterService)
		{
			this._employeeBankDetailsMasterService = employeeBankDetailsMasterService;
		}

		[HttpPost]
		[Route("AddBankDetails")]
		public async Task<IHttpActionResult> AddBankDetails([FromBody]BankAccountDetailMasterDto bankAccountDetailMasterDto)
		{
			try
			{
				await _employeeBankDetailsMasterService.AddEmployeeBankAccountMasterDetails(bankAccountDetailMasterDto);
				return Ok();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + " " + $"Exception in Controller/{nameof(EmployeeBankDetailsController.AddBankDetails)}");
				return InternalServerError();
			}
		}

		[HttpGet]
		[Route("GetBankDetails")]
		public async Task<IHttpActionResult> GetBankDetails(string empId)
		{
			try
			{
				var details = await _employeeBankDetailsMasterService.GetEmployeeBankAccountDetails(empId);
				return Ok(details);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + " " + $"Exception in Controller/{nameof(EmployeeBankDetailsController.GetBankDetails)}");
				return InternalServerError();
			}
		}
	}
}