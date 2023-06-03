using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.Accounts_Master
{
    public class AccountsSubGroupMaster
    {

        [Key]
        [Column("Acc_Sub_Group_Code")]
        public string AccSubGroupCode { get; set; }
        [Column("Acc_Head_Code")]
        public string AccHeadCode { get; set; }
        [Column("Acc_Group_Code")]
        public string AccGroupCode { get; set; }
        [Column("Sub_Group_Name")]
        public string SubGroupName { get; set; }

        [Column("Group_Name")]
        public string GroupName { get; set; }

        //Navigation Properties
        public virtual AccountsGroupMaster AccountsMaster { get; set; }
        public virtual ICollection<AccountHeadMaster> AccountHeadMasters { get; set; }
    }
}