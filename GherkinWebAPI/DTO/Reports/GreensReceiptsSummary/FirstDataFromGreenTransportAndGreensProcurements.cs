using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.Reports.GreensReceiptsSummary
{
    public class FirstDataFromGreenTransportAndGreensProcurements
    {
        public string PsNumber { get; set; }
        public DateTime HarvestDate { get; set; }

        public int GreenProcurementNo { get; set; }

        public int GreenTransVehicleDespNo { get; set; }

        public string AreaId { get; set; }

        public string WeightMentmodule { get; set; }

        public DateTime? HarvestEndingTime { get; set; }

        public string EmployeeId { get; set; }
    }
}