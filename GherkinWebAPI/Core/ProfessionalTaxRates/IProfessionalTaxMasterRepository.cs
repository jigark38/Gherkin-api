using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.ProfessionalTaxRates;

namespace GherkinWebAPI.Core.ProfessionalTaxRates
{
    public interface IProfessionalTaxMasterRepository
    {
        Task<ProfessionalTaxMaster> CreateProfessionalTaxMaster(ProfessionalTaxMaster professionalTaxMaster);
        Task<ProfessionalTaxMaster> UpdateProfessionalTaxMaster(long passingNo, ProfessionalTaxMaster professionalTaxMaster);
        Task<List<ProfessionalTax>> GetProfessionalTaxRates();
    }
}
