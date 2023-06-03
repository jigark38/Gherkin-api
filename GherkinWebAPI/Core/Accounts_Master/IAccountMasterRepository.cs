using GherkinWebAPI.Models.Accounts_Master;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.Accounts_Master
{
    public interface IAccountMasterRepository
    {
        Task<AccountsGroup> AddAccount(AccountMaster accountsMaster);
    }
}
