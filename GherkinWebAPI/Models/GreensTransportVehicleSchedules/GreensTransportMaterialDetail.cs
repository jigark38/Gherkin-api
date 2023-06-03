using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.GreensTransportVehicleSchedules
{
    public class GreensTransportMaterialDetail
    {
        [Key]
        [JsonProperty("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int ID { get; set; }
        [JsonProperty("greensTransVehicleDespNo")]
        [Column("Greens_Trans_Vehicle_Desp_No")]
        public int GreensTransVehicleDespNo { get; set; }
        [JsonProperty("rawMaterialGroupCode")]
        [Column("Raw_Material_Group_Code")]
        public string RawMaterialGroupCode { get; set; }
        [JsonProperty("rawMaterialDetailsCode")]
        [Column("Raw_Material_Details_Code")]
        public string RawMaterialDetailsCode { get; set; }
        [JsonProperty("descDetails")]
        [Column("Desc_Details")]
        public string DescDetails { get; set; }
        [JsonProperty("despQty")]
        [Column("Desp_Qty")]
        public int DespQty { get; set; }

        // Navigation Property
        public GreensTransportVehicleSchedule GreensTransportVehicleSchedule { get; set; }
    }
}