using Newtonsoft.Json;

namespace GherkinWebAPI.DTO.SowingFarming
{
    public class HBOMPracticeForSowingFarming
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("hbomPracticeNo")]
        public string HBOM_Practice_No { get; set; }

        [JsonProperty("hbomDivisionFor")]
        public string HBOM_Division_For { get; set; }

        [JsonProperty("hsCropPhaseCode")]
        public string HS_Crop_Phase_Code { get; set; }

        [JsonProperty("hbomPracticePerAcerage")]
        public string HBOM_Practice_Per_Acreage { get; set; }

        [JsonProperty("hbomDaysApplicable")]
        public int HBOM_Days_Applicable { get; set; }

        [JsonProperty("rawMaterialDetailsCode")]
        public string Raw_Material_Details_Code { get; set; }

        [JsonProperty("hbomTradeName")]
        public string HBOM_Trade_Name { get; set; }

        [JsonProperty("rawMaterialDetailsName")]
        public string Raw_Material_Details_Name { get; set; }
    }
}