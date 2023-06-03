using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository
{
    public class AreaRepository : RepositoryBase<Area>,IAreaRepository
    {
        private RepositoryContext _context;
        public AreaRepository(RepositoryContext repositoryContext)
           : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public void CreateArea(Area area)
        {
            _context.Areas.Add(area);
            _context.SaveChanges();
        }

        public async Task<List<Area>> GetAllArea()
        {
            return await FindAll().OrderBy(area=>area.Area_Name).ToListAsync();
        }

        public Task GetAreaCode()
        {
            throw new NotImplementedException();
        }
    }
}