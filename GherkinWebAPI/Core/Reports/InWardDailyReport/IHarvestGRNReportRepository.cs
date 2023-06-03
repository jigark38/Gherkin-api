using GherkinWebAPI.DTO.Reports.InWardDailyReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.Reports.InWardDailyReport
{
    public interface IHarvestGRNReportRepository
    {
        Task<object> GetReportData(InwardDetailRequest request);
    }
}
