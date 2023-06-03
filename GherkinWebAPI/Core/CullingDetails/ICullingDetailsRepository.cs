using GherkinWebAPI.DTO.CullingDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.CullingDetails
{
    public interface ICullingDetailsRepository
    {
        Task<List<GradeMaterialDetails>> GetGradedMaterialDetails(int orgOfficeNo);
        Task AddCullingDetails();
    }
}
