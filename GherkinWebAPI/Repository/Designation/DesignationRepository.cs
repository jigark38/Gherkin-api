using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;

namespace GherkinWebAPI.Repository
{
    public class DesignationRepository : RepositoryBase<Designation>, IDesignationRepository
    {
        private RepositoryContext _context;
        public DesignationRepository(RepositoryContext repositoryContext)
           : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task<Designation> CreateDesignation(Designation designation)
        {
            try
            {
                int? selectMaxDegId = await _context.Designations.MaxAsync(e => (int?)e.Id);
                if (selectMaxDegId != null)
                    designation.designationCode = "SDDC_" + Convert.ToString(selectMaxDegId + 1);
                else
                    designation.designationCode = "SDDC_" + "1";

                Designation deg = new Designation
                {
                    designationCode = designation.designationCode,
                    designattionName = designation.designattionName,
                    departmentCode = designation.departmentCode,
                    subDepartmentCode = designation.subDepartmentCode
                };

                _context.Designations.Add(deg);
                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    return deg;
                }
                return new Designation();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<Designation> UpdateDesignation(Designation designation)
        {
            try
            {
                if (designation != null)
                {
                    var selectedDesignation = await _context.Designations.SingleOrDefaultAsync(a => a.designationCode == designation.designationCode);
                    if (selectedDesignation != null)
                    {
                        selectedDesignation.designattionName = designation.designattionName;
                        var res = await _context.SaveChangesAsync();
                        if (res == 1)
                        {
                            return designation;
                        }
                    }
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<Designation>> GetAllDesignations()
        {
            return await _context.Designations.AsNoTracking().ToListAsync();
        }
        public async Task<List<Designation>> GetDesignationsByCondition(string subDepartment)
        {
            return await FindByCondition(d => d.subDepartmentCode == subDepartment).AsNoTracking().ToListAsync();
        }
    }
}