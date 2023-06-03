using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.DailyInputAndFeedingDetails
{
    [Table("Farmers_Material_Issue_Details")]
    public class FarmersMaterialIssueDetails
    {
        [JsonProperty("mifMaterialIssueTRNo")]
        [Column("MIF_Material_Issue_TR_No")]
        [Key]
        public int MIFMaterialIssueTRNo { get; set; }

        [JsonProperty("mifConsumptionNo")]
        [Column("MIF_Consumption_No")]
        public int MIFConsumptionNo { get; set; }

        [JsonProperty("rawMaterialGroupCode")]
        [Column("Raw_Material_Group_Code")]
        public string RawMaterialGroupCode { get; set; }

        [JsonProperty("rawMaterialDetailsCode")]
        [Column("Raw_Material_Details_Code")]
        public string RawMaterialDetailsCode { get; set; }

        [JsonProperty("farmersMaterialIssuedQty")]
        [Column("Farmers_Material_Issued_Qty")]
        public decimal FarmersMaterialIssuedQty { get; set; }

    }
}