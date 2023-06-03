using GherkinWebAPI.Models.Accounts_Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.Accounts_Master
{
    public interface IAccountsGroupService
    {
        IEnumerable<AccountsGroup> GetAllGroupsAsync();

        IEnumerable<AccountsGroup> GetAllGroupsForHeadAsync(string AccountHead);

        Task<AccountsGroup> AddAccountsGroup(AccountMaster accountMaster);
        Task<AccountMaster> FindAccountsGroup(string headCode, int orgCode, string accountOrGroupCode, string mainGroupCode, string parentGroupCode, bool isAccount);
    }
}
