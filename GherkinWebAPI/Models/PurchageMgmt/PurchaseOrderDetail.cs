using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.PurchageMgmt
{
    public class PurchaseOrderDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        [Column("RM_PO_Date")]
        public Nullable<DateTime> rmPoDate { get; set; } = DateTime.Now;
        [Column("RM_PO_NO")]
        [Key]
        [MaxLength(20)]
        public string rmPoNo { get; set; }
        [Column("Domestic_Import")]
        [Required]
        [MaxLength(20)]            
        public string domesticImport { get; set; }
        [Column("Supplier_Org_ID")]
        [Required]
        [MaxLength(20)]
        public string supplierOrgId { get; set; }
        [Column("Supplier_Name")]
        [MaxLength(50)]
        public string supplierName { get; set; }
        [Column("Place_Code")]
        [Required]
        [MaxLength(50)]
        public string placeCode { get; set; }
        [Column("State_Name")]
        [MaxLength(50)]
        public string stateName { get; set; }
        [Column("Country_Name")]
        [MaxLength(30)]
        public string countryName { get; set; }
        [Column("Currency")]
        [MaxLength(20)]
        public string currency { get; set; }
        [Column("GST_Type")]
        [MaxLength(10)]
        public string gstType { get; set; }
        [Column("Quotation_Type")]
        [MaxLength(10)]
        public string quotationType { get; set; }
        [Column("Packing_Condition")]
        [MaxLength(20)]
        public string packingCondition { get; set; }
        [Column("Packing_Style")]
        [MaxLength(30)]
        public string packingStyle { get; set; }
        [Column("PO_Packing_Qty")]
        public int poPackingQty { get; set; }
        [Column("PO_Packing_Rate")]
        [Range(0, 99999999.99)]
        public Nullable<decimal> poPackingRate { get; set; }
        [Column("PO_Packing_Amt")]
        [Range(0, 9999999999999.99)]
        public Nullable<decimal> poPackingAmt { get; set; }
        [Column("Delivery_Condition")]
        [MaxLength(20)]
        public string deliveryCondition { get; set; }
        [Column("Delivery_Frt_Amt")]
        [Range(0, 9999999999999.99)]
        public Nullable<decimal> deliveryFrtAmt { get; set; }
        [Column("Insurance_Condition")]
        [MaxLength(20)]
        public string insuranceCondition { get; set; }
        [Column("Insurance_Amt")]
        [Range(0, 9999999999999.99)]
        public Nullable<decimal> insuranceAmt { get; set; }
        [Column("PO_Advance_Cond")]
        [MaxLength(20)]
        public string poAdvanceCond { get; set; }
        [Column("Adv_Details")]
        [Range(0, 9999999999999.99)]
        public Nullable<decimal> advDetails { get; set; }
        [Column("PO_Credit_Days")]
        public int poCreditDays { get; set; }
        [Column("PO_Delivery_Date")]
        public Nullable<DateTime> poDeliveryDate { get; set; } = DateTime.Now;
        [Column("PO_Delivery_Days")]
        public int poDeliveryDays { get; set; }
        [Column("PO_Total_Amount")]
        [Range(0, 999999999999999999.99)]
        public Nullable<decimal> poTotalAmount { get; set; }
        [Column("PO_Validity_Condition")]
        [MaxLength(20)]
        public string poValidityCondition { get; set; }
        [Column("PO_Validity_Days")]
        public int poValidityDays { get; set; } = 0;
    }
}