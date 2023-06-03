using GherkinWebAPI.Core;
using GherkinWebAPI.Core.Accounts_Master;
using GherkinWebAPI.Models.Accounts_Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.AccountsMaster
{
    public class AccountsGroupService : IAccountsGroupService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public AccountsGroupService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<AccountsGroup> AddAccountsGroup(AccountMaster accountMaster)
        {
            return await _repositoryWrapper.AccountsGroupRepository.AddAccountsGroup(accountMaster);
        }

        public IEnumerable<AccountsGroup> GetAllGroupsAsync()
        {
            return _repositoryWrapper.AccountsGroupRepository.GetAllGroupsAsync();
        }
        
        public IEnumerable<AccountsGroup> GetAllGroupsForHeadAsync(string AccountHead)
        {
            return _repositoryWrapper.AccountsGroupRepository.GetAllGroupsForHeadAsync(AccountHead);
        }
        public async Task<AccountMaster> FindAccountsGroup(string headCode, int orgCode, string accountOrGroupCode, string mainGroupCode, string parentGroupCode, bool isAccount)
        {
            return await _repositoryWrapper.AccountsGroupRepository.FindAccountsGroup(headCode, orgCode, accountOrGroupCode, mainGroupCode, parentGroupCode, isAccount);
        }
    }
}