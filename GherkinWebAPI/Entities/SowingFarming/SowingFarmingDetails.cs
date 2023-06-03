using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace GherkinWebAPI.Entities.SowingFarming
{
    public class SowingFarmingDetails
    {
        [Key]
        [JsonProperty("sowingNo")]
        public long Sowing_No { get; set; }

        [JsonProperty("areaId")]
        public string Area_ID { get; set; }

        [JsonProperty("farmerCode")]
        public string Farmer_Code { get; set; }

        [JsonProperty("farmersAgreementCode")]
        public string Farmers_Agreement_Code { get; set; }

        [JsonProperty("farmersAccountNo")]
        public string Farmers_Account_No { get; set; }

        [JsonProperty("cropNameCode")]
        public string Crop_Name_Code { get; set; }


        [JsonProperty("farmerLocation")]
        public string Farmer_location { get; set; }

        [JsonProperty("farmPicture")]
        public byte[] Farm_Picture { get; set; }

        [JsonProperty("psNumber")]
        public string PS_Number { get; set; }

        [JsonProperty("sowingBeginingDate")]
        public DateTime Sowing_Begining_Date { get; set; }

        [JsonProperty("hsEnteredEmpId")]
        public string HS_Entered_Emp_ID { get; set; }
    }
}