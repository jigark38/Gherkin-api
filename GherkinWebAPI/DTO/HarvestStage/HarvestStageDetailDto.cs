namespace GherkinWebAPI.DTO.HarvestStage
{
    public class HarvestStageDetailDto
    {
        public string ID { get; set; }
        public string HS_Transaction_Code { get; set; }
        public string HS_Crop_Phase_Name { get; set; }
        public string HS_Crop_Phase_Code { get; set; }
        public int HS_Days_After_Sowing_From { get; set; }
        public int HS_Days_After_Sowing_To { get; set; }
        public string HS_Harvest_Details { get; set; }
    }
}