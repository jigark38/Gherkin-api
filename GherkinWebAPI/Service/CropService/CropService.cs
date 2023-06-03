namespace GherkinWebAPI.Service
{
    using GherkinWebAPI.Core;
    using GherkinWebAPI.DTO;
    using GherkinWebAPI.Models;
    using GherkinWebAPI.Models.CropsRate;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="CropService" />
    /// </summary>
    public class CropService : ICropService
    {
        /// <summary>
        /// Defines the _repositoryWrapper
        /// </summary>
        private readonly IRepositoryWrapper _repositoryWrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CropService"/> class.
        /// </summary>
        /// <param name="repositoryWrapper">The repositoryWrapper<see cref="IRepositoryWrapper"/></param>
        public CropService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        /// <summary>
        /// The AddCropGroup
        /// </summary>
        /// <param name="group">The group<see cref="CropGroup"/></param>
        /// <returns>The <see cref="Task{CropGroup}"/></returns>
        public async Task<CropGroup> AddCropGroup(CropGroup group)
        {
            group.EntryDate = DateTime.UtcNow;
            return await _repositoryWrapper.CropRepository.AddCropGroup(group);
        }

        /// <summary>
        /// The GetAllTaskGroup
        /// </summary>
        /// <returns>The <see cref="Task{List{CropGroup}}"/></returns>
        public Task<List<CropGroup>> GetAllCropGroup()
        {
            return _repositoryWrapper.CropRepository.GetAllCropGroup();
        }

        /// <summary>
        /// The AddCropScheme
        /// </summary>
        /// <param name="cropScheme">The cropScheme<see cref="List{CropScheme}"/></param>
        /// <returns>The <see cref="Task{IEnumerable{CropScheme}}"/></returns>
        public async Task<IEnumerable<CropScheme>> AddCropScheme(List<CropScheme> cropScheme)
        {
            return await _repositoryWrapper.CropRepository.AddCropScheme(cropScheme);
        }

        /// <summary>
        /// The AddCrop
        /// </summary>
        /// <param name="crop">The crop<see cref="Crop"/></param>
        /// <returns>The <see cref="Task{Crop}"/></returns>
        public async Task<Crop> AddCrop(Crop crop)
        {
            return await _repositoryWrapper.CropRepository.AddCrop(crop);
        }

        /// <summary>
        /// The AddCropAndScheme
        /// </summary>
        /// <param name="crop">The crop<see cref="Crop"/></param>
        /// <param name="scheme">The scheme<see cref="CropScheme"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task AddCropAndScheme(Crop crop, CropScheme scheme)
        {
            await _repositoryWrapper.CropRepository.AddCropAndScheme(crop, scheme);
        }

        /// <summary>
        /// The GetAllCropRates
        /// </summary>
        /// <returns>The <see cref="Task{List{CropRateDTO}}"/></returns>
        public async Task<List<CropRateDTO>> GetAllCropRates()
        {
            return await _repositoryWrapper.CropRateRepository.GetAllCropRates();
        }

        /// <summary>
        /// The GetAllGroupName
        /// </summary>
        /// <returns>The <see cref="Task{List{GroupCode}}"/></returns>
        public async Task<List<GroupCode>> GetAllGroupName()
        {
            return await _repositoryWrapper.CropRateRepository.GetAllGroupName();
        }

        /// <summary>
        /// The GetCropNameCode
        /// </summary>
        /// <param name="groupCode">The groupCode<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{CropName}}"/></returns>
        public async Task<List<CropName>> GetCropNameCode(string groupCode)
        {
            return await _repositoryWrapper.CropRateRepository.GetCropNameCode(groupCode);
        }

        /// <summary>
        /// The GetAllAreas
        /// </summary>
        /// <returns>The <see cref="Task{List{AreaName}}"/></returns>
        public async Task<List<AreaName>> GetAllAreas()
        {
            return await _repositoryWrapper.CropRateRepository.GetAllAreas();
        }

        /// <summary>
        /// The GetAllCrops
        /// </summary>
        /// <returns>The <see cref="Task{List{Crop}}"/></returns>
        public async Task<List<Crop>> GetAllCrops()
        {
            return await _repositoryWrapper.CropRepository.GetAllCrops();
        }

        /// <summary>
        /// The SearchCrop
        /// </summary>
        /// <param name="groupCode">The groupCode<see cref="string"/></param>
        /// <param name="cropName">The cropName<see cref="string"/></param>
        /// <returns>The <see cref="Task{CropSchemeDto}"/></returns>
        public async Task<CropSchemeDto> SearchCrop(string groupCode, string cropName)
        {
            return await _repositoryWrapper.CropRepository.SearchCrop(groupCode, cropName);
        }

        /// <summary>
        /// The GetVillageCode
        /// </summary>
        /// <param name="areaId">The areaId<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{VillageName}}"/></returns>
        public async Task<List<VillageName>> GetVillageCode(string areaId)
        {
            return await _repositoryWrapper.CropRateRepository.GetVillageCode(areaId);
        }

        /// <summary>
        /// The GetSeasonFromTo
        /// </summary>
        /// <param name="CNameCode">The CNameCode<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{CropFromTo}}"/></returns>
        public async Task<List<CropFromTo>> GetSeasonFromTo(string CNameCode)
        {
            return await _repositoryWrapper.CropRateRepository.GetSeasonFromTo(CNameCode);
        }

        /// <summary>
        /// The GetCropCountMM
        /// </summary>
        /// <param name="CNameCode">The CNameCode<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{CropCount}}"/></returns>        
        public async Task<List<CropCount>> GetCropCountMM(string PS_number)
        {
            return await _repositoryWrapper.CropRateRepository.GetCropCountMM(PS_number);
        }
        public async Task<List<FruitSizeCount>> GetFruitSizeCount(decimal CropCountMM)
        {
            return await _repositoryWrapper.CropRateRepository.GetFruitSizeCount(CropCountMM);
        }

        /// <summary>
        /// The AddCropRate
        /// </summary>
        /// <param name="cropRate">The cropRate<see cref="List{CropRateDTO}"/></param>
        /// <returns>The <see cref="Task{List{CropRateDTO}}"/></returns>
        public async Task<List<CropRateDTO>> AddCropRate(List<CropRateDTO> cropRate)
        {
            return await _repositoryWrapper.CropRateRepository.AddCropRate(cropRate);
        }

        /// <summary>
        /// The FindCropRateAccordingToSeason
        /// </summary>
        /// <param name="SeasonFrom">The SeasonFrom<see cref="DateTime"/></param>
        /// <param name="SeasonTo">The SeasonTo<see cref="DateTime"/></param>
        /// <returns>The <see cref="Task{List{CropRateDTO}}"/></returns>
        public async Task<List<CropRateDTO>> FindCropRateAccordingToSeason(string psNumber)
        {
            return await _repositoryWrapper.CropRateRepository.FindCropRateAccordingToSeason(psNumber);
        }

        /// <summary>
        /// The ModifySelectedCropRate
        /// </summary>
        /// <param name="cropRate">The cropRate<see cref="CropRateDTO"/></param>
        /// <returns>The <see cref="Task{List{CropRateDTO}}"/></returns>
        public async Task<List<CropRateDTO>> ModifySelectedCropRate(CropRateDTO cropRate)
        {
            return await _repositoryWrapper.CropRateRepository.ModifySelectedCropRate(cropRate);
        }

        /// <summary>
        /// The UpDateCrop
        /// </summary>
        /// <param name="crop">The crop<see cref="Crop"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task UpDateCrop(Crop crop)
        {
            await _repositoryWrapper.CropRepository.UpDateCrop(crop);
        }

        /// <summary>
        /// The UpdateCropScheme
        /// </summary>
        /// <param name="cropScheme">The cropScheme<see cref="List{CropScheme}"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task UpdateCropScheme(List<CropScheme> cropScheme)
        {
            await _repositoryWrapper.CropRepository.UpdateCropScheme(cropScheme);
        }
        public async Task<List<string>> GetCropRateUOM(string cropRate)
        {
            return await _repositoryWrapper.CropRateRepository.GetCropRateUOM(cropRate);
        }
        public async Task<List<CropRateDTO>> DeleteSelectedCropRate(string cropRateNo)
        {
            return await _repositoryWrapper.CropRateRepository.DeleteSelectedCropRate(cropRateNo);
        }

        public async Task<CropAssociationAndUOMRate> GetCropRateByCode(string cropSchemeCode, decimal cropCountMM)
        {
            return await _repositoryWrapper.CropRateRepository.GetCropRateByCode(cropSchemeCode, cropCountMM);
        }

        public async Task<List<SchemeDto>> GetCropSchemes(string cropCode)
        {
            return await _repositoryWrapper.CropRepository.GetCropSchemes(cropCode);
        }

        public async Task<List<Crop>> GetCropListByCropGroupCode(string cropGroupCode)
		{
            return await _repositoryWrapper.CropRepository.GetCropListByCropGroupCode(cropGroupCode);
        }

        public async Task<List<VillageName>> GetAllVillageCode()
        {
            return await _repositoryWrapper.CropRateRepository.GetAllVillageCode();
        }

        public async Task<List<CropFromTo>> GetAllSeasonFromTo()
        {
            return await _repositoryWrapper.CropRateRepository.GetAllSeasonFromTo();
        }
    }
}
