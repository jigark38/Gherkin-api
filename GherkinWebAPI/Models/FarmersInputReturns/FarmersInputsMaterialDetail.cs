using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.FarmersInputReturns
{
	public class FarmersInputsMaterialDetail
	{
        [Key]
        [Column("FIM_Return_TR_No")]
        [JsonProperty("fIMReturnTRNo")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int FIMReturnTRNo { get; set; }

        [Column("FIM_Return_No")]
        [JsonProperty("fIMReturnNo")]
        [Required]
        public int FIMReturnNo { get; set; }

        [Column("Raw_Material_Group_Code")]
        [JsonProperty("rawMaterialGroupCode")]
        [MaxLength(6)]
        [Required]
        public string RawMaterialGroupCode { get; set; }

        [Column("Raw_Material_Details_Code")]
        [JsonProperty("rawMaterialDetailsCode")]
        [MaxLength(7)]
        [Required]
        public string RawMaterialDetailsCode { get; set; }

        [Column("Farmers_Material_Return_Qty")]
        [JsonProperty("farmersMaterialReturnQty")]
        [Required]
        public decimal FarmersMaterialReturnQty { get; set; }



        [NotMapped]
        [JsonProperty("rawMaterialGroup")]
        public string RawMaterialGroup { get; set; }

        [NotMapped]
        [JsonProperty("rawMaterialDetailsName")]
        public string RawMaterialDetailsName { get; set; }

        [NotMapped]
        [JsonProperty("rawMaterialDetailsUOM")]
        public string RawMaterialDetailsUOM { get; set; }

        [NotMapped]
        [JsonProperty("farmersMaterialIssuedQty")]
        public decimal FarmersMaterialIssuedQty { get; set; }

        [NotMapped]
        [JsonProperty("mifConsumptionVoucherNo")]
        public string MIFConsumptionVoucherNo { get; set; }

        [NotMapped]
        [JsonProperty("returnDate")]
        public DateTime ReturnDate { get; set; }

    }
}