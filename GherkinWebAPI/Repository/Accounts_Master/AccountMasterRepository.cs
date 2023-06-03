using GherkinWebAPI.Core.Accounts_Master;
using GherkinWebAPI.Models.Accounts_Master;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.Accounts_Master
{
    public class AccountMasterRepository : RepositoryBase<AccountsMaster>, IAccountMasterRepository
    {
        private RepositoryContext _repositoryContext;

        public AccountMasterRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public async Task<AccountsGroup> AddAccount(AccountMaster accountsMaster)
        {
            try
            {
                AccountHeadMaster Account = new AccountHeadMaster();
                bool isUpdateORDeleted = false;
                if (accountsMaster.AccountCode != null)
                {
                    Account = await _repositoryContext.AccountHeadMasters.SingleOrDefaultAsync(x => x.AccountCode == accountsMaster.AccountCode);

                    if (Account.AccountCode!=null)
                    {
                        //Account.AccountCode = accountsMaster.AccountCode;
                      //  Account.AccGroupCode = accountsMaster.MainGroupCode ?? accountsMaster.AccGroupCode;
                      //  Account.AccHeadCode = accountsMaster.AccHeadCode;
                        Account.AccountName = accountsMaster.AccountName;
                        //if (accountsMaster.AccSubGroupCode != null)
                        //{
                        //    Account.AccSubGroupCode = accountsMaster.MainGroupCode == null ? null : accountsMaster.AccGroupCode;
                        //    Account.AccSubGroupCode = accountsMaster.AccSubGroupCode;
                        //}
                        Account.OpBalanceAmount = accountsMaster.OpBalanceAmount;
                        Account.OpBalanceDate = accountsMaster.OpBalanceDate;
                        Account.DebitCreditDetails = accountsMaster.DebitCreditDetails;
                        isUpdateORDeleted = true;
                        _repositoryContext.Entry(Account).State = EntityState.Modified;
                    }
                }

                else
                {

                    var accountHead = _repositoryContext.AccountHeadMasters.OrderByDescending(i => i.Id).FirstOrDefault();

                    if (accountHead != null)
                    {
                        accountsMaster.AccountCode = "A" + Convert.ToInt32(accountHead.Id + 1);
                    }
                    else
                        accountsMaster.AccountCode = "A1";

                    var accountHeadMaster = new AccountHeadMaster
                    {
                        AccountCode = accountsMaster.AccountCode,
                        AccGroupCode = accountsMaster.MainGroupCode ?? accountsMaster.AccGroupCode,
                        AccHeadCode = accountsMaster.AccHeadCode,
                        AccountName = accountsMaster.AccountName,
                        AccSubGroupCode = accountsMaster.MainGroupCode == null ? null : accountsMaster.AccGroupCode,
                        OpBalanceAmount = accountsMaster.OpBalanceAmount,
                        OpBalanceDate = accountsMaster.OpBalanceDate,
                        DebitCreditDetails = accountsMaster.DebitCreditDetails
                    };

                    _repositoryContext.AccountHeadMasters.Add(accountHeadMaster);
                    isUpdateORDeleted = true;

                }

                if (isUpdateORDeleted == true)
                {
                    var result = await _repositoryContext.SaveChangesAsync();

                    if (result == 1)
                    {
                        var addedAccount = new AccountsGroup
                        {
                            HeadCode = accountsMaster.AccHeadCode,
                            OrgCode = accountsMaster.OrgCode,
                            AccountOrGroupCode = accountsMaster.AccountCode,
                            AccountOrGroupName = accountsMaster.AccountName,
                            ParentGroupCode = accountsMaster.AccGroupCode,
                            MainGroupCode = accountsMaster.MainGroupCode,
                            IsAccount = true
                        };

                        return addedAccount;
                    }
                }
                return new AccountsGroup();                
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}