using GherkinWebAPI.Core.Reports.DepAndDes;
using GherkinWebAPI.DTO.Reports.DepAndDes;
using GherkinWebAPI.Persistence;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.Reports.DepAndDes
{
    public class DepartMentAndDesignationRepository : IDepartMentAndDesignationRepository
    {
        private readonly RepositoryContext _context;

        public DepartMentAndDesignationRepository(RepositoryContext context)
        {
            _context = context;
        }
        public async Task<List<DepAndDesDto>> GetReportForDepartmentAndDesignation(string departmentCode = "")
        {
            var data = (from di in _context.Departments
                        join sd in _context.SubDepartments on di.departmentCode equals sd.departmentCode
                        join did in _context.Designations on sd.subDepartmentCode equals did.subDepartmentCode
                        join si in _context.SkillInformations on di.departmentCode equals si.departmentCode
                        select new DepAndDesDto
                        {
                            SkillsCode = si.skillsCode,
                            SkillsName = si.skillsName,
                            DepartmentCode = di.departmentCode,
                            DepartmentName = di.departMentName,
                            SubDepartmentCode = sd.subDepartmentCode,
                            SubDepartmentName = sd.subDepartmentName,
                            DesignationCode = did.departmentCode,
                            DesignationName = did.designattionName
                        });

            if (!string.IsNullOrEmpty(departmentCode))
            {
                data = data.Where(x => x.DepartmentCode == departmentCode);
            }

            return await data.ToListAsync();
        }
    }
}