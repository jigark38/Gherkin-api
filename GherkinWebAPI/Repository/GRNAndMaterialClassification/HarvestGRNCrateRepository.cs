using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using GherkinWebAPI.Core.GRNAndMaterialClassification;
using GherkinWebAPI.Models.GRNAndMaterialClassification;
using GherkinWebAPI.Persistence;

namespace GherkinWebAPI.Repository.GRNAndMaterialClassification
{
    public class HarvestGRNCrateRepository : RepositoryBase<HarvestGRNCrate>, IHarvestGRNCrateRepository
    {
        private readonly RepositoryContext _context;

        public HarvestGRNCrateRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }
        public async Task<HarvestGRNMaterialDetail> CreateHarvestGRNCrate(HarvestGRNMaterialDetail materialDetail)
        {
            int result = 0;

            foreach (var crate in materialDetail.HarvestGRNCratesDetails)
            {
                //var harvestGRNCrate = await _context.HarvestGRNCrates.OrderByDescending(h => h.HarvestGRNCratesEntryNo).FirstOrDefaultAsync();

                var harvestGRNCrateDetail = new HarvestGRNCrate
                {
                    //HarvestGRNCratesEntryNo = harvestGRNCrate == null ? 1 : harvestGRNCrate.HarvestGRNCratesEntryNo + 1,
                    HarvestGRNNo = materialDetail.HarvestGRNNo,
                    CropSchemeCode = crate.CropSchemeCode,
                    CropNameCode = crate.CropNameCode,
                    HarvestGRNCratesDespatchNos = crate.HarvestGRNCratesDespatchNos,
                    HarvestGRNTotalApproxQty = crate.HarvestGRNTotalApproxQty
                };

                _context.HarvestGRNCrates.Add(harvestGRNCrateDetail);
            }

            result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return materialDetail;
            }
            else
                return new HarvestGRNMaterialDetail();
        }
    }
}