using GherkinWebAPI.Core.HarvestDetails;
using GherkinWebAPI.DTO.HarvestDetails;
using GherkinWebAPI.Models.HarvestDeatils;
using GherkinWebAPI.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GherkinWebAPI.Repository.HarvestDetails
{
    /// <summary>
    /// Defines the <see cref="HarvestDetailsRepository" />.
    /// </summary>
    public class HarvestDetailsRepository : IHarvestDetailsRepository
    {
        /// <summary>
        /// Defines the _context.
        /// </summary>
        private readonly RepositoryContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="HarvestDetailsRepository"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="RepositoryContext"/>.</param>
        public HarvestDetailsRepository(RepositoryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// The AddHarvestFarmersDetails.
        /// </summary>
        /// <param name="harvestDetailsDto">The harvestDetailsDto<see cref="HarvestProcurementDetails"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task AddHarvestFarmersDetails(HarvestProcurementDetails harvestDetailsDto)
        {
            long number = 0;

            using (var trans = _context.Database.BeginTransaction())
            {
                if (_context.HarvestProcurementDetails.Any())
                {
                    number = _context.HarvestProcurementDetails.OrderByDescending(i => i.Id).FirstOrDefault().Id;
                }

                number++;
                harvestDetailsDto.HarvestProcurementNo = number;

                var procurment = _context.HarvestProcurementDetails.Add(harvestDetailsDto);
                await _context.SaveChangesAsync();
                List<HarvestGradewiseDetails> gradeWiseDetails = new List<HarvestGradewiseDetails>();

                number = 0;

                if (_context.HarvestFarmersDetails.Any())
                {
                    number = _context.HarvestFarmersDetails.OrderByDescending(i => i.Id).FirstOrDefault().Id;
                }


                foreach (var fm in harvestDetailsDto.HarvestFarmersDetails)
                {
                    var crops = fm.HarvestQuantityCratewiseDetails.Select(i => i.CropSchemeCode).Distinct().ToList();

                    
                    foreach (var i in crops)
                    {
                        number++;
                        fm.FarmerwiseTotalCrates = fm.HarvestQuantityCratewiseDetails.Where(j => j.CropSchemeCode == i).Sum(k => k.NoOfCrates);
                        fm.FarmerwiseTotalQuantity = fm.HarvestQuantityCratewiseDetails.Where(j => j.CropSchemeCode == i).Sum(k => k.CrateswiseNetWeight);
                        fm.HarvestProcurementNo = procurment.HarvestProcurementNo;
                        fm.HarvestFarmersEntryNo = number;
                        var farmDetails = _context.HarvestFarmersDetails.Add(fm);
                        await _context.SaveChangesAsync();

                        var gradeDetails = gradeWiseDetails.Where(j => j.CropSchemeCode == i).FirstOrDefault();
                        gradeDetails = gradeDetails ?? new HarvestGradewiseDetails();
                        gradeDetails.CropSchemeCode = i;
                        gradeDetails.HarvestProcurementNo = procurment.HarvestProcurementNo;
                        gradeDetails.FarmerHarvestQuantity += fm.FarmerwiseTotalQuantity;
                        gradeDetails.NoOfCrates += fm.FarmerwiseTotalCrates;
                        gradeWiseDetails.Add(gradeDetails);
                    }

                    number = 0;

                    if (_context.HarvestCratewiseDetails.Any())
                    {
                        number = _context.HarvestCratewiseDetails.OrderByDescending(i => i.Id).FirstOrDefault().Id;
                    }

                    foreach (var fg in fm.HarvestQuantityCratewiseDetails)
                    {
                        number++;
                        fg.HarvestCratewiseEntryNo = number;
                        fg.HarvestProcurementNo = procurment.HarvestProcurementNo;
                        fg.FarmerCode = fm.FarmerCode;

                        var quanCrateDetail = _context.HarvestCratewiseDetails.Add(fg);
                        await _context.SaveChangesAsync();
                    }

                    await _context.SaveChangesAsync();
                }

                if (_context.HarvestGradewiseDetails.Any())
                {
                    number = _context.HarvestGradewiseDetails.OrderByDescending(i => i.Id).FirstOrDefault().Id;
                }
                else
                {
                    number = 0;
                }

                foreach (var grade in gradeWiseDetails)
                {
                    number++;

                    grade.HarvestQuantityEntryNo = number;
                    _context.HarvestGradewiseDetails.Add(grade);
                    await _context.SaveChangesAsync();
                }

                trans.Commit();
            }
        }

        /// <summary>
        /// The GetFarmerDetails.
        /// </summary>
        /// <param name="areaId">The areaId<see cref="string"/>.</param>
        /// <param name="psNumber">The psNumber<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{List{FarmerDetailsDto}}"/>.</returns>
        public async Task<List<FarmerDetailsDto>> GetFarmerDetails(string areaId, string psNumber)
        {
            var query = (from fad in _context.FarmersAgreementDetails.Where(i => i.Area_ID == areaId && i.PS_Number == psNumber)
                         from fd in _context.Farmers.Where(f => f.Farmer_Code == fad.Farmer_Code)
                         from v in _context.Villages.Where(v => v.Village_Code == fd.Village_Code)
                         select new FarmerDetailsDto()
                         {
                             AccountNumber = fad.Farmers_Account_No,
                             AggreementCode = fad.Farmers_Agreement_Code,
                             FarmerName = fd.FarmerName,
                             NoOfAcresArea = fad.Farmers_No_of_Acres_Area,
                             VillageCode = v.Village_Code,
                             Village = v.Village_Name,
                             FarmerCode = fad.Farmer_Code
                         });

            return await query.ToListAsync();
        }
    }
}
