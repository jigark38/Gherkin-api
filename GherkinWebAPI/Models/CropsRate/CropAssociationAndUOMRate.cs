using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.CropsRate
{
    public class CropAssociationAndUOMRate
    {
        [JsonProperty("cropRateAsPerAssociation")]
        public decimal CropRateAsPerAssociation { get; set; }
        [JsonProperty("cropRatePerUOM")]
        public string CropRatePerUOM { get; set; }
    }
}