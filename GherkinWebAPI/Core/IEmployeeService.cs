using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Core
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetEmployees();
        Task<List<EmployeeDTO>> GetEmployeesByCondition(string designation);
        Task SaveDocument(EmployeeDocument document);
        Task<List<EmployeeDocument>> GetDocument(string employeeId);
        Task<Employee>CreateEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<Employee> GetEmployeeById(string empId);
        Task<List<Employee>> GetAllEmployee();
        Task<List<Employee>> GetEmployeeByBioMetricId(int biometricId);
        Task<List<Employee>> GetEmployeeByEmployeeName(string empName);
        Task<bool> CheckDuplicateBiometricId(int bioId, int unitId);
        Task<EmployeePayment> GetEmployeePayment(string empId);
        Task<EmployeePayment> CreatePayment(EmployeePayment empPayment);

        Task<EmployeeDocument> GetDocumentByDocId(int docId);
        Task<EmployeePayment> UpdatePayment(EmployeePayment empPayment);
        Task <List<EmployeeWithDeptSkillsAndDesignation>> GetAllEmployeeByDeptCode(int orgOfficeNo, string deptCode);
        Task<IEnumerable<object>> GetAllEmployeeByDesignationCode(string designationcode);

        Task<List<Employee>> GetEmployeeByDesignationsManager();
    }
}