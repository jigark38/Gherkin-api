using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service
{
    public class SkillInformationService : ISkillInformationService
    {

        private readonly IRepositoryWrapper _repositoryWrapper;
        public SkillInformationService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<SkillInformation> CreateSkillInformation(SkillInformation skillInfo)
        {
            return await _repositoryWrapper.SkillInformationRepository.CreateSkillInformation(skillInfo);
        }

        public async Task<SkillInformation> UpdateSkillInformation(SkillInformation skillIndo)
        {
            return await _repositoryWrapper.SkillInformationRepository.UpdateSkillInformation(skillIndo);
        }
        public async Task<List<SkillInformation>> GetAllSkillsInformation()
        {
            return await _repositoryWrapper.SkillInformationRepository.GetAllSkillsInformation();
        }
    }
}