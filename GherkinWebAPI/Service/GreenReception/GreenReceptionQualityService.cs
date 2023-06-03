using GherkinWebAPI.Core.GreenReption;
using GherkinWebAPI.Models.GreenReception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.GreenReception
{
    public class GreenReceptionQualityService : IGreenReceptionQualityService
    {
        private readonly IGreenReceptionQualityRepository _repository;

        public GreenReceptionQualityService(IGreenReceptionQualityRepository repository)
        {
            _repository = repository;
        }      

        public async Task<List<OrganisationOfficeLocUnit>> GetAllUnit()
        {
            return await _repository.GetAllUnit();
        }

        public async Task<List<GreensReceptionDetail>> GetGreenReceptionByOrgOfficeNo(int orgOfficeNo)
        {
            return await _repository.GetGreenReceptionByOrgOfficeNo(orgOfficeNo);
        }

        public async Task<List<GreenInwardsDetail>> GetInwardDetailsByOrgOfficeNo(int orgOfficeNo)
        {
            return await _repository.GetInwardDetailsByOrgOfficeNo(orgOfficeNo);
        }

        public async Task<CreateQualityCheckAndInspection> CreateQualityCheckAndInspection(CreateQualityCheckAndInspection createQCAndInspection)
        {
            return await _repository.CreateQualityCheckAndInspection(createQCAndInspection);
        }

        public async Task<CreateQualityCheckAndInspection> GetQualityCheckAndInspection(long harvestGRNNo)
        {
            return await _repository.GetQualityCheckAndInspection(harvestGRNNo);
        }
    }
}