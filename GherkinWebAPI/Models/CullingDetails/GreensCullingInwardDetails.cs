using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.CullingDetails
{
    [Table("Greens_Culling_Inward_Details")]
    public class GreensCullingInwardDetails
    {
        [Key]
        [Column("Greens_Culling_No")]
        public int GreensCullingNo { get; set; }

        [Column("Batch_Production_No")]
        public int BatchProductionNo { get; set; }

        [Column("Batch_Size_Approx")]
        public decimal BatchSizeApprox { get; set; }

        [Column("Media_Process_Code")]
        public string MediaProcessCode { get; set; }

        [Column("Culling_Grading_Status")]
        public string CullingGradingStatus { get; set; }

        [Column("Culling_Weighment_Mode")]
        public string CullingWeighmentMode { get; set; }

        [Column("Culling_Process_Date")]
        public DateTime CullingProcessDate { get; set; }

        [Column("Employee_ID")]
        public int EmployeeID { get; set; }

        [Column("Culling_Starting_Barrel_No")]
        public int CullingStartingBarrelNo { get; set; }

        [Column("Culling_Ending_Barrel_No")]
        public int CullingEndingBarrelNo { get; set; }

        [Column("Packed_No_Of_Barrels")]
        public int PackedNoOfBarrels { get; set; }

        [Column("Culling_Packing_Team_Details")]
        public string CullingPackingTeamDetails { get; set; }

        [Column("Culling_Total_Quantity")]
        public decimal CullingTotalQuantity { get; set; }

        [Column("Packing_Barcode_Required")]
        public string PackingBarcodeRequired { get; set; }
    }
}