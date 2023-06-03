using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Entities;
using GherkinWebAPI.Models;
using GherkinWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service
{
    public class GradingWeightService : IGradingWeightService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public GradingWeightService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<ApiResponse<List<GridOneResponse>>> GetGridOneData(int OrgofficeNo)
        {
            return await _repositoryWrapper.GradingWeightRepository.GetGridOneData(OrgofficeNo);
        }
        public async Task<ApiResponse<List<OrganisationOfficeLocationDetailsResponse>>> GetOrganisationOfficesLocationsDetails()
        {
            return await _repositoryWrapper.GradingWeightRepository.GetOrganisationOfficesLocationsDetails();
        }
        public async Task<ApiResponse<GreensGradingInwardDetailsDTO>> SaveGreensGrading(GreensGradingInwardDetailsDTO GreensGradingInwardDetail)
        {
            return await _repositoryWrapper.GradingWeightRepository.SaveGreensGrading(GreensGradingInwardDetail);
        }
        public async Task<ApiResponse<GreensGradingInwardDetailsDTO>> GetGreensGradingByGrdNo(int greensGrdNo)
        {
            return await _repositoryWrapper.GradingWeightRepository.GetGreensGradingByGrdNo(greensGrdNo);
        }
        public async Task<ApiResponse<bool>> ChangeStatus(int greensGrdNo)
        {
            return await _repositoryWrapper.GradingWeightRepository.ChangeStatus(greensGrdNo);
        }
    }
}