using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    [Table("Supplier_Org_Documents")]
    public class SupplierDocumentDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("Supplier_Org_Doc_No")]
        public int supplierOrgDocNo { get; set; }

        [Column("Supplier_Org_ID")]
        public string supplierOrgID { get; set; }

        [Column("Supplier_Document_Name")]
        public string supplierDocumentName { get; set; }

        [Column("Supplier_Document_Details")]
        public byte[] supplierDocumentDetails { get; set; }

        [Column("Supplier_Document_PreappendText")]
        public string supplierDocumentPreappendText { get; set; }
    }
}