using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Entities.HarvestStage
{
    public class HarvestStageDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string HS_Transaction_Code { get; set; }
        public string HS_Crop_Phase_Name { get; set; }
        [Key]
        public string HS_Crop_Phase_Code { get; set; }
        public int HS_Days_After_Sowing_From { get; set; }
        public int HS_Days_After_Sowing_To { get; set; }
        public string HS_Harvest_Details { get; set; }
    }
}