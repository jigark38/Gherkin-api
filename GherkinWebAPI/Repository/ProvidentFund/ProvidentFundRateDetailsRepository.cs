using GherkinWebAPI.Core.ProvidentFund;
using GherkinWebAPI.DTO.ProvidentFund;
using GherkinWebAPI.Models.ProvidentFund;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.ProvidentFund
{
    public class ProvidentFundRateDetailsRepository : RepositoryBase<ProvidentFundRateDetails>, IProvidentFundRateDetailsRepository
    {
        private RepositoryContext _context;
        public ProvidentFundRateDetailsRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            this._context = repositoryContext;
        }
        public async Task<ProvidentFundRateDetails> AddProvidentFundRateDetail(ProvidentFundRateDetails providentFundRateDetail)
        {
            try
            {
                ProvidentFundRateDetails providentFundRate = new ProvidentFundRateDetails();
                {
                    providentFundRate.PF_Passing_No = providentFundRateDetail.PF_Passing_No;
                    providentFundRate.PF_Starting_Amount = providentFundRateDetail.PF_Starting_Amount;
                    providentFundRate.PF_Employer_EPF_Contribution = Convert.ToDecimal(providentFundRateDetail.PF_Employer_EPF_Contribution);
                    providentFundRate.PF_Employer_EPS_Contribution = Convert.ToDecimal(providentFundRateDetail.PF_Employer_EPS_Contribution);
                    providentFundRate.PF_Effective_From_Date = providentFundRateDetail.PF_Effective_From_Date;
                    providentFundRate.PF_Effective_To_Date = providentFundRateDetail.PF_Effective_To_Date;
                    providentFundRate.PF_EDLIS_Admin_Charges = Convert.ToDecimal(providentFundRateDetail.PF_EDLIS_Admin_Charges);
                    providentFundRate.Entry_Date = providentFundRateDetail.Entry_Date;
                    providentFundRate.Entered_Emp_ID = providentFundRateDetail.Entered_Emp_ID;
                    providentFundRate.EPF_Admin_Charges = Convert.ToDecimal(providentFundRateDetail.EPF_Admin_Charges);
                    providentFundRate.PF_EDLIS_Contribution = Convert.ToDecimal(providentFundRateDetail.PF_EDLIS_Contribution);
                    providentFundRate.PF_Effective_Date = providentFundRateDetail.PF_Effective_Date;
                    providentFundRate.PF_Employee_Contribution = Convert.ToDecimal(providentFundRateDetail.PF_Employee_Contribution);
                    providentFundRate.PF_EPF_Max_Limit = providentFundRateDetail.PF_EPF_Max_Limit;
                    providentFundRate.PF_Total_Employer_Contribution = Convert.ToDecimal(providentFundRateDetail.PF_Total_Employer_Contribution);
                    _context.ProvidentFundRateDetails.Add(providentFundRate);
                    await _context.SaveChangesAsync();
                }
                return providentFundRate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<List<ProvidentFundRateDetails>> GetProvidentFundRateDetails()
        {
            return (from row in _context.ProvidentFundRateDetails select row).OrderByDescending(x => x.PF_Effective_Date).ToListAsync();
        }

        public async Task<ProvidentFundRateDetails> UpdateProvidentFundRateDetail(ProvidentFundRateDetails providentFundRateDetail)
        {
            try
            {
                var providentFundRate = await _context.ProvidentFundRateDetails.Where(c => c.PF_Passing_No == providentFundRateDetail.PF_Passing_No).FirstOrDefaultAsync();
                if (providentFundRate != null)
                {
                    providentFundRate.PF_Passing_No = providentFundRateDetail.PF_Passing_No;
                    providentFundRate.PF_Starting_Amount = providentFundRateDetail.PF_Starting_Amount;
                    providentFundRate.PF_Employer_EPF_Contribution = Convert.ToDecimal(providentFundRateDetail.PF_Employer_EPF_Contribution);
                    providentFundRate.PF_Employer_EPS_Contribution = Convert.ToDecimal(providentFundRateDetail.PF_Employer_EPS_Contribution);
                    providentFundRate.PF_Effective_From_Date = providentFundRateDetail.PF_Effective_From_Date;
                    providentFundRate.PF_Effective_To_Date = providentFundRateDetail.PF_Effective_To_Date;
                    providentFundRate.PF_EDLIS_Admin_Charges = Convert.ToDecimal(providentFundRateDetail.PF_EDLIS_Admin_Charges);
                    providentFundRate.Entry_Date = providentFundRateDetail.Entry_Date;
                    providentFundRate.Entered_Emp_ID = providentFundRateDetail.Entered_Emp_ID;
                    providentFundRate.EPF_Admin_Charges = Convert.ToDecimal(providentFundRateDetail.EPF_Admin_Charges);
                    providentFundRate.PF_EDLIS_Contribution = Convert.ToDecimal(providentFundRateDetail.PF_EDLIS_Contribution);
                    providentFundRate.PF_Effective_Date = providentFundRateDetail.PF_Effective_Date;
                    providentFundRate.PF_Employee_Contribution = Convert.ToDecimal(providentFundRateDetail.PF_Employee_Contribution);
                    providentFundRate.PF_EPF_Max_Limit = providentFundRateDetail.PF_EPF_Max_Limit;
                    providentFundRate.PF_Total_Employer_Contribution = Convert.ToDecimal(providentFundRateDetail.PF_Total_Employer_Contribution);
                    _context.ProvidentFundRateDetails.AddOrUpdate(providentFundRate);
                    await _context.SaveChangesAsync();
                }
                return providentFundRate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}