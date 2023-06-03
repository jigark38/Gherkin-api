using GherkinWebAPI.Models.Accounts_Master;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.Accounts_Master
{
    public interface IAccountMasterService
    {
        Task<AccountsGroup> AddAccount(AccountMaster accountsMaster);
    }
}
