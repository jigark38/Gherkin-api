using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.Accounts_Master
{
    public class AccountsGroup
    {
        public string HeadCode { get; set; }
        public int OrgCode { get; set; }
        public string AccountOrGroupCode { get; set; }
        public string AccountOrGroupName { get; set; }
        public string ParentGroupCode { get; set; }
        public string MainGroupCode { get; set; }
        public bool IsAccount { get; set; } 
    }
}