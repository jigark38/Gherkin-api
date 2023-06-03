using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Entities;
using GherkinWebAPI.Models;

namespace GherkinWebAPI.Controllers
{
    [Route("api/V1/[Controller]")]
    public class FieldStaffDetailsController : ApiController
    {
        private readonly IFieldStaffDetailsService _service;

        public FieldStaffDetailsController(IFieldStaffDetailsService fieldStaffDetailsService)
        {
            _service = fieldStaffDetailsService;
        }
        
        [HttpGet, Route("GetAllFieldStaff")]
        public async Task<HttpResponseMessage> GetAllFieldStaff()
        {

            List<FieldStaffDetails> fieldStaffs = new List<FieldStaffDetails>();
            try
            {
                fieldStaffs = await _service.GetAllFieldStaff();

                if (fieldStaffs.Count > 0)
                    return Request.CreateResponse(HttpStatusCode.OK, fieldStaffs);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }


        
        [HttpGet, Route("GetFieldStaffWithEmployeeDetails/{areaid}/{staffType}")]
        public async Task<IHttpActionResult> GetFieldStaffWithEmployeeDetails(string areaid, string staffType)
        {
            try
            {
                var fieldStaffs = await _service.GetFieldStaffWithEmployeeDetails(areaid, staffType);
                if (fieldStaffs != null)
                    return Ok(fieldStaffs);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
      }

        [HttpGet, Route("GetFieldStaffbyArea")]
        public async Task<HttpResponseMessage> GetFieldStaffbyArea(string area)
        {

            List<FieldStaffDetails> fieldStaffs = new List<FieldStaffDetails>();
            try
            {
                fieldStaffs = await _service.GetFieldStaffbyArea(area);
                if (fieldStaffs != null)
                    return Request.CreateResponse(HttpStatusCode.OK, fieldStaffs);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpGet, Route("GetFieldStaffbyID")]
        public async Task<HttpResponseMessage> GetFieldStaffbyID(int ID)
        {
            FieldStaffDetails fieldStaff = new FieldStaffDetails();
            try
            {
                fieldStaff = await _service.GetFieldStaffbyID(ID);
                if (fieldStaff != null)
                    return Request.CreateResponse(HttpStatusCode.OK, fieldStaff);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }        
        [HttpPost, Route("CreateFieldStaffs")]
        public async Task<HttpResponseMessage> CreateFieldStaffs([FromBody]HarvestAreaFieldStaffDTO createFieldStaffs)
        {
            try
            {
                if (createFieldStaffs.FieldStaffs.Count > 0)
                {
                  var _staffs=  await _service.CreateFieldStaffs(createFieldStaffs);
                    return Request.CreateResponse(HttpStatusCode.OK, _staffs);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPut, Route("UpdateFieldStaff")]
        public async Task<HttpResponseMessage> UpdateFieldStaff([FromBody]FieldStaffDetails fieldStaff)
        {
            try
            {
                await _service.UpdateFieldStaff(fieldStaff.FieldStaffID, fieldStaff);
                return Request.CreateResponse(HttpStatusCode.OK, fieldStaff);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}