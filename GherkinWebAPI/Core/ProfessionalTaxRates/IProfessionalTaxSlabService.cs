using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GherkinWebAPI.Models.ProfessionalTaxRates;

namespace GherkinWebAPI.Core.ProfessionalTaxRates
{
    public interface IProfessionalTaxSlabService
    {
        Task<ProfessionalTaxSlabsDetail> CreateProfessionalSlab(ProfessionalTaxSlabsDetail professionalTaxSlab);
        Task<ProfessionalTaxSlabsDetail> UpdateProfessionalSlab(long passingNo, ProfessionalTaxSlabsDetail professionalTaxSlab);
    }
}
