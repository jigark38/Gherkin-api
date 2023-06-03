using GherkinWebAPI.Models.AttendanceDetails;
using System.Collections.Generic;
using System.Threading.Tasks;
using GherkinWebAPI.Models.Employee;
using GherkinWebAPI.DTO.AttendanceDetail;
using GherkinWebAPI.Utilities;
using GherkinWebAPI.Models.HumanResource;
using GherkinWebAPI.Request.HumanResource;

namespace GherkinWebAPI.Core.AttendanceDetails
{
    public interface IAttendanceRepository
    {

        Task<List<Attendance>> GetEmployeeByDivision(string division);
        Task<List<Attendance>> GetAttendanceDetail(string date, int orgOfficeNo, string category, string deptCode, string gender, string division, int biometricNo, string filter);
        //  Task<Attendance> loadEmployeeDetails(int empId);
        Task<ApiResponse<List<Attendance>>> GetAttendanceDetail(AttendanceDetailSearchDTO searchModel);
        Task<ApiResponse<List<Attendance>>> GetEmployeeById(AttendanceDetailSearchDTO searchModel);
        Task<ApiResponse<bool>> UpdateAttendanceDetail(AttendanceRequestmodel attendanceModel);

        Task<EmployeeAttendanceDetailsWrapper> GetAttendanceDetailsForSalaryCalculation(AttendanceRequestDetails attendanceRequestDetails);

        Task<ApiResponse<bool>> SaveAttendanceSalaryDetail(AttendanceSalaryDetailsRequest obj);
    }
}
