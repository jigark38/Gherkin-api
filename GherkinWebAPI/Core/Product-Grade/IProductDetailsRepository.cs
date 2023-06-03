using GherkinWebAPI.DTO.Product_GradeDto;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
    public interface IProductDetailsRepository
    {
        /// <summary>
        /// The AddProductGroup
        /// </summary>
        /// <param name="group">The group<see cref="ProductGroup"/></param>
        /// <returns>The <see cref="Task{ProductGroup}"/></returns>
        Task<ProductGroup> AddProductGroup(ProductGroup group);

        /// <summary>
        /// The GetAllTaskProductGroup
        /// </summary>
        /// <returns>The <see cref="Task{List{ProductGroup}}"/></returns>
        Task<List<ProductGroup>> GetAllProductGroupAsync();
        Task<ProductDetails> AddVariety(ProductDetails group);

        Task<List<ProductVarietyDto>> GetAllVarietyAsync(string productgroup);
        Task<GradeDetails> AddGrade(GradeDetails Ggroup);

        Task<List<GradeDto>> GetAllGradeByVarietyAsync(string Vgroup);
        Task<List<ProductGroupDto>> GetAllproductAsync();
    }
}
