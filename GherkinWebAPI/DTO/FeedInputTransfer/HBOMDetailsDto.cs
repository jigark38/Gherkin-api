using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.FeedInputTransfer
{
    public class HBOMDetailsDto
    {
        public List<HBOMDetails> HBOMDetails { get; set; }
        public Nullable<decimal> transferredTillDate { get; set; }
    }

    public class HBOMDetails
    {
        public string hbOMDivisonFor { get; set; }
        public string hbomPracticePerAcrage { get; set; }
        public Nullable<decimal> hbomChemicalVolume { get; set; }
        public string hbomChemicalUOM { get; set; }
        public string hSTransactionCode { get; set; }
        public string hSCropPhaseCode { get; set; }
        public DateTime hbomPracticeEffectiveDate { get; set; }
        public string hbomPracticeNo { get; set; }
        public string rawMaterialGroupCode { get; set; }
        public string rawMaterialDetailsCode { get; set; }
        public string rawMaterialGroup { get; set; }
        public string RawMaterialDetailsName { get; set; }
        public Nullable<decimal> transferredAmount { get; set; }
    }
}