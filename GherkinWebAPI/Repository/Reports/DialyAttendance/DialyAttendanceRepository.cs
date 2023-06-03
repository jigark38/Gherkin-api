using GherkinWebAPI.Core.Reports.DailyAttendance;
using GherkinWebAPI.DTO.Reports.DialyAttendance;
using GherkinWebAPI.Entities;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.AttendanceDetails;
using GherkinWebAPI.Models.ShiftDetail;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.Reports.DialyAttendance
{
    public class DialyAttendanceRepository : IDialyAttendanceRepository
    {
        private readonly RepositoryContext _context;
        public DialyAttendanceRepository(RepositoryContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse<object>> GetAttendanceDetails(string date, int orgOfficeNo, string status, string deptCode, string subDeptCode,
            string shift, string division, string filter, string category, string gender, int biometricId)
        {
            ApiResponse<object> result = new ApiResponse<object>();
            try
            {
                string DepartmentName = "All";
                string SubDepartmentName = "All";
                string Shift = "All";
                string shiftTime = "";
                DateTime entryDate = DateTime.Parse(date).Date;
                var attendanceDetails = await (from u in _context.BiometricUserLogs
                                               join e in _context.Employees on u.IndRegID equals e.empBiometricId
                                               join d in _context.Departments on e.departmentCode equals d.departmentCode
                                               join sd in _context.SubDepartments on e.subDepartmentCode equals sd.subDepartmentCode
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
                                                   SubDepartment = sd.subDepartmentName,
                                                   SubDepartmentCode = sd.subDepartmentCode,
                                                   DwInOutMode = u.DwInOutMode,
                                                   Division = e.employeeDivision,
                                                   Gender = e.employeeGender
                                               }).OrderBy(a => a.DateTimeRecord).ToListAsync();

                if (deptCode != "All")
                {
                    attendanceDetails = attendanceDetails.Where(a => a.DepartmentCode == deptCode).ToList();
                    DepartmentName = attendanceDetails.FirstOrDefault().Department;
                }
                if (subDeptCode != "All")
                {
                    attendanceDetails = attendanceDetails.Where(a => a.SubDepartmentCode == subDeptCode).ToList();
                    SubDepartmentName = attendanceDetails.FirstOrDefault().SubDepartment;
                }
                if (division != "All")
                {
                    attendanceDetails = attendanceDetails.Where(a => a.Division == division).ToList();
                }
                var groupresult = attendanceDetails.GroupBy(u => u.IndRegId, u => u.DwInOutMode, (key, g) => new { IndRgId = key, list = g.ToList() });

                string inTime = null;
                string outTime = null;
                string overTime = null;
                OrganisationOfficeLocationDetails organisationDetail = null;
                if (orgOfficeNo > 0)
                {
                    organisationDetail = await _context.OrganisationOfficeLocationDetails.Where(x => x.Org_Office_No == orgOfficeNo)?.FirstOrDefaultAsync();
                }
                Organisation org = await _context.Organisations.Where(x => x.Org_Code == organisationDetail.Org_Code).FirstOrDefaultAsync();
                ShiftDetailMaster shiftDetail = null;
                if (shift != "All")
                {
                    var longShift = Convert.ToInt64(shift);
                    shiftDetail = await _context.ShiftDetailsMaster.Where(x => x.ShiftNo == longShift).FirstOrDefaultAsync();
                    Shift = shiftDetail.ShiftName;
                }
                else
                {
                    shiftDetail = await _context.ShiftDetailsMaster.FirstOrDefaultAsync();
                }
                shiftTime = shiftDetail.ShiftTimeFrom.Hours + " : " + shiftDetail.ShiftTimeFrom.Minutes + " - " + shiftDetail.ShiftTimeTo.Hours + " : " + shiftDetail.ShiftTimeTo.Minutes;
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
                    string shiftDuration = calcuateDiffernceOfHours(shiftDetail.ShiftTimeFrom.ToString(@"hh\:mm"), shiftDetail.ShiftTimeTo.ToString(@"hh\:mm"));

                    if (!string.IsNullOrEmpty(inTime) && !string.IsNullOrEmpty(outTime))
                    {
                        string firstTime = inTime.Split(',')[0];
                        string lastTime = outTime.TrimEnd(' ').TrimEnd(',').Split(',')[outTime.TrimEnd(' ').TrimEnd(',').Split(',').Length - 1];
                        duration = calcuateDiffernceOfHours(firstTime, lastTime);
                        TimeSpan lastTimeinTimeSpan = TimeSpan.Parse(lastTime);
                        if ((division != "All" && division != "Staff") || (division == "All" && attendanceDetail.Division == "Worker"))
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
                        else
                        {
                            overTime = "";
                        }
                    }

                    list.Add(new Attendance()
                    {
                        IndRegId = attendanceDetail.IndRegId,
                        EmployeeName = attendanceDetail.EmployeeName,
                        Department = attendanceDetail.Department,
                        SubDepartment = attendanceDetail.SubDepartment,
                        InTime = !string.IsNullOrEmpty(inTime) ? inTime.TrimEnd(' ').TrimEnd(',') : null,
                        OutTime = !string.IsNullOrEmpty(outTime) ? outTime.TrimEnd(' ').TrimEnd(',') : null,
                        Duration = duration,
                        overtime = overTime,
                        Gender = attendanceDetail.Gender,
                        Designation = attendanceDetail.Designation
                        //Time = !string.IsNullOrEmpty(inTime) && !string.IsNullOrEmpty(outTime) ? string.Join(" / ", inTime.TrimEnd(' ').TrimEnd(','), outTime.TrimEnd(' ').TrimEnd(','))
                        //        : string.Join(" / ", inTime != null ? inTime.TrimEnd(' ').TrimEnd(',') : inTime, outTime != null ? outTime.TrimEnd(' ').TrimEnd(',') : outTime).Trim()
                    });

                    outTime = null;
                    inTime = null;
                    overTime = "";
                }
                var biometricUserLogs = await _context.BiometricUserLogs.Where(a => a.DateOnlyRecord == entryDate).
                   Select(x => x.IndRegID).ToListAsync();

                attendanceDetails = await (from e in _context.Employees
                                           join d in _context.Departments on e.departmentCode equals d.departmentCode
                                           join sd in _context.SubDepartments on e.subDepartmentCode equals sd.subDepartmentCode
                                           join de in _context.Designations on e.designationCode equals de.designationCode
                                           where e.orgOfficeNo == orgOfficeNo
                                           select new Attendance
                                           {
                                               IndRegId = e.empBiometricId,
                                               EmployeeName = e.employeeName,
                                               Department = d.departMentName,
                                               DepartmentCode = d.departmentCode,
                                               Designation = de.designattionName,
                                               SubDepartment = sd.subDepartmentName,
                                               SubDepartmentCode = sd.subDepartmentCode,
                                               Division = e.employeeDivision,
                                               Gender = e.employeeGender
                                           }).ToListAsync();

                if (deptCode != "All")
                {
                    attendanceDetails = attendanceDetails.Where(a => a.DepartmentCode == deptCode).ToList();
                }
                if (subDeptCode != "All")
                {
                    attendanceDetails = attendanceDetails.Where(a => a.SubDepartmentCode == subDeptCode).ToList();
                }
                if (division != "All")
                {
                    attendanceDetails = attendanceDetails.Where(a => a.Division == division).ToList();
                }
                attendanceDetails = attendanceDetails.Where(x => !biometricUserLogs.Contains(x.IndRegId)).ToList();



                foreach (var a in attendanceDetails)
                {
                    var attendanceDetail = attendanceDetails.Where(i => i.IndRegId == a.IndRegId).First();

                    list.Add(new Attendance()
                    {
                        IndRegId = attendanceDetail.IndRegId,
                        EmployeeName = attendanceDetail.EmployeeName,
                        Department = attendanceDetail.Department,
                        SubDepartment = attendanceDetail.SubDepartment,
                        Designation = attendanceDetail.Designation,
                        overtime = "",
                        Gender = attendanceDetail.Gender
                    });
                }
                if (filter == "Id")
                {
                    list = list.OrderBy(x => x.IndRegId).ToList();
                }

                if (!string.IsNullOrEmpty(gender) && gender != "All")
                {
                    list = list.Where(x => x.Gender.ToLower() == gender.ToLower()).ToList();

                }

                if (category.Equals("Total Missed", StringComparison.InvariantCultureIgnoreCase))
                {
                    list = list.Where(z => string.IsNullOrEmpty(z.InTime) && string.IsNullOrEmpty(z.OutTime)).ToList();

                }
                else if (category.Equals("Attended", StringComparison.InvariantCultureIgnoreCase))
                {
                    list = list.Where(z => !string.IsNullOrEmpty(z.InTime) && !string.IsNullOrEmpty(z.OutTime)).ToList();

                }
                else if (category.Equals("Single Punch", StringComparison.InvariantCultureIgnoreCase))
                {
                    var removeItems = list.Where(r => (!string.IsNullOrEmpty(r.InTime) && !string.IsNullOrEmpty(r.OutTime)) ||
                    (string.IsNullOrEmpty(r.InTime) && string.IsNullOrEmpty(r.OutTime)));

                    list = list.Except(removeItems).ToList();
                }


                List<string> columnsName = new List<string>();
                columnsName.Add("Sl.No");
                columnsName.Add("ID");
                columnsName.Add("Department");
                columnsName.Add("Name");
                columnsName.Add("Gender");
                columnsName.Add("Designation");
                columnsName.Add("IN Time");
                columnsName.Add("OUT Time");
                columnsName.Add("Duration");
                columnsName.Add("OT");
                columnsName.Add("Att. TD");

                List<List<string>> gridData = new List<List<string>>();
                int count = 1;
                foreach (var details in list)
                {
                    var colData = new List<string>();

                    colData.Add(count.ToString());
                    colData.Add(details.IndRegId.ToString());
                    colData.Add(details.Department);
                    colData.Add(details.EmployeeName);
                    colData.Add(details.Gender);
                    colData.Add(details.Designation);
                    colData.Add(details.InTime);
                    colData.Add(details.OutTime);
                    colData.Add(details.Duration);
                    colData.Add(details.overtime);
                    colData.Add("");
                    gridData.Add(colData);
                    count++;
                }
                var resultData = new
                {
                    ColumnsName = columnsName,
                    GridData = gridData,
                    Organisation = org,
                    Total = gridData.Count,
                    Department = DepartmentName,
                    SubDepartment = SubDepartmentName,
                    ShiftName = shiftDetail.ShiftName + " " + shiftTime
                };
                result.Data = resultData;
                result.IsSucceed = true;
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Attendance>> GetAttendanceDetailsForView(string date, int orgOfficeNo, string status, string deptCode, string subDeptCode,
            string shift, string division, string filter, string category, string gender, int biometricId)
        {
            try
            {
                DateTime entryDate = DateTime.Parse(date).Date;
                var attendanceDetails = await (from u in _context.BiometricUserLogs
                                               join e in _context.Employees on u.IndRegID equals e.empBiometricId
                                               join d in _context.Departments on e.departmentCode equals d.departmentCode
                                               join sd in _context.SubDepartments on e.subDepartmentCode equals sd.subDepartmentCode
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
                                                   SubDepartment = sd.subDepartmentName,
                                                   SubDepartmentCode = sd.subDepartmentCode,
                                                   DwInOutMode = u.DwInOutMode,
                                                   Division = e.employeeDivision,
                                                   Gender = e.employeeGender
                                               }).OrderBy(a => a.DateTimeRecord).ToListAsync();

                if (deptCode != "All")
                {
                    attendanceDetails = attendanceDetails.Where(a => a.DepartmentCode == deptCode).ToList();
                }
                if (subDeptCode != "All")
                {
                    attendanceDetails = attendanceDetails.Where(a => a.SubDepartmentCode == subDeptCode).ToList();
                }
                if (division != "All")
                {
                    attendanceDetails = attendanceDetails.Where(a => a.Division == division).ToList();
                }
                var groupresult = attendanceDetails.GroupBy(u => u.IndRegId, u => u.DwInOutMode, (key, g) => new { IndRgId = key, list = g.ToList() });

                string inTime = null;
                string outTime = null;
                string overTime = null;
                OrganisationOfficeLocationDetails organisationDetail = null;
                if (orgOfficeNo > 0)
                {
                    organisationDetail = await _context.OrganisationOfficeLocationDetails.Where(x => x.Org_Office_No == orgOfficeNo)?.FirstOrDefaultAsync();
                }
                Organisation org = await _context.Organisations.Where(x => x.Org_Code == organisationDetail.Org_Code).FirstOrDefaultAsync();
                ShiftDetailMaster shiftDetail = null;
                if (shift != "All")
                {
                    var longShift = Convert.ToInt64(shift);
                    shiftDetail = await _context.ShiftDetailsMaster.Where(x => x.ShiftNo == longShift).FirstOrDefaultAsync();
                }
                else
                {
                    shiftDetail = await _context.ShiftDetailsMaster.FirstOrDefaultAsync();
                }

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
                    string shiftDuration = calcuateDiffernceOfHours(shiftDetail.ShiftTimeFrom.ToString(@"hh\:mm"), shiftDetail.ShiftTimeTo.ToString(@"hh\:mm"));
                    if (!string.IsNullOrEmpty(inTime) && !string.IsNullOrEmpty(outTime))
                    {
                        string firstTime = inTime.Split(',')[0];
                        string lastTime = outTime.TrimEnd(' ').TrimEnd(',').Split(',')[outTime.TrimEnd(' ').TrimEnd(',').Split(',').Length - 1];
                        duration = calcuateDiffernceOfHours(firstTime, lastTime);

                        TimeSpan lastTimeinTimeSpan = TimeSpan.Parse(lastTime);
                        if ((division != "All" && division != "Staff") || (division == "All" && attendanceDetail.Division == "Worker"))
                        {
                            string subtractTime = calcuateAbsoluteDiffernceOfHours(duration, shiftDuration);
                            //if (duration == shiftDuration)
                            //{
                            //      overTime = subtractTime;
                            //}
                            //else
                            if (!string.IsNullOrWhiteSpace(duration))
                            {
                                int otHr = Convert.ToInt32(subtractTime.Split(':')[0]);

                                if (otHr > 0)
                                {
                                    overTime = subtractTime;
                                }

                            }


                        }
                        else
                        {
                            overTime = "";
                        }
                    }

                    list.Add(new Attendance()
                    {
                        IndRegId = attendanceDetail.IndRegId,
                        EmployeeName = attendanceDetail.EmployeeName,
                        Department = attendanceDetail.Department,
                        SubDepartment = attendanceDetail.SubDepartment,
                        InTime = !string.IsNullOrEmpty(inTime) ? inTime.TrimEnd(' ').TrimEnd(',') : null,
                        OutTime = !string.IsNullOrEmpty(outTime) ? outTime.TrimEnd(' ').TrimEnd(',') : null,
                        Duration = duration,
                        overtime = overTime,
                        Gender = attendanceDetail.Gender,
                        Designation = attendanceDetail.Designation
                        //Time = !string.IsNullOrEmpty(inTime) && !string.IsNullOrEmpty(outTime) ? string.Join(" / ", inTime.TrimEnd(' ').TrimEnd(','), outTime.TrimEnd(' ').TrimEnd(','))
                        //        : string.Join(" / ", inTime != null ? inTime.TrimEnd(' ').TrimEnd(',') : inTime, outTime != null ? outTime.TrimEnd(' ').TrimEnd(',') : outTime).Trim()
                    });

                    outTime = null;
                    inTime = null;
                    overTime = "";
                }
                var biometricUserLogs = await _context.BiometricUserLogs.Where(a => a.DateOnlyRecord == entryDate).
                   Select(x => x.IndRegID).ToListAsync();

                attendanceDetails = await (from e in _context.Employees
                                           join d in _context.Departments on e.departmentCode equals d.departmentCode
                                           join sd in _context.SubDepartments on e.subDepartmentCode equals sd.subDepartmentCode
                                           join de in _context.Designations on e.designationCode equals de.designationCode
                                           where e.orgOfficeNo == orgOfficeNo
                                           select new Attendance
                                           {
                                               IndRegId = e.empBiometricId,
                                               EmployeeName = e.employeeName,
                                               Department = d.departMentName,
                                               DepartmentCode = d.departmentCode,
                                               Designation = de.designattionName,
                                               SubDepartment = sd.subDepartmentName,
                                               SubDepartmentCode = sd.subDepartmentCode,
                                               Division = e.employeeDivision,
                                               Gender = e.employeeGender
                                           }).ToListAsync();

                if (deptCode != "All")
                {
                    attendanceDetails = attendanceDetails.Where(a => a.DepartmentCode == deptCode).ToList();
                }
                if (subDeptCode != "All")
                {
                    attendanceDetails = attendanceDetails.Where(a => a.SubDepartmentCode == subDeptCode).ToList();
                }
                if (division != "All")
                {
                    attendanceDetails = attendanceDetails.Where(a => a.Division == division).ToList();
                }
                attendanceDetails = attendanceDetails.Where(x => !biometricUserLogs.Contains(x.IndRegId)).ToList();

                foreach (var a in attendanceDetails)
                {
                    var attendanceDetail = attendanceDetails.Where(i => i.IndRegId == a.IndRegId).First();

                    list.Add(new Attendance()
                    {
                        IndRegId = attendanceDetail.IndRegId,
                        EmployeeName = attendanceDetail.EmployeeName,
                        Department = attendanceDetail.Department,
                        SubDepartment = attendanceDetail.SubDepartment,
                        Designation = attendanceDetail.Designation,
                        overtime = "",
                        Gender = attendanceDetail.Gender
                    });
                }

                if (biometricId != 0)
                {
                    list = list.Where(x => x.IndRegId == biometricId).ToList();

                }
                if (filter == "Id")
                {
                    list = list.OrderBy(x => x.IndRegId).ToList();
                }
                if (!string.IsNullOrEmpty(gender) && gender != "All")
                {
                    list = list.Where(x => x.Gender.ToLower() == gender.ToLower()).ToList();

                }

                if (category.Equals("Total Missed", StringComparison.InvariantCultureIgnoreCase))
                {
                    list = list.Where(z => string.IsNullOrEmpty(z.InTime) && string.IsNullOrEmpty(z.OutTime)).ToList();
                    return list;
                }
                else if (category.Equals("Attended", StringComparison.InvariantCultureIgnoreCase))
                {
                    list = list.Where(z => !string.IsNullOrEmpty(z.InTime) && !string.IsNullOrEmpty(z.OutTime)).ToList();
                    return list;
                }
                else if (category.Equals("Single Punch", StringComparison.InvariantCultureIgnoreCase))
                {
                    var removeItems = list.Where(r => (!string.IsNullOrEmpty(r.InTime) && !string.IsNullOrEmpty(r.OutTime)) ||
                    (string.IsNullOrEmpty(r.InTime) && string.IsNullOrEmpty(r.OutTime)));

                    return list.Except(removeItems).ToList();
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // 10:29, 9:30
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
    }
}