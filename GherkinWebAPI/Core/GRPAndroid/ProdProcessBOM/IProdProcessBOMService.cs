using GherkinWebAPI.Models.GRPAndroid.ProdProcessBOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.GRPAndroid.ProdProcessBOM
{
    public interface IProdProcessBOMService
    {
        Task<List<ProdGroupCode>> GetAllProductGroup();
        Task<List<RawMaterialGroupModel>> GetRawMaterialGroup();
        Task<List<GradeModel>> GetGradeDetails(string varCode);
        Task<List<VarietyModel>> GetVariety(string grpCode);
        Task<List<RawMaterialDetailModel>> GetRawMaterialDetails(string rawMaterialGrpCode);
        Task<ProductionProcessDetailsModel> SaveProductionProcess(ProductionProcessDetails prodProcessDetails);
        Task<ProdProcessCombinedModel> SaveProductionProcessBOM(ProdProcessCombinedModel prodProcessCombine);
        Task<List<string>> GetProductionUOM(string uomKey);
        Task<List<string>> GetMaterialUOM(string uomKey);
        Task<List<ProdGroupCode>> GetAllSavedProductGroup();
        Task<List<VarietyModel>> GetSavedVariety(string grpCode);
        Task<List<ProductionProcessDetails>> FetchProdProcess(string grpCode, string variety);
    }
}
