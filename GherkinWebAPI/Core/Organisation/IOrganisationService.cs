using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Core
{
    public interface IOrganisationService
    {
        Task CreateOrganisation(Organisation org);

        Task CreateManagement(Management mngmt);

        Task<List<Organisation>> GetAllOrganisations();

        Task<List<Management>> GetAllManagements();

        Task UpdateOrganisation(Organisation org);


        Task UpdateManagement(Management mngmt);

        Task DeleteOrganisation(int orgCode);

        Task DeleteManagement(int mngCode);

        Task<List<Organisation>> GetOrganisationById(int orgCode);

        Task<List<Management>> GetManagementById(int orgCode);

        Task<List<Organisation>> GetOrganisations();

        Task<List<OfficeLocation>> GetLocationsbyOrgid(int OrgCode);
    }
}