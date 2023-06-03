using Newtonsoft.Json;

namespace GherkinWebAPI.Models.FarmersInputReturns
{
	public class SeasonFromTo
	{
		[JsonProperty("psNumber")]
		public string PSNumber { get; set; }
		[JsonProperty("seasonFromToDate")]
		public string SeasonFromToDate { get; set; }
	}
}