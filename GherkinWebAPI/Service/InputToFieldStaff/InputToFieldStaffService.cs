using GherkinWebAPI.Core;
using GherkinWebAPI.Core.InputToFieldStaff;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Entities;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.InputToFieldStaff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.InputToFieldStaff
{
    public class InputToFieldStaffService : IInputToFieldStaffService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public InputToFieldStaffService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<bool> Add(List<Inputs_Issued_To_Fieldstaff_Materials> addToFieldStaffMaterialsObj)
        {
            bool added = false;
            foreach (var obj in addToFieldStaffMaterialsObj)
            {
                added = await _repositoryWrapper.InputToFieldStaffRepository.Add(obj);
            }

            return added;
        }

        public async Task<List<Area>> GetAllArea()
        {
            return await _repositoryWrapper.InputToFieldStaffRepository.GetAllArea();
        }
        public async Task<List<OrganisationOfficeLocationDetailsDto>> GetOrgofficelocationDetails()
        {
            return await _repositoryWrapper.InputToFieldStaffRepository.GetOrgofficelocationDetails();
        }
        public async Task<List<EmpInfoByHarvestArea>> GetEmpInfoByAreaId(string areaId)
        {
            return await _repositoryWrapper.InputToFieldStaffRepository.GetEmpInfoByAreaId(areaId);
        }

        public async Task<List<CropGroupDetailsByAreaId>> GetCropGroupDetailsByAreaId(string areaId)
        {
            return await _repositoryWrapper.InputToFieldStaffRepository.GetCropGroupDetailsByAreaId(areaId);
        }

        public async Task<List<CropDetailsByGroupCode>> GetCropDetailsByCode(string cropGroupCode)
        {
            return await _repositoryWrapper.InputToFieldStaffRepository.GetCropDetailsByCode(cropGroupCode);
        }

        public async Task<List<PlantationSchDetailsByAreaID>> GetPlantationSchByCropNameCode(string areaId)
        {
            return await _repositoryWrapper.InputToFieldStaffRepository.GetPlantationSchByCropNameCode(areaId);
        }

        public async Task<List<HBOMMatDetailsByCropNameCode>> GetHBOMMatDetailsByCropNameCode(string cropNameCode, string psNum)
        {
            return await _repositoryWrapper.InputToFieldStaffRepository.GetHBOMMatDetailsByCropNameCode(cropNameCode, psNum);

        }

        public async Task<List<RM_Stock_MatDetails_A>> GetRMStockDetails_A(string transferDate, string matGroupCode, string matDetailCode)
        {
            return await _repositoryWrapper.InputToFieldStaffRepository.GetRMStockDetails_A(transferDate, matGroupCode, matDetailCode);
        }

        public async Task<string> GetOutwardGatePassNo()
        {
            return await _repositoryWrapper.InputToFieldStaffRepository.GetOutwardGatePassNo();
        }

        public async Task<string> GenerateMatIssueFSNo()
        {
            return await _repositoryWrapper.InputToFieldStaffRepository.GenerateMatIssueFSNo();
        }

        public async Task<List<RawMaterialMaster>> GetAllRawMaterialGroups()
        {
            return await _repositoryWrapper.InputToFieldStaffRepository.GetAllRawMaterialGroups();
        }

        public async Task<List<RawMaterialDetails>> GetAllRawMaterialDetails()
        {
            return await _repositoryWrapper.InputToFieldStaffRepository.GetAllRawMaterialDetails();
        }
    }
}