using GherkinWebAPI.Models.ConsigneeBuyersModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.Currencys
{
   public interface ICurrencyRepositroy
    {
        Task<List<Currency>> GetAllCurrencyAsync();
        Task<Currency> GetCurrencyByIdAsync(string currencycode);
        Task<Currency> AddCurrencyIfntAsync(Currency country);
    }
}
