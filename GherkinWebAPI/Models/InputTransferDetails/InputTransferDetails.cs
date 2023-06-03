using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.InputTransferDetails
{
    [Table("RM_Input_Transfer_Details")]
    public class InputTransferDetails
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("Area_ID")]
        public string AreaId { get; set; }

        [Column("RM_Transfer_Date")]
        public DateTime TransferDate { get; set; }

        [Column("Crop_Group_Code")]
        public string CropGroupCode { get; set; }

        [Column("Crop_Name_Code")]
        public string CropNameCode { get; set; }

        [Column("Crop_Scheme_Code")]
        public string CropSchemeCode { get; set; }

        [Column("PS_Number")]
        public string PsNumber { get; set; }

        [Column("Input_Transfer_Remarks")]
        public string InutTransferRemarks { get; set; }

        [Column("RM_Transfer_No")]
        public string TransferNumber { get; set; }

        [Column("Org_Office_No")]
        public int? OrgOfficeNo { get; set; }
    }
}