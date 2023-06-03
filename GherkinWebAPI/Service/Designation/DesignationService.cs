using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GherkinWebAPI.Core;
using GherkinWebAPI.Models;

namespace GherkinWebAPI.Service
{
    public class DesignationService : IDesignationService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public DesignationService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<Designation> CreateDesignation(Designation designation)
        {
           return await _repositoryWrapper.DesignationRepository.CreateDesignation(designation);
        }

        public async Task<Designation> UpdateDesignation(Designation designation)
        {
            return await _repositoryWrapper.DesignationRepository.UpdateDesignation(designation);
        }

        public async Task<List<Designation>> GetAllDesignations()
        {
            return await _repositoryWrapper.DesignationRepository.GetAllDesignations();
        }

        public async Task<List<Designation>> GetDesignationsByCondition(string subDepartment)
        {
            return await _repositoryWrapper.DesignationRepository.GetDesignationsByCondition(subDepartment);
        }
    }
}