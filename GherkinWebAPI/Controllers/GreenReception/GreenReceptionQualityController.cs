using GherkinWebAPI.Core.GreenReption;
using GherkinWebAPI.Entities;
using GherkinWebAPI.Models.GreenReception;
using GherkinWebAPI.ValidateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers.GreenReception
{
    [Route("api/V1/[Controller]")]
    public class GreenReceptionQualityController : ApiController
    {
        private readonly IGreenReceptionQualityService _service;
        public GreenReceptionQualityController(IGreenReceptionQualityService Service)
        {
            _service = Service;
        }

        [Route("GetAllUnit")]
        public async Task<IHttpActionResult> GetAllUnit()
        {
            List<OrganisationOfficeLocUnit> units = new List<OrganisationOfficeLocUnit>();
            try
            {
                units = await _service.GetAllUnit();
                return Ok(units);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in GreenReceptionQualityController / {nameof(GreenReceptionQualityController.GetAllUnit)}");
                return InternalServerError();
            }
        }

        [Route("GetInwardDetailsByOrgOfficeNo")]
        public async Task<IHttpActionResult> GetInwardDetailsByOrgOfficeNo(int orgOfficeNo)
        {
            List<GreenInwardsDetail> inwardDetails = new List<GreenInwardsDetail>();
            try
            {
                inwardDetails = await _service.GetInwardDetailsByOrgOfficeNo(orgOfficeNo);
                return Ok(inwardDetails);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in GreenReceptionQualityController / {nameof(GreenReceptionQualityController.GetInwardDetailsByOrgOfficeNo)}");
                return InternalServerError();
            }
        }

        [Route("GetGreenReceptionByOrgOfficeNo")]
        public async Task<IHttpActionResult> GetGreenReceptionByOrgOfficeNo(int orgOfficeNo)
        {
            List<GreensReceptionDetail> receptionDetails = new List<GreensReceptionDetail>();
            try
            {
                receptionDetails = await _service.GetGreenReceptionByOrgOfficeNo(orgOfficeNo);
                return Ok(receptionDetails);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in GreenReceptionQualityController / {nameof(GreenReceptionQualityController.GetGreenReceptionByOrgOfficeNo)}");
                return InternalServerError();
            }
        }

        [CheckModelForNull]
        [ValidateModelState]
        [Route("CreateQualityCheckAndInspection")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateQualityCheckAndInspection([FromBody] CreateQualityCheckAndInspection createQCAndInspection)
        {
            try
            {
                if (createQCAndInspection == null)
                    return null;
                var qcAndInsPection = await _service.CreateQualityCheckAndInspection(createQCAndInspection);
                return Ok(qcAndInsPection);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in GreenReceptionQualityController / {nameof(GreenReceptionQualityController.CreateQualityCheckAndInspection)}");
                return InternalServerError();
            }
        }

        [Route("GetQualityCheckAndInspection")]
        public async Task<IHttpActionResult> GetQualityCheckAndInspection(long harvestGRNNo)
        {
            CreateQualityCheckAndInspection createQualityCheckAndInspection = new CreateQualityCheckAndInspection();
            try
            {
                createQualityCheckAndInspection = await _service.GetQualityCheckAndInspection(harvestGRNNo);
                return Ok(createQualityCheckAndInspection);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in GreenReceptionQualityController / {nameof(GreenReceptionQualityController.GetGreenReceptionByOrgOfficeNo)}");
                return InternalServerError();
            }
        }
    }
}
