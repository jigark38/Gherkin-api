using GherkinWebAPI.Core.Contractor;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;

namespace GherkinWebAPI.Repository.Contractor
{
    public class ContractorRepository : RepositoryBase<Models.Contractor>, IContractorRepository
    {
        private RepositoryContext _context;
        public ContractorRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }
        public async Task<Models.Contractor> CreateContractor(Models.Contractor ctor)
        {
            try
            {
                int? selectMaxCtorId = _context.Contractors.Max(e => (int?)e.Id);
                if (selectMaxCtorId != null)
                    ctor.contractorCode = "ECC_" + (Convert.ToInt16(selectMaxCtorId) + 1).ToString();
                else
                    ctor.contractorCode = "ECC_" + "1";

                _context.Contractors.Add(ctor);
                await _context.SaveChangesAsync();
                return ctor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Models.Contractor>> GetAllContractor()
        {
            return await _context.Contractors.AsNoTracking().ToListAsync();
        }

        public async Task<Models.Contractor> GetEmployeeByContractorCode(string contractorCode)
        {
            return await _context.Contractors.Where(e => e.contractorCode == contractorCode).AsNoTracking().FirstOrDefaultAsync();
        }
    }
}