using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;

namespace GherkinWebAPI.Repository
{
    public class SkillsInformationRepository : RepositoryBase<SkillInformation>, ISkillsInformationRepository
    {
        private RepositoryContext _context;
        public SkillsInformationRepository(RepositoryContext repositoryContext)
           : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task<SkillInformation> CreateSkillInformation(SkillInformation skillInfo)
        {
            try
            {
                int? selectMaxSkillInfoId = await _context.SkillInformations.MaxAsync(e => (int?)e.Id);
                if (selectMaxSkillInfoId != null)
                    skillInfo.skillsCode = "SKC_" + (Convert.ToInt16(selectMaxSkillInfoId) + 1).ToString();
                else
                    skillInfo.skillsCode = "SKC_" + "1";

                SkillInformation skf = new SkillInformation
                {
                    skillsCode = skillInfo.skillsCode,
                    skillsName = skillInfo.skillsName,
                    departmentCode = skillInfo.departmentCode
                };

                _context.SkillInformations.Add(skf);
                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    return skf;
                }
                return new SkillInformation();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<SkillInformation> UpdateSkillInformation(SkillInformation skillInfo)
        {
            try
            {
                if (skillInfo != null)
                {
                    var selectedSkill = await _context.SkillInformations.SingleOrDefaultAsync(a => a.skillsCode == skillInfo.skillsCode);
                    if (selectedSkill != null)
                    {
                        selectedSkill.skillsName = skillInfo.skillsName;
                        var res = await _context.SaveChangesAsync();
                        if (res == 1)
                        {
                            return skillInfo;
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
        public async Task<List<SkillInformation>> GetAllSkillsInformation()
        {
            return await _context.SkillInformations.AsNoTracking().ToListAsync();
        }
    }
}