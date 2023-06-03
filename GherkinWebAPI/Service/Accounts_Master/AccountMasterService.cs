using GherkinWebAPI.Core;
using GherkinWebAPI.Core.Accounts_Master;
using GherkinWebAPI.Models.Accounts_Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.Accounts_Master
{
    public class AccountMasterService : IAccountMasterService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public AccountMasterService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<AccountsGroup> AddAccount(AccountMaster accountsMaster)
        {
            return await _repositoryWrapper.AccountMasterRepository.AddAccount(accountsMaster);
        }
    }
}