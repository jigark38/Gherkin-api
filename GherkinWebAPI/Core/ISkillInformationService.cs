using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
    public interface ISkillInformationService
    {
        Task<List<SkillInformation>> GetAllSkillsInformation();
        Task<SkillInformation> CreateSkillInformation(SkillInformation skillInfo);
        Task<SkillInformation> UpdateSkillInformation(SkillInformation skillIndo);
    }
}
