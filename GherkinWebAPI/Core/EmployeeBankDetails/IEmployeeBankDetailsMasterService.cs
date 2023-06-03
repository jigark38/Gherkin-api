using GherkinWebAPI.DTO.BankAccount;
using GherkinWebAPI.Models.Employee;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.EmployeeBankDetails
{
	public interface IEmployeeBankDetailsMasterService
	{
		Task AddEmployeeBankAccountMasterDetails(BankAccountDetailMasterDto bankAccountDetailMasterDto);

		Task AddEmployeeBankAccountDetails(BankAccountDetailMasterDto bankAccountDetailMasterDto);

		Task<List<BankAccountDetailMasterDto>> GetEmployeeBankAccountDetails(string employeeId);
	}
}