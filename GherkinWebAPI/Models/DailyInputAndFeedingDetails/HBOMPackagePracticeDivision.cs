using GherkinWebAPI.Models.PackageOfPracticeModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.DailyInputAndFeedingDetails
{
    public class HBOMPackagePracticeDivision
    {
        [JsonProperty("hbomdivisionFor")]
        public string HBOMDivisionFor { get; set; }
        [JsonProperty("hbompracticeperAcre")]
        public string HBOMPracticePerAcre { get; set; }
        [JsonProperty("hbompracticeNo")]
        public string HBOMPracticeNo { get; set; }
        public PackagePracticeMaterials ListPackage { get; set; }
    }
}