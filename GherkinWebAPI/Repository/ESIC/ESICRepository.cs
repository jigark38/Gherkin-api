using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GherkinWebAPI.Core;
using System.Threading.Tasks;
using GherkinWebAPI.Persistence;
using System.Data.Entity;
using GherkinWebAPI.DTO;
using System.Runtime.InteropServices;
using System.Web.Http.Results;
using GherkinWebAPI.Models.Employee;
using System.Data.Entity.Migrations;
using Unity.Injection;

namespace GherkinWebAPI.Repository
{
    public class ESICRepository : RepositoryBase<ESICRate>, IESICRepository
    {
        private RepositoryContext _context;
        public ESICRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task<List<ESICRate>> GetESICRates()
        {
            return await _context.ESICRates.OrderByDescending(e => e.ESI_Effective_Date).ToListAsync();
        }


        public async Task<ESICRate> CreateESICRates(ESICRate esic)
        {
            try
            {
                ESICRate esicRate = new ESICRate
                {
                    Entered_Emp_ID = esic.Entered_Emp_ID,
                    Entry_Date = esic.Entry_Date,
                    ESI_Effective_Date = esic.ESI_Effective_Date,
                    ESI_Effective_From_Date = esic.ESI_Effective_From_Date,
                    ESI_Effective_To_Date = esic.ESI_Effective_To_Date,
                    ESI_Employee_Contr = esic.ESI_Employee_Contr,
                    ESI_Employer_Contr = esic.ESI_Employer_Contr,
                    ESI_Max_Limit = esic.ESI_Max_Limit,
                    ESI_Total_Contr = esic.ESI_Total_Contr,
                };

                _context.ESICRates.Add(esicRate);
                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    return esicRate;
                }
                return new ESICRate();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ESICRate> UpdateESICRates(ESICRate esic)
        {
            try
            {
                var esicRate = await _context.ESICRates.Where(c => c.ESI_Passing_No == esic.ESI_Passing_No).FirstOrDefaultAsync();
                if (esicRate != null && esic != null)
                {
                    esicRate.Entered_Emp_ID = esic.Entered_Emp_ID;
                    esicRate.Entry_Date = esic.Entry_Date;
                    esicRate.ESI_Effective_Date = esic.ESI_Effective_Date;
                    esicRate.ESI_Effective_From_Date = esic.ESI_Effective_From_Date;
                    esicRate.ESI_Effective_To_Date = esic.ESI_Effective_To_Date;
                    esicRate.ESI_Employee_Contr = esic.ESI_Employee_Contr;
                    esicRate.ESI_Employer_Contr = esic.ESI_Employer_Contr;
                    esicRate.ESI_Max_Limit = esic.ESI_Max_Limit;
                    esicRate.ESI_Total_Contr = esic.ESI_Total_Contr;

                    _context.ESICRates.AddOrUpdate(esicRate);
                    await _context.SaveChangesAsync();
                    return esicRate;
                }

                return new ESICRate();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}