using Newtonsoft.Json;

namespace GherkinWebAPI.Models.FarmersInputReturns
{
	public class FarmerAndVillage
	{
		[JsonProperty("farmerCode")]
		public string FarmerCode { get; set; }

		[JsonProperty("farmerName")]
		public string FarmerName { get; set; }

		[JsonProperty("farmerAltContactPerson")]
		public string FarmerAltContactPerson { get; set; }

		[JsonProperty("villageCode")]
		public int VillageCode { get; set; }

		[JsonProperty("villageName")]
		public string VillageName { get; set; }

		[JsonProperty("accountNo")]
		public string AccountNo { get; set; }
		[JsonProperty("areaId")]
		public string Area_ID { get; set; }
		[JsonProperty("PSNumber")]
		public string PS_Number { get; set; }
	}
}