using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO.Reports.InWardDailyReport
{
    public class InwardDetailRequest
    {
        public DateTime SelectedDate { get; set; }
        public string AreaId { get; set; }
        public int OwnAgent { get; set; } = 1;
    }
}