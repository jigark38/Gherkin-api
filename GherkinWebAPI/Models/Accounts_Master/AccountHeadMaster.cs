using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.Accounts_Master
{
    public class AccountHeadMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        [Column("Account_Code")]
        public string AccountCode { get; set; }
        [Column("Account_Name")]
        public string AccountName { get; set; }
        [Column("Acc_Head_Code")]
        public string AccHeadCode { get; set; }
        [Column("Acc_Group_Code")]
        public string AccGroupCode { get; set; }
        [Column("Acc_Sub_Group_Code")]
        public string AccSubGroupCode { get; set; }
        [Column("Op_Balance_Date")]
        public DateTime? OpBalanceDate { get; set; }
        [Column("Op_Balance_Amount")]
        public decimal? OpBalanceAmount { get; set; }
        [Column("Debit_Credit_Details")]
        public string DebitCreditDetails { get; set; }

        public virtual AccountsGroupMaster AccountsGroupMaster { get; set; }
        public virtual AccountsSubGroupMaster AccountsSubGroupMaster { get; set; }
    }
}