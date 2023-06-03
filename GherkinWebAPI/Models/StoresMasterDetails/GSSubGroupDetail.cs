using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.StoresMasterDetails
{
	public class GSSubGroupDetail
	{
		[Key]
		[Column("GS_Sub_Group_Code")]
		[JsonProperty("gsSubGroupCode")]
		[Required]
		public int GSSubGroupCode { get; set; }

		[Column("GS_Group_Code")]
		[JsonProperty("gsGroupCode")]
		[Required]
		public int GSGroupCode { get; set; }

		[Column("GS_Sub_Group_Name")]
		[JsonProperty("gsSubGroupName")]
		[MaxLength(100)]
		[Required]
		public string GSSubGroupName { get; set; }

		[NotMapped]
		[JsonProperty("gsGroupName")]
		public string GSGroupName { get; set; }
	}
}