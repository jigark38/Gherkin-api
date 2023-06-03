using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
    public  interface IAreaRepository : IRepositoryBase<Area>
    {
        void CreateArea(Area area);
        Task GetAreaCode();

        Task<List<Area>> GetAllArea();
    }
}
