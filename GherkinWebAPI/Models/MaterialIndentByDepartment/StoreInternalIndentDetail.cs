using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.MaterialIndentByDepartment
{
	public class StoreInternalIndentDetail
	{
		[Key]
		[Column("Id")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[JsonProperty("id")]
		public int Id { get; set; }

		[Column("Store_Internal_Indent_No")]
		[JsonProperty("storeInternalIndentNo")]
		[MaxLength(13)]
		[Required]
		public string StoreInternalIndentNo { get; set; }

		[Column("Store_Material_Group_Code")]
		[JsonProperty("storeMaterialGroupCode")]
		[Required]
		public int StoreMaterialGroupCode { get; set; }

		[Column("Store_Material_SubGroup_Code")]
		[JsonProperty("storeMaterialSubGroupCode")]
		[Required]
		public int StoreMaterialSubGroupCode { get; set; }

		[Column("Store_Material_Item_Code")]
		[JsonProperty("storeMaterialItemCode")]
		[Required]
		public int StoreMaterialItemCode { get; set; }

		[Column("Store_Department_Indent_Quantity")]
		[JsonProperty("storeDeptIndentQty")]
		[Required]
		public decimal StoreDeptIndentQty { get; set; }

		[Column("Store_Department_Indent_Required_Date")]
		[JsonProperty("storeDeptIndentReqDate")]
		[Required]
		public DateTime StoreDeptIndentReqDate { get; set; }

		[NotMapped]
		[JsonProperty("gsGroupName")]
		public string StoreMaterialGroupName { get; set; }

		[NotMapped]
		[JsonProperty("gsSubGroupName")]
		public string StoreMaterialSubGroupName { get; set; }

		[NotMapped]
		[JsonProperty("gsMaterialName")]
		public string MaterialDetailName { get; set; }

	}
}