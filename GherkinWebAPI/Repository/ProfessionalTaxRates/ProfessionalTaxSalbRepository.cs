using GherkinWebAPI.Core.ProfessionalTaxRates;
using GherkinWebAPI.Models.ProfessionalTaxRates;
using GherkinWebAPI.Persistence;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace GherkinWebAPI.Repository.ProfessionalTaxRates
{
    public class ProfessionalTaxSalbRepository : RepositoryBase<ProfessionalTaxSlabsDetail>, IProfessionalTaxSlabRepository
    {
        private RepositoryContext _context;
        public ProfessionalTaxSalbRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task<ProfessionalTaxSlabsDetail> CreateProfessionalSlab(ProfessionalTaxSlabsDetail professionalTaxSlab)
        {
            try
            {
                _context.ProfessionalTaxSlabsDetails.Add(professionalTaxSlab);

                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    return professionalTaxSlab;
                }

                return new ProfessionalTaxSlabsDetail();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProfessionalTaxSlabsDetail> UpdateProfessionalSlab(long passingNo, ProfessionalTaxSlabsDetail professionalTaxSlab)
        {
            try
            {
                var slabDetail = await _context.ProfessionalTaxSlabsDetails.SingleOrDefaultAsync(sb => sb.PTPassingNo == passingNo && sb.PTSalarySlabID == professionalTaxSlab.PTSalarySlabID);

                if (slabDetail != null)
                {
                    slabDetail.PTSalaryFrom = professionalTaxSlab.PTSalaryFrom;
                    slabDetail.PTSalaryTo = professionalTaxSlab.PTSalaryTo;
                    slabDetail.PTEmployeesAmountPayable = professionalTaxSlab.PTEmployeesAmountPayable;

                    var result = await _context.SaveChangesAsync();

                    if (result == 1)
                    {
                        return professionalTaxSlab;
                    }

                    return new ProfessionalTaxSlabsDetail();
                }

                return new ProfessionalTaxSlabsDetail();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}