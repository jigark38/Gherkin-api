using GherkinWebAPI.Core.FarmersInputReturns;
using GherkinWebAPI.Models.FarmersInputReturns;
using GherkinWebAPI.ValidateModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.FarmersInputReturns
{
	[RoutePrefix("api/v1/FarmersInputReturns")]
	public class FarmersInputReturnsController : ApiController
	{
		private readonly IFarmersInputReturnsService _farmersInputReturnsService;
		public readonly string controller = nameof(FarmersInputReturnsController);
		public FarmersInputReturnsController(IFarmersInputReturnsService farmersInputReturnsService)
		{
			_farmersInputReturnsService = farmersInputReturnsService;
		}

		[Route("GetFieldStaffCropGroupSeasonByAreaId/{areaId}")]
		[HttpGet]
		public async Task<IHttpActionResult> GetFieldStaffCropGroupSeasonByAreaId(string areaId)
		{
			try
			{
				var fieldStaffList = await _farmersInputReturnsService.GetFieldStaffCropGroupSeasonByAreaId(areaId);
				return Ok(fieldStaffList);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + " " + $"Exception in GetFieldStaffCropGroupSeasonByAreaId / {nameof(FarmersInputReturnsController.GetFieldStaffCropGroupSeasonByAreaId)}");
				return InternalServerError();
			}
		}

		[Route("GetFarmersInputsMaterialDetail/{psNumber}/{farmerCode}/{cropNameCode}/{areaId}/{fIMReturnNo}")]
		[HttpGet]
		public async Task<IHttpActionResult> GetFarmersInputsMaterialDetail(string psNumber, string farmerCode, string cropNameCode, string areaId, int fIMReturnNo)
		{
			try
			{
				var farmersInputsMaterialDetails = await _farmersInputReturnsService.GetFarmersInputsMaterialDetail(psNumber, farmerCode, cropNameCode, areaId, fIMReturnNo);
				return Ok(farmersInputsMaterialDetails);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + " " + $"Exception in GetFarmersInputsMaterialDetail / {nameof(FarmersInputReturnsController.GetFarmersInputsMaterialDetail)}");
				return InternalServerError();
			}
		}

		[Route("GetFarmersInputsMaterialMaster/{psNumber}/{farmerCode}/{cropNameCode}/{areaId}")]
		[HttpGet]
		public async Task<IHttpActionResult> GetFarmersInputsMaterialMaster(string psNumber, string farmerCode, string cropNameCode, string areaId)
		{
			try
			{
				var farmersInputsMaterialDetails = await _farmersInputReturnsService.GetFarmersInputsMaterialMaster(psNumber, farmerCode, cropNameCode, areaId);
				return Ok(farmersInputsMaterialDetails);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + " " + $"Exception in GetFarmersInputsMaterialMaster / {nameof(FarmersInputReturnsController.GetFarmersInputsMaterialMaster)}");
				return InternalServerError();
			}
		}

		[Route("CreateInputReturnsFromFarmersMaster")]
		[ValidateModelState]
		[HttpPost]
		public async Task<IHttpActionResult> CreateInputReturnsFromFarmersMaster([FromBody] FarmersInputsMaterialMaster farmersInputsMaterialMaster)
		{
			try
			{
				if (await _farmersInputReturnsService.CheckIfFarmerAlreadyReturnedItems(
					farmersInputsMaterialMaster.PSNumber,
					farmersInputsMaterialMaster.FarmerCode,
					farmersInputsMaterialMaster.CropNameCode,
					farmersInputsMaterialMaster.AreaID))
				{
					return Ok();
				}
				farmersInputsMaterialMaster = await _farmersInputReturnsService.CreateInputReturnsFromFarmersMaster(farmersInputsMaterialMaster);
				return Ok(farmersInputsMaterialMaster);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + " " + $"Exception in CreateInputReturnsFromFarmersMaster / {nameof(FarmersInputReturnsController.CreateInputReturnsFromFarmersMaster)}");
				return InternalServerError();
			}
		}

		[Route("UpdateInputReturnsFromFarmersMaster")]
		[ValidateModelState]
		[HttpPut]
		public async Task<IHttpActionResult> UpdateInputReturnsFromFarmersMaster([FromBody] FarmersInputsMaterialMaster farmersInputsMaterialMaster)
		{
			try
			{
				if (farmersInputsMaterialMaster == null)
				{
					return null;
				}
				farmersInputsMaterialMaster = await _farmersInputReturnsService.UpdateInputReturnsFromFarmersMaster(farmersInputsMaterialMaster);
				return Ok(farmersInputsMaterialMaster);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + " " + $"Exception in UpdateInputReturnsFromFarmersMaster / {nameof(FarmersInputReturnsController.UpdateInputReturnsFromFarmersMaster)}");
				return InternalServerError();
			}
		}

		[Route("CreateInputReturnsFromFarmersDetail")]
		[ValidateModelState]
		[HttpPost]
		public async Task<IHttpActionResult> CreateInputReturnsFromFarmersDetail([FromBody] IEnumerable<FarmersInputsMaterialDetail> farmersInputsMaterialDetailList)
		{
			try
			{
				if (farmersInputsMaterialDetailList == null)
				{
					return null;
				}
				farmersInputsMaterialDetailList = await _farmersInputReturnsService.CreateInputReturnsFromFarmersDetail(farmersInputsMaterialDetailList);
				return Ok(farmersInputsMaterialDetailList);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + " " + $"Exception in CreateInputReturnsFromFarmersDetail / {nameof(FarmersInputReturnsController.CreateInputReturnsFromFarmersDetail)}");
				return InternalServerError();
			}
		}

		[Route("UpdateInputReturnsFromFarmersDetail")]
		[ValidateModelState]
		[HttpPut]
		public async Task<IHttpActionResult> UpdateInputReturnsFromFarmersDetail([FromBody] IEnumerable<FarmersInputsMaterialDetail> farmersInputsMaterialDetailList)
		{
			try
			{
				if (farmersInputsMaterialDetailList == null)
				{
					return null;
				}
				farmersInputsMaterialDetailList = await _farmersInputReturnsService.UpdateInputReturnsFromFarmersDetail(farmersInputsMaterialDetailList);
				return Ok(farmersInputsMaterialDetailList);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + " " + $"Exception in UpdateInputReturnsFromFarmersDetail / {nameof(FarmersInputReturnsController.UpdateInputReturnsFromFarmersDetail)}");
				return InternalServerError();
			}
		}

	}
}
