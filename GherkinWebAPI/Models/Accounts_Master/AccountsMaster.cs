using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.Accounts_Master
{
    public class AccountsMaster
    {
        [Key]
        [Column("Acc_Head_Code")]
        public string AccHeadCode { get; set; }
        [Column("Org_Code")]
        public int OrgCode { get; set; }
        [Column("Head_Name")]
        public string HeadName { get; set; }

        //Navigation Properties
        public virtual ICollection<AccountsGroupMaster> AccountsGroupMasters { get; set; }
    }
}