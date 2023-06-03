using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.StoresMasterDetails
{
	public class GSGroupDetail
	{
		[Key]
		[Column("GS_Group_Code")]
		[JsonProperty("gsGroupCode")]
		[Required]
		public int GSGroupCode { get; set; }

		[Column("GS_Group_Name")]
		[JsonProperty("gsGroupName")]
		[MaxLength(100)]
		[Required]
		public string GSGroupName { get; set; }
	}
}