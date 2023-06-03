using GherkinWebAPI.Core;
using GherkinWebAPI.CustomExceptions;
using GherkinWebAPI.DTO.BankAccount;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository
{
    public class BankAccountDetailsRepository : RepositoryBase<BankAccountDetails>, IBankAccountDetailsRepository
    {
        private RepositoryContext _context;
        public BankAccountDetailsRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            this._context = repositoryContext;
        }
        public async Task<BankAccountClose> CloseBankAccount(BankAccountClose bankAccountClose)
        {
            try
            {
                var _getAccount = await _context.BankAccountDetails.Where(x => x.Bank_Code == bankAccountClose.Bank_Code).FirstOrDefaultAsync();
                if (_getAccount != null)
                {
                    var _chkAccountStatus = await _context.BankAccountCloses.Where(acc => acc.Bank_Code == _getAccount.Bank_Code)
                                                                        .OrderByDescending(x => x.Bank_Account_Closing_Date).FirstOrDefaultAsync();
                    if (_chkAccountStatus != null && _chkAccountStatus.Bank_Account_Close_Status.Trim().ToUpper() == "IN ACTIVE")
                    {
                        throw new CustomException("Account already deactivated!");
                    }
                    else
                    {
                        _context.BankAccountCloses.Add(bankAccountClose);
                        await _context.SaveChangesAsync();
                        return bankAccountClose;

                    }
                }
                else
                {
                    throw new CustomException("Invalid Bank Details Entered. No Account Information present with this code!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<BankAccountClose> UpdateClosedAccount(string bankCode, BankAccountClose bankAccountClose)
        {
            BankAccountClose _accountClose = new BankAccountClose();
            try
            {
                _accountClose = await _context.BankAccountCloses.FirstOrDefaultAsync(x => x.Bank_Code == bankCode);
                if (_accountClose != null)
                {
                    _accountClose.Bank_Account_Close_Status = bankAccountClose.Bank_Account_Close_Status;
                    _accountClose.Bank_Account_Closing_Date = bankAccountClose.Bank_Account_Closing_Date;
                    _accountClose.Bank_Account_Reasons = bankAccountClose.Bank_Account_Reasons;

                    await _context.SaveChangesAsync();
                    return _accountClose;
                }
                else
                {
                    throw new CustomException("Invalid Bank Details Entered. No Account Information present with this code!");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<BankAccountClose> GetAccountStatus(string bankCode)
        {
            try
            {
                BankAccountClose accountStatus = new BankAccountClose();
                accountStatus = await _context.BankAccountCloses.Where(account => account.Bank_Code == bankCode).FirstOrDefaultAsync();

                return accountStatus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<BankAccountDetails> CreateBankAccount(BankAccountDetails bankAccountDetails)
        {
            try
            {
                var orgBanks = await _context.BankAccountDetails.ToListAsync();
                if (orgBanks.Count > 0)
                {
                    var orgBankID = orgBanks.OrderByDescending(b => b.ID).Take(1).FirstOrDefault().ID;
                    bankAccountDetails.Bank_Code = "OBC_" + (Convert.ToInt16(orgBankID) + 1).ToString();
                }

                else
                    bankAccountDetails.Bank_Code = "OBC_" + "1";

                _context.BankAccountDetails.Add(bankAccountDetails);
                await _context.SaveChangesAsync();
                return bankAccountDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<BankAccountDetails> UpdateBankAccount(string bankCode, BankAccountDetails bankAccountDetails)
        {
            try
            {
                BankAccountDetails orgBank = new BankAccountDetails();
                orgBank = await _context.BankAccountDetails.FirstOrDefaultAsync(x => x.Bank_Code == bankCode);
                if (orgBank != null)
                {
                    orgBank.Bank_Code = bankAccountDetails.Bank_Code;
                    orgBank.Bank_Address = bankAccountDetails.Bank_Address;
                    orgBank.Org_code = bankAccountDetails.Org_code;
                    orgBank.Bank_IFSC = bankAccountDetails.Bank_IFSC;
                    orgBank.Bank_Name = bankAccountDetails.Bank_Name;
                    orgBank.Bank_Account_Number = bankAccountDetails.Bank_Account_Number;
                    orgBank.Bank_Swift_Code = bankAccountDetails.Bank_Swift_Code;
                    orgBank.Bank_Authorised_Employee_ID = bankAccountDetails.Bank_Authorised_Employee_ID;
                    orgBank.Bank_Other_Details = bankAccountDetails.Bank_Other_Details;
                    orgBank.Bank_Operation_Date = bankAccountDetails.Bank_Operation_Date;
                    orgBank.Bank_Branch = bankAccountDetails.Bank_Branch;
                    orgBank.Date_Of_Entry = bankAccountDetails.Date_Of_Entry;
                    orgBank.Bank_Salary_Link_Account = bankAccountDetails.Bank_Salary_Link_Account;

                    await _context.SaveChangesAsync();
                    return orgBank;
                }
                else
                {
                    throw new CustomException("Invalid Bank Details Entered. No Account Information present with this code!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<IEnumerable<BankAccountDetailsDTO>> GetAccountDetails()
        {
            try
            {
                var _listBankAccountDetails = await (from banks in _context.BankAccountDetails
                                                     join status in _context.BankAccountCloses on banks.Bank_Code equals status.Bank_Code
                                                     into bankInfo
                                                     from bankstatus in bankInfo.DefaultIfEmpty()
                                                     select new BankAccountDetailsDTO
                                                     {
                                                         ID = banks.ID,
                                                         Bank_Code = banks.Bank_Code,
                                                         Bank_Address = banks.Bank_Address,
                                                         Org_code = banks.Org_code,
                                                         Bank_IFSC = banks.Bank_IFSC,
                                                         Bank_Name = banks.Bank_Name,
                                                         Bank_Account_Number = banks.Bank_Account_Number,
                                                         Bank_Swift_Code = banks.Bank_Swift_Code,
                                                         Bank_Authorised_Employee_ID = banks.Bank_Authorised_Employee_ID,
                                                         Bank_Other_Details = banks.Bank_Other_Details,
                                                         Bank_Operation_Date = banks.Bank_Operation_Date,
                                                         Bank_Branch = banks.Bank_Branch,
                                                         Date_Of_Entry = banks.Date_Of_Entry,
                                                         Bank_Salary_Link_Account = banks.Bank_Salary_Link_Account,
                                                         Bank_Account_Close_Status = (bankstatus.Bank_Account_Close_Status == null) ? "Active" : bankstatus.Bank_Account_Close_Status
                                                     }).ToListAsync();
                return _listBankAccountDetails.OrderBy(s => s.Bank_Account_Close_Status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<BankAccountDetails> GetAccountDetailsByBankCode(string bankCode)
        {
            try
            {
                return await FindByCondition(x => x.Bank_Code == bankCode).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}