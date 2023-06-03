using GherkinWebAPI.DTO;
using GherkinWebAPI.DTO.Product_GradeDto;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
   public interface IProductDetailsService
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
