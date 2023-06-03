using GherkinWebAPI.Core;
using GherkinWebAPI.DTO.BankAccount;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service
{
    public class BankAccountDetailsService : IBankAccountDetailsService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public BankAccountDetailsService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<BankAccountClose> CloseBankAccount(BankAccountClose bankAccountClose)
        {
            return await _repositoryWrapper.BankAccountDetailsRepository.CloseBankAccount(bankAccountClose);          
        }
        public async Task<BankAccountClose> UpdateClosedAccount(string bankCode, BankAccountClose bankAccountClose)
        {
            return await _repositoryWrapper.BankAccountDetailsRepository.UpdateClosedAccount(bankCode,bankAccountClose);
        }

        public async Task<BankAccountClose> GetAccountStatus(string bankCode)
        {
           return await _repositoryWrapper.BankAccountDetailsRepository.GetAccountStatus(bankCode);
        }
        public async Task<BankAccountDetails> CreateBankAccount(BankAccountDetails bankAccountDetails)
        {
            return await _repositoryWrapper.BankAccountDetailsRepository.CreateBankAccount(bankAccountDetails);
        }

        public async Task<BankAccountDetails> UpdateBankAccount(string bankCode, BankAccountDetails bankAccountDetails)
        {
            return await _repositoryWrapper.BankAccountDetailsRepository.UpdateBankAccount(bankCode, bankAccountDetails);
        }
        public async Task<IEnumerable<BankAccountDetailsDTO>> GetAccountDetails()
        {
            return await _repositoryWrapper.BankAccountDetailsRepository.GetAccountDetails();
        }
        public async Task<BankAccountDetails> GetAccountDetailsByBankCode(string bankCode)
        {
            return await _repositoryWrapper.BankAccountDetailsRepository.GetAccountDetailsByBankCode(bankCode);
        }
     }
}