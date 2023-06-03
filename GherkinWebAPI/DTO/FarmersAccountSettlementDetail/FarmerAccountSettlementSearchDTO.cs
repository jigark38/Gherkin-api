using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.FarmersAccountSettlementDetail
{
    public class FarmerAccountSettlementSearchDTO
    {
        public int? Unit { get; set; }
        public string CropGroup { get; set; }
        public string CropName { get; set; }
        public string SeasonFromTo { get; set; }
        public string AreaId { get; set; }
        public string FieldStaffId { get; set; }
        public string FarmerName { get; set; }
        public string FarmerAccountNo { get; set; }
        public string FarmerCode { get; set; }
        public string FarmersAgreementCode { get; set; }
        public string PsNumber { get; set; }


    }

    public class FarmerNameAccountVM
    {
        public string farmerName { get; set; }
        public string farmerAccountNo { get; set; }
        public decimal noOfAcres { get; set; }
        public string farmerAgreementCode { get; set; }
        public string farmerCode { get; set; }
        public decimal greensReceived { get; set; }
        public decimal InputsIssued { get; set; }
        public decimal advanceAmount { get; set; }
        public decimal inputReturn { get; set; }
        public decimal payable { get; set; }
    }
    public class GreensReceivingDetailsVM
    {
        public int greensProcurementNo { get; set; }
        public string cropSchemeCode { get; set; }
        public DateTime harvestDate { get; set; }
        public GreensCountVM count { get; set; }
        public decimal rate { get; set; }
        public decimal amount { get; set; }
    }
    public class GreensCountVM
    {
        public int From { get; set; }
        public string Sign { get; set; }
        public decimal Count { get; set; }
    }
    public class SettlementVM
    {
        public string farmerName { get; set; }
        public string farmerAccountNo { get; set; }
        public decimal noOfAcres { get; set; }
        public string farmerAgreementCode { get; set; }
        public string farmerCode { get; set; }
        public decimal greensReceived { get; set; }
        public decimal InputsIssued { get; set; }
        public decimal advanceAmount { get; set; }
        public decimal inputReturn { get; set; }
        public decimal payable { get; set; }
    }

    public class FarmerAdvancesPaidGRID
    {
        public string AreaID { get; set; }
        public int Village_Code { get; set; }
        public string FarmerName { get; set; }
        public string Farmers_Account_No { get; set; }
        public string Farmers_Agreement_Code { get; set; }
        public DateTime Date { get; set; }
        public DateTime Farmers_Agreement_Date { get; set; }
        public string NameOfAccountHolder { get; set; }
        public string IFSC { get; set; }
        public decimal? Amount { get; set; }
        public decimal? CumulativeAmount { get; set; }
        public decimal Farmers_No_of_Acres_Area { get; set; }
        public string Farmer_Code { get; set; }
        public string FarmerAddress { get; set; }
        public string VillageName { get; set; }


    }

    public class FarmerInputReturnGRID
    {
        public int FIM_Return_No { get; set; }
        public string Area_ID { get; set; }
        public DateTime FMIR_Date { get; set; }
        public string Employee_ID { get; set; }
        public string Crop_Group_Code { get; set; }
        public string Crop_Name_Code { get; set; }
        public string PS_Number { get; set; }
        public string Farmer_Code { get; set; }
        public string FIM_Return_Voucher_No { get; set; }
        public string Stock_Return_Status { get; set; }
        public int? Org_office_No { get; set; }
        public int FIM_Return_TR_No { get; set; }
        public string Raw_Material_Group_Code { get; set; }
        public string Raw_Material_Details_Code { get; set; }
        public string Raw_Material_Group_Name { get; set; }
        public string Raw_Material_Details_Name { get; set; }
        public decimal Farmers_Material_Return_Qty { get; set; }
    }


    public class FarmerInputsIssuedGRID
    {

        public DateTime MIF_Date_of_Issue { get; set; }
        public string Employee_ID { get; set; }
        public int MIF_Consumption_No { get; set; }
        public string MIF_Consumption_Voucher_No { get; set; }
        public int MIF_Material_Issue_TR_No { get; set; }
        public string Raw_Material_Group_Code { get; set; }
        public string Raw_Material_Details_Code { get; set; }
        public string Raw_Material_Group_Name { get; set; }
        public string Raw_Material_Details_Name { get; set; }
        public decimal Farmers_Material_Issued_Qty { get; set; }

        public string Area_ID { get; set; }
        public int? FI_Rate_Passing_No { get; set; }
        public int? Farmers_Rates_Area_ID { get; set; }
        public int? Material_Rate_ID { get; set; }
        public string Raw_Material_UOM { get; set; }
        public decimal Farmer_Material_Rate { get; set; }
    }

    public class FarmerAgreementAndSettlementInfo
    {
        public DateTime Farmers_Agreement_Date { get; set; }
        public string Farmers_Agreement_Code { get; set; }
        public string Village_Code { get; set; }
        public string FarmerName { get; set; }
        public string Farmers_Account_No { get; set; }
        public decimal Farmers_No_of_Acres_Area { get; set; }
        public string Farmer_Code { get; set; }

        //public decimal? TotalGreenReceivedQTY { get; set; }
        //public decimal? TotalInputsIssuedQTY { get; set; }
        //public decimal? TotalAdvanceAmount { get; set; }
        //public decimal? TotalInputReturn { get; set; }
        //public decimal? TotalPayable { get; set; }
    }


    public class FarmerGreenReceivingGRID : FarmerAgreementAndSettlementInfo
    {
        public int Greens_Procurement_No { get; set; }
        public int Count_wise_Total_Crates { get; set; }
        public decimal Count_wise_Total_Quantity { get; set; }
        public DateTime Harvest_Date { get; set; }
        public string All_Areas { get; set; } //Crop_Rate_Details
        public string Area_ID { get; set; }
        public string All_Villages { get; set; }
        public string Crop_Scheme_Code { get; set; }
        public DateTime? Crop_Rate_Effective_Date { get; set; }
        public string Crop_Rate_No { get; set; }

        public decimal? Crop_Count_mm { get; set; } //Crop_Effective_Assocation_Rates
        public int? Crop_Scheme_From { get; set; }
        public string Crop_Scheme_Sign { get; set; }
        public decimal? Crop_Rate_As_per_Association { get; set; }
        public string Crop_Rate_Per_UOM { get; set; }

        public decimal Crop_Rate_As_per_Our_Agreement { get; set; } //Farmers_Agreement_Size_Details

        public string CropSchemePattern { get; set; }
        public int CropSchemeId { get; set; }
    }

    public class InputIssue
    {
        public DateTime MIF_Date_of_Issue { get; set; }

        public string Raw_Material_Details_Name { get; set; }

        public decimal Farmers_Material_Issued_Qty { get; set; }

        public decimal Farmer_Material_Rate { get; set; }

        public string Raw_Material_UOM { get; set; }

        public string Farmer_Material_Rate_Raw_Material_UOM { get; set; }

        public decimal Calculation { get; set; }

        public decimal Cumulative { get; set; }
    }

    public class InputReturn
    {
        public DateTime FMIR_Date { get; set; }

        public string Raw_Material_Details_Name { get; set; }

        public decimal Farmers_Material_Return_Qty { get; set; }

        public decimal Farmer_Material_Rate { get; set; }

        public string Raw_Material_UOM { get; set; }

        public string Farmer_Material_Rate_Raw_Material_UOM { get; set; }

        public decimal Calculation { get; set; }

        public decimal Cumulative { get; set; }
    }

}