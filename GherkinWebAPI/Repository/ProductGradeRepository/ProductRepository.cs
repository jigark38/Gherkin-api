using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.DTO.Product_GradeDto;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
namespace GherkinWebAPI.Repository
{
    public class ProductRepository : RepositoryBase<ProductGroup>, IProductDetailsRepository
    {
        private readonly RepositoryContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CropRepository"/> class.
        /// </summary>
        /// <param name="repositoryContext">The repositoryContext<see cref="RepositoryContext"/></param>
        public ProductRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        /// <summary>
        /// The AddProductGroup
        /// </summary>
        /// <param name="Prod">The prod<see cref="ProductGroup"/></param>
        /// <returns>The <see cref="Task{ProductGroup}"/></returns>
        public async Task<ProductGroup> AddProductGroup(ProductGroup productgroup)
        {
            try
            {
                int? selectMaxDeptId = await _context.ProductGroups.MaxAsync(e => (int?)e.ID);
                if (selectMaxDeptId != null)
                    productgroup.GroupCode = "FPG_" + Convert.ToString(selectMaxDeptId + 1);
                else
                    productgroup.GroupCode = "FPG_" + "1";

                if (productgroup.CreatedDate == null)
                {
                    productgroup.CreatedDate = DateTime.Now;
                }

                ProductGroup pg = new ProductGroup { GroupCode = productgroup.GroupCode, GrpName = productgroup.GrpName, CreatedDate = productgroup.CreatedDate };

                _context.ProductGroups.Add(pg);
                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    return pg;
                }
                return new ProductGroup();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<List<ProductGroup>> GetAllProductGroupAsync()
        {

            return await FindAll().ToListAsync();
            //throw new NotImplementedException();
        }


        public async Task<ProductDetails> AddVariety(ProductDetails variety)
        {

            try
            {
                int? selectMaxDeptId = await _context.ProductDetails.MaxAsync(e => (int?)e.ID);
                if (selectMaxDeptId != null)
                    variety.VarietyCode = "FPV_" + Convert.ToString(selectMaxDeptId + 1);
                else
                    variety.VarietyCode = "FPV_" + "1";

                ProductDetails dpt = new ProductDetails { GroupCode = variety.GroupCode, VarietyCode = variety.VarietyCode, VarietyName = variety.VarietyName, ScientificName = variety.ScientificName };
                _context.ProductDetails.Add(dpt);
                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    return dpt;
                }
                return new ProductDetails();
            }

            catch (Exception ex) { throw ex; }

        }

        public async Task<List<ProductVarietyDto>> GetAllVarietyAsync(string productgroup)
        {
            var vareities = await (from variety in _context.ProductDetails
                                   join prodgrp in _context.ProductGroups on variety.GroupCode equals prodgrp.GroupCode
                                   where variety.GroupCode == productgroup
                                   select new ProductVarietyDto
                                   {
                                       GroupCode = variety.GroupCode,
                                       VarietyCode = variety.VarietyCode,
                                       VarietyName = variety.VarietyName,
                                       ScientificName = variety.ScientificName
                                   }).Take(10).ToListAsync();
            return vareities;

        }


        public async Task<GradeDetails> AddGrade(GradeDetails grade)
        {
            try
            {
                int? selectMaxDeptId = await _context.gradeDetails.MaxAsync(e => (int?)e.ID);
                if (selectMaxDeptId != null)
                    grade.GradeCode = "FPGR_" + Convert.ToString(selectMaxDeptId + 1);
                else
                    grade.GradeCode = "FPGR_" + "1";

                GradeDetails grd = new GradeDetails { VarietyCode = grade.VarietyCode, GradeCode = grade.GradeCode, GradeFrom = grade.GradeFrom, GradeTo = grade.GradeTo };
                _context.gradeDetails.Add(grd);
                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    return grd;
                }
                return new GradeDetails();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<List<GradeDto>> GetAllGradeByVarietyAsync(string varietycode)
        {

            try
            {
                var grades = await ((from grade in _context.gradeDetails
                                     join prodvariety in _context.ProductDetails on grade.VarietyCode equals prodvariety.VarietyCode
                                     where grade.VarietyCode == varietycode
                                     select new GradeDto
                                     {
                                         VarietyCode = grade.VarietyCode,
                                         GradeCode = grade.GradeCode,
                                         GradeFrom = grade.GradeFrom,
                                         GradeTo = grade.GradeTo,
                                     }).ToListAsync());
                return grades;
            }

            catch (Exception ex) { throw ex; }
        }

        public async Task<List<ProductGroupDto>> GetAllproductAsync()
        {
            var proddetails = await ((from pgroup in _context.ProductGroups
                                      join prodvariety in _context.ProductDetails on pgroup.GroupCode equals prodvariety.GroupCode
                                      join grade in _context.gradeDetails on prodvariety.VarietyCode equals grade.VarietyCode
                                      select new ProductGroupDto
                                      {
                                          ID = pgroup.ID,
                                          GrpName = pgroup.GrpName,
                                          GroupCode = pgroup.GroupCode,
                                          VarietyName = prodvariety.VarietyName,
                                          ScintificName = prodvariety.ScientificName,
                                          GradeFromTo = grade.GradeFrom + "-" + grade.GradeTo,
                                      }).ToListAsync());
            return proddetails;

        }
    }
}