using GherkinWebAPI.Models.HarvestDeatils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.HarvestDetails
{
    public class FarmerDetailsDto
    {
        public string FarmerName { get; set; }

        public string AccountNumber { get; set; }

        public string AggreementCode { get; set; }

        public decimal NoOfAcresArea { get; set; }

        public int VillageCode { get; set; }

        public string Village { get; set; }

        public string FarmerCode { get; set; }
    }
}