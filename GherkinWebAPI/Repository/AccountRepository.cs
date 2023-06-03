using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ESSWebAPI.Repository
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await FindAll()
               .OrderBy(account => account.accountName)
               .ToListAsync();
        }

        public async Task<Account> GetAccountByIdAsync(int accountId)
        {
            return await FindByCondition(account => account.accountId.Equals(accountId))
                .FirstOrDefaultAsync();
        }

        //public async Task<Account> GetAccountWithDetailsAsync(int accountId)
        //{
        //    return await FindByCondition(account => account.accountId.Equals(accountId))
        //        .Include(c => c.Customers)
        //        .FirstOrDefaultAsync();
        //}

        public void CreateAccount(Account account)
        {
            Create(account);
        }

        public void UpdateAccount(Account account)
        {
            Update(account);
        }

        public void DeleteAccount(Account account)
        {
            Delete(account);
        }

       
    }
}
