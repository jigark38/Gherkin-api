using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.MaterialIndentByDepartment
{
	public class StoreInternalIndentMaster
	{
		[Column("Id")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[JsonProperty("id")]
		public int Id { get; set; }

		[Key]
		[Column("Store_Internal_Indent_No")]
		[JsonProperty("storeInternalIndentNo")]
		[MaxLength(13)]
		public string StoreInternalIndentNo { get; set; }

		[Column("Store_Internal_Indent_Date")]
		[JsonProperty("storeInternalIndentDate")]
		[Required]
		public DateTime StoreInternalIndentDate { get; set; }

		[Column("Emp_Id")]
		[JsonProperty("empId")]
		[MaxLength(10)]
		[Required]
		public string EmpId { get; set; }

		[Column("Dept_Id")]
		[JsonProperty("deptId")]
		[MaxLength(6)]
		public string DeptId { get; set; }

	}
}