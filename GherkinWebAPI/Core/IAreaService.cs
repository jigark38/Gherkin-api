using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GherkinWebAPI.Models;

namespace GherkinWebAPI.Core
{
    public interface IAreaService
    {
        Task GetAreaCode();

        Task<List<Area>> GetAllArea();

        Task CreateArea(Area area);
    }
}
