using GherkinWebAPI.Models.FarmersInputReturns;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core.FarmersInputReturns
{
	public interface IFarmersInputReturnsService
	{
		Task<FieldStaffCropGroupSeason> GetFieldStaffCropGroupSeasonByAreaId(string areaId);

		Task<FarmersInputsMaterialMaster> CreateInputReturnsFromFarmersMaster(FarmersInputsMaterialMaster farmersInputsMaterialMaster);
		Task<FarmersInputsMaterialMaster> UpdateInputReturnsFromFarmersMaster(FarmersInputsMaterialMaster farmersInputsMaterialMaster);

		Task<IEnumerable<FarmersInputsMaterialDetail>> CreateInputReturnsFromFarmersDetail(IEnumerable<FarmersInputsMaterialDetail> farmersInputsMaterialDetailList);
		Task<IEnumerable<FarmersInputsMaterialDetail>> UpdateInputReturnsFromFarmersDetail(IEnumerable<FarmersInputsMaterialDetail> farmersInputsMaterialDetailList);
		Task<List<FarmersInputsMaterialDetail>> GetFarmersInputsMaterialDetail(string psNumber, string farmerCode, string cropNameCode, string areaId, int fIMReturnNo);
		Task<FarmersInputsMaterialMaster> GetFarmersInputsMaterialMaster(string psNumber, string farmerCode, string cropNameCode, string areaId);
		Task<bool> CheckIfFarmerAlreadyReturnedItems(string psNumber, string farmerCode, string cropNameCode, string areaId);
	}
}