using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO
{
    public class SupplierDocumentsDto
    {
        public string supplierOrgID { get; set; }

        public string supplierDocumentName { get; set; }

        public byte[] supplierDocumentDetails { get; set; }
        public string supplierDocumentPreappendText { get; set; }
        public int supplierOrgDocNo { get; set; }
    }
}