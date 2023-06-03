using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.GreenReception
{
    public class GreenReceptionQualityCheck
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Greens_Quality_Check_No")]
        [Key]
        public long greenQualityCheckNo { get; set; }
        [Column("Org_office_No")]
        [Required]
        public int orgofficeNo { get; set; }
        [Column("Inward_Gate_Pass_No")]
        [Required]
        [MaxLength(20)]
        public string inwardGatePassNo { get; set; }
        [Column("Area_ID")]
        [Required]
        [MaxLength(10)]
        public string areaId { get; set; }
        [Column("Harvest_GRN_No")]
        [Required]
        public long harvestGRNNo { get; set; }
        [Column("Greens_Recv_Sample_Date")]
        [Required]
        public DateTime greensRecvSampleDate { get; set; } = DateTime.Now;
        [Column("Greens_Recv_Sample_Qty")]
        public int greensRecvSampleQty { get; set; } = 0;
        [Column("Greens_Recv_Trunck_Condition")]
        [MaxLength(100)]
        public string greensRecvTrunkCondition { get; set; }
        [Column("Greens_Checked_Employee_ID")]
        public long greensCheckedEmployeeId { get; set; }
        [Column("Greens_Verified_Employee_ID")]
        public long greensVerifiedEmployeeId { get; set; }
        [Column("Greens_Recv_AG_No")]
        [MaxLength(25)]
        public string greensRecvAGNo { get; set; }
        [Column("Greens_Recv_Remarks")]
        [MaxLength(300)]
        public string greensRecvRemarks { get; set; }
    }
}