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
    public class AccountsGroupRepository : RepositoryBase<AccountsGroupMaster>, IAccountsGroupRepository
    {
        private RepositoryContext _repositoryContext;

        public AccountsGroupRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public async Task<AccountsGroup> AddAccountsGroup(AccountMaster accountMaster)
        {
            try
            {
                bool IsSubgroupUpdated = false;
                var headDetails = await _repositoryContext.AccountsMasters.SingleOrDefaultAsync(am => am.AccHeadCode == accountMaster.AccHeadCode && am.OrgCode == accountMaster.OrgCode);
                if (headDetails == null)
                {
                    var accountsMaster = new AccountsMaster
                    {
                        AccHeadCode = accountMaster.AccHeadCode,
                        OrgCode = accountMaster.OrgCode,
                        HeadName = accountMaster.HeadName
                    };

                    _repositoryContext.AccountsMasters.Add(accountsMaster);
                }

                var accountsGroup = await _repositoryContext.AccountsGroupMasters.OrderByDescending(i => i.Id).FirstOrDefaultAsync();

                if (accountsGroup != null)
                {
                    if (string.IsNullOrEmpty(accountMaster.GroupName))
                    {
                        accountMaster.AccGroupCode = "0" + Convert.ToInt32(accountsGroup.Id + 1);
                    }
                }
                else
                    accountMaster.AccGroupCode = "01";

                if (accountMaster.AccSubGroupCode != null)
                {
                    var subgroup = await _repositoryContext.AccountsSubGroupMasters.SingleOrDefaultAsync(x => x.AccSubGroupCode == accountMaster.AccSubGroupCode);

                    if ((subgroup != null) && (subgroup.SubGroupName != accountMaster.SubGroupName))
                    {
                        subgroup.SubGroupName = accountMaster.SubGroupName;
                        if(subgroup.AccGroupCode != accountMaster.AccGroupCode)
                        subgroup.AccGroupCode = accountMaster.AccGroupCode;
                        IsSubgroupUpdated = true;
                        _repositoryContext.Entry(subgroup).State = EntityState.Modified;
                    }
                }
                if (accountMaster.AccGroupCode != null)
                {
                    var group = await _repositoryContext.AccountsGroupMasters.SingleOrDefaultAsync(x => x.AccGroupCode == accountMaster.AccGroupCode);

                    if((group!=null)&& (group.GroupName != accountMaster.GroupName))
                    {
                         group.GroupName = accountMaster.SubGroupName;
                         group.AccGroupCode = accountMaster.AccSubGroupCode;
                        _repositoryContext.Entry(group).State = EntityState.Modified;
                    }
                }
                

                if ((!string.IsNullOrEmpty(accountMaster.GroupName))&&(IsSubgroupUpdated==false))
                {
                    var accountsSubGroupCodes = await _repositoryContext.AccountsSubGroupMasters.Select(sg => sg.AccSubGroupCode).ToListAsync();
                    if (accountsSubGroupCodes?.Any() ?? false)
                    {
                        for (int i = 0; i <= accountsSubGroupCodes.Count; i++)
                        {
                            if (!accountsSubGroupCodes.Contains(accountMaster.AccGroupCode + "0" + Convert.ToInt32(i + 1)))
                            {
                                accountMaster.AccSubGroupCode = accountMaster.AccGroupCode + "0" + Convert.ToInt32(i + 1);
                                break;
                            }
                        }
                    }
                    else
                        accountMaster.AccSubGroupCode = accountMaster.AccGroupCode + "01";
                }

                var g = await _repositoryContext.AccountsGroupMasters.SingleOrDefaultAsync(x => x.AccGroupCode == accountMaster.AccGroupCode);

                if (g == null && string.IsNullOrEmpty(accountMaster.GroupName))
                {
                    var group = new AccountsGroupMaster
                    {
                        AccGroupCode = accountMaster.AccGroupCode,
                        AccHeadCode = accountMaster.AccHeadCode,
                        GroupName = accountMaster.SubGroupName
                    };

                    _repositoryContext.AccountsGroupMasters.Add(group);
                }
               

                if ((accountMaster.AccGroupCode != null)&&(accountMaster.SubGroupName.Trim()!=string.Empty)&&(accountMaster.AccSubGroupCode!=null))
                {
                    var subGroup = new AccountsSubGroupMaster
                    {
                        AccSubGroupCode = accountMaster.AccSubGroupCode,
                        AccGroupCode = accountMaster.MainGroupCode ?? accountMaster.AccGroupCode,
                        AccHeadCode = accountMaster.AccHeadCode,
                        SubGroupName = accountMaster.SubGroupName,
                        GroupName=accountMaster.GroupName
                    };
                    _repositoryContext.AccountsSubGroupMasters.Add(subGroup);
                }               

                var result = await _repositoryContext.SaveChangesAsync();

                if (result == 3 || result == 2 || result == 1)
                {
                    var addedGroup = new AccountsGroup
                    {
                        HeadCode = accountMaster.AccHeadCode,
                        OrgCode = accountMaster.OrgCode,
                        AccountOrGroupCode = accountMaster.AccSubGroupCode ?? accountMaster.AccGroupCode,
                        AccountOrGroupName = accountMaster.SubGroupName,
                        ParentGroupCode = accountMaster.AccSubGroupCode != null ? accountMaster.AccGroupCode : accountMaster.AccHeadCode,
                        MainGroupCode = accountMaster.MainGroupCode,
                        IsAccount = false
                    };

                    return addedGroup;
                }
                else
                    return new AccountsGroup();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<AccountsGroup> GetAllGroupsAsync()
        {
            var allGroups = new List<AccountsGroup>();

            try
            {
                var allAccountsGroups = (from ams in _repositoryContext.AccountsMasters
                                         join ag in _repositoryContext.AccountsGroupMasters on ams.AccHeadCode equals ag.AccHeadCode into ag1
                                         from ag2 in ag1.DefaultIfEmpty()
                                         join asg in _repositoryContext.AccountsSubGroupMasters on ag2.AccGroupCode equals asg.AccGroupCode into asg1
                                         from asg2 in asg1.DefaultIfEmpty()
                                         select ams).ToList();

                var groupCodes = allAccountsGroups.SelectMany(x => x.AccountsGroupMasters).Select(y => y.AccGroupCode).Distinct().ToList();
                var subGroupCodes = allAccountsGroups.SelectMany(a => a.AccountsGroupMasters).SelectMany(b => b.AccountsSubGroupMasters).Select(c => c.AccSubGroupCode).Distinct().ToList();
                var allGroupCodes = groupCodes.Union(subGroupCodes).ToList();

             var accounts = _repositoryContext.AccountHeadMasters.Where(gc => allGroupCodes.Contains(gc.AccGroupCode)).ToList();

                foreach (var am in allAccountsGroups)
                {
                    foreach (var ag in am.AccountsGroupMasters)
                    {
                        if (!allGroups.Select(x => x.AccountOrGroupCode).ToList().Contains(ag.AccGroupCode))
                        {
                            allGroups.Add(new AccountsGroup
                            {
                                HeadCode = am.AccHeadCode,
                                OrgCode = am.OrgCode,
                                AccountOrGroupCode = ag.AccGroupCode,
                                AccountOrGroupName = ag.GroupName,
                                ParentGroupCode = ag.AccHeadCode,
                                IsAccount = false
                            });
                        }

                        foreach (var sg in ag.AccountsSubGroupMasters)
                        {
                            if (!allGroups.Select(x => x.AccountOrGroupCode).ToList().Contains(sg.AccSubGroupCode))
                            {
                                allGroups.Add(new AccountsGroup
                                {
                                    HeadCode = am.AccHeadCode,
                                    OrgCode = am.OrgCode,
                                    AccountOrGroupCode = sg.AccSubGroupCode,
                                    AccountOrGroupName = sg.SubGroupName,
                                    ParentGroupCode = sg.AccSubGroupCode.Substring(0, sg.AccSubGroupCode.Length - 2),
                                    MainGroupCode = sg.AccGroupCode,
                                    IsAccount = false
                                });
                            }
                         
                                foreach (var ac in accounts.Where(g => g.AccGroupCode == ag.AccGroupCode || g.AccGroupCode == sg.AccSubGroupCode))
                                {
                                    if (!allGroups.Select(x => x.AccountOrGroupCode).ToList().Contains(ac.AccountCode))
                                    {
                                        allGroups.Add(new AccountsGroup
                                        {
                                            HeadCode = ac.AccHeadCode,
                                            OrgCode = am.OrgCode,
                                            AccountOrGroupCode = ac.AccountCode,                                         
                                            AccountOrGroupName = ac.AccountName + " - A/C",
                                            ParentGroupCode = ac.AccSubGroupCode ?? ac.AccGroupCode,
                                            MainGroupCode = ac.AccGroupCode,
                                            IsAccount = true
                                        });
                                    }
     
                            }
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return allGroups;
        }

        
        public IEnumerable<AccountsGroup> GetAllGroupsForHeadAsync(string AccountHead)
        {
            var allGroups = new List<AccountsGroup>();

            try
            {
                var allAccountsGroups = (from ams in _repositoryContext.AccountsMasters
                                         join ag in _repositoryContext.AccountsGroupMasters on ams.AccHeadCode equals ag.AccHeadCode into ag1 where ams.AccHeadCode==AccountHead
                                         from ag2 in ag1.DefaultIfEmpty()
                                         join asg in _repositoryContext.AccountsSubGroupMasters on ag2.AccGroupCode equals asg.AccGroupCode into asg1
                                         from asg2 in asg1.DefaultIfEmpty()
                                         select ams).ToList();

                var groupCodes = allAccountsGroups.SelectMany(x => x.AccountsGroupMasters).Select(y => y.AccGroupCode).Distinct().ToList();
                var subGroupCodes = allAccountsGroups.SelectMany(a => a.AccountsGroupMasters).SelectMany(b => b.AccountsSubGroupMasters).Select(c => c.AccSubGroupCode).Distinct().ToList();
                var allGroupCodes = groupCodes.Union(subGroupCodes).ToList();

                var accounts = _repositoryContext.AccountHeadMasters.Where(gc => allGroupCodes.Contains(gc.AccGroupCode)).ToList();

                foreach (var am in allAccountsGroups)
                {
                    foreach (var ag in am.AccountsGroupMasters)
                    {
                        if (!allGroups.Select(x => x.AccountOrGroupCode).ToList().Contains(ag.AccGroupCode))
                        {
                            allGroups.Add(new AccountsGroup
                            {
                                HeadCode = am.AccHeadCode,
                                OrgCode = am.OrgCode,
                                AccountOrGroupCode = ag.AccGroupCode,
                                AccountOrGroupName = ag.GroupName,
                                ParentGroupCode = ag.AccHeadCode,
                                IsAccount = false
                            });
                        }

                        foreach (var sg in ag.AccountsSubGroupMasters)
                        {
                            if (!allGroups.Select(x => x.AccountOrGroupCode).ToList().Contains(sg.AccSubGroupCode))
                            {
                                allGroups.Add(new AccountsGroup
                                {
                                    HeadCode = am.AccHeadCode,
                                    OrgCode = am.OrgCode,
                                    AccountOrGroupCode = sg.AccSubGroupCode,
                                    AccountOrGroupName = sg.SubGroupName,
                                    ParentGroupCode = sg.AccSubGroupCode.Substring(0, sg.AccSubGroupCode.Length - 2),
                                    MainGroupCode = sg.AccGroupCode,
                                    IsAccount = false
                                });
                            }

                            foreach (var ac in accounts.Where(g => g.AccGroupCode == ag.AccGroupCode || g.AccGroupCode == sg.AccSubGroupCode))
                            {
                                if (!allGroups.Select(x => x.AccountOrGroupCode).ToList().Contains(ac.AccountCode))
                                {
                                    allGroups.Add(new AccountsGroup
                                    {
                                        HeadCode = ac.AccHeadCode,
                                        OrgCode = am.OrgCode,
                                        AccountOrGroupCode = ac.AccountCode,
                                        AccountOrGroupName = ac.AccountName + " - A/C",
                                        ParentGroupCode = ac.AccSubGroupCode ?? ac.AccGroupCode,
                                        MainGroupCode = ac.AccGroupCode,
                                        IsAccount = true
                                    });
                                }

                            }
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return allGroups;
        }

        public async Task<AccountMaster> FindAccountsGroup(string headCode, int orgCode, string accountOrGroupCode, string mainGroupCode, string parentGroupCode, bool isAccount)
        {
            AccountMaster accountMaster = null;

            if (isAccount)
            {
                accountMaster = await (from am in _repositoryContext.AccountsMasters
                                       join ahm in _repositoryContext.AccountHeadMasters on am.AccHeadCode equals ahm.AccHeadCode
                                       join ag in _repositoryContext.AccountsGroupMasters on ahm.AccGroupCode equals ag.AccGroupCode
                                      // join sg in _repositoryContext.AccountsSubGroupMasters on ahm.AccSubGroupCode equals sg.AccSubGroupCode into sg1
                                      // from sg2 in sg1.DefaultIfEmpty()
                                       where am.OrgCode == orgCode && ahm.AccountCode == accountOrGroupCode
                                       && ahm.AccGroupCode == mainGroupCode
                                       select new AccountMaster
                                       {
                                           OrgCode = am.OrgCode,
                                           AccHeadCode = am.AccHeadCode,
                                           HeadName = am.HeadName,
                                           AccGroupCode = ahm.AccSubGroupCode ?? ahm.AccGroupCode,
                                           GroupName = ag.GroupName,
                                           AccountCode = ahm.AccountCode,
                                           AccountName = ahm.AccountName,
                                           OpBalanceDate = ahm.OpBalanceDate,
                                           OpBalanceAmount = ahm.OpBalanceAmount,
                                           DebitCreditDetails = ahm.DebitCreditDetails
                                           
                                       }).SingleOrDefaultAsync();

            }
            else
            {

                var accountGroupsDetail = await (from am in _repositoryContext.AccountsMasters
                                                 join agm in _repositoryContext.AccountsGroupMasters on am.AccHeadCode equals agm.AccHeadCode
                                                 join asgm in _repositoryContext.AccountsSubGroupMasters on agm.AccGroupCode equals asgm.AccGroupCode into asgm1
                                                 from asgm2 in asgm1.DefaultIfEmpty()
                                                 where agm.AccHeadCode == headCode && am.OrgCode == orgCode
                                                   // && agm.AccGroupCode == mainGroupCode || agm.AccGroupCode == parentGroupCode
                                                 select am).ToListAsync();

                foreach (var ac in accountGroupsDetail)
                {
                    foreach (var ag in ac.AccountsGroupMasters)
                    {
                        if (ag.AccGroupCode == accountOrGroupCode.TrimEnd())
                        {
                            accountMaster = new AccountMaster
                            {
                                AccHeadCode = ag.AccHeadCode,
                                HeadName = ac.HeadName,
                                OrgCode = ac.OrgCode,
                                AccSubGroupCode = ag.AccGroupCode,
                                SubGroupName = ag.GroupName
                            };

                            break;
                        }

                        foreach (var sg in ag.AccountsSubGroupMasters)
                        {
                            if (sg.AccSubGroupCode == accountOrGroupCode.TrimEnd())
                            {
                                accountMaster = new AccountMaster
                                {
                                    AccHeadCode = sg.AccHeadCode,
                                    HeadName = ac.HeadName,
                                    OrgCode = ac.OrgCode,
                                    AccGroupCode = parentGroupCode,
                                    GroupName = ag.AccountsSubGroupMasters.FirstOrDefault(sgn => sgn.AccSubGroupCode == parentGroupCode) == null ? ac.AccountsGroupMasters.FirstOrDefault(g => g.AccGroupCode == parentGroupCode).GroupName : ag.AccountsSubGroupMasters.FirstOrDefault(sgn => sgn.AccSubGroupCode == parentGroupCode).SubGroupName,
                                    AccSubGroupCode = sg.AccSubGroupCode,
                                    SubGroupName = sg.SubGroupName
                                };

                                break;
                            }
                        }
                    }
                }

            }
            return accountMaster;
        }
    }
}