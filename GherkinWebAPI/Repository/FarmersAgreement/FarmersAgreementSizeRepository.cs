using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GherkinWebAPI.Repository
{
    public class FarmersAgreementSizeRepository : RepositoryBase<FarmersAgreementSizeDetail>, IFarmersAgreementSizeRepository
    {
        private RepositoryContext _context;
        public FarmersAgreementSizeRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task<FarmersAgreementSizeDetail> CreateAgreementSize(FarmersAgreementSizeDetail farmersAgreementSize)
        {
            try
            {
                _context.FarmersAgreementSizeDetails.Add(farmersAgreementSize);

                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    return farmersAgreementSize;
                }

                return new FarmersAgreementSizeDetail();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteAgreementSize(string farmersAgreementCode, string cropSchemeCode)
        {
            try
            {
                var agreementSize = await _context.FarmersAgreementSizeDetails.SingleOrDefaultAsync(a => a.Farmers_Agreement_Code == farmersAgreementCode && a.Crop_Scheme_Code == cropSchemeCode);
                _context.FarmersAgreementSizeDetails.Remove(agreementSize);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<FarmersAgreementSizeDetail> UpdateAgreementSize(string farmersAgreementCode, FarmersAgreementSizeDetail farmersAgreementSize)
        {
            try
            {
                var sizeDetail = await _context.FarmersAgreementSizeDetails.FirstOrDefaultAsync(fc => fc.Farmers_Agreement_Code == farmersAgreementCode && fc.Crop_Scheme_Code == farmersAgreementSize.Crop_Scheme_Code);

                if (sizeDetail != null)
                {
                    sizeDetail.Crop_Scheme_Code = farmersAgreementSize.Crop_Scheme_Code;
                    sizeDetail.Crop_Count_mm = farmersAgreementSize.Crop_Count_mm;
                    sizeDetail.Crop_Scheme_From = farmersAgreementSize.Crop_Scheme_From;
                    sizeDetail.Crop_Scheme_Sign = farmersAgreementSize.Crop_Scheme_Sign;
                    sizeDetail.Crop_Rate_As_per_Association = farmersAgreementSize.Crop_Rate_As_per_Association;
                    sizeDetail.Crop_Rate_Per_UOM = farmersAgreementSize.Crop_Rate_Per_UOM;
                    sizeDetail.Crop_Rate_As_per_Our_Agreement = farmersAgreementSize.Crop_Rate_As_per_Our_Agreement;
                    sizeDetail.Crop_Rates_Remarks = farmersAgreementSize.Crop_Rates_Remarks;

                    var result = await _context.SaveChangesAsync();

                    if (result == 1)
                    {
                        return farmersAgreementSize;
                    }
                    else
                        return new FarmersAgreementSizeDetail();
                }
                else
                {
                    return new FarmersAgreementSizeDetail();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}