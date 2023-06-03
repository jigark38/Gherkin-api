using GherkinWebAPI.Core;
using GherkinWebAPI.CustomExceptions;
using GherkinWebAPI.Entities;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository
{
    public class OrganisationRepository : RepositoryBase<Organisation>, IOrganisationRepository
    {
        private RepositoryContext _context;
        public OrganisationRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task CreateOrganisation(Organisation org)
        {
            _context.Organisations.Add(org);
            await _context.SaveChangesAsync();
        }

        public async Task CreateManagement(Management mngnt)
        {
            _context.Managements.Add(mngnt);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Organisation>> GetAllOrganisations()
        {
            return await _context.Organisations.ToListAsync();
        }

        public async Task<List<Management>> GetAllManagements()
        {
            return await _context.Managements.ToListAsync();
        }

        public async Task UpdateOrganisation(Organisation org)
        {
            Organisation organisation = await _context.Organisations.FirstOrDefaultAsync(x => x.Org_Code == org.Org_Code);
            if (organisation != null)
            {
                organisation.Organisation_Name = org.Organisation_Name;
                organisation.Org_Status = org.Org_Status;
                organisation.Reg_Certificate_Details = org.Reg_Certificate_Details;
                organisation.Certification_No = org.Certification_No;
                organisation.Other_Certificate_No = org.Other_Certificate_No;
                organisation.Org_Mng_Email_Id = org.Org_Mng_Email_Id;
                organisation.Website_Details = org.Website_Details;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new CustomException("Organisation doesn't exist");
            }
        }

        public async Task UpdateManagement(Management mngmt)
        {
            Management management = await _context.Managements.FirstOrDefaultAsync(x => x.Org_Mng_Code == mngmt.Org_Mng_Code);
            if (management != null)
            {
                management.Management_Designation = mngmt.Management_Designation;
                management.Management_Name = mngmt.Management_Name;
                management.Org_Mng_Contact_No = mngmt.Org_Mng_Contact_No;
                management.Org_Mng_Alt_Contact_No = mngmt.Org_Mng_Alt_Contact_No;
                management.Org_Mng_Email_Id = mngmt.Org_Mng_Email_Id;
                management.Org_Mng_Residence_Details = mngmt.Org_Mng_Residence_Details;
                management.Org_Mng_Pan_Details = mngmt.Org_Mng_Pan_Details;
                management.Org_Mng_Din_Details = mngmt.Org_Mng_Din_Details;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new CustomException("Management doesn't exist");
            }
        }

        public async Task DeleteOrganisation(int orgCode)
        {
            Organisation org = await _context.Organisations.FirstOrDefaultAsync(x => x.Org_Code == orgCode);
            OrganisationOfficeLocationDetails office = await _context.OrganisationOfficeLocationDetails.FirstOrDefaultAsync(x => x.Org_Code == orgCode);
            Management mngmt = await _context.Managements.FirstOrDefaultAsync(x => x.Org_Code == orgCode);

            if (org != null)
            {
                if (office != null)
                {
                    _context.OrganisationOfficeLocationDetails.Remove(office);
                    await _context.SaveChangesAsync();
                }


                if (mngmt != null)
                {
                    _context.Managements.Remove(mngmt);
                    await _context.SaveChangesAsync();
                }

                _context.Organisations.Remove(org);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new CustomException("Organisation doesn't exist to delete");
            }
        }

        public async Task DeleteManagement(int mngCode)
        {
            Management mngmt = await _context.Managements.FirstOrDefaultAsync(x => x.Org_Mng_Code == mngCode);
            if (mngmt != null)
            {
                _context.Managements.Remove(mngmt);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new CustomException("Management doesn't exist to delete");
            }
        }

        public async Task<List<Organisation>> GetOrganisationById(int orgCode)
        {
            try
            {
                return await _context.Organisations.Where(o => o.Org_Code == orgCode).ToListAsync();
            }
            catch (Exception ex) { throw new Exception("Org Code does not exist"); }
        }

        public async Task<List<Management>> GetManagementById(int orgCode)
        {
            try
            {
                return await _context.Managements.Where(e => e.Org_Code == orgCode).ToListAsync();
            }
            catch (Exception ex) { throw new Exception("Org Code does not exist"); }
        }

        public async Task<List<Organisation>> GetOrganisations()
        {
            return await _context.Database.SqlQuery<Organisation>("select Org_Code,Organisation_Name from Organisations").ToListAsync();      
        }

        public async Task<List<OfficeLocation>> GetLocationsbyOrgid(int orgCode)
        {
            return await _context.Database.SqlQuery<OfficeLocation>($"select Org_Code,Org_Office_No,Org_Office_Name from Organisations where org_code={orgCode}").ToListAsync();
        }
    }
}