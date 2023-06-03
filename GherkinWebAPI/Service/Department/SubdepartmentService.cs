using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service
{
    public class SubdepartmentService : ISubDepartmentService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public SubdepartmentService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<SubDepartment> CreateSubDepartment(SubDepartment subdepartment)
        {
            return await _repositoryWrapper.SubDepartmentRepository.CreateSubDepartment(subdepartment);
        }

        public async Task<SubDepartment> UpdateSubDepartment(SubDepartment subdepartment)
        {
            return await _repositoryWrapper.SubDepartmentRepository.UpdateSubDepartment(subdepartment);
        }

        public async Task<List<SubDepartment>> GetSubDepartments()
        {
            return await _repositoryWrapper.SubDepartmentRepository.GetSubDepartments();
        }

        public async Task<List<SubDepartment>> GetSubDepartmentsByCondition(string subDepartment)
        {
            return await _repositoryWrapper.SubDepartmentRepository.GetSubDepartmentsByCondition(subDepartment);
        }
    }
}