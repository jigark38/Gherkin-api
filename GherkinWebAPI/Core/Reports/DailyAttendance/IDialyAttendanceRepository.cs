using GherkinWebAPI.DTO.Reports.DialyAttendance;
using GherkinWebAPI.Models.AttendanceDetails;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.Reports.DailyAttendance
{
    public interface IDialyAttendanceRepository
    {
        Task<ApiResponse<object>> GetAttendanceDetails(string date, int orgOfficeNo, string status, string deptCode, string subDeptCode, string shift, string division, string filter, string category, string gender, int biometricId);
        Task<List<Attendance>> GetAttendanceDetailsForView(string date, int orgOfficeNo, string status, string deptCode, string subDeptCode, string shift, string division, string filter, string category, string gender, int biometricId);
    }
}
