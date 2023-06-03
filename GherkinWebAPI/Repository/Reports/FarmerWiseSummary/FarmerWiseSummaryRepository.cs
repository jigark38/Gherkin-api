using GherkinWebAPI.Core.Reports.FarmerWiseSummary;
using GherkinWebAPI.Models.Farmers;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.Reports.FarmerWiseSummary
{
    public class FarmerWiseSummaryRepository : IFarmerWiseSummaryRepository
    {
        private readonly RepositoryContext _context;

        public FarmerWiseSummaryRepository(RepositoryContext context)
        {
            this._context = context;
        }

        public async Task<List<FarmersDetail>> GetFarmersByAreaIdAndPsNumber(string areaId, string psNumber)
        {
            var farmersCodes = await _context.FarmersAgreementDetails.Where(x => x.Area_ID == areaId && x.PS_Number == psNumber).Select(x => x.Farmer_Code).ToListAsync();

            var farmersDetail = await (from f in _context.Farmers
                                       join c in _context.Countries on f.Country_Code equals c.Country_Code
                                       join s in _context.States on f.State_Code equals s.State_Code
                                       join d in _context.Districts on f.District_Code equals d.District_Code
                                       join m in _context.Mandals on f.Mandal_Code equals m.Mandal_Code
                                       join v in _context.Villages on f.Village_Code equals v.Village_Code
                                       where farmersCodes.Contains(f.Farmer_Code)
                                       select new FarmersDetail
                                       {
                                           ID = f.ID,
                                           FarmerCode = f.Farmer_Code,
                                           DateOfEntry = f.DateOfEntry,
                                           UserName = f.UserName,
                                           FarmerName = f.FarmerName,
                                           FarmerAddress = f.Farmer_Address,
                                           CountryCode = f.Country_Code,
                                           CountryName = c.Country_Name,
                                           StateCode = f.State_Code,
                                           StateName = s.State_Name,
                                           DistrictCode = f.District_Code,
                                           DistrictName = d.District_Name,
                                           MandalCode = f.Mandal_Code,
                                           MandalName = m.Mandal_Name,
                                           VillageCode = f.Village_Code,
                                           VillageName = v.Village_Name,
                                           PINCode = f.PINCode,
                                           AlternativeContactPerson = f.AlternativeContactPerson,
                                           ContactNumber = f.ContactNumber,
                                           AadharCardNo = f.AadharCardNo,
                                           NoOfAcres = f.NoOfAcres,
                                           BankName = f.BankName,
                                           BankBranch = f.BankBranch,
                                           BankAccountNo = f.BankAccountNo,
                                           BankIFSC = f.BankIFSC,
                                           ApprovedBy = f.ApprovedBy
                                       }).ToListAsync();

            return farmersDetail;
        }
    }
}