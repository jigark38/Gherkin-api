using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.DTO.Product_GradeDto;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service
{
    public class ProductService : IProductDetailsService
    {
        public IProductDetailsRepository _repository { get; }

        /// <summary>-Gr
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public ProductService(IProductDetailsRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// The AddCropGroup
        /// </summary>
        /// <param name="group">The group<see cref="ProductGroup"/></param>
        /// <returns>The <see cref="Task{ProductGroup}"/></returns>
        public async Task<ProductGroup> AddProductGroup(ProductGroup prdgroup)
        {
             return await _repository.AddProductGroup(prdgroup);
        }
        ///// <summary>
        ///// The GetAllTaskGroup
        ///// </summary>
        ///// <returns>The <see cref="Task{List{ProductGroup}}"/></returns>
        public async Task<List<ProductGroup>> GetAllProductGroupAsync()
        {
            return await _repository.GetAllProductGroupAsync();
        }

        public async Task<ProductDetails> AddVariety(ProductDetails variety)
        {
            
            return await _repository.AddVariety(variety);
        }

        public async Task<List<ProductVarietyDto>> GetAllVarietyAsync(string prodcode)
        {
            return await _repository.GetAllVarietyAsync(prodcode);
        }

        public async Task<GradeDetails> AddGrade(GradeDetails grade)
        {

            return await _repository.AddGrade(grade);
        }

        public async Task<List<GradeDto>> GetAllGradeByVarietyAsync(string vcode)
        {
            return await _repository.GetAllGradeByVarietyAsync(vcode);
        }

        public async Task<List<ProductGroupDto>> GetAllproductAsync()
        {
            return await _repository.GetAllproductAsync();
        }

    }
}