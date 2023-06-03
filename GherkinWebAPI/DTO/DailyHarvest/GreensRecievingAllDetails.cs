using GherkinWebAPI.DTO.HarvestDetails;
using GherkinWebAPI.Models.DailyHarvestDetails;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.DailyHarvest
{
   // [NotMapped]
    public class GreensRecievingAllDetails
    {
        [NotMapped]
        public List<BuyerSchedule> buyerSchedules { get; set; }
        [NotMapped]
        public List<GreensProcurement> greensProcurements { get; set; }
        [NotMapped]
        public List<GreensQuantityCratewiseDetailDTO> greensQuantityCratewiseDetails { get; set; }
        [NotMapped]
        public List<GreenFarmerDetailDto> greenFarmerDetails { get; set; }
    }
}