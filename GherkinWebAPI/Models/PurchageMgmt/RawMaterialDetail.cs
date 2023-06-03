using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace GherkinWebAPI.Models.PurchageMgmt
{
    public class RawMaterialDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        [Key]
        [Column("Raw_Material_Details_Code")]
        public string RawMaterialDetailsCode { get; set; }
        [Column("Raw_Material_Group_Code")]
        public string RawMaterialGroupCode { get; set; }
        [Column("Raw_Material_Details_Name")]
        public string RawMaterialDetailsName { get; set; }
        [Column("Raw_Material_QC_Norms")]
        public string RawMaterialQCNorms { get; set; }
        [Column("Raw_Material_UOM")]
        public string RawMaterialUOM { get; set; }
        [Column("Raw_Material_Reorder_Stock")]
        public int RawMaterialReorderStock { get; set; }
        [Column("Raw_Material_HSN_CODE_No")]
        public int RawMaterialReorderHSNCodeNo { get; set; }
        [Column("Raw_Material_IGST_Rate")]
        public int RawMaterialIGSTRate { get; set; }
        [Column("Raw_Material_CGST_Rate")]
        public int RawMaterialCGSTRate { get; set; }
        [Column("Raw_Material_SGST_Rate")]
        public int RawMaterialSGSTRate { get; set; }
        [Column("Raw_Material_Cess_Rate")]
        public int RawMaterialCessRate { get; set; }
    }
}