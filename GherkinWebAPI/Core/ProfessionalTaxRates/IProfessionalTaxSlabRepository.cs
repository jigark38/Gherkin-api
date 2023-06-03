using GherkinWebAPI.Models.ProfessionalTaxRates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.ProfessionalTaxRates
{
    public interface IProfessionalTaxSlabRepository
    {
        Task<ProfessionalTaxSlabsDetail> CreateProfessionalSlab(ProfessionalTaxSlabsDetail professionalTaxSlab);
        Task<ProfessionalTaxSlabsDetail> UpdateProfessionalSlab(long passingNo, ProfessionalTaxSlabsDetail professionalTaxSlab);
    }
}
