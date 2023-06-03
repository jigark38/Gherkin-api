using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
    public interface IESICService
    {
        Task<ESICRate> CreateESICRates(ESICRate esicRates);
        Task<ESICRate> UpdateESICRates(ESICRate esicRates);
        Task<List<ESICRate>> GetESICRates();
    }
}
