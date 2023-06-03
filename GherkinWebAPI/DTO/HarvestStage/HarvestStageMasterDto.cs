using System;

namespace GherkinWebAPI.DTO.HarvestStage
{
    public class HarvestStageMasterDto
    {
        public int ID { get; set; }
        public DateTime HS_Entry_Date { get; set; }
        public string HS_Transaction_Code { get; set; }
        public string HS_Entered_Employee_ID_By { get; set; }
        public string Crop_Group_Code { get; set; }
        public string Crop_Name_Code { get; set; }
        public DateTime HS_Effective_Date { get; set; }
        public string HBOMDivisionFor { get; set; }
    }
}