using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using contractor = GherkinWebAPI.Models.Contractor;

namespace GherkinWebAPI.Core.Contractor
{
    public interface IContractorRepository
    {
        Task<contractor> CreateContractor(contractor employee);
        Task<contractor> GetEmployeeByContractorCode(string contractorCode);
        Task<List<contractor>> GetAllContractor();
    }
}