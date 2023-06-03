using Newtonsoft.Json;
using System.Collections.Generic;

namespace GherkinWebAPI.Models.FarmersInputReturns
{
	public class FieldStaffCropGroupSeason
	{
		[JsonProperty("fieldStaffList")]
		public IEnumerable<Employee.Employee> FieldStaffList { get; set; }
		[JsonProperty("cropGroupList")]
		public IEnumerable<CropGroup> CropGroupList { get; set; }
		[JsonProperty("seasonFromTo")]
		public IEnumerable<SeasonFromTo> SeasonFromTo { get; set; }

	}
}