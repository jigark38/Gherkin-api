using Newtonsoft.Json;

namespace GherkinWebAPI.Models.HarvestStage
{
	public class EffectiveDateForHarvestDetails
	{
		[JsonProperty("effectiveDate")]
		public string EffectiveDate { get; set; }
		[JsonProperty("hsTransactionCode")]
		public string HSTransactionCode { get; set; }
	}
}