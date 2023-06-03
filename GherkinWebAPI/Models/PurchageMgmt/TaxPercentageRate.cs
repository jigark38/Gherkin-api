using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.PurchageMgmt
{
    public class TaxPercentageRate
    {
        public Nullable<decimal> igstRate { get; set; }
        public Nullable<decimal> cgstRate { get; set; }
        public Nullable<decimal> sgstRate { get; set; }
    }
}