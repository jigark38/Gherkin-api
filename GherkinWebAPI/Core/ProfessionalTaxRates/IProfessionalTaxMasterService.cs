using GherkinWebAPI.Models.ProfessionalTaxRates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.ProfessionalTaxRates
{
    public interface IProfessionalTaxMasterService
    {
        Task<ProfessionalTaxMaster> CreateProfessionalTaxMaster(ProfessionalTaxMaster professionalTaxMaster);
        Task<ProfessionalTaxMaster> UpdateProfessionalTaxMaster(long passingNo, ProfessionalTaxMaster professionalTaxMaster);
        Task<List<ProfessionalTax>> GetProfessionalTaxRates();
    }
}
