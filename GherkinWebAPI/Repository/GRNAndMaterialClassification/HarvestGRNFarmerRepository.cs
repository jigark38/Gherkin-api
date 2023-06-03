using GherkinWebAPI.Core.GRNAndMaterialClassification;
using GherkinWebAPI.Models.GRNAndMaterialClassification;
using GherkinWebAPI.Persistence;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace GherkinWebAPI.Repository.GRNAndMaterialClassification
{
    public class HarvestGRNFarmerRepository : RepositoryBase<HarvestGRNFarmer>, IHarvestGRNFarmerRepository
    {
        private readonly RepositoryContext _context;

        public HarvestGRNFarmerRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }
        public async Task<HarvestGRNMaterialDetail> CreateHarvestGRNFarmer(HarvestGRNMaterialDetail materialDetail)
        {
            int result = 0;

            foreach (var fd in materialDetail.HarvestGRNFarmersDetails)
            {
                //var harvestGRNFarmer = await _context.HarvestGRNFarmers.OrderByDescending(h => h.HarvestGRNFarmerEntryNo).FirstOrDefaultAsync();

                var harvestGRNFarmerDetail = new HarvestGRNFarmer
                {
                    //HarvestGRNFarmerEntryNo = harvestGRNFarmer == null ? 1 : harvestGRNFarmer.HarvestGRNFarmerEntryNo + 1,
                    HarvestGRNNo = materialDetail.HarvestGRNNo,
                    GreensFarmersEntryNo = fd.HarvestFarmerEntryNo,
                    CropNameCode = fd.CropNameCode,
                    CropSchemeCode = fd.CropSchemeCode,
                    NoofCrates = fd.NoOfCrates,
                    FarmerWiseTotalQuantity = fd.FarmerWiseTotalQuantity
                };

                _context.HarvestGRNFarmers.Add(harvestGRNFarmerDetail);
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