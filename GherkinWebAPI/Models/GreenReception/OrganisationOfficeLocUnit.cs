using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models.GreenReception
{
    public class OrganisationOfficeLocUnit
    {
        public long orgCode { get; set; }
        public long orgOfficeNo { get; set; }
        public string orgOfficeName { get; set; }
    }
}