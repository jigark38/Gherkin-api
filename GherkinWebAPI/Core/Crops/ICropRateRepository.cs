using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.CropsRate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
    public interface ICropRateRepository
    {
        Task<List<CropRateDTO>> GetAllCropRates();
        Task<List<GroupCode>> GetAllGroupName();
        Task<List<CropName>> GetCropNameCode(string groupCode);
        Task<List<AreaName>> GetAllAreas();
        Task<List<VillageName>> GetVillageCode(string areaId);
        Task<List<CropFromTo>> GetSeasonFromTo(string CNameCode);
        Task<List<CropCount>> GetCropCountMM(string PS_number);
        Task<List<FruitSizeCount>> GetFruitSizeCount(decimal CropCountMM);
        Task<List<CropRateDTO>> AddCropRate(List<CropRateDTO> cropRate);
        Task<List<CropRateDTO>> FindCropRateAccordingToSeason(string psNumber);
        Task<List<CropRateDTO>> ModifySelectedCropRate(CropRateDTO cropRate);
        Task<List<string>> GetCropRateUOM(string cropRate);
        Task<List<CropRateDTO>> DeleteSelectedCropRate(string cropRateNo);
        Task<CropAssociationAndUOMRate> GetCropRateByCode(string cropSchemeCode, decimal cropCountMM);
        Task<List<VillageName>> GetAllVillageCode();
        Task<List<CropFromTo>> GetAllSeasonFromTo();
    }
}
