using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.PurchageMgmt
{
    public class RowMaterialGroupMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        [Key]
        [Column("Raw_Material_Group_Code")]
        public string RawMatrialGroupCode { get; set; }
        [Column("Material_Purchases")]
        public string MaterialPurchases { get; set; }
        [Column("Raw_Material_Group")]
        public string RawMaterialGroup { get; set; }
    }
}