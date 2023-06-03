using GherkinWebAPI.DTO;
using GherkinWebAPI.Entities;
using GherkinWebAPI.Models;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Core
{
    public interface IGradingWeightService
    {
        Task<ApiResponse<List<OrganisationOfficeLocationDetailsResponse>>> GetOrganisationOfficesLocationsDetails();
        Task<ApiResponse<List<GridOneResponse>>> GetGridOneData(int OrgofficeNo);
        Task<ApiResponse<GreensGradingInwardDetailsDTO>> SaveGreensGrading(GreensGradingInwardDetailsDTO GreensGradingInwardDetail);
        Task<ApiResponse<GreensGradingInwardDetailsDTO>> GetGreensGradingByGrdNo(int greensGrdNo);
        Task<ApiResponse<bool>> ChangeStatus(int greensGrdNo);
    }
}