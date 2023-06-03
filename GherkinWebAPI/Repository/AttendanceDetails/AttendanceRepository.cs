using GherkinWebAPI.Core.AttendanceDetails;
using GherkinWebAPI.Models.AttendanceDetails;
using GherkinWebAPI.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System;
using GherkinWebAPI.Models.ShiftDetail;
using GherkinWebAPI.Models.Employee;
using GherkinWebAPI.DTO.AttendanceDetail;
using GherkinWebAPI.Utilities;
using GherkinWebAPI.Models.HumanResource;
using GherkinWebAPI.Request.HumanResource;
using System.Globalization;

namespace GherkinWebAPI.Repository.AttendanceDetails
{
    public class AttendanceRepository : RepositoryBase<BiometricUserLog>, IAttendanceRepository
    {
        private readonly RepositoryContext _context;
        public AttendanceRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Attendance>> GetAttendanceDetail(string date, int orgOfficeNo, string category, string deptCode, string gender,
            string division, int biometricNo, string filter)
        {
            try
            {

                ShiftDetailMaster shiftDetail = null;

                shiftDetail = await _context.ShiftDetailsMaster.FirstOrDefaultAsync();


                DateTime entryDate = DateTime.Parse(date).Date;

                if (category.Equals("Total Missed", StringComparison.InvariantCultureIgnoreCase))
                {

                    var biometricUserLogs = await _context.BiometricUserLogs.Where(a => a.DateOnlyRecord == entryDate).
                       Select(x => x.IndRegID).ToListAsync();



                    var attendanceDetails = await (from e in _context.Employees
                                                   join d in _context.Departments on e.departmentCode equals d.departmentCode
                                                   join de in _context.Designations on e.designationCode equals de.designationCode
                                                   where e.orgOfficeNo == orgOfficeNo
                                                   select new Attendance
                                                   {
                                                       IndRegId = e.empBiometricId,
                                                       EmployeeName = e.employeeName,
                                                       Department = d.departMentName,
                                                       DepartmentCode = d.departmentCode,
                                                       Designation = de.designattionName,
                                                       Division = e.employeeDivision,
                                                       Gender = e.employeeGender
                                                   }).ToListAsync();

                    if (filter == "Time")
                    {
                        attendanceDetails = attendanceDetails.ToList().OrderBy(a => a.DateTimeRecord).ToList();
                    }
                    else
                    {
                        attendanceDetails = attendanceDetails.ToList().OrderBy(a => a.IndRegId).ToList();

                    }

                    if (deptCode != "ALL")
                    {
                        attendanceDetails = attendanceDetails.Where(a => a.DepartmentCode == deptCode).ToList();
                    }

                    if (!string.IsNullOrEmpty(gender) && gender != "ALL")
                    {
                        attendanceDetails = attendanceDetails.Where(a => a.Gender.ToString().ToLower() == gender.ToString().ToLower()).ToList();
                    }

                    if (!string.IsNullOrEmpty(division) && division != "ALL")
                    {
                        attendanceDetails = attendanceDetails.Where(a => a.Division.ToString().ToLower() == division.ToString().ToLower()).ToList();
                    }

                    if (biometricNo != 0)
                    {
                        attendanceDetails = attendanceDetails.Where(a => a.IndRegId.ToString().ToLower() == biometricNo.ToString().ToLower()).ToList();
                    }

                    attendanceDetails = attendanceDetails.Where(x => !biometricUserLogs.Contains(x.IndRegId)).ToList();

                    var list = new List<Attendance>();

                    foreach (var a in attendanceDetails)
                    {
                        var attendanceDetail = attendanceDetails.Where(i => i.IndRegId == a.IndRegId).First();

                        list.Add(new Attendance()
                        {
                            IndRegId = attendanceDetail.IndRegId,
                            EmployeeName = attendanceDetail.EmployeeName,
                            Department = attendanceDetail.Department,
                            Designation = attendanceDetail.Designation
                        });
                    }

                    return list;
                }
                else
                {

                    var attendanceDetails = await (from u in _context.BiometricUserLogs
                                                   join e in _context.Employees on u.IndRegID equals e.empBiometricId
                                                   join d in _context.Departments on e.departmentCode equals d.departmentCode
                                                   join de in _context.Designations on e.designationCode equals de.designationCode
                                                   where u.DateOnlyRecord == entryDate && e.orgOfficeNo == orgOfficeNo
                                                   select new Attendance
                                                   {
                                                       IndRegId = e.empBiometricId,
                                                       EmployeeName = e.employeeName,
                                                       Department = d.departMentName,
                                                       DepartmentCode = d.departmentCode,
                                                       Designation = de.designattionName,
                                                       DateTimeRecord = u.DateTimeRecord,
                                                       DwInOutMode = u.DwInOutMode,
                                                       Division = e.employeeDivision,
                                                       Gender = e.employeeGender
                                                   }).ToListAsync();
                    if (filter == "Time")
                    {
                        attendanceDetails = attendanceDetails.ToList().OrderBy(a => a.DateTimeRecord).ToList();
                    }
                    else
                    {
                        attendanceDetails = attendanceDetails.ToList().OrderBy(a => a.IndRegId).ToList();

                    }
                    if (deptCode != "ALL")
                    {
                        attendanceDetails = attendanceDetails.Where(a => a.DepartmentCode == deptCode).ToList();
                    }

                    if (!string.IsNullOrEmpty(gender) && gender != "ALL")
                    {
                        attendanceDetails = attendanceDetails.Where(a => a.Gender.ToString().ToLower() == gender.ToString().ToLower()).ToList();
                    }

                    if (!string.IsNullOrEmpty(division) && division != "ALL")
                    {
                        attendanceDetails = attendanceDetails.Where(a => a.Division.ToString().ToLower() == division.ToString().ToLower()).ToList();
                    }

                    if (biometricNo != 0)
                    {
                        attendanceDetails = attendanceDetails.Where(a => a.IndRegId.ToString().ToLower() == biometricNo.ToString().ToLower()).ToList();
                    }

                    var groupresult = attendanceDetails.GroupBy(u => u.IndRegId, u => u.DwInOutMode, (key, g) => new { IndRgId = key, list = g.ToList() });

                    string inTime = null;
                    string outTime = null;

                    var list = new List<Attendance>();

                    foreach (var a in groupresult)
                    {

                        foreach (var l in a.list)
                        {
                            if (l == 0)
                            {
                                if (string.IsNullOrEmpty(inTime))
                                {
                                    var attendances = attendanceDetails.Where(i => i.IndRegId == a.IndRgId && i.DwInOutMode == l);

                                    foreach (var at in attendances)
                                    {
                                        var dt = DateTime.Parse(at.DateTimeRecord.ToString());
                                        inTime = string.Concat(inTime, dt.ToString("HH:mm"), ", ");
                                    }
                                }
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(outTime))
                                {
                                    var attendances = attendanceDetails.Where(i => i.IndRegId == a.IndRgId && i.DwInOutMode == l);
                                    foreach (var at in attendances)
                                    {
                                        var dt = DateTime.Parse(at.DateTimeRecord.ToString());
                                        outTime = string.Concat(outTime, dt.ToString("HH:mm"), ", ");
                                    }
                                }
                            }
                        }

                        var attendanceDetail = attendanceDetails.Where(i => i.IndRegId == a.IndRgId).First();
                        string duration = null;
                        string overTime = null;
                        string shiftDuration = calcuateDiffernceOfHours(shiftDetail.ShiftTimeFrom.ToString(@"hh\:mm"), shiftDetail.ShiftTimeTo.ToString(@"hh\:mm"));

                        if (!string.IsNullOrEmpty(inTime) && !string.IsNullOrEmpty(outTime))
                        {
                            string firstTime = inTime.Split(',')[0];
                            string lastTime = outTime.TrimEnd(' ').TrimEnd(',').Split(',')[outTime.TrimEnd(' ').TrimEnd(',').Split(',').Length - 1];
                            duration = calcuateDiffernceOfHours(firstTime, lastTime);
                            if (attendanceDetail.Division == "Worker")
                            {
                                string subtractTime = calcuateAbsoluteDiffernceOfHours(duration, shiftDuration);
                                if (!string.IsNullOrWhiteSpace(duration))
                                {
                                    int otHr = Convert.ToInt32(subtractTime.Split(':')[0]);

                                    if (otHr > 0)
                                    {
                                        overTime = subtractTime;
                                    }
                                };

                            }
                        }

                        list.Add(new Attendance()
                        {
                            IndRegId = attendanceDetail.IndRegId,
                            EmployeeName = attendanceDetail.EmployeeName,
                            Department = attendanceDetail.Department,
                            Designation = attendanceDetail.Designation,
                            InTime = !string.IsNullOrEmpty(inTime) ? inTime.TrimEnd(' ').TrimEnd(',') : null,
                            OutTime = !string.IsNullOrEmpty(outTime) ? outTime.TrimEnd(' ').TrimEnd(',') : null,
                            Duration = duration,
                            overtime = overTime,
                            Gender = attendanceDetail.Gender
                            //Time = !string.IsNullOrEmpty(inTime) && !string.IsNullOrEmpty(outTime) ? string.Join(" / ", inTime.TrimEnd(' ').TrimEnd(','), outTime.TrimEnd(' ').TrimEnd(','))
                            //        : string.Join(" / ", inTime != null ? inTime.TrimEnd(' ').TrimEnd(',') : inTime, outTime != null ? outTime.TrimEnd(' ').TrimEnd(',') : outTime).Trim()
                        });

                        outTime = null;
                        inTime = null;
                    }

                    if (category.Equals("Single Punch", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var removeItems = list.Where(r => !string.IsNullOrEmpty(r.InTime) && !string.IsNullOrEmpty(r.OutTime));
                        return list.Except(removeItems).ToList();
                    }
                    if (category.Equals("Attended", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var removeItems = list.Where(r => string.IsNullOrEmpty(r.InTime) || string.IsNullOrEmpty(r.OutTime));
                        return list.Except(removeItems).ToList();
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string calcuateAbsoluteDiffernceOfHours(string durationTime, string shiftTime)
        {
            int durationHour = Convert.ToInt32(durationTime.Split(':')[0]);
            int durationMinutes = Convert.ToInt32(durationTime.Split(':')[1]);
            int shiftHour = Convert.ToInt32(shiftTime.Split(':')[0]);
            int shiftMinutes = Convert.ToInt32(shiftTime.Split(':')[1]);

            int totalHours = durationHour - shiftHour;
            if (totalHours <= 0)
            {
                return "00:00";
            }
            else
            {
                int diff = durationMinutes - shiftMinutes;
                if (diff > 0)
                {
                    return totalHours.ToString("00") + ":" + diff.ToString("00");
                }
                else
                {
                    if (diff == 0)
                    {
                        return totalHours.ToString("00") + ":" + diff.ToString("00");
                    }
                    totalHours = totalHours - 1;
                    return totalHours.ToString("00") + ":" + (shiftMinutes + durationMinutes).ToString("00");
                }
            }


        }


        private string calcuateDiffernceOfHours(string inTime, string outTime)
        {
            try
            {
                int inHour = Convert.ToInt32(inTime.Split(':')[0]);
                int inMinutes = Convert.ToInt32(inTime.Split(':')[1]);
                int outHour = Convert.ToInt32(outTime.Split(':')[0]);
                int outMinutes = Convert.ToInt32(outTime.Split(':')[1]);

                int totalHours = outHour - inHour;
                if (totalHours > 0 || totalHours == 0)
                {
                    int diff = outMinutes - inMinutes;

                    if (diff > 0)
                    {
                        return totalHours.ToString("00") + ":" + diff.ToString("00");
                    }
                    else if (diff < 0)
                    {
                        if (totalHours == 0)
                        {
                            return null;
                        }
                        return (totalHours - 1).ToString("00") + ":" + (60 + diff).ToString("00");
                    }
                    else
                    {
                        return totalHours.ToString("00") + ":00";
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return null;
        }

        private string calcuateDiffernceOfHoursForAttendance(string durationTime, string shiftTime)
        {
            int durationHour = Convert.ToInt32(durationTime.Split(':')[0]);
            int durationMinutes = Convert.ToInt32(durationTime.Split(':')[1]);
            int shiftHour = Convert.ToInt32(shiftTime.Split(':')[0]);
            int shiftMinutes = Convert.ToInt32(shiftTime.Split(':')[1]);

            int totalHours = durationHour - shiftHour;
            int diff = durationMinutes - shiftMinutes;
            if (diff > 0)
            {
                return totalHours.ToString("00") + ":" + diff.ToString("00");
            }
            else
            {
                if (diff == 0)
                {
                    return totalHours.ToString("00") + ":" + diff.ToString("00");
                }
                totalHours = totalHours - 1;
                return totalHours.ToString("00") + ":" + (shiftMinutes + durationMinutes).ToString("00");
            }

        }

        public async Task<List<Attendance>> GetEmployeeByDivision(string division)
        {
            var attendanceDetails = await (from e in _context.Employees
                                           join bio in _context.BiometricUserLogs on e.empBiometricId equals bio.IndRegID
                                           join de in _context.Departments on e.departmentCode equals de.departmentCode
                                           where e.employeeDivision == division
                                           select new Attendance
                                           {
                                               EmployeeName = e.employeeName,
                                               IndRegId = e.empBiometricId,
                                               DepartmentCode = e.departmentCode,
                                               Department = de.departMentName,
                                               DateTimeRecord = bio.DateTimeRecord,
                                               DwInOutMode = bio.DwInOutMode,
                                               Division = e.employeeDivision,
                                               SubDepartmentCode = e.subDepartmentCode,
                                               EmployeeId = e.employeeId
                                           }).ToListAsync();

            var groupresult = attendanceDetails.GroupBy(u => u.IndRegId, u => u.DwInOutMode, (key, g) => new { IndRgId = key, list = g.ToList() });

            string inTime = null;
            string outTime = null;

            var lstEmployee = new List<Attendance>();

            foreach (var a in groupresult)
            {

                foreach (var l in a.list)
                {
                    if (l == 0)
                    {
                        if (string.IsNullOrEmpty(inTime))
                        {
                            var attendances = attendanceDetails.Where(i => i.IndRegId == a.IndRgId && i.DwInOutMode == l);

                            foreach (var at in attendances)
                            {
                                var dt = DateTime.Parse(at.DateTimeRecord.ToString());
                                inTime = string.Concat(inTime, dt.ToString("HH:mm"), ", ");
                            }
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(outTime))
                        {
                            var attendances = attendanceDetails.Where(i => i.IndRegId == a.IndRgId && i.DwInOutMode == l);
                            foreach (var at in attendances)
                            {
                                var dt = DateTime.Parse(at.DateTimeRecord.ToString());
                                outTime = string.Concat(outTime, dt.ToString("HH:mm"), ", ");
                            }
                        }
                    }
                }

                var attendanceDetail = attendanceDetails.Where(i => i.IndRegId == a.IndRgId).First();
                string duration = null;
                string overTime = null;
                if (!string.IsNullOrEmpty(inTime) && !string.IsNullOrEmpty(outTime))
                {
                    string firstTime = inTime.Split(',')[0];
                    string lastTime = outTime.TrimEnd(' ').TrimEnd(',').Split(',')[outTime.TrimEnd(' ').TrimEnd(',').Split(',').Length - 1];
                    duration = calcuateDiffernceOfHours(firstTime, lastTime);
                }

                lstEmployee.Add(new Attendance()
                {
                    IndRegId = attendanceDetail.IndRegId,
                    EmployeeName = attendanceDetail.EmployeeName,
                    DepartmentCode = attendanceDetail.DepartmentCode,
                    Department = attendanceDetail.Department,
                    InTime = !string.IsNullOrEmpty(inTime) ? inTime.Split(',')[0] : null,
                    OutTime = !string.IsNullOrEmpty(outTime) ? outTime.TrimEnd(' ').TrimEnd(',').Split(',')[outTime.TrimEnd(' ').TrimEnd(',').Split(',').Length - 1] : null,
                    Duration = duration,
                    overtime = overTime,
                    SubDepartmentCode = attendanceDetail.SubDepartmentCode,
                    EmployeeId = attendanceDetail.EmployeeId
                });

                outTime = null;
                inTime = null;
            }
            lstEmployee.OrderBy(o => o.EmployeeId);

            return lstEmployee;
        }

        public async Task<ApiResponse<List<Attendance>>> GetAttendanceDetail(AttendanceDetailSearchDTO searchModel)
        {
            ApiResponse<List<Attendance>> result = new ApiResponse<List<Attendance>>();
            try
            {
                var query = await (from e in _context.Employees
                                   join b in _context.BiometricUserLogs on e.empBiometricId equals b.IndRegID into a
                                   from bio in a.DefaultIfEmpty()
                                   join de in _context.Departments on e.departmentCode equals de.departmentCode
                                   join subd in _context.SubDepartments on e.subDepartmentCode equals subd.subDepartmentCode
                                   where e.orgOfficeNo == searchModel.orgOfficeNo && e.employeeDivision == searchModel.division
                                   select new Attendance
                                   {
                                       EmployeeName = e.employeeName,
                                       IndRegId = e.empBiometricId,
                                       DepartmentCode = e.departmentCode,
                                       Department = de.departMentName,
                                       DateTimeRecord = bio.DateTimeRecord,
                                       DwInOutMode = bio.DwInOutMode,
                                       Division = e.employeeDivision,
                                       SubDepartmentCode = e.subDepartmentCode,
                                       EmployeeId = e.employeeId,
                                       Gender = e.employeeGender,
                                       SubDepartment = subd.subDepartmentName,
                                   }).ToListAsync();

                if (searchModel.date != null && searchModel.date != DateTime.MinValue)
                {
                    query = query.Where(x => x.DateTimeRecord == null || (x.DateTimeRecord.Value.Day == searchModel.date.Day
                       && x.DateTimeRecord.Value.Month == searchModel.date.Month
                       && x.DateTimeRecord.Value.Year == searchModel.date.Year)).ToList();
                }

                if (searchModel.deptCode != null && searchModel.deptCode != "" && searchModel.deptCode != "ALL")
                {
                    query = query.Where(x => x.DepartmentCode == searchModel.deptCode).ToList();
                }

                if (searchModel.subDeptCode != null && searchModel.subDeptCode != "")
                {
                    query = query.Where(x => x.SubDepartmentCode == searchModel.subDeptCode).ToList();
                }

                var attendanceDetails = (from r in query select r).ToList();


                var groupresult = attendanceDetails.GroupBy(u => u.IndRegId, u => u.DwInOutMode, (key, g) => new { IndRgId = key, list = g.ToList() });

                string inTime = null;
                string outTime = null;
                TimeSpan shiftDuration = new TimeSpan();
                var shiftDetailsMaster = await _context.ShiftDetailsMaster.FirstOrDefaultAsync();
                if (shiftDetailsMaster != null)
                {
                    shiftDuration = shiftDetailsMaster.ShiftDuration;
                }
                var lstEmployee = new List<Attendance>();

                foreach (var a in groupresult)
                {

                    foreach (var l in a.list)
                    {
                        if (l == 0)
                        {
                            if (string.IsNullOrEmpty(inTime))
                            {
                                var attendances = attendanceDetails.Where(i => i.IndRegId == a.IndRgId && i.DwInOutMode == l
                                && i.DateTimeRecord.Value.Date == searchModel.date.Date
                                && i.DateTimeRecord.Value.Year == searchModel.date.Year
                                && i.DateTimeRecord.Value.Month == searchModel.date.Month);

                                var ordredList = attendances.OrderBy(x => x.DateTimeRecord).ToList();

                                foreach (var at in ordredList)
                                {
                                    var dt = DateTime.Parse(at.DateTimeRecord.ToString());
                                    inTime = string.Concat(inTime, dt.ToString("HH:mm"), ", ");
                                }
                            }
                        }
                        else if (l == 1)
                        {
                            if (string.IsNullOrEmpty(outTime))
                            {
                                var attendances = attendanceDetails.Where(i => i.IndRegId == a.IndRgId && i.DwInOutMode == l
                                && i.DateTimeRecord.Value.Date == searchModel.date.Date
                                && i.DateTimeRecord.Value.Year == searchModel.date.Year
                                && i.DateTimeRecord.Value.Month == searchModel.date.Month);
                                var ordredList = attendances.OrderBy(x => x.DateTimeRecord).ToList();
                                foreach (var at in ordredList)
                                {
                                    var dt = DateTime.Parse(at.DateTimeRecord.ToString());
                                    outTime = string.Concat(outTime, dt.ToString("HH:mm"), ", ");
                                }
                            }
                        }
                    }

                    var attendanceDetail = attendanceDetails.Where(i => i.IndRegId == a.IndRgId).First();
                    string duration = null;
                    string overTime = null;
                    if (!string.IsNullOrEmpty(inTime) && !string.IsNullOrEmpty(outTime))
                    {
                        string firstTime = inTime.Split(',')[0];
                        string lastTime = outTime.TrimEnd(' ').TrimEnd(',').Split(',')[outTime.TrimEnd(' ').TrimEnd(',').Split(',').Length - 1];
                        duration = calcuateDiffernceOfHours(firstTime, lastTime);
                    }

                    //OverTime
                    if (!string.IsNullOrWhiteSpace(duration))
                    {
                        overTime = calcuateDiffernceOfHoursForAttendance(duration, shiftDuration.ToString(@"hh\:mm"));
                    }

                    lstEmployee.Add(new Attendance()
                    {
                        IndRegId = attendanceDetail.IndRegId,
                        EmployeeName = attendanceDetail.EmployeeName,
                        DepartmentCode = attendanceDetail.DepartmentCode,
                        Department = attendanceDetail.Department,
                        InTime = !string.IsNullOrEmpty(inTime) ? inTime.Split(',')[0] : null,
                        OutTime = !string.IsNullOrEmpty(outTime) ? outTime.TrimEnd(' ').TrimEnd(',').Split(',')[outTime.TrimEnd(' ').TrimEnd(',').Split(',').Length - 1] : null,
                        Duration = duration,
                        overtime = !string.IsNullOrEmpty(overTime) && overTime.Contains("-") ? string.Empty : overTime,
                        SubDepartmentCode = attendanceDetail.SubDepartmentCode,
                        SubDepartment = attendanceDetail.SubDepartment,
                        EmployeeId = attendanceDetail.EmployeeId,
                        Gender = attendanceDetail.Gender,
                        ShiftDuration = shiftDuration,
                        DateTimeRecord = Convert.ToDateTime(searchModel.date.ToString("d"))
                    });

                    outTime = null;
                    inTime = null;
                }
                lstEmployee.OrderBy(o => o.IndRegId);

                if (lstEmployee.Any())
                {
                    var employeeAttendanceUpdatedDetails = await _context.EmployeeAttendanceUpdatedDetails.Where(_ => _.AttendanceDate == searchModel.date).ToListAsync();
                    if (employeeAttendanceUpdatedDetails.Any())
                    {
                        foreach (var item in employeeAttendanceUpdatedDetails)
                        {
                            var data = lstEmployee.FirstOrDefault(_ => _.EmployeeId == item.EmployeeID);
                            if (data != null)
                            {
                                lstEmployee.Remove(data);
                            }
                        }
                    }
                }

                result.IsSucceed = true;
                result.Data = lstEmployee.ToList();
            }
            catch (Exception ex)
            {
                result.IsSucceed = false;
                result.Exception = ex;
                result.ErrorMessages = new List<string>();
                result.ErrorMessages.Add(ex.Message);
            }
            return result;
        }

        public async Task<ApiResponse<List<Attendance>>> GetEmployeeById(AttendanceDetailSearchDTO searchModel)
        {
            ApiResponse<List<Attendance>> result = new ApiResponse<List<Attendance>>();
            try
            {
                var attendanceDetails = await (from e in _context.Employees
                                               join b in _context.BiometricUserLogs on e.empBiometricId equals b.IndRegID into y
                                               from bio in y.DefaultIfEmpty()
                                               join de in _context.Departments on e.departmentCode equals de.departmentCode
                                               join subd in _context.SubDepartments on e.subDepartmentCode equals subd.subDepartmentCode
                                               where e.empBiometricId == searchModel.biometricNo
                                                || (bio.DateTimeRecord.Value.Day == searchModel.date.Day
                                                && bio.DateTimeRecord.Value.Month == searchModel.date.Month
                                                && bio.DateTimeRecord.Value.Year == searchModel.date.Year)
                                               select new Attendance
                                               {
                                                   EmployeeName = e.employeeName,
                                                   IndRegId = e.empBiometricId,
                                                   DepartmentCode = e.departmentCode,
                                                   Department = de.departMentName,
                                                   DateTimeRecord = bio.DateTimeRecord,
                                                   DwInOutMode = bio.DwInOutMode,
                                                   Division = e.employeeDivision,
                                                   SubDepartmentCode = e.subDepartmentCode,
                                                   SubDepartment = subd.subDepartmentName,
                                                   EmployeeId = e.employeeId,
                                                   Gender = e.employeeGender,
                                               }).ToListAsync();

                var groupresult = attendanceDetails.GroupBy(u => u.IndRegId, u => u.DwInOutMode, (key, g) => new { IndRgId = key, list = g.ToList() });

                string inTime = null;
                string outTime = null;

                var lstEmployee = new List<Attendance>();

                foreach (var a in groupresult)
                {

                    foreach (var l in a.list)
                    {
                        if (l == 0)
                        {
                            if (string.IsNullOrEmpty(inTime))
                            {
                                var attendances = attendanceDetails.Where(i => i.IndRegId == a.IndRgId && i.DwInOutMode == l);
                                var ordredList = attendances.OrderBy(x => x.DateTimeRecord).ToList();
                                foreach (var at in ordredList)
                                {
                                    var dt = DateTime.Parse(at.DateTimeRecord.ToString());
                                    inTime = string.Concat(inTime, dt.ToString("HH:mm"), ", ");
                                }
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(outTime))
                            {
                                var attendances = attendanceDetails.Where(i => i.IndRegId == a.IndRgId && i.DwInOutMode == l);
                                var ordredList = attendances.OrderBy(x => x.DateTimeRecord).ToList();
                                foreach (var at in ordredList)
                                {
                                    var dt = DateTime.Parse(at.DateTimeRecord.ToString());
                                    outTime = string.Concat(outTime, dt.ToString("HH:mm"), ", ");
                                }
                            }
                        }
                    }

                    var attendanceDetail = attendanceDetails.Where(i => i.IndRegId == a.IndRgId).First();
                    string duration = null;
                    string overTime = null;
                    if (!string.IsNullOrEmpty(inTime) && !string.IsNullOrEmpty(outTime))
                    {
                        string firstTime = inTime.Split(',')[0];
                        string lastTime = outTime.TrimEnd(' ').TrimEnd(',').Split(',')[outTime.TrimEnd(' ').TrimEnd(',').Split(',').Length - 1];
                        duration = calcuateDiffernceOfHours(firstTime, lastTime);
                    }

                    lstEmployee.Add(new Attendance()
                    {
                        IndRegId = attendanceDetail.IndRegId,
                        EmployeeName = attendanceDetail.EmployeeName,
                        DepartmentCode = attendanceDetail.DepartmentCode,
                        Department = attendanceDetail.Department,
                        SubDepartment = attendanceDetail.SubDepartment,
                        InTime = !string.IsNullOrEmpty(inTime) ? inTime.Split(',')[0] : null,
                        OutTime = !string.IsNullOrEmpty(outTime) ? outTime.TrimEnd(' ').TrimEnd(',').Split(',')[outTime.TrimEnd(' ').TrimEnd(',').Split(',').Length - 1] : null,
                        Duration = duration,
                        overtime = overTime,
                        SubDepartmentCode = attendanceDetail.SubDepartmentCode,
                        EmployeeId = attendanceDetail.EmployeeId,
                        Gender = attendanceDetail.Gender,
                    });

                    outTime = null;
                    inTime = null;
                }
                lstEmployee = lstEmployee.Where(x => x.IndRegId == searchModel.biometricNo).ToList();
                lstEmployee.OrderBy(o => o.IndRegId);

                result.IsSucceed = true;
                result.Data = lstEmployee;
            }
            catch (Exception ex)
            {
                result.IsSucceed = false;
                result.Exception = ex;
                result.ErrorMessages = new List<string>();
                result.ErrorMessages.Add(ex.Message);
            }
            return result;

        }

        public async Task<ApiResponse<bool>> UpdateAttendanceDetail(AttendanceRequestmodel attendanceModel)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                foreach (var data in attendanceModel.Attendances)
                {
                    var inTimeData = Convert.ToDateTime(attendanceModel.Attnd_Updated_Date.ToString("d"));
                    if (!string.IsNullOrEmpty(data.InTime))
                    {
                        var split = data.InTime.Split(':');
                        inTimeData = inTimeData.AddHours(Convert.ToDouble(split[0]));
                        inTimeData = inTimeData.AddMinutes(Convert.ToDouble(split[1]));
                    }

                    var outTimeData = Convert.ToDateTime(attendanceModel.Attnd_Updated_Date.ToString("d"));
                    if (!string.IsNullOrEmpty(data.OutTime))
                    {
                        var split = data.OutTime.Split(':');
                        outTimeData = outTimeData.AddHours(Convert.ToInt32(split[0]));
                        outTimeData = outTimeData.AddMinutes(Convert.ToInt32(split[1]));
                    }

                    var employeeAttendanceUpdatedDetails = new EmployeeAttendanceUpdatedDetails
                    {
                        OrgOfficeNo = attendanceModel.Org_office_No,
                        AttendanceDate = attendanceModel.Attendance_Date,
                        EntryUpdatedbyEmployeeID = attendanceModel.Entry_Updated_by_Employee_ID,
                        AttndUpdatedDate = attendanceModel.Attnd_Updated_Date,
                        EmployeeID = data.EmployeeId,
                        SubDepartmentCode = data.SubDepartmentCode,
                        AttndInTime_Updated = inTimeData,
                        AttndInOutUpdated = outTimeData,
                        Duration = !string.IsNullOrEmpty(data.Duration) ? (decimal)TimeSpan.Parse(data.Duration).TotalHours : 0M,
                        OThours = !string.IsNullOrEmpty(data.overtime) ? (decimal)TimeSpan.Parse(data.overtime).TotalHours : 0M
                    };

                    _context.EmployeeAttendanceUpdatedDetails.Add(employeeAttendanceUpdatedDetails);
                }

                await _context.SaveChangesAsync();
                UpdateBiometricUserLog(attendanceModel);
                result.IsSucceed = true;
            }
            catch (Exception ex)
            {
                result.IsSucceed = false;
                result.Exception = ex;
                result.ErrorMessages = new List<string>();
                result.ErrorMessages.Add(ex.Message);
            }
            return result;
        }

        public void UpdateBiometricUserLog(AttendanceRequestmodel request)
        {
            foreach (var attendanceModel in request.Attendances)
            {
                if (attendanceModel.IndRegId != 0)
                {
                    var bioEmpList = (from b in _context.BiometricUserLogs
                                      where b.IndRegID == attendanceModel.IndRegId
                                      select new BiometricLogDTO
                                      {
                                          ID = b.ID,
                                          MachineNumber = b.MachineNumber,
                                          IndRegID = b.IndRegID,
                                          DateTimeRecord = b.DateTimeRecord,
                                          TimeOnlyRecord = b.TimeOnlyRecord,
                                          DwInOutMode = b.DwInOutMode,
                                          IsBackedUp = b.IsBackedUp,
                                      }).ToList();

                    if (attendanceModel.DateTimeRecord != null && attendanceModel.DateTimeRecord != DateTime.MinValue)
                    {
                        bioEmpList = bioEmpList.Where(x => x.DateTimeRecord.Value.Day == attendanceModel.DateTimeRecord.Value.Day
                           && x.DateTimeRecord.Value.Month == attendanceModel.DateTimeRecord.Value.Month
                           && x.DateTimeRecord.Value.Year == attendanceModel.DateTimeRecord.Value.Year).ToList();
                    }
                    if (bioEmpList.Any(x => x.DwInOutMode == 0))
                    {
                        var firstDate = bioEmpList.Where(d => d.DwInOutMode == 0).OrderBy(o => o.DateOnlyRecord).FirstOrDefault();
                        string[] tim = attendanceModel.InTime.Split(':');
                        TimeSpan ts = new TimeSpan(Convert.ToInt16(tim[0]), Convert.ToInt16(tim[1]), 0);
                        firstDate.DateTimeRecord = attendanceModel.DateTimeRecord.Value.Date + ts;
                        firstDate.TimeOnlyRecord = attendanceModel.DateTimeRecord.Value.Date + ts;
                        firstDate.DateOnlyRecord = attendanceModel.DateTimeRecord.Value.Date;
                        var tempBio = _context.BiometricUserLogs.Where(a => a.ID == firstDate.ID).FirstOrDefault();
                        tempBio.TimeOnlyRecord = firstDate.TimeOnlyRecord;
                        tempBio.DateTimeRecord = firstDate.DateTimeRecord;
                        tempBio.DateOnlyRecord = firstDate.DateOnlyRecord.Value.Date;
                    }
                    else
                    {
                        string[] tim = attendanceModel.InTime.Split(':');
                        TimeSpan ts = new TimeSpan(Convert.ToInt16(tim[0]), Convert.ToInt16(tim[1]), 0);
                        var bio = new BiometricUserLog()
                        {
                            IndRegID = attendanceModel.IndRegId,
                            DateTimeRecord = attendanceModel.DateTimeRecord.Value.Date + ts,
                            DateOnlyRecord = attendanceModel.DateTimeRecord.Value.Date.Date,
                            TimeOnlyRecord = attendanceModel.DateTimeRecord.Value.Date + ts,
                            DwInOutMode = 0,
                            IsBackedUp = 0,
                        };
                        _context.BiometricUserLogs.Add(bio);
                    }

                    if (bioEmpList.Any(x => x.DwInOutMode == 1))
                    {
                        var lasttime = bioEmpList.Where(d => d.DwInOutMode == 1).OrderByDescending(o => o.DateOnlyRecord).FirstOrDefault();
                        string[] tim = attendanceModel.OutTime.Split(':');
                        TimeSpan ts = new TimeSpan(Convert.ToInt16(tim[0]), Convert.ToInt16(tim[1]), 0);
                        lasttime.DateTimeRecord = attendanceModel.DateTimeRecord.Value.Date + ts;
                        lasttime.TimeOnlyRecord = attendanceModel.DateTimeRecord.Value.Date + ts;
                        lasttime.DateOnlyRecord = attendanceModel.DateTimeRecord.Value.Date;
                        var tempBio1 = _context.BiometricUserLogs.Where(a => a.ID == lasttime.ID).FirstOrDefault();
                        tempBio1.TimeOnlyRecord = lasttime.TimeOnlyRecord;
                        tempBio1.DateTimeRecord = lasttime.DateTimeRecord;
                        tempBio1.DateOnlyRecord = lasttime.DateOnlyRecord.Value.Date;
                    }
                    else
                    {
                        string[] tim = attendanceModel.OutTime.Split(':');
                        TimeSpan ts = new TimeSpan(Convert.ToInt16(tim[0]), Convert.ToInt16(tim[1]), 0);
                        var bio = new BiometricUserLog()
                        {
                            IndRegID = attendanceModel.IndRegId,
                            DateTimeRecord = attendanceModel.DateTimeRecord.Value.Date + ts,
                            DateOnlyRecord = attendanceModel.DateTimeRecord.Value.Date.Date,
                            TimeOnlyRecord = attendanceModel.DateTimeRecord.Value.Date + ts,
                            DwInOutMode = 1,
                            IsBackedUp = 0,

                        };
                        _context.BiometricUserLogs.Add(bio);
                    }

                    if (attendanceModel.DepartmentCode != null || attendanceModel.SubDepartmentCode != null)
                    {
                        var emp = _context.Employees.Where(x => x.empBiometricId == attendanceModel.IndRegId).FirstOrDefault();
                        emp.departmentCode = attendanceModel.DepartmentCode;
                        emp.subDepartmentCode = attendanceModel.SubDepartmentCode;
                    }
                }
            }

            _context.SaveChanges();
        }

        private int GetSundayofSelectedMonth(int Month)
        {
            int count = 0;
            try
            {


                //First We find out last date of mont
                DateTime today = DateTime.Today;
                DateTime endOfMonth = new DateTime(today.Year, Month, DateTime.DaysInMonth(today.Year, Month));
                //get only last day of month
                int day = endOfMonth.Day;

                DateTime now = DateTime.Now;


                for (int i = 0; i < day; ++i)
                {
                    DateTime d = new DateTime(now.Year, Month, i + 1);
                    //Compare date with sunday
                    if (d.DayOfWeek == DayOfWeek.Sunday)
                    {
                        count = count + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return count;
        }

        private string GetLastDayOfAMonth(int Month)
        {
            var lastDayOfMonth = DateTime.DaysInMonth(DateTime.Now.Year, Month);
            string month = "";
            if (Month <= 9)
            {
                month = "0" + Month.ToString();
            }
            else
            {
                month = Month.ToString();
            }
            return (lastDayOfMonth.ToString() + "-" + month + "-" + DateTime.Now.Year.ToString());
        }


        public async Task<EmployeeAttendanceDetailsWrapper> GetAttendanceDetailsForSalaryCalculation(AttendanceRequestDetails attendanceRequestDetails)
        {
            EmployeeAttendanceDetailsWrapper objResult = new EmployeeAttendanceDetailsWrapper();
            try
            {
                var shiftDetails = await (_context.ShiftDetailsMaster.FirstOrDefaultAsync());
                int shiftValue = Convert.ToInt32(shiftDetails.ShiftDuration.ToString(@"hh\:mm").Split(':')[0]);
                shiftValue = shiftValue - 1;
                int SundaysAdded = 0;
                string lastDayofMonth = GetLastDayOfAMonth(attendanceRequestDetails.Attendance_Date);
                string lastDayofLastMonth = "";
                if (attendanceRequestDetails.Attendance_Date == 1)
                {

                }
                else
                {
                    lastDayofLastMonth = GetLastDayOfAMonth(attendanceRequestDetails.Attendance_Date - 1);
                }

                if (attendanceRequestDetails.EmploymentType == "Permanent")
                {
                    SundaysAdded = GetSundayofSelectedMonth(attendanceRequestDetails.Attendance_Date);
                }
                DateTime lastDay = DateTime.ParseExact(lastDayofMonth.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var loanDetails = (from loan in _context.LoansAdvancesDetails
                                   join monthlySal in _context.MonthlyEmployeesSalariesFinalizations
                                   on new { p = loan.EmployeeID, q = loan.OrgofficeNo } equals new { p = monthlySal.Employee_ID, q = monthlySal.Org_office_No }
                                   where loan.LAApprovedDate <= lastDay
                                   group new { loan, monthlySal } by new
                                   {
                                       loan.EmployeeID,
                                       monthlySal.Employee_ID,
                                       loan.OrgofficeNo,
                                       monthlySal.Org_office_No
                                   }
                                   into grp
                                   let laDeductedTillDate = grp.Sum(m => m.loan.LA_Deducted_Till_Date)
                                   let sumLoanDeductionAmount = grp.Sum(m => m.monthlySal.Loan_Deduction_Amount)
                                   let sumLoanApprovedAmount = grp.Sum(m => m.loan.LAApprovedAmount)
                                   let sumMonthlyLoanDeduction = grp.Sum(m => m.loan.LAMonthlyDeduction)
                                   let officeNo = grp.Key.OrgofficeNo
                                   select new
                                   {
                                       LoanAmount = (sumLoanDeductionAmount + laDeductedTillDate < sumLoanApprovedAmount) ? sumMonthlyLoanDeduction : 0,
                                       EmployeeID = grp.Key.EmployeeID,
                                       OfficeNo = officeNo
                                   }
                                  ).ToList();



                var attendancedetailssummary = (from attendedSummarry in _context.AttendanceSummaryDetails
                                                where
                                                attendedSummarry.Month_Year == (lastDayofLastMonth == "" ? null : lastDayofLastMonth)

                                                select attendedSummarry
                                               ).ToList();

                var employeeAttendanceDetail = await (from emp in _context.Employees
                                                      join empAttnDet in _context.EmployeeAttendanceUpdatedDetails
                                                      on new { p = emp.employeeId, q = emp.orgOfficeNo } equals new { p = empAttnDet.EmployeeID, q = (int?)empAttnDet.OrgOfficeNo }
                                                      join dept in _context.Departments on emp.departmentCode equals dept.departmentCode
                                                      join desg in _context.Designations on emp.designationCode equals desg.designationCode
                                                      join subdept in _context.SubDepartments on emp.subDepartmentCode equals subdept.subDepartmentCode
                                                      join empPayment in _context.EmployeePayments on emp.employeeId equals empPayment.employeeId


                                                      where
                                                     emp.orgOfficeNo == attendanceRequestDetails.Org_office_No
                                                     &&
                                                     empAttnDet.AttendanceDate.Month == attendanceRequestDetails.Attendance_Date
                                                     &&
                                                     emp.departmentCode == (attendanceRequestDetails.Department == "All" ? emp.departmentCode : attendanceRequestDetails.Department)
                                                     &&
                                                     emp.subDepartmentCode == (attendanceRequestDetails.Division == "" ? emp.subDepartmentCode : attendanceRequestDetails.Division)
                                                     &&
                                                     !(_context.MonthlyEmployeesSalariesFinalizations.Any(entry => (entry.Org_office_No == emp.orgOfficeNo && entry.Employee_ID == emp.employeeId)))
                                                      select new EmployeeAttendanceDetails
                                                      {
                                                          EmployeeIDString = emp.employeeId,
                                                          EmployeeName = emp.employeeName,
                                                          EmploymentStatusAsOn = emp.employeeStatusAsOn,
                                                          ContactorCode = emp.contractorCode,
                                                          Department = dept.departMentName,
                                                          Division = subdept.subDepartmentName,
                                                          EmployeeNameDesignation = String.Concat(emp.employeeName, "/", desg.designattionName),
                                                          EmployeeID = emp.empBiometricId,
                                                          LeavesBF = 0,
                                                          DaysAttnd = 0,
                                                          DaysConsider = 0,
                                                          GrossSalary = empPayment.employeeGrossSalary,
                                                          EmployeePaymentCategory = empPayment.employeePaymentCategory,
                                                          EmployeeBasicSalary = empPayment.employeeBasicSalary,
                                                          EmployeeDA = empPayment.employeeDA,
                                                          EmployeeCA = empPayment.employeeCA,
                                                          PerDaySalary =
                                                          (
                                                              empPayment.employeePaymentCategory == "Monthly" ? empPayment.employeeGrossSalary / 30 :
                                                              empPayment.employeePaymentCategory == "Daily" ? empPayment.employeeGrossSalary : 0
                                                          ),
                                                          AttendanceDaysCount = 0,
                                                          NoOfDaysCarryForward = 0,//attnSummary.No_of_Days_Carry_Forward,

                                                          PFDed = 0,
                                                          ESIDed = 0,
                                                          PTDed = 0,
                                                          Loans = 0,
                                                          Canteen = 0,
                                                          Others = 0,
                                                          TDS = 0,
                                                          NetPayable = 0,
                                                          Othours = empAttnDet.OThours,
                                                          NoOfDaysAddedAfterCalulation = 0,
                                                          Duration = empAttnDet.Duration,
                                                          NoOfDaysWorked = 0,
                                                          SundayAdded = SundaysAdded,
                                                          PerHourSalary = 0,
                                                          MonthlySalary = 0,
                                                          OTPay = 0,
                                                          Selected = false


                                                      }).ToListAsync();



                var results = (from empDet in employeeAttendanceDetail
                               group empDet by new
                               {
                                   Department = empDet.Department,
                                   Division = empDet.Division,
                                   EmployeeNameDesignation = empDet.EmployeeNameDesignation,
                                   EmployeeID = empDet.EmployeeID,
                                   LeavesBF = empDet.LeavesBF,
                                   DaysAttnd = empDet.DaysAttnd,
                                   DaysConsider = empDet.DaysConsider,
                                   GrossSalary = empDet.GrossSalary,
                                   EmployeePaymentCategory = empDet.EmployeePaymentCategory,
                                   EmployeeBasicSalary = empDet.EmployeeBasicSalary,
                                   EmployeeDA = empDet.EmployeeDA,
                                   EmployeeCA = empDet.EmployeeCA,
                                   PerDaySalary = empDet.PerDaySalary,
                                   AttendanceDaysCount = empDet.AttendanceDaysCount,
                                   NoOfDaysCarryForward = empDet.NoOfDaysCarryForward,
                                   PFDed = empDet.PFDed,
                                   ESIDed = empDet.ESIDed,
                                   PTDed = empDet.PTDed,
                                   Loans = empDet.Loans,
                                   Canteen = empDet.Canteen,
                                   Others = empDet.Others,
                                   TDS = empDet.TDS,
                                   NetPayable = empDet.NetPayable,
                                   NoOfDaysAddedAfterCalulation = empDet.NoOfDaysAddedAfterCalulation,
                                   NoOfDaysWorked = empDet.NoOfDaysWorked,
                                   EmploymentStatusAsOn = empDet.EmploymentStatusAsOn,
                                   EmployeeIDString = empDet.EmployeeIDString,
                                   EmployeeName = empDet.EmployeeName,
                                   PerHourSalary = empDet.PerHourSalary,
                                   MonthlySalary = empDet.MonthlySalary,
                                   OTPay = empDet.OTPay,
                                   Selected = empDet.Selected


                               } into grp
                               orderby grp.Key.Department,grp.Key.Division,grp.Key.EmployeeID
                               select new EmployeeAttendanceDetails
                               {
                                   EmployeeIDString = grp.Key.EmployeeIDString,
                                   EmployeeName = grp.Key.EmployeeName,
                                   Department = grp.Key.Department,
                                   Division = grp.Key.Division,
                                   EmployeeNameDesignation = grp.Key.EmployeeNameDesignation,
                                   EmployeeID = grp.Key.EmployeeID,
                                   LeavesBF = 0,
                                   DaysAttnd = (decimal)grp.Count(),
                                   DaysConsider = 0,
                                   GrossSalary = grp.Key.GrossSalary,
                                   PFDed = 0,
                                   ESIDed = 0,
                                   PTDed = 0,
                                   Loans = 0,
                                   Canteen = 0,
                                   Others = 0,
                                   TDS = 0,
                                   NetPayable = 0,
                                   Othours = grp.Sum(pc => pc.Othours),
                                   NoOfDaysAddedAfterCalulation = Math.Round(grp.Sum(pc => pc.Othours) / shiftValue),
                                   Duration = grp.Sum(pc => pc.Duration),
                                   NoOfDaysWorked = grp.Count(),
                                   SundayAdded = SundaysAdded,
                                   EmployeePaymentCategory = grp.Key.EmployeePaymentCategory,
                                   EmployeeBasicSalary = grp.Key.EmployeeBasicSalary,
                                   EmployeeDA = grp.Key.EmployeeDA,
                                   EmployeeCA = grp.Key.EmployeeCA,
                                   PerDaySalary = grp.Key.PerDaySalary,
                                   AttendanceDaysCount = (grp.Key.EmploymentStatusAsOn == "Permanent" ? grp.Count() + 4 : grp.Count()),
                                   NoOfDaysCarryForward = grp.Key.NoOfDaysCarryForward,
                                   EmploymentStatusAsOn = grp.Key.EmploymentStatusAsOn,
                                   PerHourSalary = grp.Key.PerDaySalary / 8,
                                   MonthlySalary = 0,
                                   OTPay = (grp.Key.PerDaySalary / 8) * grp.Sum(pc => pc.Othours),
                                   Selected = grp.Key.Selected
                               }).ToList();
                var pfDetails = (from epfDetails in _context.ProvidentFundRateDetails
                                 where
                                 epfDetails.PF_Effective_From_Date >= lastDay
                                 select epfDetails
                                 ).FirstOrDefault();

                var esiDetails = (from esi in _context.ESICRates
                                  where
                                  esi.ESI_Effective_From_Date >= lastDay
                                  select esi
                                ).FirstOrDefault();

                var pfTaxmaster = (from tax in _context.ProfessionalTaxMasters
                                   join taxSlab in _context.ProfessionalTaxSlabsDetails on tax.PTPassingNo equals taxSlab.PTPassingNo
                                   where
                                   tax.PTEffectiveDate >= lastDay
                                   select new
                                   {
                                       PTEffectiveDate = tax.PTEffectiveDate,
                                       PTPassingNo = tax.PTPassingNo,
                                       PTExemptedTillSalary = tax.PTExemptedTillSalary,
                                       PTSalarySlabID = taxSlab.PTSalarySlabID,
                                       PTSalaryFrom = taxSlab.PTSalaryFrom,
                                       PTSalaryTo = taxSlab.PTSalaryTo,
                                       PTEmployeesAmountPayable = taxSlab.PTEmployeesAmountPayable,
                                   }
                                ).FirstOrDefault();

                IEnumerable<EmployeeAttendanceDetails> pageResult = results;
                objResult.totalCount = results.Count();
                List<EmployeeAttendanceDetails> result = pageResult.ToList();
                objResult.employeeAttendanceDetails = result;
                objResult.providentFundRateDetails = pfDetails;
                objResult.esiRate = esiDetails;
                objResult.TaxMaster = pfTaxmaster;
                objResult.LoanDetails = loanDetails;
                objResult.AttendanceSummaryDetails = attendancedetailssummary;
                return objResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<ApiResponse<bool>> SaveAttendanceSalaryDetail(AttendanceSalaryDetailsRequest obj)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                foreach (var data in obj.attendanceSalaryDetails)
                {
                    _context.MonthlyEmployeesSalariesFinalizations.Add(data.monthlyEmployeesSalariesFinalization);
                    await _context.SaveChangesAsync();
                    data.monthlyEmployerContributions.Salary_Attendance_Process_ID = data.monthlyEmployeesSalariesFinalization.Salary_Attendance_Process_ID;
                    _context.MonthlyEmployerContributions.Add(data.monthlyEmployerContributions);
                    _context.AttendanceSummaryDetails.Add(data.attendanceSummaryDetails);

                }

                await _context.SaveChangesAsync();
                result.IsSucceed = true;
            }
            catch (Exception ex)
            {
                result.IsSucceed = false;
                result.Exception = ex;
                result.ErrorMessages = new List<string>();
                result.ErrorMessages.Add(ex.Message);
            }
            return result;
        }
    }
}