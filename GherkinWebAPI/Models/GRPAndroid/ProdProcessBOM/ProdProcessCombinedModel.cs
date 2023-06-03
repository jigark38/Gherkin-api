using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.GRPAndroid.ProdProcessBOM
{
    public class ProdProcessCombinedModel
    {
        public MediaProcessDetails MediaProcessDetails { get; set; }
        public ProductionStandardBOM ProdStandardBOM { get; set; }
        public List<ProductionProcessMaterialDetails> ProdProcessMaterialDetails { get; set; }
        public string status { get; set; }
    }
}