using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;

namespace GherkinWebAPI.Repository
{
    public class SubDepartmentRepository : RepositoryBase<SubDepartment>, ISubDepartmentRepository
    {
        private RepositoryContext _context;
        public SubDepartmentRepository(RepositoryContext repositoryContext)
           : base(repositoryContext)
        {
            _context = repositoryContext;
        }
        public async Task<SubDepartment> CreateSubDepartment(SubDepartment subdepartment)
        {
            try
            {
                int? selectMaxSubDeptId = await _context.SubDepartments.MaxAsync(e => (int?)e.Id);
                if (selectMaxSubDeptId != null)
                    subdepartment.subDepartmentCode = "SDC_" + Convert.ToString(selectMaxSubDeptId + 1);
                else
                    subdepartment.subDepartmentCode = "SDC_" + "1";

                SubDepartment dpt = new SubDepartment
                {
                    subDepartmentCode = subdepartment.subDepartmentCode,
                    subDepartmentName = subdepartment.subDepartmentName,
                    departmentCode = subdepartment.departmentCode
                };
                _context.SubDepartments.Add(dpt);
                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    return dpt;
                }
                return new SubDepartment();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<SubDepartment> UpdateSubDepartment(SubDepartment subdepartment)
        {
            try
            {

                if (subdepartment != null)
                {
                    var selectedSubDepartmentCode = await _context.SubDepartments.SingleOrDefaultAsync(a => a.subDepartmentCode == subdepartment.subDepartmentCode);
                    if (selectedSubDepartmentCode != null)
                    {
                        selectedSubDepartmentCode.subDepartmentName = subdepartment.subDepartmentName;
                        var result = await _context.SaveChangesAsync();
                        if (result == 1)
                        {
                            return subdepartment;
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
        public async Task<List<SubDepartment>> GetSubDepartments()
        {
            return await _context.SubDepartments.AsNoTracking().ToListAsync();
        }

        public async Task<List<SubDepartment>> GetSubDepartmentsByCondition(string department)
        {
            return await FindByCondition(subDepartment => subDepartment.departmentCode == department).AsNoTracking().ToListAsync();
        }
    }
}