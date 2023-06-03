using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models.DailyHarvestDetails
{
    public class GreensProcurement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Greens_Procurement_No")]
        [JsonProperty("greensProcurementNo")]
        public int GreensProcurementNo { get; set; }
        [Column("Greens_Trans_Vehicle_Desp_No")]
        [JsonProperty("greensTransVehicleDespNo")]
        public int GreensTransVehicleDespNo { get; set; }
        [Column("Harvest_Date")]
        [JsonProperty("harvestDate")]
        public DateTime HarvestDate { get; set; }
        [Column("Area_ID")]
        [JsonProperty("areaID")]
        public string AreaID { get; set; }
        [Column("Crop_Group_Code")]
        [JsonProperty("cropGroupCode")]
        public string CropGroupCode { get; set; }
        [Column("Crop_Name_Code")]
        [JsonProperty("cropNameCode")]
        public string CropNameCode { get; set; }
        [Column("PS_Number")]
        [JsonProperty("pSNumber")]
        public string PSNumber { get; set; }
        [Column("Buying_Asst1_Employee_ID")]
        [JsonProperty("buyingAsst1EmployeeID")]
        public int? BuyingAsst1EmployeeID { get; set; }
        [Column("Buying_Asst2_Employee_ID")]
        [JsonProperty("buyingAsst2EmployeeID")]
        public int? BuyingAsst2EmployeeID { get; set; }
        [Column("Weighment_Mode")]
        [JsonProperty("weighmentMode")]
        public string WeighmentMode { get; set; }
        [Column("Harvest_Ending_Time")]
        [JsonProperty("harvestEndingTime")]
        public DateTime? HarvestEndingTime { get; set; }
        [Column("Harvest_Ending_KMS")]
        [JsonProperty("harvestEndingKMS")]
        public int? HarvestEndingKMS { get; set; }
        [Column("Harvest_Time_Duration")]
        [JsonProperty("harvestTimeDuration")]
        public decimal? HarvestTimeDuration { get; set; }
        [Column("Haverst_KMS_Total_Reading")]
        [JsonProperty("haverstKMSTotalReading")]
        public int? HaverstKMSTotalReading { get; set; }
        [Column("Harvest_Other_Charges")]
        [JsonProperty("harvestOtherCharges")]
        public int? HarvestOtherCharges { get; set; }
        [Column("Trip_Total_Quantity")]
        [JsonProperty("tripTotalQuantity")]
        public decimal? TripTotalQuantity { get; set; }
        [Column("Trip_Total_Crates")]
        [JsonProperty("tripTotalCrates")]
        public int? TripTotalCrates { get; set; }
        [Column("Vehicle_End_Point")]
        [JsonProperty("vehicleEndPoint")]
        public string VehicleEndPoint { get; set; }
        [Column("Org_office_No")]
        [JsonProperty("orgOfficeNo")]
        public int? OrgofficeNo { get; set; }
        [Column("Location_Area_ID")]
        [JsonProperty("locationAreaID")]
        public string LocationAreaID { get; set; }
        [Column("Receiving_Completed")]
        [JsonProperty("receivingCompleted")]
        public int? ReceivingCompleted { get; set; }
    }
}