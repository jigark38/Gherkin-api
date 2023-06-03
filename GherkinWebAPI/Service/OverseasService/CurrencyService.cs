using GherkinWebAPI.Core.Currencys;
using GherkinWebAPI.Models.ConsigneeBuyersModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Service.OverseasService
{
    public class CurrencyService : ICurrencyService
    {
        public ICurrencyRepositroy _repository { get; }

        public CurrencyService(ICurrencyRepositroy repository)
        {
            this._repository = repository;
        }
        public Task<List<Currency>> GetAllCurrencyAsync()
        {
            return _repository.GetAllCurrencyAsync();
        }

        public Task<Currency> GetCurrencyByIdAsync(string currencyCode)
        {
            return _repository.GetCurrencyByIdAsync(currencyCode);
        }

        public Task<Currency> AddCurrencyIfntAsync(Currency currency)
        {
            return _repository.AddCurrencyIfntAsync(currency);
        }


    }
}