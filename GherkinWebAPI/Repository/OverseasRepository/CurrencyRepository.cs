using GherkinWebAPI.Core;
using GherkinWebAPI.Core.Currencys;
using GherkinWebAPI.CustomExceptions;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.ConsigneeBuyersModel;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.OverseasRepository
{
    public class CurrecncyRepositroy : RepositoryBase<Currency>, ICurrencyRepositroy
    {
        private RepositoryContext _context;
        public CurrecncyRepositroy(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            this._context = repositoryContext;
        }
        public async Task<List<Currency>> GetAllCurrencyAsync()
        {

            return await _context.CurrenyOverseas.AsNoTracking().ToListAsync();
            //var currecnies = await Task.Run(() => ((from Curr in _context.CurrenyOverseas
            //                                             select new { Curr.Currency_Code, Curr.Currency_Name })
            //                                       .AsEnumerable()
            //                                       .Select(c => new Currency
            //                                       {
            //                                           Currency_Name = c.Currency_Name,
            //                                           Currency_Code = c.Currency_Code
            //                                       }).ToList()));
            //return currecnies;
        }

        public async Task<Currency> GetCurrencyByIdAsync(string currencyCode)
        {
            var currecnies = await (from Curr in _context.CurrenyOverseas
                                    where Curr.Currency_Code == currencyCode
                                    select Curr
                                                       ).AsNoTracking().FirstOrDefaultAsync();
            return currecnies;
        }



        public async Task<Currency> AddCurrencyIfntAsync(Currency currencys)
        {
            Currency obj = null;
            var res = await this._context.CurrenyOverseas.FirstOrDefaultAsync(x => x.Currency_Name == currencys.Currency_Name);
            if (res == null)
            {

                int? selectMaxDeptId = await _context.CurrenyOverseas.MaxAsync(e => (int?)e.ID);
                if (selectMaxDeptId != null)
                    currencys.Currency_Code = "INR_" + Convert.ToString(selectMaxDeptId + 1);
                else
                    currencys.Currency_Code = "INR_" + "1";

                Currency curr = new Currency { Currency_Code = currencys.Currency_Code, Currency_Name = currencys.Currency_Name };
                _context.CurrenyOverseas.Add(curr);
                var result = await _context.SaveChangesAsync();

                obj = await (from c in _context.CurrenyOverseas
                             where c.Currency_Name == currencys.Currency_Name
                             select c
                                                           ).FirstOrDefaultAsync();
                return obj;
            }
            else
            {
                throw new CustomException("Currency Is already Exits");
            }

        }
    }
}