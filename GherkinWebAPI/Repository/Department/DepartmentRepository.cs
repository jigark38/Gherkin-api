using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;

namespace GherkinWebAPI.Repository
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(RepositoryContext repositoryContext)
           : base(repositoryContext)
        {
            _context = repositoryContext;
        }
        private RepositoryContext _context;


        public async Task<Department> CreateDepartment(Department department)
        {
            try
            {
                int? selectMaxDeptId = await _context.Departments.MaxAsync(e => (int?)e.Id);
                if (selectMaxDeptId != null)
                    department.departmentCode = "DC_" + Convert.ToString(selectMaxDeptId + 1);
                else
                    department.departmentCode = "DC_" + "1";

                Department dpt = new Department { departmentCode = department.departmentCode, departMentName = department.departMentName };
                _context.Departments.Add(dpt);
                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    return dpt;
                }
                return new Department();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<Department> UpdateDeparment(Department department)
        {
            try
            {
                if (department != null)
                {
                    var selectedDepartment = await _context.Departments.SingleOrDefaultAsync(a => a.departmentCode == department.departmentCode);
                    if (selectedDepartment != null)
                    {
                        selectedDepartment.departMentName = department.departMentName;
                        var result = await _context.SaveChangesAsync();
                        if (result == 1)
                        {
                            return department;
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

        public async Task<List<Department>> GetDepartments()
        {
            return await _context.Departments.AsNoTracking().OrderBy(s => s.departMentName).ToListAsync();
        }

        public async Task<List<Department>> GetDepartmentsByOrganiation(int orgOfficeNo)
        {
            var departmentCodes = await _context.Employees.Where(org => org.orgOfficeNo == orgOfficeNo).Select(dc => dc.departmentCode).Distinct().ToListAsync();

            return await _context.Departments.Where(d => departmentCodes.Contains(d.departmentCode)).ToListAsync();
        }
    }
}