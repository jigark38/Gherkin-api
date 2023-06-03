using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service
{
    public class OrganisationService : IOrganisationService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public OrganisationService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task CreateManagement(Management mngmt)
        {
            await _repositoryWrapper.Organisation.CreateManagement(mngmt);
        }

        //public async Task CreateOfficeLocation(OfficeLocation office)
        //{
        //    await _repositoryWrapper.Organisation.CreateOfficeLocation(office);
        //}

        public async Task CreateOrganisation(Organisation org)
        {
            await _repositoryWrapper.Organisation.CreateOrganisation(org);
        }

        public async Task DeleteManagement(int mngCode)
        {
            await _repositoryWrapper.Organisation.DeleteManagement(mngCode);
        }

        public async Task DeleteOrganisation(int orgCode)
        {
            await _repositoryWrapper.Organisation.DeleteOrganisation(orgCode);
        }

        public async Task<List<Management>> GetAllManagements()
        {
            return await _repositoryWrapper.Organisation.GetAllManagements();
        }

        public async Task<List<Organisation>> GetAllOrganisations()
        {
           return  await _repositoryWrapper.Organisation.GetAllOrganisations();
        }

        public async Task<List<Management>> GetManagementById(int orgCode)
        {
            return await _repositoryWrapper.Organisation.GetManagementById(orgCode);
        }

        public async Task<List<Organisation>> GetOrganisationById(int orgCode)
        {
            return await _repositoryWrapper.Organisation.GetOrganisationById(orgCode);
        }

        public async Task UpdateManagement(Management mngmt)
        {
            await _repositoryWrapper.Organisation.UpdateManagement(mngmt);
        }

        public async Task UpdateOrganisation(Organisation org)
        {
            await _repositoryWrapper.Organisation.UpdateOrganisation(org);
        }

        public async Task<List<Organisation>> GetOrganisations()
        {
            return await _repositoryWrapper.Organisation.GetOrganisations();
        }

        public async Task<List<OfficeLocation>> GetLocationsbyOrgid(int OrgCode)
        {
            return await _repositoryWrapper.Organisation.GetLocationsbyOrgid(OrgCode);
        }

      
    }
}