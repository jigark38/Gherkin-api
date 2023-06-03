using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.BatchProduction
{
    public class BatchProductionDetails
    {
        public BatchScheduleDetails batchScheduleDetails { get; set; }
        public BatchScheduleDummyProduction batchScheduleDummyProduction { get; set; }
        public List<BatchScheduleGreensGRNDetails> batchScheduleGreensGRNDetails { get; set; }

        //public BatchScheduleDrumsBarcodeDetails batchScheduleDrumsBarcodeDetails { get; set; }
        public string status { get; set; }
    }
}