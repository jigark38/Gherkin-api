using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class SupplierDetailsDTO
    {
        public int supplierOrgDocNo { get; set; }

        public string supplierOrgID { get; set; }

        public string supplierDocumentName { get; set; }

        public string supplierDocumentDetails { get; set; }
        public string supplierDocumentPreappendText { get; set; }
    }
}