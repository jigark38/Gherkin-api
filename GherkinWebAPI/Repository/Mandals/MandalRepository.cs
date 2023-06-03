using GherkinWebAPI.Core.Mandals;
using GherkinWebAPI.Models.Mandals;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository.Mandals
{
    public class MandalRepository : RepositoryBase<Mandal>, IMandalRepository
    {
        private readonly RepositoryContext _context;

        public MandalRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }
        public async Task<MandalDetail> AddMandal(MandalDetail mandalDetail)
        {
            try
            {
                var mandal = new Mandal
                {
                    Mandal_Code = mandalDetail.MandalCode,
                    Mandal_Name = mandalDetail.MandalName,
                    District_Code = mandalDetail.DistrictCode,
                    State_Code = mandalDetail.StateCode,
                    Country_Code = mandalDetail.CountryCode
                };

                _context.Mandals.Add(mandal);

                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    mandalDetail.MandalCode = mandal.Mandal_Code;
                    return mandalDetail;
                }
                else
                    return new MandalDetail();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<MandalDetail>> GetAllMandalsAysnc()
        {
            var mandals = (from m in _context.Mandals
                           select new MandalDetail
                           {
                               MandalCode = m.Mandal_Code,
                               MandalName = m.Mandal_Name,
                               CountryCode = m.Country_Code,
                               DistrictCode = m.District_Code,
                               StateCode = m.State_Code
                           }).ToListAsync();

            return await mandals;
        }

        public async Task<List<MandalDetail>> GetMandalByDistrictCodeAsync(int districtCode)
        {
            var mandals = (from m in _context.Mandals
                           where m.District_Code == districtCode
                           select new MandalDetail
                           {
                               MandalCode = m.Mandal_Code,
                               MandalName = m.Mandal_Name,
                               CountryCode = m.Country_Code,
                               DistrictCode = m.District_Code,
                               StateCode = m.State_Code
                           }).ToListAsync();

            return await mandals;
        }
    }
}