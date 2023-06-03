using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GherkinWebAPI.Core;
using GherkinWebAPI.Models;

namespace GherkinWebAPI.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public DepartmentService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<Department> CreateDepartment(Department department)
        {
            return await _repositoryWrapper.DepartmentRepository.CreateDepartment(department);
        }
        public async Task<Department> UpdateDeparment(Department department)
        {
            return await _repositoryWrapper.DepartmentRepository.UpdateDeparment(department);
        }
        public async Task<List<Department>> GetDepartments()
        {
            return await _repositoryWrapper.DepartmentRepository.GetDepartments();
        }

        public async Task<List<Department>> GetDepartmentsByOrganiation(int orgOfficeNo)
        {
            return await _repositoryWrapper.DepartmentRepository.GetDepartmentsByOrganiation(orgOfficeNo);
        }
    }
}