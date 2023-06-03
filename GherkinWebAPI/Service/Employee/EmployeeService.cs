using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.Employee;

namespace GherkinWebAPI.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public EmployeeService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public Task<List<Employee>> GetEmployees()
        {
            throw new NotImplementedException();
        }

        public async Task<List<EmployeeDTO>> GetEmployeesByCondition(string designation)
        {
            return await _repositoryWrapper.EmployeeRepository.GetEmployeesByCondition(designation);
            //throw new NotImplementedException();
        }

        public async Task<List<EmployeeDocument>> GetDocument(string employeeId)
        {
            return await _repositoryWrapper.EmployeeRepository.GetDocument(employeeId);
        }

        public async Task SaveDocument(EmployeeDocument document)
        {
            await _repositoryWrapper.EmployeeRepository.SaveDocument(document);
        }

        public async Task<Employee> CreateEmployee(Employee employee)
        {
            return await _repositoryWrapper.EmployeeRepository.CreateEmployee(employee);
        }

        public async Task<Employee> GetEmployeeById(string empId)
        {
            return await _repositoryWrapper.EmployeeRepository.GetEmployeeById(empId);
        }

        public async Task<List<Employee>> GetAllEmployee()
        {
            return await _repositoryWrapper.EmployeeRepository.GetAllEmployee();
        }

        public async Task<List<Employee>> GetEmployeeByBioMetricId(int biometricId)
        {
            return await _repositoryWrapper.EmployeeRepository.GetEmployeeByBioMetricId(biometricId);
        }
        public async Task<List<Employee>> GetEmployeeByEmployeeName(string empName)
        {
            return await _repositoryWrapper.EmployeeRepository.GetEmployeeByEmployeeName(empName);
        }

        public async Task<bool> CheckDuplicateBiometricId(int bioId, int unitId)
        {
            return await _repositoryWrapper.EmployeeRepository.CheckDuplicateBiometricId(bioId, unitId);
        }

        public async Task<EmployeePayment> GetEmployeePayment(string empId)
        {
            return await _repositoryWrapper.EmployeeRepository.GetEmployeePayment(empId);
        }

        public async Task<EmployeePayment> CreatePayment(EmployeePayment empPayment)
        {
            return await _repositoryWrapper.EmployeeRepository.CreatePayment(empPayment);
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            return await _repositoryWrapper.EmployeeRepository.UpdateEmployee(employee);
        }

        public async Task<EmployeeDocument> GetDocumentByDocId(int docId)
        {
            return await _repositoryWrapper.EmployeeRepository.GetDocumentByDocId(docId);
        }

        public async Task<EmployeePayment> UpdatePayment(EmployeePayment empPayment)
        {
            return await _repositoryWrapper.EmployeeRepository.UpdatePayment(empPayment);
        }

        public async Task<List<EmployeeWithDeptSkillsAndDesignation>> GetAllEmployeeByDeptCode(int orgOfficeNo, string deptId)
        {
            return await _repositoryWrapper.EmployeeRepository.GetAllEmployeeByDeptCode(orgOfficeNo, deptId);
        }

        public async Task<IEnumerable<object>> GetAllEmployeeByDesignationCode(string designationcode)
        {
            return await _repositoryWrapper.EmployeeRepository.GetAllEmployeeByDesignationCodeCustomObject(designationcode);
        }

        public async Task<List<Employee>> GetEmployeeByDesignationsManager()
        {
            return await _repositoryWrapper.EmployeeRepository.GetEmployeeByDesignationsManager();
        }
    }
}