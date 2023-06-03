using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.GreenReception
{
    public class GreenReceptionQualityDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Greens_QC_Details_No")]
        [Key]
        public long greensQCDetailsNo { get; set; }
        [Column("Greens_Quality_Check_No")]
        [Required]
        public long greensQualityCheckNo { get; set; } = 0;
        [Column("BORRER_QC_UOM")]
        [MaxLength(10)]
        public string borrerQCUOM { get; set; }
        [Column("BORRER_QC_Qty")]
        [Required]
        [Range(0, 999.999)]
        public decimal borrerQCQty { get; set; } = 0;
        [Column("FF_QC_UOM")]
        [MaxLength(10)]
        public string ffQCUOM { get; set; }
        [Column("FF_QC_Qty")]
        [Required]
        [Range(0, 999.999)]
        public decimal ffQCQty { get; set; } = 0;
        [Column("SOFT_QC_UOM")]
        [MaxLength(10)]
        public string softQCUOM { get; set; }
        [Column("SOFT_QC_Qty")]
        [Required]
        [Range(0, 999.999)]
        public decimal softQCQty { get; set; } = 0;
        [Column("FUNGUS_QC_UOM")]
        [MaxLength(10)]
        public string fungusQCUOM { get; set; }
        [Column("FUNGUS_QC_Qty")]
        [Required]
        [Range(0, 999.999)]
        public decimal fungusQCQty { get; set; } = 0;
        [Column("STEMS_QC_UOM")]
        [MaxLength(10)]
        public string stemsQCUOM { get; set; }
        [Column("STEMS_QC_Qty")]
        [Required]
        [Range(0, 999.999)]
        public decimal stemsQCQty { get; set; } = 0;
        [Column("VIRUS_QC_UOM")]
        [MaxLength(10)]
        public string virusQCUOM { get; set; }
        [Column("VIRUS_QC_Qty")]
        [Required]
        [Range(0, 999.999)]
        public decimal virusQCQty { get; set; } = 0;
        [Column("FLOWERS_QC_UOM")]
        [MaxLength(10)]
        public string flowersQCUOM { get; set; }
        [Column("FLOWERS_QC_Qty")]
        [Required]
        [Range(0, 999.999)]
        public decimal flowersQCQty { get; set; } = 0;
        [Column("MUDDY_QC_UOM")]
        [MaxLength(10)]
        public string muddyQCUOM { get; set; }
        [Column("MUDDY_QC_Qty")]
        [Required]
        [Range(0, 999.999)]
        public decimal muddyQCQty { get; set; } = 0;
        [Column("PEANUT_QC_UOM")]
        [MaxLength(10)]
        public string peanutQCUOM { get; set; }
        [Column("PEANUT_QC_Qty")]
        [Required]
        [Range(0, 999.999)]
        public decimal peanutQCQty { get; set; } = 0;
        [Column("CAL_QC_UOM")]
        [MaxLength(10)]
        public string calQCUOM { get; set; }
        [Column("CAL_QC_Qty")]
        [Required]
        [Range(0, 999.999)]
        public decimal calQCQty { get; set; } = 0;
        [Column("ENDCROP_QC_UOM")]
        [MaxLength(10)]
        public string endcorpQCUOM { get; set; }
        [Column("ENDCROP_QC_Qty")]
        [Required]
        [Range(0, 999.999)]
        public decimal endcorpQCQty { get; set; } = 0;
        [Column("Rotten_QC_UOM")]
        [MaxLength(10)]
        public string rottenQCUOM { get; set; }
        [Column("Rotten_QC_Qty")]
        [Required]
        [Range(0, 999.999)]
        public decimal rottenQCQty { get; set; } = 0;
    }
}