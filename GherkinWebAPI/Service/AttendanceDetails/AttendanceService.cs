using GherkinWebAPI.Core.AttendanceDetails;
using GherkinWebAPI.Models.AttendanceDetails;
using GherkinWebAPI.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;
using GherkinWebAPI.Models.Employee;
using GherkinWebAPI.DTO.AttendanceDetail;
using GherkinWebAPI.Utilities;
using GherkinWebAPI.Request.HumanResource;
using GherkinWebAPI.Models.HumanResource;

namespace GherkinWebAPI.Service.AttendanceDetails
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _repository;
        public AttendanceService(IAttendanceRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<Attendance>> GetAttendanceDetail(string date, int orgOfficeNo, string category, string deptCode, string gender, string division, int biometricNo, string filter)
        {
            return await _repository.GetAttendanceDetail(date, orgOfficeNo, category, deptCode, gender, division, biometricNo, filter);
        }

        public async Task<List<Attendance>> GetEmployeeByDivision(string division)
        {
            return await _repository.GetEmployeeByDivision(division);
        }

        public async Task<ApiResponse<List<Attendance>>> GetAttendanceDetail(AttendanceDetailSearchDTO searchModel)
        {
            return await _repository.GetAttendanceDetail(searchModel);
        }

        public async Task<ApiResponse<List<Attendance>>> GetEmployeeById(AttendanceDetailSearchDTO searchModel)
        {
            return await _repository.GetEmployeeById(searchModel);
        }

        public async Task<ApiResponse<bool>> UpdateAttendanceDetail(AttendanceRequestmodel attendanceModel)
        {
            return await _repository.UpdateAttendanceDetail(attendanceModel);
        }

        public async Task<EmployeeAttendanceDetailsWrapper> GetAttendanceDetailsForSalaryCalculation(AttendanceRequestDetails attendanceRequestDetails)
        {
            return await _repository.GetAttendanceDetailsForSalaryCalculation(attendanceRequestDetails);
        }

        public async Task<ApiResponse<bool>> SaveAttendanceSalaryDetail(AttendanceSalaryDetailsRequest obj)
        {
            return await _repository.SaveAttendanceSalaryDetail(obj);
        }
    }
}