using GherkinWebAPI.Models.GRNAndMaterialClassification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.GRNAndMaterialClassification
{
    public class GreensRecievedDetailDTO
    {
        public long HarvestGRNNo { get; set; }
        public List<GreensReceivedDetail> greensReceivedDetails {get;set;}
        public List<HarvestGRNCountWeightDetailsDTO> weightDetails { get; set; }
    }
}