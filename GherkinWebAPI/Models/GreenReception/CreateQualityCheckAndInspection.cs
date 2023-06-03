using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.GreenReception
{
    public class CreateQualityCheckAndInspection
    {
        public GreenReceptionQualityCheck greenReceptionQualityCheck { get; set; }
        public GreenReceptionQualityDetails greenReceptionQualityDetails { get; set; }
    }
}