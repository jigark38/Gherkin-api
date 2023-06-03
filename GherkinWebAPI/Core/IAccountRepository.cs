using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESSWebAPI.Repository;
using GherkinWebAPI.Models;

namespace GherkinWebAPI.Core
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<Account> GetAccountByIdAsync(int accountId);
        //Task<Account> GetAccountWithDetailsAsync(int accountId);
        void CreateAccount(Account account);
        void UpdateAccount(Account account);
        void DeleteAccount(Account account);
    }
}
