using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.Repository;

namespace GherkinWebAPI.Service
{
    public class FieldStaffDetailsService : IFieldStaffDetailsService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public FieldStaffDetailsService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<List<FieldStaffDetails>> GetAllFieldStaff()
        {
            return await _repositoryWrapper.FieldStaffDetailsRepository.GetAllFieldStaff();
        }
        public async Task<List<FieldStaffDetails>> GetFieldStaffbyArea(string area)
        {
            return await _repositoryWrapper.FieldStaffDetailsRepository.GetFieldStaffbyArea(area);
        }
        public async Task<FieldStaffDetails> GetFieldStaffbyID(int ID)
        {
            return await _repositoryWrapper.FieldStaffDetailsRepository.GetFieldStaffbyID(ID);
        }
        public async Task CreateFieldStaff(FieldStaffDetails fieldStaffDetails)
        {

            _repositoryWrapper.FieldStaffDetailsRepository.CreateFieldStaff(fieldStaffDetails);
            await _repositoryWrapper.SaveAsync();

        }

        public async Task<List<FieldStaffDetails>> CreateFieldStaffs(HarvestAreaFieldStaffDTO fieldStaffDetails)
        {
            return await _repositoryWrapper.FieldStaffDetailsRepository.CreateFieldStaffs(fieldStaffDetails);
            //await _repositoryWrapper.SaveAsync();

        }
        public async Task UpdateFieldStaff(int fieldStaffID, FieldStaffDetails fieldStaffDetails)
        {
            _repositoryWrapper.FieldStaffDetailsRepository.UpdateFieldStaff(fieldStaffID, fieldStaffDetails);
            await _repositoryWrapper.SaveAsync();
            // throw new NotImplementedException();
        }


        public async Task<object> GetFieldStaffWithEmployeeDetails(string areaid, string staffType)
        {
            var employeeDetails = await _repositoryWrapper.EmployeeRepository.GetAllEmployee();

            return (await _repositoryWrapper.FieldStaffDetailsRepository.GetFieldStaffbyAreaAndStaff(areaid, staffType))
                  //.Where(fieldStaff => fieldStaff.StaffType.ToLower() == staffType.ToLower())
                  .Select(fieldStaff => new
                  {
                      employeeName = employeeDetails.FirstOrDefault(employee => employee.employeeId == fieldStaff.Employee_ID).employeeName,
                      areaCode = fieldStaff.AreaCode,
                      areaID = fieldStaff.Area_ID,
                      dateOfEntry = fieldStaff.DateOfEntry,
                      departmentCode = fieldStaff.DepartmentCode,
                      designationCode = fieldStaff.DesignationCode,
                      effectiveDate = fieldStaff.EffectiveDate,
                      employeeID = fieldStaff.Employee_ID,
                      loginUserName = fieldStaff.LoginUserName,
                      staffType = fieldStaff.StaffType,
                      subDepartmentCode = fieldStaff
                  });
        }
    }
}