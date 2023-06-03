namespace GherkinWebAPI.Core
{
    using GherkinWebAPI.DTO;
    using GherkinWebAPI.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ICropRepository" />
    /// </summary>
    public interface ICropRepository
    {
        /// <summary>
        /// The AddCropGroup
        /// </summary>
        /// <param name="group">The group<see cref="CropGroup"/></param>
        /// <returns>The <see cref="Task{CropGroup}"/></returns>
        Task<CropGroup> AddCropGroup(CropGroup group);

        /// <summary>
        /// The GetAllCropGroup
        /// </summary>
        /// <returns>The <see cref="Task{List{CropGroup}}"/></returns>
        Task<List<CropGroup>> GetAllCropGroup();

        /// <summary>
        /// The AddCrop
        /// </summary>
        /// <param name="crop">The crop<see cref="Crop"/></param>
        /// <returns>The <see cref="Task{Crop}"/></returns>
        Task<Crop> AddCrop(Crop crop);

        /// <summary>
        /// The AddCropScheme
        /// </summary>
        /// <param name="cropScheme">The cropScheme<see cref="CropScheme"/></param>
        /// <returns>The <see cref="Task{CropScheme}"/></returns>
        Task<IEnumerable<CropScheme>> AddCropScheme(List<CropScheme> cropScheme);

        /// <summary>
        /// The AddCropAndScheme
        /// </summary>
        /// <param name="crop">The crop<see cref="Crop"/></param>
        /// <param name="scheme">The scheme<see cref="CropScheme"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task AddCropAndScheme(Crop crop, CropScheme scheme);

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
        Task<List<SchemeDto>> GetCropSchemes(string cropCode);

        Task<List<Crop>> GetCropListByCropGroupCode(string cropGroupCode);
    }
}
