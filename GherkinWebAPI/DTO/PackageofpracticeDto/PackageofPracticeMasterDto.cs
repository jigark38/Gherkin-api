using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GherkinWebAPI.DTO.PackageofpracticeDto
{
    public class PackageofPracticeMasterDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("entryDate")]
        public DateTime EntryDate { get; set; }
        [JsonProperty("employeeId")]
        public string EmployeeID { get; set; }
        [JsonProperty("cropgroupCode")]
        public string CropGroupCode { get; set; }
        [JsonProperty("cropnameCode")]
        public string CropNameCode { get; set; }

        [JsonProperty("divisionFor")]
        public string DivisionFor { get; set; }

        [JsonProperty("practiceperAcre")]
        public string PracticePerAcre { get; set; }
        [JsonProperty("psNo")]
        public string PSNO { get; set; }

        [JsonProperty("transCode")]
        public string TransCode { get; set; }
        [JsonProperty("cropphaseCode")]
        public string CropphaseCode { get; set; }
        [JsonProperty("cropphaseName")]
        public string CropPhaseName { get; set; }
        [JsonProperty("practiceeffectiveDate")]
        public DateTime PracticeEffectiveDate { get; set; }
        [JsonProperty("practiceperNo")]
        public string PracticeNo { get; set; }
        [JsonProperty("harvestDetails")]
        public string HarvestDetails { get; set; }

        [JsonProperty("createdDate")]
        public Nullable<DateTime> CreatedDate { get; set; }
        [JsonProperty("modifyDate")]
        public Nullable<DateTime> ModifyDate { get; set; } 

        public List<PackageofPracticeMaterialsDto> packageofMaterials { get; set; }

    }
}