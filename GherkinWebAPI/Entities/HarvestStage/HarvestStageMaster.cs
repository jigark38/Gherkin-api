using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace GherkinWebAPI.Entities.HarvestStage
{
    public class HarvestStageMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime HS_Entry_Date { get; set; }
        [Key]
        public string HS_Transaction_Code { get; set; }
        public string HS_Entered_Employee_ID_By { get; set; }
        public string Crop_Group_Code { get; set; }
        public string Crop_Name_Code { get; set; }
        public DateTime HS_Effective_Date { get; set; }

        [Column("HBOM_Division_For")]
        public string HBOMDivisionFor { get; set; }

    }
}