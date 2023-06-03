using GherkinWebAPI.Core;
using GherkinWebAPI.Core.GRPAndroid.ProdProcessBOM;
using GherkinWebAPI.Models.GRPAndroid.ProdProcessBOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.GRPAndroid.ProdProcessBOM
{
    public class ProdProcessBOMService: IProdProcessBOMService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public ProdProcessBOMService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<List<ProdGroupCode>> GetAllProductGroup()
        {
           return await _repositoryWrapper.ProdProcessBOMRepository.GetAllProductGroup();
        }
        public async Task<List<RawMaterialGroupModel>> GetRawMaterialGroup()
        {
            return await _repositoryWrapper.ProdProcessBOMRepository.GetRawMaterialGroup();
        }
        public async Task<List<GradeModel>> GetGradeDetails(string varCode)
        {
            return await _repositoryWrapper.ProdProcessBOMRepository.GetGradeDetails(varCode);
        }
        public async Task<List<VarietyModel>> GetVariety(string grpCode)
        {
            return await _repositoryWrapper.ProdProcessBOMRepository.GetVariety(grpCode);
        }
        public async Task<List<RawMaterialDetailModel>> GetRawMaterialDetails(string rawMaterialGrpCode)
        {
            return await _repositoryWrapper.ProdProcessBOMRepository.GetRawMaterialDetails(rawMaterialGrpCode);
        }
        public async Task<ProductionProcessDetailsModel> SaveProductionProcess(ProductionProcessDetails prodProcessDetails)
        {
            return await _repositoryWrapper.ProdProcessBOMRepository.SaveProductionProcess(prodProcessDetails);
        }
        public async Task<ProdProcessCombinedModel> SaveProductionProcessBOM(ProdProcessCombinedModel prodProcessCombine)
        {
            return await _repositoryWrapper.ProdProcessBOMRepository.SaveProductionProcessBOM(prodProcessCombine);

        }
        public async Task<List<string>> GetProductionUOM(string uomKey)
        {
            return await _repositoryWrapper.ProdProcessBOMRepository.GetProductionUOM(uomKey);
        }
        public async Task<List<string>> GetMaterialUOM(string uomKey)
        {
            return await _repositoryWrapper.ProdProcessBOMRepository.GetMaterialUOM(uomKey);
        }
        public async Task<List<ProdGroupCode>> GetAllSavedProductGroup()
        {
            return await _repositoryWrapper.ProdProcessBOMRepository.GetAllSavedProductGroup();
        }
        public async Task<List<VarietyModel>> GetSavedVariety(string grpCode)
        {
            return await _repositoryWrapper.ProdProcessBOMRepository.GetSavedVariety(grpCode);
        }
        public async Task<List<ProductionProcessDetails>> FetchProdProcess(string grpCode, string variety)
        {
            return await _repositoryWrapper.ProdProcessBOMRepository.FetchProdProcess(grpCode,variety);
        }
    }
}