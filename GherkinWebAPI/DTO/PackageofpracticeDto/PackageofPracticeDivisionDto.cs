using GherkinWebAPI.Models.PackageOfPracticeModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.PackageofpracticeDto
{
    public class PackageofPracticeDivisionDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("divisionFor")]
        public string DivisionFor { get; set; }
        [JsonProperty("cropnameCode")]
        public string CropNameCode { get; set; }
        [JsonProperty("cropName")]
        public string CropName { get; set; }
        [JsonProperty("cropgroupCode")]
        public string CropGroupCode { get; set; }
        [JsonProperty("practiceperAcre")]
        public string PracticePerAcre { get; set; }
        [JsonProperty("psNo")]
        public string PSNO { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("transCode")]
        public string TransCode { get; set; }
        [JsonProperty("cropphaseCode")]
        public string CropphaseCode { get; set; }
        [JsonProperty("cropphaseName")]
        public string CropPhaseName { get; set; }
        [JsonProperty("practiceEffectiveDate")]
        public DateTime PracticeEffectiveDate { get; set; }
        [JsonProperty("practiceEffectiveDateString")]
        public string PracticeEffectiveDateString { get; set; }
        [JsonProperty("practiceperNo")]
        public string PracticeNo { get; set; }
        [JsonProperty("harvestDetails")]
        public string HarvestDetails { get; set; }

        [JsonProperty("fromDays")]
        public int HS_Days_After_Sowing_From { get; set; }

        [JsonProperty("toDays")]
        public int HS_Days_After_Sowing_To { get; set; }

        [JsonProperty("createdDate")]
        public Nullable<DateTime> CreatedDate { get; set; }
        [JsonProperty("modifyDate")]
        public Nullable<DateTime> ModifyDate { get; set; }
        [JsonProperty("packagepracticeMaterials")]
        public ICollection<PackageofPracticeMaterialsDto> PackagePracticeMaterials { get; set; }

    }

}