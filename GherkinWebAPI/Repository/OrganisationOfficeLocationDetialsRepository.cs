using AutoMapper;
using GherkinWebAPI.Core;
using GherkinWebAPI.CustomExceptions;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Entities;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

namespace GherkinWebAPI.Repository
{
    public class OrganisationOfficeLocationDetialsRepository : RepositoryBase<OrganisationOfficeLocationDetails>, IOrganisationOfficeLocationDetialsRepository
    {
        private RepositoryContext _context;

        public OrganisationOfficeLocationDetialsRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            this._context = repositoryContext;
        }

       

        public async Task CreateOfficeLocation(OrganisationOfficeLocationDetails office)
        {
            try
            {
                var CountriesList = await _context.Countries.AsNoTracking().Select(x => x.Country_Name).ToListAsync();
                var StateList = await _context.States.AsNoTracking().Select(x => x.State_Name).ToListAsync();
                var DistrictList = await _context.Districts.AsNoTracking().Select(x => x.District_Name).ToListAsync();
                var PlaceList = await _context.Places.AsNoTracking().Select(x => x.PlaceName).ToListAsync();
                if (CountriesList.Equals(office.Country_Name) && StateList.Equals(office.State_Name) && DistrictList.Equals(office.District_Name) && PlaceList.Equals(office.Place_Name))
                {
                    _context.OrganisationOfficeLocationDetails.Add(office);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    if (CountriesList != null && !CountriesList.Contains(office.Country_Name.ToUpper()))
                    {
                        _context.Countries.AddOrUpdate(new Models.Country { Country_Name = office.Country_Name.ToUpper()});
                        await _context.SaveChangesAsync();
                    }

                    var countryCode = await _context.Countries.AsNoTracking().Where(x => x.Country_Name.Equals( office.Country_Name)).Select(x => x.Country_Code).FirstOrDefaultAsync();

                    if (StateList != null && !StateList.Contains(office.State_Name.ToUpper()))
                    {
                        _context.States.AddOrUpdate(new Models.State { State_Name = office.State_Name.ToUpper(), Country_Code = countryCode });
                        await _context.SaveChangesAsync();
                    }

                    var stateCode = await _context.States.AsNoTracking().Where(x => x.State_Name.Equals(office.State_Name)).Select(x => x.State_Code).FirstOrDefaultAsync();

                    if (DistrictList != null && !DistrictList.Contains(office.District_Name.ToUpper()))
                    {
                        _context.Districts.AddOrUpdate(new Models.District { District_Name = office.District_Name.ToUpper(), State_Code = stateCode, Country_Code = countryCode });
                        await _context.SaveChangesAsync();
                    }

                    var districtCode = await _context.Districts.AsNoTracking().Where(x => x.District_Name.Equals(office.District_Name)).Select(x => x.District_Code).FirstOrDefaultAsync();

                    if (PlaceList != null && !PlaceList.Contains(office.Place_Name.ToUpper()))
                    {
                        _context.Places.AddOrUpdate(new Models.Place { PlaceName = office.Place_Name.ToUpper(), DistrictCode = districtCode, StateCode = stateCode, CountryCode = countryCode });
                        await _context.SaveChangesAsync();
                    }

                    _context.OrganisationOfficeLocationDetails.Add(office);
                    await _context.SaveChangesAsync();

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task DeleteOrganisationOfficeLocations(int officeNo)
        {
            OrganisationOfficeLocationDetails office = await _context.OrganisationOfficeLocationDetails.FirstOrDefaultAsync(x => x.Org_Office_No == officeNo);
            if(office != null)
            {
                _context.OrganisationOfficeLocationDetails.Remove(office);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new CustomException("Office location doesn't exist to delete");
            }
        }

        public async Task<List<OrganisationOfficeLocationDTO>> GetOrganisationOfficeLocationById(int orgId)
        {
            try
            {
                List<OrganisationOfficeLocationDTO> officeLocations = new List<OrganisationOfficeLocationDTO>();
                var offices = await this._context.OrganisationOfficeLocationDetails.AsNoTracking().Where(x => x.Org_Code == orgId).ToListAsync();
                var countries = await this._context.Countries.AsNoTracking().ToListAsync();
                var states = await this._context.States.AsNoTracking().ToListAsync();
                var districts = await this._context.Districts.AsNoTracking().ToListAsync();
                var places = await this._context.Places.AsNoTracking().ToListAsync();

                foreach (OrganisationOfficeLocationDetails office in offices)
                {
                    var officeDTO = new OrganisationOfficeLocationDTO();
                    officeDTO.Org_Code = office.Org_Code;
                    officeDTO.Org_Office_No = office.Org_Office_No;
                    officeDTO.Org_Office_Name = office.Org_Office_Name;
                    officeDTO.Nature_Office_Details = office.Nature_Office_Details;
                    officeDTO.Location_Office_Address = office.Location_Office_Address;
                    officeDTO.Country_Name = office.Country_Name;
                    officeDTO.State_Name = office.State_Name;
                    officeDTO.Place_Name = office.Place_Name;
                    officeDTO.District_Name = office.District_Name;
                    officeDTO.Location_Phone_Details = office.Location_Phone_Details;
                    officeDTO.Location_Fax_Details = office.Location_Fax_Details;
                    officeDTO.Location_Cell_Phone = office.Location_Cell_Phone;
                    officeDTO.Location_Email_Id = office.Location_Email_Id;
                    officeDTO.Labor_License_No = office.Labor_License_No;
                    officeDTO.Other_License_Details = office.Other_License_Details;
                    officeDTO.Country_Code = countries.Where(x => x.Country_Name == office.Country_Name).Select(x => x.Country_Code).FirstOrDefault();
                    officeDTO.State_Code = states.Where(x => x.State_Name == office.State_Name).Select(x => x.State_Code).FirstOrDefault();
                    officeDTO.District_Code = districts.Where(x => x.District_Name == office.District_Name).Select(x => x.District_Code).FirstOrDefault();
                    officeDTO.Place_Code = places.Where(x => x.PlaceName == office.Place_Name).Select(x => x.PlaceCode).FirstOrDefault();
                    officeLocations.Add(officeDTO);
                }
                return officeLocations;
            }
            catch (Exception ex) { throw new Exception("Org Code does not exist"); }
            
        }

        public async Task<List<OrganisationOfficeLocationDetailsDto>> GetOrganisationOfficeLocationDetials()
        {
            var result = await this._context.OrganisationOfficeLocationDetails.AsNoTracking().ToListAsync();

            List<OrganisationOfficeLocationDetailsDto> list = new List<OrganisationOfficeLocationDetailsDto>();

            foreach (var i in result)
            {
                list.Add(Mapper.Map<OrganisationOfficeLocationDetailsDto>(i));
            }
            return list;
        }

        public async Task<List<OrganisationOfficeLocationDTO>> GetOrganisationOfficeLocations()
        {
            List<OrganisationOfficeLocationDTO> officeLocations = new List<OrganisationOfficeLocationDTO>();
            var offices = await this._context.OrganisationOfficeLocationDetails.AsNoTracking().ToListAsync();
            var countries = await this._context.Countries.AsNoTracking().ToListAsync();
            var states = await this._context.States.AsNoTracking().ToListAsync();
            var districts = await this._context.Districts.AsNoTracking().ToListAsync();
            var places = await this._context.Places.AsNoTracking().ToListAsync();

            foreach (OrganisationOfficeLocationDetails office in offices)
            {
                var officeDTO = new OrganisationOfficeLocationDTO();
                officeDTO.Org_Code = office.Org_Code;
                officeDTO.Org_Office_No = office.Org_Office_No;
                officeDTO.Org_Office_Name = office.Org_Office_Name;
                officeDTO.Nature_Office_Details = office.Nature_Office_Details;
                officeDTO.Location_Office_Address = office.Location_Office_Address;
                officeDTO.Country_Name = office.Country_Name;
                officeDTO.State_Name = office.State_Name;
                officeDTO.Place_Name = office.Place_Name;
                officeDTO.District_Name = office.District_Name;
                officeDTO.Location_Phone_Details = office.Location_Phone_Details;
                officeDTO.Location_Fax_Details = office.Location_Fax_Details;
                officeDTO.Location_Cell_Phone = office.Location_Cell_Phone;
                officeDTO.Location_Email_Id = office.Location_Email_Id;
                officeDTO.Labor_License_No = office.Labor_License_No;
                officeDTO.Other_License_Details = office.Other_License_Details;
                officeDTO.Country_Code = countries.Where(x => x.Country_Name == office.Country_Name).Select(x => x.Country_Code).FirstOrDefault();
                officeDTO.State_Code = states.Where(x => x.State_Name == office.State_Name).Select(x => x.State_Code).FirstOrDefault();
                officeDTO.District_Code = districts.Where(x => x.District_Name == office.District_Name).Select(x => x.District_Code).FirstOrDefault();
                officeDTO.Place_Code = places.Where(x => x.PlaceName == office.Place_Name).Select(x => x.PlaceCode).FirstOrDefault();
                officeLocations.Add(officeDTO);
            }
            return officeLocations.OrderBy(_ => _.Org_Office_Name).ToList();
        }

        public async Task UpdateOrganisationOfficeLocations(OrganisationOfficeLocationDetails officeLocation)
        {
            OrganisationOfficeLocationDetails office = await _context.OrganisationOfficeLocationDetails.FirstOrDefaultAsync(x => x.Org_Office_No == officeLocation.Org_Office_No);
            var country = await _context.Countries.AsNoTracking().FirstOrDefaultAsync(x => x.Country_Name.Equals(officeLocation.Country_Name));
            var state = await _context.States.AsNoTracking().FirstOrDefaultAsync(x => x.State_Name.Equals(officeLocation.State_Name));
            var district = await _context.Districts.AsNoTracking().FirstOrDefaultAsync(x => x.District_Name.Equals(officeLocation.District_Name));
            var place = await _context.Places.AsNoTracking().FirstOrDefaultAsync(x => x.PlaceName.Equals(officeLocation.Place_Name));
      
            if (office == null)
            {

                _context.Countries.Add(new Models.Country { Country_Name = officeLocation.Country_Name });
                _context.SaveChanges();
            }

            var countryCode = await _context.Countries.AsNoTracking().Where(x => x.Country_Name == officeLocation.Country_Name).Select(x => x.Country_Code).FirstOrDefaultAsync();

            if (state == null)
            {
                _context.States.Add(new Models.State { State_Name = officeLocation.State_Name, Country_Code = countryCode });
                await _context.SaveChangesAsync();
            }

            var stateCode = await _context.States.AsNoTracking().Where(x => x.State_Name == officeLocation.State_Name).Select(x => x.State_Code).FirstOrDefaultAsync();

            if (district == null)
            {
                _context.Districts.Add(new Models.District { District_Name = officeLocation.District_Name, State_Code = stateCode, Country_Code = countryCode });
                await _context.SaveChangesAsync();
            }

            var districtCode = await _context.Districts.AsNoTracking().Where(x => x.District_Name == officeLocation.District_Name).Select(x => x.District_Code).FirstOrDefaultAsync();

            if (place == null)
            {
                _context.Places.Add(new Models.Place { PlaceName = officeLocation.Place_Name, DistrictCode = districtCode, StateCode = stateCode, CountryCode = countryCode });
                await _context.SaveChangesAsync();
            }

            try
            {
                office.Org_Code = officeLocation.Org_Code;
                office.Org_Office_Name = officeLocation.Org_Office_Name;
                office.Nature_Office_Details = officeLocation.Nature_Office_Details;
                office.Location_Office_Address = officeLocation.Location_Office_Address;
                office.Country_Name = officeLocation.Country_Name;
                office.State_Name = officeLocation.State_Name;
                office.Place_Name = officeLocation.Place_Name;
                office.District_Name = officeLocation.District_Name;
                office.Location_Phone_Details = officeLocation.Location_Phone_Details;
                office.Location_Fax_Details = officeLocation.Location_Fax_Details;
                office.Location_Cell_Phone = officeLocation.Location_Cell_Phone;
                office.Location_Email_Id = officeLocation.Location_Email_Id;
                office.Labor_License_No = officeLocation.Labor_License_No;
                office.Other_License_Details = officeLocation.Other_License_Details;
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<OrganisationOfficeLocationDetailsDto>> GetOrganisationOfficeLocationsNames()
        {
            var _organisation = from _orgLoc in _context.OrganisationOfficeLocationDetails
                                orderby _orgLoc.Org_Office_Name
                                select new OrganisationOfficeLocationDetailsDto()
                            {
                                Org_Office_No= _orgLoc.Org_Office_No,
                                Org_Code= _orgLoc.Org_Code,
                                Org_Office_Name= _orgLoc.Org_Office_Name,
                                };
            return await _organisation.ToListAsync();
        }
    }
}