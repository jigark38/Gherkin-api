using GherkinWebAPI.Core.MaterialIndentByDepartment;
using GherkinWebAPI.Models;
using GherkinWebAPI.Models.MaterialIndentByDepartment;
using GherkinWebAPI.ValidateModel;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.MaterialIndentByDepartment
{
	[RoutePrefix("api/v1/MaterialIndentByDepartment")]
	public class MaterialIndentByDepartmentController : ApiController
	{
		private readonly IStoreInternalIndentMasterService _storeInternalIndentMasterService;
		private readonly IStoreInternalIndentDetailService _storeInternalIndentDetailService;
		public readonly string controller = nameof(MaterialIndentByDepartmentController);

		public MaterialIndentByDepartmentController(IStoreInternalIndentMasterService storeInternalIndentMasterService,
			IStoreInternalIndentDetailService storeInternalIndentDetailService)
		{
			_storeInternalIndentMasterService = storeInternalIndentMasterService;
			_storeInternalIndentDetailService = storeInternalIndentDetailService;
		}

		#region Master

		[Route("GetGroupNameList")]
		[HttpGet]
		public async Task<IHttpActionResult> GetGroupNameList()
		{
			return Ok(await _storeInternalIndentMasterService.GetGroupNameList());
		}

		[Route("GetSubGroupNameListByGroupCode/{groupCode}")]
		[HttpGet]
		public async Task<IHttpActionResult> GetSubGroupNameListByGroupCode(int groupCode)
		{
			return Ok(await _storeInternalIndentMasterService.GetSubGroupNameListByGroupCode(groupCode));
		}

		[Route("GetMaterialListByGroupSubGroupCode/{groupCode}/{subGroupCode}")]
		[HttpGet]
		public async Task<IHttpActionResult> GetMaterialListByGroupSubGroupCode(int groupCode, int subGroupCode)
		{
			return Ok(await _storeInternalIndentMasterService.GetMaterialListByGroupSubGroupCode(groupCode, subGroupCode));
		}

		[Route("GetMaterialIndentListByIndentDate/{indentDate}")]
		[HttpGet]
		public async Task<IHttpActionResult> GetMaterialIndentListByIndentDate(DateTime indentDate)
		{
			return Ok(await _storeInternalIndentMasterService.GetMaterialIndentListByIndentDate(indentDate));
		}

		[CheckModelForNull]
		[ValidateModelState]
		[Route("SaveMaterialIndentMaster")]
		[HttpPost]
		public async Task<IHttpActionResult> SaveMaterialIndentMaster([FromBody] StoreInternalIndentMaster storeInternalIndentMaster)
		{
			return Ok(await _storeInternalIndentMasterService.SaveMaterialIndentMaster(storeInternalIndentMaster));
		}

		#endregion

		#region Detail

		[Route("GetMaterialIndentDetailByIndentNo/{indentNo}")]
		[HttpGet]
		public async Task<IHttpActionResult> GetMaterialIndentDetailByIndentNo(string indentNo)
		{
			return Ok(await _storeInternalIndentDetailService.GetMaterialIndentDetailByIndentNo(indentNo));
		}

		[CheckModelForNull]
		[ValidateModelState]
		[Route("SaveMaterialIndentDetail")]
		[HttpPost]
		public async Task<IHttpActionResult> SaveMaterialIndentDetail([FromBody] StoreInternalIndentDetail storeInternalIndentDetail)
		{
			return Ok(await _storeInternalIndentDetailService.SaveMaterialIndentDetail(storeInternalIndentDetail));
		}

		#endregion
	}
}