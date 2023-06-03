using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DistrictDetail = GherkinWebAPI.Models.Districts.DistrictDetail;


namespace GherkinWebAPI.Repository
{
    public class DistrictRepository : RepositoryBase<District>, IDistrictRepository
    {
        private RepositoryContext _context;

        public DistrictRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            this._context = repositoryContext;
        }

        public async Task<DistrictDetail> AddDistrict(DistrictDetail districtDetail)
        {
            try
            {
                var district = new District
                {
                    District_Code = districtDetail.DistrictCode,
                    District_Name = districtDetail.DistrictName,
                    State_Code = districtDetail.StateCode,
                    Country_Code = districtDetail.CountryCode
                };

                _context.Districts.Add(district);

                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    districtDetail.DistrictCode = district.District_Code;
                    return districtDetail;
                }
                else
                    return new DistrictDetail();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<District>> GetAllDistrictsByStateIdAsync(int stateCode)
        {
            var districts = await Task.Run(() => ((from district in _context.Districts
                                                   where district.State_Code == stateCode
                                                   select new { district.District_Code, district.District_Name })
                                                   .AsEnumerable()
                                                   .Select(c => new District
                                                   {
                                                       District_Code = c.District_Code,
                                                       District_Name = c.District_Name
                                                   }).ToList()));
            return districts;
        }

        public async Task<District> GetDistrictByIdAsync(int districtCode)
        {
            var districtDetail = await Task.Run(() => (from district in _context.Districts
                                                       where district.District_Code == districtCode
                                                       select new { district.District_Code, district.District_Name })
                                                       .AsEnumerable()
                                                   .Select(c => new District
                                                   {
                                                       District_Code = c.District_Code,
                                                       District_Name = c.District_Name
                                                   }).SingleOrDefault());
            return districtDetail;
        }
    }
}