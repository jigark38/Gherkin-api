using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.MediaBatchDetails
{
    public class MediaBatchProductionAndMaterialDetails
    {
        public List<MediaBatchMaterialDetails> mediaMaterialDetails { get; set; }
        public MediaBatchProductionDetails mediaProductionDetails { get; set; }
        public string status { get; set; }
    }
}