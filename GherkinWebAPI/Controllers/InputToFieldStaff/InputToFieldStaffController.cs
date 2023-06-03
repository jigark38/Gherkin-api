using GherkinWebAPI.Core.InputToFieldStaff;
using GherkinWebAPI.Models.InputToFieldStaff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.InputToFieldStaff
{
    [Route("api/V1/[Controller]")]
    public class InputToFieldStaffController : ApiController
    {
        private readonly IInputToFieldStaffService _inputFieldStaffService;
        private readonly string controller = nameof(InputToFieldStaffController);
        public InputToFieldStaffController(IInputToFieldStaffService fieldStaffService)
        {
            _inputFieldStaffService = fieldStaffService;
        }

        /// <summary>
        /// Add new Issues to Field Staff
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost, Route("AddToFieldStaffMaterials")]
        public async Task<IHttpActionResult> Post([FromBody] List<Inputs_Issued_To_Fieldstaff_Materials> addToFieldStaffMaterialsObj)
        {
            try
            {
                var res = await _inputFieldStaffService.Add(addToFieldStaffMaterialsObj);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(InputToFieldStaffController.Post)}");
                return InternalServerError();
            }
        }


        [HttpGet, Route("GetOutwardGatePassNo")]
        public async Task<IHttpActionResult> GetOutwardGatePassNo()
        {
            try
            {
                var res = await _inputFieldStaffService.GetOutwardGatePassNo();
                return Ok(res);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(InputToFieldStaffController.GetOutwardGatePassNo)}");
                return InternalServerError();
            }

        }

        [HttpGet, Route("GenerateMatIssueFSNo")]
        public async Task<IHttpActionResult> GenerateMatIssueFSNo()
        {
            try
            {
                var res = await _inputFieldStaffService.GenerateMatIssueFSNo();
                return Ok(res);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(InputToFieldStaffController.GenerateMatIssueFSNo)}");
                return InternalServerError();
            }

        }

        [HttpGet, Route("GetAllOrgOfficeLocDetails")]
        public async Task<IHttpActionResult> GetOrgofficelocationDetails()
        {
            try
            {
                var res = await _inputFieldStaffService.GetOrgofficelocationDetails();
                return Ok(res);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(InputToFieldStaffController.GetOrgofficelocationDetails)}");
                return InternalServerError();
            }

        }


        [HttpGet, Route("GetAllArea")]
        public async Task<IHttpActionResult> GetAllArea()
        {
            try
            {
                var res = await _inputFieldStaffService.GetAllArea();
                if (res.Count > 0)
                    return Ok(res);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(InputToFieldStaffController.GetAllArea)}");
                return InternalServerError();
            }
        }


        [HttpGet, Route("GetEmpInfoByAreaId")]
        public async Task<IHttpActionResult> GetEmpInfoByAreaId(string areaId)
        {
            try
            {
                var res = await _inputFieldStaffService.GetEmpInfoByAreaId(areaId);
                if (res.Count > 0)
                    return Ok(res);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(InputToFieldStaffController.GetEmpInfoByAreaId)}");
                return InternalServerError();
            }
        }


        [HttpGet, Route("GetCropGroupDetailsByAreaId")]
        public async Task<IHttpActionResult> GetCropGroupDetailsByAreaId(string areaId)
        {
            try
            {
                var res = await _inputFieldStaffService.GetCropGroupDetailsByAreaId(areaId);
                if (res.Count > 0)
                    return Ok(res);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(InputToFieldStaffController.GetCropGroupDetailsByAreaId)}");
                return InternalServerError();
            }
        }

        [HttpGet, Route("GetCropDetailsByCode")]
        public async Task<IHttpActionResult> GetCropDetailsByCode(string cropGroupCode)
        {
            try
            {
                var res = await _inputFieldStaffService.GetCropDetailsByCode(cropGroupCode);
                if (res.Count > 0)
                    return Ok(res);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(InputToFieldStaffController.GetEmpInfoByAreaId)}");
                return InternalServerError();
            }
        }

        [HttpGet, Route("GetPlantationSchByCropNameCode")]
        public async Task<IHttpActionResult> GetPlantationSchByAreaId(string cropNameCode)
        {
            try
            {
                var res = await _inputFieldStaffService.GetPlantationSchByCropNameCode(cropNameCode);
                if (res.Count > 0)
                    return Ok(res);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(InputToFieldStaffController.GetPlantationSchByAreaId)}");
                return InternalServerError();
            }
        }

        [HttpGet, Route("GetHBOMMatDetailsByCropNameCode")]
        public async Task<IHttpActionResult> GetHBOMMatDetailsByCropNameCode(string cropNameCode, string psNum)
        {
            try
            {
                List<HBOMMatDetailsByCropNameCode> res = new List<HBOMMatDetailsByCropNameCode>();

                res = await _inputFieldStaffService.GetHBOMMatDetailsByCropNameCode(cropNameCode, psNum);
                if (res.Count > 0)
                    return Ok(res);
                else
                    return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(InputToFieldStaffController.GetHBOMMatDetailsByCropNameCode)}");
                return InternalServerError();
            }
        }

        [HttpGet, Route("GetRMStockDetails_A")]
        public async Task<IHttpActionResult> GetRMStockDetails_A(string transferDate, string matGroupCode, string matDetailCode)
        {
            try
            {
                List<RM_Stock_MatDetails_A> res = new List<RM_Stock_MatDetails_A>();

                res = await _inputFieldStaffService.GetRMStockDetails_A(transferDate, matGroupCode, matDetailCode);
                if (res.Count > 0)
                    return Ok(res);
                else
                    return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(InputToFieldStaffController.GetHBOMMatDetailsByCropNameCode)}");
                return InternalServerError();
            }
        }
    }
}
