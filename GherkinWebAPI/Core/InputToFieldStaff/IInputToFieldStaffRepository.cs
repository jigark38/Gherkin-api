using GherkinWebAPI.DTO;
using GherkinWebAPI.Entities;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.InputToFieldStaff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.InputToFieldStaff
{
    public interface IInputToFieldStaffRepository
    {
        Task<bool> Add(Inputs_Issued_To_Fieldstaff_Materials addToFieldStaffMaterialsObj);
        Task<List<OrganisationOfficeLocationDetailsDto>> GetOrgofficelocationDetails();
        Task<List<Area>> GetAllArea();
        Task<List<EmpInfoByHarvestArea>> GetEmpInfoByAreaId(string areaId);
        Task<List<CropGroupDetailsByAreaId>> GetCropGroupDetailsByAreaId(string areaId);
        Task<List<CropDetailsByGroupCode>> GetCropDetailsByCode(string cropGroupCode);
        Task<List<PlantationSchDetailsByAreaID>> GetPlantationSchByCropNameCode(string cropNameCode);
        Task<List<HBOMMatDetailsByCropNameCode>> GetHBOMMatDetailsByCropNameCode(string cropNameCode, string psNum);
        Task<List<RM_Stock_MatDetails_A>> GetRMStockDetails_A(string transferDate, string matGroupCode, string matDetailCode);
        Task<string> GetOutwardGatePassNo();
        Task<string> GenerateMatIssueFSNo();
        Task<List<RawMaterialDetails>> GetAllRawMaterialDetails();
        Task<List<RawMaterialMaster>> GetAllRawMaterialGroups();
    }
}
