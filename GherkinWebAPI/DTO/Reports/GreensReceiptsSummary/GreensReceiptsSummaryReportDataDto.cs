using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.Reports.GreensReceiptsSummary
{
    public class GreensReceiptsSummaryReportDataDto
    {

        public string PsNumber { get; set; }

        public string CropNameCode { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
    }
}