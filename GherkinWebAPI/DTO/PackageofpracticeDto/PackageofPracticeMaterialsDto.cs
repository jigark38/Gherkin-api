using Newtonsoft.Json;
using System;
namespace GherkinWebAPI.DTO.PackageofpracticeDto
{
	public class PackageofPracticeMaterialsDto
	{

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("practiceNo")]
        public string PracticeNo { get; set; }

        [JsonProperty("cropnameCode")]
        public string CropNameCode { get; set; }
        
        [JsonProperty("cropPhaseCode")]
        public string CropPhaseCode { get; set; }

        [JsonProperty("cropPhaseName")]
        public string CropPhaseName { get; set; }

        [JsonProperty("daysApplicable")]
        public int DaysApplicable { get; set; }

        [JsonProperty("tradeName")]
        public string TradeName { get; set; }        

        [JsonProperty("chemicalVolume")]
        public decimal Chemicalvolume { get; set; }

        [JsonProperty("chemicalUom")]
        public string ChemicalUOM { get; set; }

        [JsonProperty("sprayVolume")]
        public int Sprayvolume { get; set; }

        [JsonProperty("chemicalQty")]
        public int ChemicalQty { get; set; }

        [JsonProperty("targetPest")]
        public string TragetPest { get; set; }

        [JsonProperty("rawmaterialGroupcode")]
        public string RawmaterialGroupcode { get; set; }

        [JsonProperty("rawmaterialDetailscode")]
        public string Raw_Material_Details_Code { get; set; }
        [JsonProperty("chemicalName")]
        public string ChemicalName { get; set; }   

        [JsonProperty("createdDate")]
        public Nullable<DateTime> CreatedDate { get; set; }

        [JsonProperty("modifyDate")]
        public Nullable<DateTime> ModifyDate { get; set; }

        [JsonProperty("practiceeffectiveDate")]
        public Nullable<DateTime> PracticeEffectiveDate { get; set; }

    }
}