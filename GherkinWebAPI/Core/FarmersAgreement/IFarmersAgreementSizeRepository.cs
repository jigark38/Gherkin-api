using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
    public interface IFarmersAgreementSizeRepository
    {
        Task<FarmersAgreementSizeDetail> CreateAgreementSize(FarmersAgreementSizeDetail farmersAgreementSize);
        Task<FarmersAgreementSizeDetail> UpdateAgreementSize(string farmersAgreementCode, FarmersAgreementSizeDetail farmersAgreementSize);
        Task DeleteAgreementSize(string farmersAgreementCode, string cropSchemeCode);
    }
}
