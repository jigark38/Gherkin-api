using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.CropsRate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
    /// <summary>
    /// Defines the <see cref="ICropService" />
    /// </summary>
    public interface ICropService
    {
        /// <summary>
        /// The AddCropGroup
        /// </summary>
        /// <param name="group">The group<see cref="CropGroup"/></param>
        /// <returns>The <see cref="Task{CropGroup}"/></returns>
        Task<CropGroup> AddCropGroup(CropGroup group);

        /// <summary>
        /// The GetAllTaskGroup
        /// </summary>
        /// <returns>The <see cref="Task{List{CropGroup}}"/></returns>
        Task<List<CropGroup>> GetAllCropGroup();

        /// <summary>
        /// The AddCropScheme
        /// </summary>
        /// <param name="cropScheme">The cropScheme<see cref="CropScheme"/></param>
        /// <returns>The <see cref="Task{CropScheme}"/></returns>
        Task<IEnumerable<CropScheme>> AddCropScheme(List<CropScheme> cropScheme);

        /// <summary>
        /// The AddCrop
        /// </summary>
        /// <param name="crop">The crop<see cref="Crop"/></param>
        /// <returns>The <see cref="Task{Crop}"/></returns>
        Task<Crop> AddCrop(Crop crop);

        /// <summary>
        /// The AddCropAndScheme
        /// </summary>
        /// <param name="crop">The crop<see cref="Crop"/></param>
        /// <param name="scheme">The scheme<see cref="CropScheme"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task AddCropAndScheme(Crop crop, CropScheme scheme);

        //Task<List<CropRate>> GetAllCropRates();
        //Task<List<GroupCode>> GetAllGroupName();
        //Task<List<CropName>> GetCropNameCode(string groupCode);
        //Task<List<AreaName>> GetAllAreas();
        //Task AddCropAndScheme(Crop crop, CropScheme scheme);
        /// <summary>
        /// The GetAllCropRates
        /// </summary>
        /// <returns>The <see cref="Task{List{CropRateDTO}}"/></returns>
        Task<List<CropRateDTO>> GetAllCropRates();

        /// <summary>
        /// The GetAllGroupName
        /// </summary>
        /// <returns>The <see cref="Task{List{GroupCode}}"/></returns>
        Task<List<GroupCode>> GetAllGroupName();

        /// <summary>
        /// The GetCropNameCode
        /// </summary>
        /// <param name="groupCode">The groupCode<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{CropName}}"/></returns>
        Task<List<CropName>> GetCropNameCode(string groupCode);

        /// <summary>
        /// The GetAllAreas
        /// </summary>
        /// <returns>The <see cref="Task{List{AreaName}}"/></returns>
        Task<List<AreaName>> GetAllAreas();

        /// <summary>
        /// The GetVillageCode
        /// </summary>
        /// <param name="areaId">The areaId<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{VillageName}}"/></returns>
        Task<List<VillageName>> GetVillageCode(string areaId);

        /// <summary>
        /// The GetSeasonFromTo
        /// </summary>
        /// <param name="CNameCode">The CNameCode<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{CropFromTo}}"/></returns>
        Task<List<CropFromTo>> GetSeasonFromTo(string CNameCode);

        /// <summary>
        /// The GetCropCountMM
        /// </summary>
        /// <param name="CNameCode">The CNameCode<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{CropCount}}"/></returns>
        Task<List<CropCount>> GetCropCountMM(string PS_number);

        /// <summary>
        /// The AddCropRate
        /// </summary>
        /// <param name="cropRate">The cropRate<see cref="List{CropRateDTO}"/></param>
        /// <returns>The <see cref="Task{List{CropRateDTO}}"/></returns>
        Task<List<CropRateDTO>> AddCropRate(List<CropRateDTO> cropRate);
        Task<List<FruitSizeCount>> GetFruitSizeCount(decimal CropCountMM);

        /// <summary>
        /// The FindCropRateAccordingToSeason
        /// </summary>
        /// <param name="SeasonFrom">The SeasonFrom<see cref="DateTime"/></param>
        /// <param name="SeasonTo">The SeasonTo<see cref="DateTime"/></param>
        /// <returns>The <see cref="Task{List{CropRateDTO}}"/></returns>
        Task<List<CropRateDTO>> FindCropRateAccordingToSeason(string psNumber);

        /// <summary>
        /// The ModifySelectedCropRate
        /// </summary>
        /// <param name="cropRate">The cropRate<see cref="CropRateDTO"/></param>
        /// <returns>The <see cref="Task{List{CropRateDTO}}"/></returns>
        Task<List<CropRateDTO>> ModifySelectedCropRate(CropRateDTO cropRate);

        /// <summary>
        /// The GetAllCrops
        /// </summary>
        /// <returns>The <see cref="Task{List{Crop}}"/></returns>
        Task<List<Crop>> GetAllCrops();

        /// <summary>
        /// The SearchCrop
        /// </summary>
        /// <param name="groupCode">The groupCode<see cref="string"/></param>
        /// <param name="cropName">The cropName<see cref="string"/></param>
        /// <returns>The <see cref="Task{CropSchemeDto}"/></returns>
        Task<CropSchemeDto> SearchCrop(string groupCode, string cropName);

        /// <summary>
        /// The UpDateCrop
        /// </summary>
        /// <param name="crop">The crop<see cref="Crop"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task UpDateCrop(Crop crop);

        /// <summary>
        /// The UpdateCropScheme
        /// </summary>
        /// <param name="cropScheme">The cropScheme<see cref="List{CropScheme}"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task UpdateCropScheme(List<CropScheme> cropScheme);
        Task<List<string>> GetCropRateUOM(string cropRate);
        Task<List<CropRateDTO>> DeleteSelectedCropRate(string cropRateNo);
        Task<CropAssociationAndUOMRate> GetCropRateByCode(string cropSchemeCode, decimal cropCountMM);
        Task<List<SchemeDto>> GetCropSchemes(string cropCode);

        Task<List<Crop>> GetCropListByCropGroupCode(string cropGroupCode);
        Task<List<VillageName>> GetAllVillageCode();
        Task<List<CropFromTo>> GetAllSeasonFromTo();
    }
}
