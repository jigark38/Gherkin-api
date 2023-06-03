using GherkinWebAPI.Core.Reports.MonthlyAttendance;
using GherkinWebAPI.DTO.Reports.MonthlyAttendance;
using GherkinWebAPI.Entities;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.AttendanceDetails;
using GherkinWebAPI.Models.Employee;
using GherkinWebAPI.Models.ShiftDetail;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.Reports.MonthlyAttendance
{
    public class MonthlyAttendanceRepository : IMonthlyAttendanceRepository
    {
        private readonly RepositoryContext _context;
        public MonthlyAttendanceRepository(RepositoryContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse<object>> GetMonthlyAttendanceDetails(int orgOfficeNo, string status, string division, string deptCode, string subDeptCode,
            string month, string year, string filter)
        {
             ApiResponse<object> result = new ApiResponse<object>();
            
            try
            {
                object attendanceDetails = "";
                int intmonth = Convert.ToInt32(month) + 1;
                int intyear = Convert.ToInt32(year);

                if (division == "All" && deptCode == "All" && subDeptCode == "All")

                {
                    attendanceDetails = await (from eaud in _context.EmployeeAttendanceUpdatedDetails
                                               join ee in _context.Employees on eaud.EmployeeID equals ee.employeeId
                                               join dId in _context.Departments on ee.departmentCode equals dId.departmentCode
                                               where eaud.OrgOfficeNo == orgOfficeNo
                                                //&& ee.employeeDivision == division
                                                //&& ee.departmentCode == deptCode
                                                //&& eaud.SubDepartmentCode == subDeptCode
                                                && eaud.AttendanceDate.Month == intmonth
                                                && eaud.AttendanceDate.Year == intyear
                                               orderby filter == "Time" ? dId.departMentName : dId.departMentName ascending
                                               orderby ee.empBiometricId ascending
                                               //orderby eaud.AttendanceDate ascending
                                               select new MonthlyAttendanceDto
                                               {
                                                   OrgOfficeNo = eaud.OrgOfficeNo,
                                                   EmployeeID = eaud.EmployeeID,
                                                   AttendanceDate = eaud.AttendanceDate,
                                                   OThours = eaud.OThours,
                                                   EmpBiometricID = ee.empBiometricId,
                                                   EmployeeName = ee.employeeName,
                                                   DepartmentCode = ee.departmentCode,
                                                   SubDepartmentCode = ee.subDepartmentCode,
                                                   EmployeeDivision = ee.employeeDivision,
                                                   EmploymentStatusAsOn = ee.employeeStatusAsOn,
                                               }).ToListAsync();

                }
                if (division == "All" && deptCode != "All" && subDeptCode == "All")

                {
                    attendanceDetails = await (from eaud in _context.EmployeeAttendanceUpdatedDetails
                                               join ee in _context.Employees on eaud.EmployeeID equals ee.employeeId
                                               join dId in _context.Departments on ee.departmentCode equals dId.departmentCode
                                               where eaud.OrgOfficeNo == orgOfficeNo
                                                //&& ee.employeeDivision == division
                                                && ee.departmentCode == deptCode
                                                //&& eaud.SubDepartmentCode == subDeptCode
                                                && eaud.AttendanceDate.Month == intmonth
                                                && eaud.AttendanceDate.Year == intyear
                                               orderby filter == "Time" ? dId.departMentName : dId.departMentName ascending
                                               orderby ee.empBiometricId ascending
                                               //orderby eaud.AttendanceDate ascending
                                               select new MonthlyAttendanceDto
                                               {
                                                   OrgOfficeNo = eaud.OrgOfficeNo,
                                                   EmployeeID = eaud.EmployeeID,
                                                   AttendanceDate = eaud.AttendanceDate,
                                                   OThours = eaud.OThours,
                                                   EmpBiometricID = ee.empBiometricId,
                                                   EmployeeName = ee.employeeName,
                                                   DepartmentCode = ee.departmentCode,
                                                   SubDepartmentCode = ee.subDepartmentCode,
                                                   EmployeeDivision = ee.employeeDivision,
                                                   EmploymentStatusAsOn = ee.employeeStatusAsOn,
                                               }).ToListAsync();
                }

                if (division == "All" && deptCode == "All" && subDeptCode != "All")

                {
                    attendanceDetails = await (from eaud in _context.EmployeeAttendanceUpdatedDetails
                                               join ee in _context.Employees on eaud.EmployeeID equals ee.employeeId
                                               join dId in _context.Departments on ee.departmentCode equals dId.departmentCode
                                               where eaud.OrgOfficeNo == orgOfficeNo
                                                //&& ee.employeeDivision == division
                                                //&& ee.departmentCode == deptCode
                                                && eaud.SubDepartmentCode == subDeptCode
                                                && eaud.AttendanceDate.Month == intmonth
                                                && eaud.AttendanceDate.Year == intyear
                                               orderby filter == "Time" ? dId.departMentName : dId.departMentName ascending
                                               orderby ee.empBiometricId ascending
                                               //orderby eaud.AttendanceDate ascending
                                               select new MonthlyAttendanceDto
                                               {
                                                   OrgOfficeNo = eaud.OrgOfficeNo,
                                                   EmployeeID = eaud.EmployeeID,
                                                   AttendanceDate = eaud.AttendanceDate,
                                                   OThours = eaud.OThours,
                                                   EmpBiometricID = ee.empBiometricId,
                                                   EmployeeName = ee.employeeName,
                                                   DepartmentCode = ee.departmentCode,
                                                   SubDepartmentCode = ee.subDepartmentCode,
                                                   EmployeeDivision = ee.employeeDivision,
                                                   EmploymentStatusAsOn = ee.employeeStatusAsOn,
                                               }).ToListAsync();

                }

                if (division == "All" && deptCode != "All" && subDeptCode != "All")

                {
                    attendanceDetails = await (from eaud in _context.EmployeeAttendanceUpdatedDetails
                                               join ee in _context.Employees on eaud.EmployeeID equals ee.employeeId
                                               join dId in _context.Departments on ee.departmentCode equals dId.departmentCode
                                               where eaud.OrgOfficeNo == orgOfficeNo
                                                //&& ee.employeeDivision == division
                                                && ee.departmentCode == deptCode
                                                && eaud.SubDepartmentCode == subDeptCode
                                                && eaud.AttendanceDate.Month == intmonth
                                                && eaud.AttendanceDate.Year == intyear //&& ee.employeeId == "579"
                                               orderby filter == "Time" ? dId.departMentName : dId.departMentName ascending
                                               orderby ee.empBiometricId ascending
                                               //orderby eaud.AttendanceDate ascending
                                               select new MonthlyAttendanceDto
                                               {
                                                   OrgOfficeNo = eaud.OrgOfficeNo,
                                                   EmployeeID = eaud.EmployeeID,
                                                   AttendanceDate = eaud.AttendanceDate,
                                                   OThours = eaud.OThours,
                                                   EmpBiometricID = ee.empBiometricId,
                                                   EmployeeName = ee.employeeName,
                                                   DepartmentCode = ee.departmentCode,
                                                   SubDepartmentCode = ee.subDepartmentCode,
                                                   EmployeeDivision = ee.employeeDivision,
                                                   EmploymentStatusAsOn = ee.employeeStatusAsOn,
                                               }).ToListAsync();

                }

                if (division != "All" && deptCode == "All" && subDeptCode == "All")

                {
                    attendanceDetails = await (from eaud in _context.EmployeeAttendanceUpdatedDetails
                                               join ee in _context.Employees on eaud.EmployeeID equals ee.employeeId
                                               join dId in _context.Departments on ee.departmentCode equals dId.departmentCode
                                               where eaud.OrgOfficeNo == orgOfficeNo
                                                && ee.employeeDivision == division
                                                //&& ee.departmentCode == deptCode
                                                //&& eaud.SubDepartmentCode == subDeptCode
                                                && eaud.AttendanceDate.Month == intmonth
                                                && eaud.AttendanceDate.Year == intyear //&& ee.employeeId == "388" || ee.employeeId == "389" || ee.employeeId == "443"
                                               //orderby  filter == "Time" ? dId.departMentName : dId.departMentName ascending, ee.empBiometricId ascending
                                               //orderby filter == "Time" ? dId.departMentName : dId.departMentName, ee.empBiometricId
                                               orderby filter == "Time" ? dId.departMentName : dId.departMentName , ee.empBiometricId ascending
                                               orderby ee.empBiometricId ascending
                                               //orderby eaud.AttendanceDate ascending
                                               select new MonthlyAttendanceDto
                                               {
                                                   OrgOfficeNo = eaud.OrgOfficeNo,
                                                   EmployeeID = eaud.EmployeeID,
                                                   AttendanceDate = eaud.AttendanceDate,
                                                   OThours = eaud.OThours,
                                                   EmpBiometricID = ee.empBiometricId,
                                                   EmployeeName = ee.employeeName,
                                                   DepartmentCode = ee.departmentCode,
                                                   SubDepartmentCode = ee.subDepartmentCode,
                                                   EmployeeDivision = ee.employeeDivision,
                                                   EmploymentStatusAsOn = ee.employeeStatusAsOn,
                                              }).ToListAsync();
                    //}).OrderBy(e => e.EmpBiometricID).ToListAsync();

                }

                if (division != "All" && deptCode != "All" && subDeptCode == "All")

                {
                    attendanceDetails = await (from eaud in _context.EmployeeAttendanceUpdatedDetails
                                               join ee in _context.Employees on eaud.EmployeeID equals ee.employeeId
                                               join dId in _context.Departments on ee.departmentCode equals dId.departmentCode
                                               where eaud.OrgOfficeNo == orgOfficeNo
                                                && ee.employeeDivision == division
                                                && ee.departmentCode == deptCode
                                                //&& eaud.SubDepartmentCode == subDeptCode
                                                && eaud.AttendanceDate.Month == intmonth
                                                && eaud.AttendanceDate.Year == intyear
                                               orderby filter == "Time" ? dId.departMentName : dId.departMentName ascending
                                               orderby ee.empBiometricId ascending
                                               //orderby eaud.AttendanceDate ascending
                                               select new MonthlyAttendanceDto
                                               {
                                                   OrgOfficeNo = eaud.OrgOfficeNo,
                                                   EmployeeID = eaud.EmployeeID,
                                                   AttendanceDate = eaud.AttendanceDate,
                                                   OThours = eaud.OThours,
                                                   EmpBiometricID = ee.empBiometricId,
                                                   EmployeeName = ee.employeeName,
                                                   DepartmentCode = ee.departmentCode,
                                                   SubDepartmentCode = ee.subDepartmentCode,
                                                   EmployeeDivision = ee.employeeDivision,
                                                   EmploymentStatusAsOn = ee.employeeStatusAsOn,
                                               }).ToListAsync();

                }

                if (division != "All" && deptCode == "All" && subDeptCode != "All")

                {
                    attendanceDetails = await (from eaud in _context.EmployeeAttendanceUpdatedDetails
                                               join ee in _context.Employees on eaud.EmployeeID equals ee.employeeId
                                               join dId in _context.Departments on ee.departmentCode equals dId.departmentCode
                                               where eaud.OrgOfficeNo == orgOfficeNo
                                                && ee.employeeDivision == division
                                                //&& ee.departmentCode == deptCode
                                                && eaud.SubDepartmentCode == subDeptCode
                                                && eaud.AttendanceDate.Month == intmonth
                                                && eaud.AttendanceDate.Year == intyear
                                               orderby filter == "Time" ? dId.departMentName : dId.departMentName ascending
                                               orderby ee.empBiometricId ascending
                                               //orderby eaud.AttendanceDate ascending
                                               select new MonthlyAttendanceDto
                                               {
                                                   OrgOfficeNo = eaud.OrgOfficeNo,
                                                   EmployeeID = eaud.EmployeeID,
                                                   AttendanceDate = eaud.AttendanceDate,
                                                   OThours = eaud.OThours,
                                                   EmpBiometricID = ee.empBiometricId,
                                                   EmployeeName = ee.employeeName,
                                                   DepartmentCode = ee.departmentCode,
                                                   SubDepartmentCode = ee.subDepartmentCode,
                                                   EmployeeDivision = ee.employeeDivision,
                                                   EmploymentStatusAsOn = ee.employeeStatusAsOn,
                                               }).ToListAsync();

                }
                if (division != "All" && deptCode != "All" && subDeptCode != "All")

                {
                    attendanceDetails = await (from eaud in _context.EmployeeAttendanceUpdatedDetails
                                               join ee in _context.Employees on eaud.EmployeeID equals ee.employeeId
                                               join dId in _context.Departments on ee.departmentCode equals dId.departmentCode
                                               where eaud.OrgOfficeNo == orgOfficeNo
                                                && ee.employeeDivision == division
                                                && ee.departmentCode == deptCode
                                                && eaud.SubDepartmentCode == subDeptCode
                                                && eaud.AttendanceDate.Month == intmonth
                                                && eaud.AttendanceDate.Year == intyear
                                               orderby filter == "Time" ? dId.departMentName : dId.departMentName ascending
                                               orderby ee.empBiometricId ascending
                                               //orderby eaud.AttendanceDate ascending
                                               select new MonthlyAttendanceDto
                                               {
                                                   OrgOfficeNo = eaud.OrgOfficeNo,
                                                   EmployeeID = eaud.EmployeeID,
                                                   AttendanceDate = eaud.AttendanceDate,
                                                   OThours = eaud.OThours,
                                                   EmpBiometricID = ee.empBiometricId,
                                                   EmployeeName = ee.employeeName,
                                                   DepartmentCode = ee.departmentCode,
                                                   SubDepartmentCode = ee.subDepartmentCode,
                                                   EmployeeDivision = ee.employeeDivision,
                                                   EmploymentStatusAsOn = ee.employeeStatusAsOn,
                                               }).ToListAsync();
                    //}).OrderBy(e => e.EmpBiometricID).ToListAsync();

                }
                result.Data = attendanceDetails;
                result.IsSucceed = true;
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}