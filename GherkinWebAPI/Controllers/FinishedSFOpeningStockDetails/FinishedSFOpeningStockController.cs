using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.Request.FinishedSFOpeningStockDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers
{
    [RoutePrefix("api/V1/FinishedSFOpeningStock")]
    public class FinishedSFOpeningStockController : ApiController
    {
        private readonly IFinishedSFOpeningStockService _service;
        public FinishedSFOpeningStockController(IFinishedSFOpeningStockService service)
        {
            _service = service;
        }

        [HttpGet, Route("GetOrganisationOfficeUnits")]
        public async Task<IHttpActionResult> GetOrganisationOfficeUnitList()
        {
            return Ok(await _service.GetOrganisationOfficeUnitList());
        }

        [HttpGet, Route("GetHarvestAreas")]
        public async Task<IHttpActionResult> GetHarvestAreaList()
        {
            return Ok(await _service.GetHarvestAreaList());
        }

        [HttpGet, Route("GetCountryOverSeas")]
        public async Task<IHttpActionResult> GetCountryOverSeasList()
        {
            return Ok(await _service.GetCountryOverSeasList());
        }

        [HttpGet, Route("GetConsigneeBuyersList")]
        public async Task<IHttpActionResult> GetConsigneeBuyersList(string overseasCountryId)
        {
            return Ok(await _service.GetConsigneeBuyersList(overseasCountryId));

        }

        [HttpGet, Route("GetProformaInvoices")]
        public async Task<IHttpActionResult> GetProformaInvoiceList(string cBCode)
        {
            return Ok(await _service.GetProformaInvoiceList(cBCode));
        }

        [HttpGet, Route("GetFinishedProductGroups")]
        public async Task<IHttpActionResult> GetFinishedProductGroupList()
        {
            return Ok(await _service.GetFinishedProductGroupList());
        }

        [HttpGet, Route("GetFinishedProductDetails")]
        public async Task<IHttpActionResult> GetFinishedProductDetailsList(string GrpCode)
        {
            return Ok(await _service.GetFinishedProductDetailsList(GrpCode));
        }

        [HttpGet, Route("GetProductionProcessDetails")]
        public async Task<IHttpActionResult> GetProductionProcessDetailsList(string VarietyCode)
        {
            return Ok(await _service.GetProductionProcessDetailsList(VarietyCode));
        }

        [HttpGet, Route("GetMediaProcessDetails")]
        public async Task<IHttpActionResult> GetMediaProcessDetailsList(string ProductionProcessCode)
        {
            return Ok(await _service.GetMediaProcessDetailsList(ProductionProcessCode));
        }

        [HttpGet, Route("GetFPGradesDetails")]
        public async Task<IHttpActionResult> GetFPGradesDetailsList(string VarietyCode)
        {
            return Ok(await _service.GetFPGradesDetailsList(VarietyCode));

        }

        [HttpGet, Route("GetContainerPackingDetails")]
        public async Task<IHttpActionResult> GetContainerPackingDetailsList()
        {
            return Ok(await _service.GetContainerPackingDetailsList());
        }

        [HttpGet, Route("GetUOMDetails")]
        public async Task<IHttpActionResult> GetUOMDetailsList()
        {
            return Ok(await _service.GetUOMDetailsList());
        }

        [HttpPost, Route("SaveFinishedSFOpeningStock")]
        public async Task<IHttpActionResult> SaveFinishedSFOpeningStock(FinishedSFStockProductDetailsRequest finishedStkProdDetail)
        {
            return Ok(await _service.SaveFinishedSFOpeningStock(finishedStkProdDetail));

        }

        [HttpPost, Route("GetStockDetails")]
        public async Task<IHttpActionResult> GetStockDetails(FinishedSFStockQuantityFindRequest finishedStkProdDetail)
        {
            return Ok(await _service.GetStockDetails(finishedStkProdDetail));
        }

        [HttpPost, Route("UpdateStockDetals")]
        public async Task<IHttpActionResult> UpdateStockDetals(FinishedSFStockQuantityDetails finishedQtyDetail)
        {
            return Ok(await _service.UpdateStockDetals(finishedQtyDetail));
        }

        [HttpGet, Route("DeleteStockDetals")]
        public async Task<IHttpActionResult> DeleteStockDetals(int FSFStockQuantityNo)
        {
            return Ok(await _service.DeleteStockDetals(FSFStockQuantityNo));
        }
    }
}
