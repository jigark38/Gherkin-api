using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Models
{
    public class FarmersInputsIssueRatesMaster
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("FI_Rate_Passing_No")]
        [JsonProperty("fiRatePassingNo")]
        public int FIRatePassingNo { get; set; }

        [Column("FI_Rates_Entry_Date")]
        [JsonProperty("fiRatesEntryDate")]
        public DateTime FIRatesEntryDate { get; set; }

        [Column("FI_Rate_Entered_By_Employee_ID")]
        [JsonProperty("fiRateEnteredByEmpId")]
        public string FIRateEnteredByEmployeeID { get; set; }

        [Column("Crop_Rate_Approved_By_Employee_ID")]
        [JsonProperty("cropRateApprovedByEmpId")]
        public string CropRateApprovedByEmployeeID { get; set; }

        [Column("Crop_Group_Code")]
        [JsonProperty("cropGroupCode")]
        public string CropGroupCode { get; set; }

        [Column("Crop_Name_Code")]
        [JsonProperty("cropNameCode")]
        public string CropNameCode { get; set; }

        [Column("PS_Number")]
        [JsonProperty("psNumber")]
        public string PSNumber { get; set; }

        [Column("Crop_Rate_Effective_Date")]
        [JsonProperty("cropRateEffectiveDate")]
        public DateTime CropRateEffectiveDate { get; set; }

        [Column("Country_Code")]
        [JsonProperty("countryCode")]
        public int CountryCode { get; set; }

        [Column("State_Code")]
        [JsonProperty("stateCode")]
        public int StateCode { get; set; }

    }


    public class FarmersInputsAreaDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("ID")]
        [JsonProperty("iD")]
        public int ID { get; set; }

        [Column("Farmers_Rates_Area_ID")]
        [JsonProperty("farmersRatesAreaID")]
        public int? FarmersRatesAreaID { get; set; }

        [Column("FI_Rate_Passing_No")]
        [JsonProperty("fiRatePassingNo")]
        public int FIRatePassingNo { get; set; }

        [Column("PS_Number")]
        [JsonProperty("psNumber")]
        public string PSNumber { get; set; }

        [Column("Area_ID")]
        [JsonProperty("areaID")]
        public string AreaID { get; set; }

       
        

    }

    public class FarmersInputsMaterialRate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("Material_Rate_ID")]
        [JsonProperty("materialRateID")]
        public int MaterialRateID { get; set; }

        [Column("FI_Rate_Passing_No")]
        [JsonProperty("fiRatePassingNo")]
        public int FIRatePassingNo { get; set; }

        [Column("Raw_Material_Group_Code")]
        [JsonProperty("rawMaterialGroupCode")]
        public string RawMaterialGroupCode { get; set; }

        [Column("Raw_Material_Details_Code")]
        [JsonProperty("rawMaterialDetailsCode")]
        public string RawMaterialDetailsCode { get; set; }

        [Column("Raw_Material_UOM")]
        [JsonProperty("rawMaterialUOM")]
        public string RawMaterialUOM { get; set; }

        [Column("Farmer_Material_Rate")]
        [JsonProperty("farmerMaterialRate")]
        public decimal FarmerMaterialRate { get; set; }
    }


    public class RawMaterial
    {
        public string rawMaterialDetailsCode { get; set; }
        public string rawMaterialGroupCode { get; set; }
        public string rawMaterialGroupName { get; set; }
        public string rawMaterialDetailsName { get; set; }
        public string rawMaterialUOM { get; set; }
    }


    public class FarmersInputRatesSeasonWise
    {
        public int fiRatePassingNo { get; set; }

        public DateTime fiRatesEntryDate { get; set; }

        public string fiRateEnteredByEmployeeID { get; set; }

        public string cropRateApprovedByEmployeeID { get; set; }

        public string cropGroupCode { get; set; }

        public string cropNameCode { get; set; }

        public string psNumber { get; set; }

        public DateTime cropRateEffectiveDate { get; set; }

        public int countryCode { get; set; }

        public int stateCode { get; set; }

        public List<FarmersInputsAreaDetail> FarmersInputsAreaDetails { get; set; }
        public List<FarmersInputsMaterialRate> FarmersInputsMaterialRates { get; set; }

    }



}