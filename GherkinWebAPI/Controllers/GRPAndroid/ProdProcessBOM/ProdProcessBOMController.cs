using GherkinWebAPI.Core.GRPAndroid.ProdProcessBOM;
using GherkinWebAPI.Models.GRPAndroid.ProdProcessBOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.GRPAndroid.ProdProcessBOM
{
    [Route("api/V1/[Controller]")]
    public class ProdProcessBOMController : ApiController
    {
        private readonly IProdProcessBOMService _service;
        private readonly string controller = nameof(ProdProcessBOMController);
        public ProdProcessBOMController(IProdProcessBOMService prodService)
        {
            _service = prodService;
        }
        [HttpGet]
        [Route("GetAllProdGroup")]
        public async Task<IHttpActionResult> GetAllProdGroup()
        {
            try
            {
                var res = await _service.GetAllProductGroup();
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(ProdProcessBOMController.GetAllProdGroup)}");
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("GetVariety/{grpCode}")]
        public async Task<IHttpActionResult> GetVariety(string grpCode)
        {
            try
            {
                var res = await _service.GetVariety(grpCode);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(ProdProcessBOMController.GetVariety)}");
                return InternalServerError();
            }
        }
        //[HttpGet]
        //[Route("GetGradeDetails/{varCode}")]
        //public async Task<IHttpActionResult> GetGradeDetails(string varCode)
        //{
        //    try
        //    {
        //        var res = await _service.GetGradeDetails(varCode);
        //        return Ok(res);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(ProdProcessBOMController.GetGradeDetails)}");
        //        return InternalServerError();
        //    }
        //}
        [HttpGet]
        [Route("GetRawMaterialGroup")]
        public async Task<IHttpActionResult> GetRawMaterialGroup()
        {
            try
            {
                var res = await _service.GetRawMaterialGroup();
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(ProdProcessBOMController.GetRawMaterialGroup)}");
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("GetRawMaterialDetails/{rawMaterialGrpCode}")]
        public async Task<IHttpActionResult> GetRawMaterialDetails(string rawMaterialGrpCode)
        {
            try
            {
                var res = await _service.GetRawMaterialDetails(rawMaterialGrpCode);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(ProdProcessBOMController.GetRawMaterialDetails)}");
                return InternalServerError();
            }
        }
        [Route("SaveProductionProcess"), HttpPost]
        public async Task<IHttpActionResult> SaveProductionProcess(ProductionProcessDetails prodProcessDetails)
        {
            try
            {
                var res = await _service.SaveProductionProcess(prodProcessDetails);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(ProdProcessBOMController.SaveProductionProcess)}");
                return InternalServerError();
            }
        }

        [Route("SaveProductionProcessBOM"), HttpPost]
        public async Task<IHttpActionResult> SaveProductionProcessBOM(ProdProcessCombinedModel prodProcessCombine)
        {
            try
            {
                var res = await _service.SaveProductionProcessBOM(prodProcessCombine);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(ProdProcessBOMController.SaveProductionProcessBOM)}");
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("GetProductionUOM/{uomKey}")]
        public async Task<IHttpActionResult> GetProductionUOM(string uomKey)
        {
            try
            {
                var res = await _service.GetProductionUOM(uomKey);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(ProdProcessBOMController.GetProductionUOM)}");
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("GetMaterialUOM/{uomKey}")]
        public async Task<IHttpActionResult> GetMaterialUOM(string uomKey)
        {
            try
            {
                var res = await _service.GetMaterialUOM(uomKey);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(ProdProcessBOMController.GetMaterialUOM)}");
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("GetAllSavedProductGroup")]
        public async Task<IHttpActionResult> GetAllSavedProductGroup()
        {
            try
            {
                var res = await _service.GetAllSavedProductGroup();
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(ProdProcessBOMController.GetAllSavedProductGroup)}");
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("GetSavedVariety/{grpCode}")]
        public async Task<IHttpActionResult> GetSavedVariety(string grpCode)
        {
            try
            {
                var res = await _service.GetSavedVariety(grpCode);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(ProdProcessBOMController.GetSavedVariety)}");
                return InternalServerError();
            }
        }
        [HttpPost]
        [Route("FetchProdProcess")]
        public async Task<IHttpActionResult> FetchProdProcess(GroupAndVarietyCode grpVarCode)
        {
            try
            {
                var res = await _service.FetchProdProcess(grpVarCode.FPGroupCode,grpVarCode.FPVarietyCode);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(ProdProcessBOMController.FetchProdProcess)}");
                return InternalServerError();
            }
        }
    }
}
