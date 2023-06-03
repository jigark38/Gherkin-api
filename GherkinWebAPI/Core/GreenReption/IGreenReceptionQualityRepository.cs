using GherkinWebAPI.Models.GreenReception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.GreenReption
{
    public interface IGreenReceptionQualityRepository
    {
        Task<List<OrganisationOfficeLocUnit>> GetAllUnit();
        Task<List<GreenInwardsDetail>> GetInwardDetailsByOrgOfficeNo(int orgOfficeNo);
        Task<List<GreensReceptionDetail>> GetGreenReceptionByOrgOfficeNo(int orgOfficeNo);
        Task<CreateQualityCheckAndInspection> CreateQualityCheckAndInspection(CreateQualityCheckAndInspection createQCAndInspection);
        Task<CreateQualityCheckAndInspection> GetQualityCheckAndInspection(long harvestGRNNo);
    }
}
