using GherkinWebAPI.Core.ManualAttendence;
using GherkinWebAPI.Models.ManualAttendence;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.ManualAttendence
{
    public class ManualAttendenceDetailsRepository : RepositoryBase<ManualAttendenceDetails>, IManualAttendenceDetailsRepository
    {
        private readonly RepositoryContext _context;
        public ManualAttendenceDetailsRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task<List<ManualAttendenceDto>> GetManualAttendenceDetailsList(string areadId, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize)
        {
            try
            {
                var employeeData = new List<ManualAttendenceDto>();
                if (string.IsNullOrEmpty(areadId) || areadId.ToLower() == "all")
                {
                    employeeData = (from fieldStaff in _context.FieldStaffDetails
                                    join emp in _context.Employees on fieldStaff.Employee_ID equals emp.employeeId
                                    join desig in _context.Designations on fieldStaff.DesignationCode equals desig.designationCode
                                    from area in _context.Areas
                                    where area.Area_ID == fieldStaff.Area_ID &&
                                    fieldStaff.EffectiveDate <= fromDate
                                    select new ManualAttendenceDto
                                    {
                                        EmployeeID = emp.employeeId,
                                        EmployeeName = emp.employeeName,
                                        Designation = desig.designattionName,
                                        AreaId = fieldStaff.Area_ID,
                                        AreaName = area.Area_Name
                                    }).OrderBy(v => v.EmployeeName).ToList();

                }
                else
                {
                    employeeData = (from fieldStaff in _context.FieldStaffDetails
                                    join emp in _context.Employees on fieldStaff.Employee_ID equals emp.employeeId
                                    join desig in _context.Designations on emp.designationCode equals desig.designationCode
                                    from area in _context.Areas
                                    where fieldStaff.Area_ID == areadId &&
                                    area.Area_ID == fieldStaff.Area_ID &&
                                    fieldStaff.EffectiveDate <= fromDate
                                    select new ManualAttendenceDto
                                    {
                                        EmployeeID = emp.employeeId,
                                        EmployeeName = emp.employeeName,
                                        Designation = desig.designattionName,
                                        AreaId = fieldStaff.Area_ID,
                                        AreaName = area.Area_Name
                                    }).OrderBy(v => v.EmployeeName).ToList();

                }

                if (employeeData == null || employeeData.Count == 0)
                {
                    return new List<ManualAttendenceDto>();
                }

                var manualAttendenceList = await (from manDetails in _context.ManualAttendenceDetails
                                                  join manMaster in _context.ManualAttendenceMasters on manDetails.ManualAttendanceProcessID equals manMaster.ManualAttendanceProcessID
                                                  where manDetails.ManualAttendanceDate >= fromDate && manDetails.ManualAttendanceDate <= toDate
                                                  && ((areadId.ToLower() == "all") ? (areadId == areadId) : (manMaster.OrgOfficeNo == areadId))
                                                  select new ManualAttendenceDto
                                                  {
                                                      EmployeeID = manDetails.EmployeeID,
                                                      ManualAttendanceStatus = manDetails.ManualAttendanceStatus,
                                                      ManualAttendanceDate = manDetails.ManualAttendanceDate,
                                                      ManualAttendanceID = manDetails.ManualAttendanceID,
                                                      ManualAttendanceProcessID = manDetails.ManualAttendanceProcessID
                                                  }).ToListAsync();

                manualAttendenceList.ForEach(s =>
                {
                    var empObj = employeeData.FirstOrDefault(a => a.EmployeeID == s.EmployeeID);
                    if (empObj != null)
                    {
                        s.EmployeeName = empObj.EmployeeName;
                        s.Designation = empObj.Designation;
                        s.AreaName = empObj.AreaName;
                        s.AreaId = empObj.AreaId;
                    }

                });

                employeeData.ForEach(a =>
                {
                    var nonMatchedEmp = manualAttendenceList.Any(b => b.EmployeeID == a.EmployeeID);

                    if (!nonMatchedEmp)
                    {
                        manualAttendenceList.Add(new ManualAttendenceDto
                        {
                            EmployeeID = a.EmployeeID,
                            EmployeeName = a.EmployeeName,
                            Designation = a.Designation,
                            DesignationCode = a.DesignationCode,
                            AreaId = a.AreaId,
                            AreaName = a.AreaName,
                            ManualAttendanceProcessID = a.ManualAttendanceProcessID,
                            ManualAttendanceDate = a.ManualAttendanceDate,
                            ManualAttendanceID = a.ManualAttendanceID,
                            ManualAttendanceStatus = a.ManualAttendanceStatus
                        });

                    }
                });

                return manualAttendenceList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> SaveManualAttendence(List<ManualAttendenceDto> manualAttendenceDtos)
        {
            try
            {
                bool isMasterTableUptodate = false;
                bool isDetailsTableUptodate = false;
                var manAttManster = new ManualAttendenceMaster();
                var firstManualAttendece = manualAttendenceDtos[0];
                var manualAttenenceMaster = await _context.ManualAttendenceMasters.SingleOrDefaultAsync(a => a.ManualAttendanceProcessID == firstManualAttendece.ManualAttendanceProcessID);
                if (manualAttenenceMaster == null)
                {

                    manAttManster.OrgOfficeNo = firstManualAttendece.AreaId;
                    manAttManster.PassingManualAttendanceDate = DateTime.Now;
                    _context.ManualAttendenceMasters.Add(manAttManster);
                    var res = await _context.SaveChangesAsync();
                    if (res == 1)
                    {
                        isMasterTableUptodate = true;
                    }
                    else
                    {
                        isMasterTableUptodate = false;
                    }
                }
                else
                {
                    isMasterTableUptodate = true;
                    manAttManster = manualAttenenceMaster;

                }

                if (manualAttendenceDtos != null && manualAttendenceDtos.Count > 0 && isMasterTableUptodate)
                {
                    var existingEmp = manualAttendenceDtos.Where(a => a.ManualAttendanceID != 0).ToList();
                    if (existingEmp != null && existingEmp.Count > 0)
                    { // update existing records
                        try
                        {
                            var empDataTobeSaved = new List<ManualAttendenceDetails>();
                            _context.Configuration.AutoDetectChangesEnabled = false;
                            existingEmp.ForEach(a =>
                            {
                                var entityToSave = new ManualAttendenceDetails
                                {
                                    ManualAttendanceID = a.ManualAttendanceID,
                                    ManualAttendanceProcessID = manAttManster.ManualAttendanceProcessID,
                                    EmployeeID = a.EmployeeID,
                                    ManualAttendanceDate = a.ManualAttendanceDate,
                                    ManualAttendanceStatus = a.ManualAttendanceStatus,
                                };
                                _context.ManualAttendenceDetails.Attach(entityToSave);
                                _context.Entry(entityToSave).Property(x => x.ManualAttendanceStatus).IsModified = true;
                            });

                            var res = await _context.SaveChangesAsync();
                            isDetailsTableUptodate = res > 0;

                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                        finally
                        {
                            _context.Configuration.AutoDetectChangesEnabled = true;
                        }

                    }
                    else
                    {
                        isDetailsTableUptodate = true;
                    }

                    var newEmp = manualAttendenceDtos.Where(a => a.ManualAttendanceID == 0).ToList();
                    if (newEmp != null && newEmp.Count > 0) //create new records
                    {
                        var empDataTobeSaved = new List<ManualAttendenceDetails>();
                        newEmp.ForEach(a =>
                        {
                            empDataTobeSaved.Add(new ManualAttendenceDetails
                            {
                                ManualAttendanceProcessID = manAttManster.ManualAttendanceProcessID,
                                EmployeeID = a.EmployeeID,
                                ManualAttendanceDate = a.ManualAttendanceDate,
                                ManualAttendanceStatus = a.ManualAttendanceStatus
                            });
                        });
                        _context.ManualAttendenceDetails.AddRange(empDataTobeSaved);
                        var res = await _context.SaveChangesAsync();

                        isDetailsTableUptodate = res > 0;

                    }
                    else
                    {
                        isDetailsTableUptodate = true;

                    }
                }

                if (isDetailsTableUptodate && isMasterTableUptodate)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}