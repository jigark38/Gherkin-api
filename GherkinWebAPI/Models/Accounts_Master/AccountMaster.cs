using System;


namespace GherkinWebAPI.Models.Accounts_Master
{
    public class AccountMaster
    {
        public int OrgCode { get; set; }
        public string AccHeadCode { get; set; }
        public string HeadName { get; set; }
        public string MainGroupCode { get; set; }
        public string AccGroupCode { get; set; }
        public string GroupName { get; set; }
        public string AccSubGroupCode { get; set; }
        public string SubGroupName { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public DateTime? OpBalanceDate { get; set; }
        public decimal? OpBalanceAmount { get; set; }
        public string DebitCreditDetails { get; set; }
    }
}