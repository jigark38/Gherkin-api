using GherkinWebAPI.Core.ProfessionalTaxRates;
using GherkinWebAPI.Models.ProfessionalTaxRates;
using System;
using System.Linq;
using System.Threading.Tasks;
using GherkinWebAPI.Persistence;
using System.Data.Entity;
using System.Collections.Generic;

namespace GherkinWebAPI.Repository.ProfessionalTaxRates
{
    public class ProfessionalTaxMasterRepository : RepositoryBase<ProfessionalTaxMaster>, IProfessionalTaxMasterRepository
    {
        private RepositoryContext _context;
        public ProfessionalTaxMasterRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }
        public async Task<ProfessionalTaxMaster> CreateProfessionalTaxMaster(ProfessionalTaxMaster professionalTaxMaster)
        {
            try
            {
                ProfessionalTaxMaster professionalTax = new ProfessionalTaxMaster
                {
                    PTPassingNo = professionalTaxMaster.PTPassingNo,
                    EntryDate = professionalTaxMaster.EntryDate,
                    EnteredEmpID = professionalTaxMaster.EnteredEmpID,
                    PTEffectiveDate = professionalTaxMaster.PTEffectiveDate,
                    PTChallanPaymentDate = professionalTaxMaster.PTChallanPaymentDate,
                    PTDirectorsPayable = professionalTaxMaster.PTDirectorsPayable,
                    PTExemptedTillSalary = professionalTaxMaster.PTExemptedTillSalary
                };

                _context.ProfessionalTaxMasters.Add(professionalTax);

                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    return professionalTax;
                }

                return new ProfessionalTaxMaster();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProfessionalTax>> GetProfessionalTaxRates()
        {

            var professionalTax = (from ptm in _context.ProfessionalTaxMasters
                                   select new ProfessionalTax
                                   {
                                       PTPasssingNo = ptm.PTPassingNo,
                                       DateofEntry = ptm.EntryDate,
                                       LoginUserName = ptm.EnteredEmpID,
                                       EffectiveDate = ptm.PTEffectiveDate,
                                       MonthlyChallanDate = ptm.PTChallanPaymentDate,
                                       DirectorsPayable = ptm.PTDirectorsPayable,
                                       TaxAmountExemptedSalary = ptm.PTExemptedTillSalary,
                                       ProfessionalTaxSlabs = ptm.ProfessionalTaxSlabsDetails.Select(ps => new ProfessionalTaxSlab
                                       {
                                           PTPassingSlabID = ps.PTSalarySlabID,
                                           SalaryFrom = ps.PTSalaryFrom,
                                           SalaryTo = ps.PTSalaryTo,
                                           ProfessionalTaxAmount = ps.PTEmployeesAmountPayable
                                       }).ToList()
                                   }).OrderByDescending(ed => ed.EffectiveDate).ToListAsync();

            return await professionalTax;
        }

        public async Task<ProfessionalTaxMaster> UpdateProfessionalTaxMaster(long passingNo, ProfessionalTaxMaster professionalTaxMaster)
        {
            try
            {
                var professionalTax = await _context.ProfessionalTaxMasters.SingleOrDefaultAsync(pt => pt.PTPassingNo == passingNo);

                if (professionalTax != null)
                {
                    professionalTax.EntryDate = professionalTaxMaster.EntryDate;
                    professionalTax.EnteredEmpID = professionalTaxMaster.EnteredEmpID;
                    professionalTax.PTChallanPaymentDate = professionalTaxMaster.PTChallanPaymentDate;
                    professionalTax.PTDirectorsPayable = professionalTaxMaster.PTDirectorsPayable;
                    professionalTax.PTExemptedTillSalary = professionalTaxMaster.PTExemptedTillSalary;
                    professionalTax.PTEffectiveDate = professionalTaxMaster.PTEffectiveDate;

                    var result = await _context.SaveChangesAsync();

                    if (result == 1)
                    {
                        return professionalTaxMaster;
                    }
                    return new ProfessionalTaxMaster();
                }

                return new ProfessionalTaxMaster();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}