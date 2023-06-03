using GherkinWebAPI.Models.DailyHarvestDetails;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.DailyHarvest
{
    
    public class BulkGreensInsertDTO
    {
        [NotMapped]
        public GreensProcurement greensProcurement { get; set; }

        [NotMapped]
        public List<GreenFarmerQuanityCrateWiseDTO> greenFarmerQuanityCrateWiseDTOs { get; set; }

        [NotMapped]
        public GreensProcurement greensProcurementSave { get; set; }
    }
}