using GherkinWebAPI.DTO;
using GherkinWebAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
    public interface IOrganisationOfficeLocationDetialsRepository
    {
        Task<List<OrganisationOfficeLocationDetailsDto>> GetOrganisationOfficeLocationDetials();

        Task CreateOfficeLocation(OrganisationOfficeLocationDetails office);

        Task<List<OrganisationOfficeLocationDTO>> GetOrganisationOfficeLocations();

        Task UpdateOrganisationOfficeLocations(OrganisationOfficeLocationDetails officeLocation);

        Task DeleteOrganisationOfficeLocations(int orgOfficeNo);

        Task<List<OrganisationOfficeLocationDTO>> GetOrganisationOfficeLocationById(int orgCode);

        Task<List<OrganisationOfficeLocationDetailsDto>> GetOrganisationOfficeLocationsNames();
    }
}
