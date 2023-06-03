using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GherkinWebAPI.Core;
using System.Threading.Tasks;
using GherkinWebAPI.Persistence;
using System.Data.Entity;
using GherkinWebAPI.DTO;
using System.Runtime.InteropServices;
using System.Web.Http.Results;
using GherkinWebAPI.Models.Employee;
using System.Data.Entity.Migrations;
using Unity.Injection;
using Microsoft.Ajax.Utilities;

namespace GherkinWebAPI.Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        private RepositoryContext _context;
        public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public Task<List<Employee>> GetEmployees()
        {
            throw new NotImplementedException();
        }

        public async Task<List<EmployeeDTO>> GetEmployeesByCondition(string designation)
        {
            var _employee = from _emp in _context.Employees
                            where _emp.designationCode == designation
                            select new EmployeeDTO()
                            {
                                employeeId = _emp.employeeId,
                                employeeName = _emp.employeeName,
                                departmentCode = _emp.departmentCode,
                                subDepartmentCode = _emp.subDepartmentCode,
                                designationCode = _emp.designationCode
                            };
            return await _employee.ToListAsync();
        }

        public async Task SaveDocument(EmployeeDocument document)
        {
            _context.EmployeeDocument.Add(document);
            await _context.SaveChangesAsync();
        }
        public async Task<List<EmployeeDocument>> GetDocument(string employeeId)
        {
            try
            {
                return await _context.EmployeeDocument.Where(e => e.employeeId == employeeId).ToListAsync();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<EmployeeDocument> GetDocumentByDocId(int docId)
        {
            try
            {
                return await _context.EmployeeDocument.Where(e => e.docId == docId).FirstOrDefaultAsync();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<List<Employee>> GetAllEmployee()
        {
            return await _context.Employees.OrderBy(e => e.employeeName).ToListAsync();
        }

        public async Task<Employee> GetEmployeeById(string empId)
        {
            return await _context.Employees.Where(e => e.employeeId == empId).FirstOrDefaultAsync();
        }

        public async Task<Employee> CreateEmployee(Employee emp)
        {
            try
            {
                var firstEmployee = await _context.Employees.OrderByDescending(c => c.employeeId).FirstOrDefaultAsync();
                if (firstEmployee != null)
                {
                    var employee = await _context.Employees.OrderByDescending(c => c.employeeId.Length).ThenByDescending(c => c.employeeId).FirstOrDefaultAsync();
                    if (employee != null)
                        emp.employeeId = (Convert.ToInt16(employee.employeeId) + 1).ToString();
                    else
                        emp.employeeId = "1";
                }
                else
                    emp.employeeId = "1";

                emp.employeeMailId = string.IsNullOrEmpty(emp.employeeMailId) ? "NA" : emp.employeeMailId;
                _context.Employees.Add(emp);
                await _context.SaveChangesAsync();
                return emp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Employee>> GetEmployeeByBioMetricId(int biometricId = 0)
        {
            return await _context.Employees.Where(e => e.empBiometricId.ToString().StartsWith(biometricId.ToString())).ToListAsync();
        }

        public async Task<List<Employee>> GetEmployeeByDesignationsManager()
        {
            // var employeeList = new List<Employee>();
            var employee = await (from emp in _context.Employees
                                  join dept in _context.Departments on emp.departmentCode equals dept.departmentCode
                                  join desg in _context.Designations on emp.designationCode equals desg.designationCode
                                  where desg.designattionName.ToUpper() == "FACTORY MANAGER" || desg.designattionName.ToUpper() == "AGRI ACCOUNTS EXECUTIVE"
                                  || desg.designattionName.ToUpper() == "AGRI EXECUTIVE"
                                  || desg.designattionName.ToUpper() == "HR OFFICER"
                                  select emp).ToListAsync();

            return employee;

        }

        public async Task<List<Employee>> GetEmployeeByEmployeeName(string empName)
        {
            return await _context.Employees.Where(e => e.employeeName.Contains(empName)).ToListAsync();
        }

        public async Task<bool> CheckDuplicateBiometricId(int bioId, int unitId)
        {
            if (unitId > 0)
            {
                var employee = await _context.Employees.Where(e => e.empBiometricId == bioId && e.orgOfficeNo == unitId).ToListAsync();

                if (employee.Count > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<EmployeePayment> GetEmployeePayment(string empId)
        {
            return await _context.EmployeePayments.Where(e => e.employeeId == empId).FirstOrDefaultAsync();
        }

        public async Task<EmployeePayment> CreatePayment(EmployeePayment empPayment)
        {
            try
            {
                var employeePay = await _context.EmployeePayments.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                if (employeePay != null)
                    empPayment.Id = Convert.ToInt16(employeePay.Id) + 1;
                else
                    empPayment.Id = 1;

                _context.EmployeePayments.Add(empPayment);
                await _context.SaveChangesAsync();
                return empPayment;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            try
            {
                var emp = await _context.Employees.Where(c => c.employeeId == employee.employeeId).FirstOrDefaultAsync();
                if (emp != null && employee != null)
                {
                    emp.employeeName = employee.employeeName;
                    emp.employeeCreationDate = employee.employeeCreationDate;
                    emp.empCreatedId = employee.empCreatedId;
                    emp.orgOfficeNo = employee.orgOfficeNo;
                    emp.contractorCode = employee.contractorCode;
                    emp.empBiometricId = employee.empBiometricId;
                    emp.employeePicture = employee.employeePicture;
                    emp.employeeGender = employee.employeeGender;
                    emp.employeeDivision = employee.employeeDivision;
                    emp.employeeDOB = employee.employeeDOB;
                    emp.employeeFatherSpouseName = employee.employeeFatherSpouseName;
                    emp.employeeRelationship = employee.employeeRelationship;
                    emp.employeeContactNo = employee.employeeContactNo;
                    emp.employeeAltContactNo = employee.employeeAltContactNo;
                    emp.employeeMailId = string.IsNullOrEmpty(employee.employeeMailId) ? "NA" : employee.employeeMailId;
                    // emp.employeeMailId = employee.employeeMailId;
                    emp.employeeMaritalStatus = employee.employeeMaritalStatus;
                    emp.employeeNoOfDependents = employee.employeeNoOfDependents;
                    emp.employeePresentAddress = employee.employeePresentAddress;
                    emp.employeePermanentAddress = employee.employeePermanentAddress;
                    emp.employeeBloodGroup = employee.employeeBloodGroup;
                    emp.departmentCode = employee.departmentCode;
                    emp.subDepartmentCode = employee.subDepartmentCode;
                    emp.designationCode = employee.designationCode;
                    emp.skillsCode = employee.skillsCode;
                    emp.employeeDOJ = employee.employeeDOJ;
                    emp.employeeIHExp = employee.employeeIHExp;
                    emp.employeeTOTExp = employee.employeeTOTExp;
                    emp.employeePFNo = employee.employeePFNo;
                    emp.employeeEsiNo = employee.employeeEsiNo;
                    emp.employeeAadharNo = employee.employeeAadharNo;
                    emp.employeePassportNo = employee.employeePassportNo;
                    emp.employeePan = employee.employeePan;
                    emp.employeeStatusAsOn = employee.employeeStatusAsOn;

                    _context.Employees.AddOrUpdate(emp);
                    await _context.SaveChangesAsync();
                    return emp;
                }

                return new Employee();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EmployeePayment> UpdatePayment(EmployeePayment empPayment)
        {
            try
            {
                var empPay = await _context.EmployeePayments.Where(c => c.employeeId == empPayment.employeeId).FirstOrDefaultAsync();
                if (empPay != null && empPayment != null)
                {
                    empPay.employeeId = empPayment.employeeId;
                    empPay.employeePaymentCategory = empPayment.employeePaymentCategory;
                    empPay.employeeBasicSalary = empPayment.employeeBasicSalary;
                    empPay.employeeHRA = empPayment.employeeHRA;
                    empPay.employeeDA = empPayment.employeeDA;
                    empPay.employeeCA = empPayment.employeeCA;
                    empPay.employeeMA = empPayment.employeeMA;
                    empPay.employeeIncentives = empPayment.employeeIncentives;
                    empPay.employeeOA = empPayment.employeeOA;
                    empPay.employeeGrossSalary = empPayment.employeeGrossSalary;
                    empPay.educationAllowance = empPayment.educationAllowance;
                    _context.EmployeePayments.AddOrUpdate(empPay);
                    await _context.SaveChangesAsync();
                    return empPay;
                }

                return new EmployeePayment();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<EmployeeWithDeptSkillsAndDesignation>> GetAllEmployeeByDeptCode(int orgOfficeNo, string deptCode)
        {
            //return await _context.Employees.Where(e => e.departmentCode == deptCode).OrderByDescending(e => e.empBiometricId).ToListAsync();

            var employeeDetails = await (from emp in _context.Employees
                                         join dept in _context.Departments on emp.departmentCode equals dept.departmentCode
                                         join desg in _context.Designations on emp.designationCode equals desg.designationCode
                                         join subdept in _context.SubDepartments on emp.subDepartmentCode equals subdept.subDepartmentCode
                                         join skill in _context.SkillInformations on emp.skillsCode equals skill.skillsCode

                                         where emp.departmentCode == deptCode && emp.orgOfficeNo == orgOfficeNo

                                         select new EmployeeWithDeptSkillsAndDesignation()
                                         {
                                             employeeId = emp.employeeId,
                                             employeeName = emp.employeeName,
                                             employeeCreationDate = emp.employeeCreationDate,
                                             empCreatedId = emp.empCreatedId,
                                             orgOfficeNo = emp.orgOfficeNo == null ? 0 : emp.orgOfficeNo,
                                             orgOfficeName = emp.orgOfficeNo == null ? string.Empty : _context.OrganisationOfficeLocationDetails.FirstOrDefault
                                                                                        (e => e.Org_Office_No == emp.orgOfficeNo).Org_Office_Name,
                                             contractorCode = emp.contractorCode,
                                             empBiometricId = emp.empBiometricId,
                                             employeePicture = emp.employeePicture,
                                             employeeGender = emp.employeeGender,
                                             employeeDivision = emp.employeeDivision,
                                             employeeDOB = emp.employeeDOB,
                                             employeeFatherSpouseName = emp.employeeFatherSpouseName,
                                             employeeRelationship = emp.employeeRelationship,
                                             employeeContactNo = emp.employeeContactNo,
                                             employeeAltContactNo = emp.employeeAltContactNo,
                                             employeeMailId = emp.employeeMailId,
                                             employeeMaritalStatus = emp.employeeMaritalStatus,
                                             employeeNoOfDependents = emp.employeeNoOfDependents,
                                             employeePresentAddress = emp.employeePresentAddress,
                                             employeePermanentAddress = emp.employeePermanentAddress,
                                             employeeBloodGroup = emp.employeeBloodGroup,
                                             departmentCode = emp.departmentCode,
                                             departmenName = dept.departMentName,
                                             subDepartmentCode = emp.subDepartmentCode,
                                             subDepartmentName = subdept.subDepartmentName,
                                             designationCode = emp.designationCode,
                                             designationName = desg.designattionName,
                                             skillsCode = emp.skillsCode,
                                             skillsName = skill.skillsName,
                                             employeeDOJ = emp.employeeDOJ,
                                             employeeIHExp = emp.employeeIHExp,
                                             employeeTOTExp = emp.employeeTOTExp,
                                             employeePFNo = emp.employeePFNo,
                                             employeeEsiNo = emp.employeeEsiNo,
                                             employeeAadharNo = emp.employeeAadharNo,
                                             employeePassportNo = emp.employeePassportNo,
                                             employeePan = emp.employeePan,
                                             employeeStatusAsOn = emp.employeeStatusAsOn
                                         }).OrderByDescending(e => e.empBiometricId).ToListAsync();

            return employeeDetails;
        }

        public async Task<IEnumerable<object>> GetAllEmployeeByDesignationCodeCustomObject(string designationcode)
        {
            return _context.Employees.Where(employee => employee.designationCode == designationcode).Select(emp => new
            {
                empId = emp.employeeId,
                empName = emp.employeeName,
                contactNo = emp.employeeContactNo
            });
        }
        public async Task<IList<Employee>> GetAllEmployeeByDesignationCode(string designationcode)
        {
            return await _context.Employees.Where(employee => employee.designationCode == designationcode).ToListAsync();
        }

    }
}