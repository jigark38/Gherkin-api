using GherkinWebAPI.Core.StoresMasterDetails;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.StoresMasterDetails;
using GherkinWebAPI.ValidateModel;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.StoresMasterDetails
{
	[RoutePrefix("api/v1/StoresMasterDetails")]
	public class StoresMasterDetailsController : ApiController
	{
		private readonly IGSGroupDetailService _groupDetailService;
		private readonly IGSSubGroupDetailService _subGroupDetailService;
		private readonly IGSCUOMDetailService _uomDetailService;
		private readonly IGSMaterialDetailService _materialDetailService;
		public readonly string controller = nameof(StoresMasterDetailsController);

		public StoresMasterDetailsController(IGSGroupDetailService groupDetailService, IGSSubGroupDetailService subGroupDetailService,
			IGSCUOMDetailService uomDetailService, IGSMaterialDetailService materialDetailService)
		{
			_groupDetailService = groupDetailService;
			_subGroupDetailService = subGroupDetailService;
			_uomDetailService = uomDetailService;
			_materialDetailService = materialDetailService;
		}

		#region GS Group Detail

		[Route("GetStoresMasterGroupByName/{groupName}")]
		[HttpGet]
		public async Task<IHttpActionResult> GetStoresMasterGroupByName(string groupName)
		{
			return Ok(await _groupDetailService.GetStoresMasterGroupByName(groupName));
		}

		[CheckModelForNull]
		[ValidateModelState]
		[Route("SaveStoresMasterGroup")]
		[HttpPost]
		public async Task<IHttpActionResult> SaveStoresMasterGroup([FromBody] GSGroupDetail groupDetail)
		{
			return Ok(await _groupDetailService.SaveStoresMasterGroup(groupDetail));
		}

		[Route("IsStoresMasterGroupExists/{groupName}")]
		[HttpGet]
		public async Task<IHttpActionResult> IsStoresMasterGroupExists(string groupName)
		{
			return Ok(await _groupDetailService.IsStoresMasterGroupExists(groupName));
		}

		#endregion

		#region GS Sub Group Detail

		[Route("GetStoresMasterSubGroupByName/{subGroupName}/{groupCode}")]
		[HttpGet]
		public async Task<IHttpActionResult> GetStoresMasterSubGroupByName(string subGroupName, int groupCode)
		{
			return Ok(await _subGroupDetailService.GetStoresMasterSubGroupByName(subGroupName, groupCode));
		}

		[CheckModelForNull]
		[ValidateModelState]
		[Route("SaveStoresMasterSubGroup")]
		[HttpPost]
		public async Task<IHttpActionResult> SaveStoresMasterSubGroup([FromBody] GSSubGroupDetail subGroupDetail)
		{
			return Ok(await _subGroupDetailService.SaveStoresMasterSubGroup(subGroupDetail));
		}

		[Route("IsStoresMasterSubGroupExists/{subGroupName}/{groupCode}")]
		[HttpGet]
		public async Task<IHttpActionResult> IsStoresMasterSubGroupExists(string subGroupName, int groupCode)
		{
			return Ok(await _subGroupDetailService.IsStoresMasterSubGroupExists(subGroupName, groupCode));
		}

		#endregion

		#region GSC UOM Detail

		[Route("GetStoresMasterUOMByName/{uomName}")]
		[HttpGet]
		public async Task<IHttpActionResult> GetStoresMasterUOMByName(string uomName)
		{
			return Ok(await _uomDetailService.GetStoresMasterUOMByName(uomName));
		}

		[CheckModelForNull]
		[ValidateModelState]
		[Route("SaveStoresMasterUOM")]
		[HttpPost]
		public async Task<IHttpActionResult> SaveStoresMasterUOM([FromBody] GSCUomDetails uomDetail)
		{
			return Ok(await _uomDetailService.SaveStoresMasterUOM(uomDetail));
		}

		[Route("IsStoresMasterUOMExists/{uomName}")]
		[HttpGet]
		public async Task<IHttpActionResult> IsStoresMasterUOMExists(string uomName)
		{
			return Ok(await _uomDetailService.IsStoresMasterUOMExists(uomName));
		}

		#endregion

		#region GS Material Detail

		[Route("GetStoresMasterMaterialByName/{materialName}/{groupCode}/{subGroupCode}")]
		[HttpGet]
		public async Task<IHttpActionResult> GetStoresMasterMaterialByName(string materialName, int groupCode, int subGroupCode)
		{
			return Ok(await _materialDetailService.GetStoresMasterMaterialByName(materialName, groupCode, subGroupCode));
		}

		[CheckModelForNull]
		[ValidateModelState]
		[Route("SaveStoresMasterMaterial")]
		[HttpPost]
		public async Task<IHttpActionResult> SaveStoresMasterMaterial([FromBody] GSMaterialDetail materialDetail)
		{
			return Ok(await _materialDetailService.SaveStoresMasterMaterial(materialDetail));
		}

		[Route("IsStoresMasterMaterialExists/{materialName}")]
		[HttpGet]
		public async Task<IHttpActionResult> IsStoresMasterMaterialExists(string materialName)
		{
			return Ok(await _materialDetailService.IsStoresMasterMaterialExists(materialName));
		}

		#endregion

	}
}
