using GherkinWebAPI.Models;
using GherkinWebAPI.DTO.BankAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Core
{
    public interface IBankAccountDetailsRepository
    {
        Task<IEnumerable<BankAccountDetailsDTO>> GetAccountDetails();
        Task<BankAccountDetails> GetAccountDetailsByBankCode(string bankCode);
        Task<BankAccountDetails> CreateBankAccount(BankAccountDetails bankAccountDetails);
        Task<BankAccountDetails> UpdateBankAccount(string bankCode, BankAccountDetails bankAccountDetails);
        Task<BankAccountClose> CloseBankAccount(BankAccountClose bankAccountClose);
        Task<BankAccountClose> GetAccountStatus(string bankCode);
        Task<BankAccountClose> UpdateClosedAccount(string bankCode, BankAccountClose bankAccountClose);
    }
}