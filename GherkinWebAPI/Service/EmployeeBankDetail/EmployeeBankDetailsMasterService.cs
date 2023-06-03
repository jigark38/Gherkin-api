using GherkinWebAPI.Core;
using GherkinWebAPI.Core.EmployeeBankDetails;
using GherkinWebAPI.DTO.BankAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.EmployeeBankDetail
{
	public class EmployeeBankDetailsMasterService : IEmployeeBankDetailsMasterService
	{
		private readonly IRepositoryWrapper _repositoryWrapper;
		public EmployeeBankDetailsMasterService(IRepositoryWrapper repositoryWrapper)
		{
			_repositoryWrapper = repositoryWrapper;
		}

		public async Task AddEmployeeBankAccountDetails(BankAccountDetailMasterDto bankAccountDetailMasterDto)
		{
			await _repositoryWrapper.EmployeeBankDetailsMasterRepository.AddEmployeeBankAccountDetails(bankAccountDetailMasterDto);
		}

		public async Task AddEmployeeBankAccountMasterDetails(BankAccountDetailMasterDto bankAccountDetailMasterDto)
		{
			await _repositoryWrapper.EmployeeBankDetailsMasterRepository.AddEmployeeBankAccountMasterDetails(bankAccountDetailMasterDto);
		}

		public async Task<List<BankAccountDetailMasterDto>> GetEmployeeBankAccountDetails(string employeeId)
		{
			return await _repositoryWrapper.EmployeeBankDetailsMasterRepository.GetEmployeeBankAccountDetails(employeeId);
		}
	}
}