using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.Accounts_Master
{
    public class AccountsGroupMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        [Column("Acc_Group_Code")]
        public string AccGroupCode { get; set; }
        [Column("Acc_Head_Code")]
        public string AccHeadCode { get; set; }
        [Column("Group_Name")]
        public string GroupName { get; set; }

        //Navigation Properties
        //public virtual AccountMaster AccountMaster { get; set; }
        public virtual ICollection<AccountsSubGroupMaster> AccountsSubGroupMasters { get; set; }
        public virtual ICollection<AccountHeadMaster> AccountHeadMasters { get; set; }
    }
}