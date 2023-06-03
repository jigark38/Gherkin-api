using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.GRNAndMaterialClassification
{
    public class HarvestBuyerWeighingDetailsDTO
    {
		[JsonProperty("HarvestGRNNo")]
		public Int64 HarvestGRNNo { get; set; }
		[JsonProperty("CWGreensCratewiseEntryNo")]
		public int CWGreensCratewiseEntryNo { get; set; }
		[JsonProperty("greensProcurementNo")]
		public int GreensProcurementNo { get; set; }
		[JsonProperty("cropGroupCode")]
		public string CropGroupCode { get; set; }
		[JsonProperty("cropNameCode")]
		public string CropNameCode { get; set; }
		[JsonProperty("cropSchemeCode")]
		public string CropSchemeCode { get; set; }
		[JsonProperty("noOfCrates")]
		public int NoofCrates { get; set; }
		[JsonProperty("eachCrateWt")]
		public decimal EachCrateWt { get; set; }
		[JsonProperty("crateNoFrom")]
		public int? CrateNoFrom { get; set; }
		[JsonProperty("crateNoTo")]
		public int? CrateNoTo { get; set; }
		[JsonProperty("grossWeight")]
		public decimal CWGrossWeight { get; set; }
		[JsonProperty("tareweight")]
		public decimal CWTareweight { get; set; }
		[JsonProperty("crateswiseNetWeight")]
		public decimal CWCrateswiseNetWeight { get; set; }
		[JsonProperty("BuyerEmployeeID")]
		public string BuyerEmployeeID { get; set; }
		[JsonProperty("completeBuyer")]
		public int CompleteBuyer { get; set; }
	}
}