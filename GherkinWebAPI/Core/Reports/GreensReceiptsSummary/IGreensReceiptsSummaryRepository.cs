using GherkinWebAPI.DTO.Reports.GreensReceiptsSummary;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.Reports.GreensReceiptsSummary
{
    public interface IGreensReceiptsSummaryRepository
    {
        Task<List<PlantationSchedule>> GetSeasonFromToDllData();
        Task<List<CropGroup>> GetMaterialGroupDllData();
        Task<List<CropName>> GetMaterialNameFromGroupDllData(string cropGroupCode);
        Task<object> GetReportData(GreensReceiptsSummaryReportDataDto data);
    }
}
