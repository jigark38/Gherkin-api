using GherkinWebAPI.DTO.Reports.MonthlyAttendance;
using GherkinWebAPI.Models.AttendanceDetails;
using GherkinWebAPI.Models.Employee;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.Reports.MonthlyAttendance
{
    public interface IMonthlyAttendanceRepository
    {

        Task<ApiResponse<object>> GetMonthlyAttendanceDetails(int orgOfficeNo, string status, string division, string deptCode, string subDeptCode,string month, string year,string filter);
    }
}
