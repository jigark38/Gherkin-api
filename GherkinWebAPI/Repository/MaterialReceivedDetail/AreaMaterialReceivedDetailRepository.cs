using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GherkinWebAPI.Repository
{
    public class AreaMaterialReceivedDetailRepository : RepositoryBase<AreaMaterialReceivedDetailsModel>, IAreaMaterialReceivedRepository
    {
        private RepositoryContext _context;
        public AreaMaterialReceivedDetailRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            this._context = repositoryContext;
        }

        public async Task<AreaMaterialReceivedDetailsModel> CreateAreaDetails(AreaMaterialReceivedDetailsModel areaMaterialReceivedDetailsModel)
        {
            try
            {
                var result = _context.AreaMaterialReceivedDetails.Add(areaMaterialReceivedDetailsModel);
                await _context.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<AreaMaterialReceivedDetailsModel> GetAllAsync()
        {
            try
            {
                return _context.AreaMaterialReceivedDetails.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Area> GetHarvestAreaDetails()
        {
            return _context.Areas.OrderBy(a => a.Area_Name).ToList();
        }

        public async Task<AreaMaterialReceivedDetailsModel> UpdateAreaDetails(int id, AreaMaterialReceivedDetailsModel areaMaterialReceivedDetailsModel)
        {
            try
            {
                AreaMaterialReceivedDetailsModel areameti = new AreaMaterialReceivedDetailsModel();
                areameti = await _context.AreaMaterialReceivedDetails.FirstOrDefaultAsync(x => x.Area_MR_No == id);

                if (areameti != null)
                {
                    // update all required details
                    areameti.MR_Details_AG_No = areaMaterialReceivedDetailsModel.MR_Details_AG_No;
                    await _context.SaveChangesAsync();
                    return areameti;
                }
                else
                {
                    throw new Exception("Invalid Details");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}