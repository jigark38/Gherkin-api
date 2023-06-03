using GherkinWebAPI.Core;
using GherkinWebAPI.Core.FarmersInputReturns;
using GherkinWebAPI.Models.FarmersInputReturns;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Service.FarmersInputReturns
{
	public class FarmersInputReturnsService : IFarmersInputReturnsService
	{
		private readonly IRepositoryWrapper _repositoryWrapper;
		public FarmersInputReturnsService(IRepositoryWrapper repositoryWrapper)
		{
			this._repositoryWrapper = repositoryWrapper;
		}

		public async Task<FarmersInputsMaterialMaster> CreateInputReturnsFromFarmersMaster(FarmersInputsMaterialMaster farmersInputsMaterialMaster)
		{
			return await _repositoryWrapper.FarmersInputReturnsRepository.CreateInputReturnsFromFarmersMaster(farmersInputsMaterialMaster);
		}

		public async Task<IEnumerable<FarmersInputsMaterialDetail>> CreateInputReturnsFromFarmersDetail(IEnumerable<FarmersInputsMaterialDetail> farmersInputsMaterialDetailList)
		{
			return await _repositoryWrapper.FarmersInputReturnsRepository.CreateInputReturnsFromFarmersDetail(farmersInputsMaterialDetailList);
		}

		public async Task<FieldStaffCropGroupSeason> GetFieldStaffCropGroupSeasonByAreaId(string areaId)
		{
			return await _repositoryWrapper.FarmersInputReturnsRepository.GetFieldStaffCropGroupSeasonByAreaId(areaId);
		}

		public async Task<IEnumerable<FarmersInputsMaterialDetail>> UpdateInputReturnsFromFarmersDetail(IEnumerable<FarmersInputsMaterialDetail> farmersInputsMaterialDetailList)
		{
			return await _repositoryWrapper.FarmersInputReturnsRepository.UpdateInputReturnsFromFarmersDetail(farmersInputsMaterialDetailList);
		}

		public async Task<FarmersInputsMaterialMaster> UpdateInputReturnsFromFarmersMaster(FarmersInputsMaterialMaster farmersInputsMaterialMaster)
		{
			return await _repositoryWrapper.FarmersInputReturnsRepository.UpdateInputReturnsFromFarmersMaster(farmersInputsMaterialMaster);
		}

		public async Task<List<FarmersInputsMaterialDetail>> GetFarmersInputsMaterialDetail(string psNumber, string farmerCode, string cropNameCode, string areaId, int fIMReturnNo)
		{
			return await _repositoryWrapper.FarmersInputReturnsRepository.GetFarmersInputsMaterialDetail(psNumber, farmerCode, cropNameCode, areaId, fIMReturnNo);
		}

		public async Task<FarmersInputsMaterialMaster> GetFarmersInputsMaterialMaster(string psNumber, string farmerCode, string cropNameCode, string areaId)
		{
			return await _repositoryWrapper.FarmersInputReturnsRepository.GetFarmersInputsMaterialMaster(psNumber, farmerCode, cropNameCode, areaId);
		}

		public async Task<bool> CheckIfFarmerAlreadyReturnedItems(string psNumber, string farmerCode, string cropNameCode, string areaId)
		{
			return await _repositoryWrapper.FarmersInputReturnsRepository.CheckIfFarmerAlreadyReturnedItems(psNumber, farmerCode, cropNameCode, areaId);
		}
	}
}