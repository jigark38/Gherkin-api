using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Core.Reports.MonthlyAttendance
{
    public interface IMonthlyAttendanceService
    {
        Task<ApiResponse<object>> GetMonthlyAttendanceDetails(int orgOfficeNo, string status, string division, string deptCode, string subDeptCode, string month, string year, string filter);

    }
}