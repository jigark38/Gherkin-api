using GherkinWebAPI.Core;
using GherkinWebAPI.DTO.BankAccount;
using GherkinWebAPI.Models.Employee;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

namespace GherkinWebAPI.Repository
{
    public class EmployeeBankDetailsMasterRepository : IEmployeeBankDetailsMasterRepository
    {

        private RepositoryContext _context;
        public EmployeeBankDetailsMasterRepository(RepositoryContext repositoryContext)
        {
            _context = repositoryContext;
        }
        public async Task AddEmployeeBankAccountMasterDetails(BankAccountDetailMasterDto employeeBankDetailsMaster)
        {
            var firstBankAccountMaster = await _context.EmployeeBankAccountMasterDetails.OrderByDescending(c => c.empAccountId).FirstOrDefaultAsync();
            if (firstBankAccountMaster != null)
            {
                var bankAccountMaster = await _context.EmployeeBankAccountMasterDetails.OrderByDescending(c => c.empAccountId.ToString().Length).ThenByDescending(c => c.empAccountId).FirstOrDefaultAsync();
                if (bankAccountMaster != null)
                    employeeBankDetailsMaster.empAccountId = bankAccountMaster.empAccountId + 1;
                else
                    employeeBankDetailsMaster.empAccountId = 1;
            }
            else
                employeeBankDetailsMaster.empAccountId = 1;

            var employeeBankAccountMasterDetails = new EmployeeBankDetailsMaster()
            {
                empAccountId = employeeBankDetailsMaster.empAccountId,
                empId = employeeBankDetailsMaster.empId,
                enteredEmployeeId = employeeBankDetailsMaster.enteredEmployeeId,
                entryDate = employeeBankDetailsMaster.entryDate,
                modeOfAccount = employeeBankDetailsMaster.modeOfAccount,
                orgOfficeNo = employeeBankDetailsMaster.orgOfficeNo
            };

            _context.EmployeeBankAccountMasterDetails.Add(employeeBankAccountMasterDetails);
            await _context.SaveChangesAsync();

            await AddEmployeeBankAccountDetails(employeeBankDetailsMaster);


        }

        public async Task AddEmployeeBankAccountDetails(BankAccountDetailMasterDto employeeBankDetailsMaster)
        {
            var firstBankAccount = await _context.EmployeeBankAccountDetails.OrderByDescending(c => c.bankAccountId).FirstOrDefaultAsync();
            if (firstBankAccount != null)
            {
                var bankAccount = await _context.EmployeeBankAccountDetails.OrderByDescending(c => c.bankAccountId.ToString().Length).ThenByDescending(c => c.bankAccountId).FirstOrDefaultAsync();
                if (bankAccount != null)
                    employeeBankDetailsMaster.bankAccountId = bankAccount.bankAccountId + 1;
                else
                    employeeBankDetailsMaster.bankAccountId = 1;
            }
            else
                employeeBankDetailsMaster.bankAccountId = 1;

            if (employeeBankDetailsMaster.preferredAccount.ToLower().Equals("yes"))
            {
                var bankAccounts = await _context.EmployeeBankAccountDetails.Where(c => c.empId == employeeBankDetailsMaster.empId).ToListAsync();
                for (int i = 0; i < bankAccounts.Count; i++)
                {
                    if (bankAccounts[i].preferredAccount.ToLower().Equals("yes"))
                    {
                        bankAccounts[i].preferredAccount = "No";
                        _context.EmployeeBankAccountDetails.AddOrUpdate(bankAccounts[i]);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            var employeeBankAccountDetails = new EmployeeBankAccountDetails()
            {
                accountEffectiveDateFrom = employeeBankDetailsMaster.accountEffectiveDateFrom,
                bankAccountId = employeeBankDetailsMaster.bankAccountId,
                bankAccountNumber = employeeBankDetailsMaster.bankAccountNumber,
                bankBranch = employeeBankDetailsMaster.bankBranch,
                empAccountId = employeeBankDetailsMaster.empAccountId,
                bankCode = employeeBankDetailsMaster.bankCode,
                bankIfsc = employeeBankDetailsMaster.bankIfsc,
                bankName = employeeBankDetailsMaster.bankName,
                empId = employeeBankDetailsMaster.empId,
                nomineeName = employeeBankDetailsMaster.nomineeName,
                nomineeRelationship = employeeBankDetailsMaster.nomineeRelationship,
                preferredAccount = employeeBankDetailsMaster.preferredAccount
            };

            _context.EmployeeBankAccountDetails.Add(employeeBankAccountDetails);
            await _context.SaveChangesAsync();
        }

        public async Task<List<BankAccountDetailMasterDto>> GetEmployeeBankAccountDetails(string employeeId)
        {
            try
            {
                var _listBankAccountDetails = await (from banks in _context.EmployeeBankAccountDetails
                                                     where banks.empId == employeeId
                                                     select new BankAccountDetailMasterDto
                                                     {
                                                         bankName = banks.bankName,
                                                         bankIfsc = banks.bankIfsc,
                                                         bankAccountNumber = banks.bankAccountNumber,
                                                         accountEffectiveDateFrom = banks.accountEffectiveDateFrom,
                                                         preferredAccount = banks.preferredAccount,
                                                         bankBranch = banks.bankBranch
                                                     }).ToListAsync();
                return _listBankAccountDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}