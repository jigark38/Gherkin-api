using GherkinWebAPI.Core;
using GherkinWebAPI.Core.MediaProcessDetail;
using GherkinWebAPI.DTO;
using GherkinWebAPI.DTO.MediaProcessDetail;
using GherkinWebAPI.Entities;
using GherkinWebAPI.Filter;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GherkinWebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("Organisation")]
    public class OrganisationController : ApiController
    {
        private IOrganisationService _orgService;

        private IOrganisationOfficeLocationDetialsRepository _organisationOfficeLocationDetialsRepository;

        private IMediaProcessDetailsService _mediaProcessDetailsService;


        public OrganisationController(IOrganisationService orgService, IOrganisationOfficeLocationDetialsRepository organisationOfficeLocationDetialsRepository
            , IMediaProcessDetailsService mediaProcessDetailsService)
        {
            _orgService = orgService;
            _organisationOfficeLocationDetialsRepository = organisationOfficeLocationDetialsRepository;
            _mediaProcessDetailsService = mediaProcessDetailsService;
        }


        [HttpPost]
        [Route("CreateOrganisation")]
        
        public async Task<IHttpActionResult> CreateOrganisation([FromBody] Organisation org)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _orgService.CreateOrganisation(org);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " " + $"Exception in OrganisationController / {nameof(OrganisationController.CreateOrganisation)}");
                    return InternalServerError(ex);
                }

                return Ok();
            }

            return BadRequest(ModelState.Values.First().Errors[0].ErrorMessage);

        }

        [HttpPost]
        [Route("CreateManagement")]
        
        public async Task<IHttpActionResult> CreateManagement([FromBody] Management mngmt)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _orgService.CreateManagement(mngmt);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " " + $"Exception in OrganisationController / {nameof(OrganisationController.CreateManagement)}");
                    return InternalServerError(ex);
                }

                return Ok();
            }

            return BadRequest(ModelState.Values.First().Errors[0].ErrorMessage);

        }

        [HttpPost]
        [Route("CreateOfficeLocation")]
        
        public async Task<IHttpActionResult> CreateOfficeLocation([FromBody] OrganisationOfficeLocationDetails office)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _organisationOfficeLocationDetialsRepository.CreateOfficeLocation(office);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " " + $"Exception in OrganisationController / {nameof(OrganisationController.CreateOfficeLocation)}");
                    return InternalServerError(ex);
                }

                return Ok();
            }

            return BadRequest(ModelState.Values.First().Errors[0].ErrorMessage);

        }


        [HttpGet]
        [Route("GetOrganisations")]
        public async Task<IHttpActionResult> GetOrganisations()
        {
            List<Organisation> orgList = new List<Organisation>();  
                try
                {
                    orgList = await _orgService.GetAllOrganisations();
                
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " " + $"Exception in OrganisationController / {nameof(OrganisationController.GetOrganisations)}");
                    return InternalServerError(ex);
                }

            return Ok(orgList);

        }

        [HttpGet]
        [Route("GetManagements")]
        public async Task<IHttpActionResult> GetManagements()
        {
            List<Management> mngmtList = new List<Management>();
            try
            {
                mngmtList = await _orgService.GetAllManagements();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in OrganisationController / {nameof(OrganisationController.GetManagements)}");
                return InternalServerError(ex);
            }

            return Ok(mngmtList);

        }


        [HttpGet]
        [Route("GetOfficeLocations")]
        public async Task<IHttpActionResult> GetOfficeLocations()
        {
            List<OrganisationOfficeLocationDTO> officeLocationlist = new List<OrganisationOfficeLocationDTO>();
            try
            {
                officeLocationlist = await _organisationOfficeLocationDetialsRepository.GetOrganisationOfficeLocations();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in OrganisationController / {nameof(OrganisationController.GetManagements)}");
                return InternalServerError(ex);
            }

            return Ok(officeLocationlist);

        }


        [HttpPost]
        [Route("UpdateOrganisation")]

        public async Task<IHttpActionResult> UpdateOrganisation([FromBody] Organisation org)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _orgService.UpdateOrganisation(org);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " " + $"Exception in OrganisationController / {nameof(OrganisationController.UpdateOrganisation)}");
                    return InternalServerError(ex);
                }

                return Ok();
            }

            return BadRequest(ModelState.Values.First().Errors[0].ErrorMessage);

        }

        [HttpPost]
        [Route("UpdateManagement")]

        public async Task<IHttpActionResult> UpdateManagement([FromBody] Management management)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _orgService.UpdateManagement(management);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " " + $"Exception in OrganisationController / {nameof(OrganisationController.UpdateManagement)}");
                    return InternalServerError(ex);
                }

                return Ok();
            }

            return BadRequest(ModelState.Values.First().Errors[0].ErrorMessage);

        }

        [HttpPost]
        [Route("UpdateOfficeLocation")]

        public async Task<IHttpActionResult> UpdateOfficeLocation([FromBody] OrganisationOfficeLocationDetails office)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _organisationOfficeLocationDetialsRepository.UpdateOrganisationOfficeLocations(office);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " " + $"Exception in OrganisationController / {nameof(OrganisationController.UpdateOfficeLocation)}");
                    return InternalServerError(ex);
                }

                return Ok();
            }

            return BadRequest(ModelState.Values.First().Errors[0].ErrorMessage);

        }

        [HttpDelete]
        [Route("DeleteOfficeLocation")]

        public async Task<IHttpActionResult> DeleteOfficeLocation(int officeNo)
        {
           
            try
            {
                await _organisationOfficeLocationDetialsRepository.DeleteOrganisationOfficeLocations(officeNo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in OrganisationController / {nameof(OrganisationController.DeleteOfficeLocation)}");
                return InternalServerError(ex);
            }

            return Ok();
          
        }


        [HttpDelete]
        [Route("DeleteManagement")]

        public async Task<IHttpActionResult> DeleteManagement(int mngCode)
        {

            try
            {
                await _orgService.DeleteManagement(mngCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in OrganisationController / {nameof(OrganisationController.DeleteManagement)}");
                return InternalServerError();
            }

            return Ok();

        }


        [HttpDelete]
        [Route("DeleteOrganisation")]

        public async Task<IHttpActionResult> DeleteOrganisation(int orgCode)
        {

            try
            {
                await _orgService.DeleteOrganisation(orgCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in OrganisationController / {nameof(OrganisationController.DeleteOrganisation)}");
                return InternalServerError(ex);
            }

            return Ok();

        }



        [HttpGet]
        [Route("GetOrganisationById")]

        public async Task<IHttpActionResult> GetOrganisationById(int orgCode)
        {
            List<Organisation> orgList = new List<Organisation>();
            try
            {
                orgList = await _orgService.GetOrganisationById(orgCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in OrganisationController / {nameof(OrganisationController.GetOrganisationById)}");
                return InternalServerError(ex);
            }

            return Ok(orgList);

        }


        [HttpGet]
        [Route("GetManagementById")]

        public async Task<IHttpActionResult> GetManagementById(int orgCode)
        {
            List<Management> mngmtList = new List<Management>();
            try
            {
                mngmtList = await _orgService.GetManagementById(orgCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in OrganisationController / {nameof(OrganisationController.GetManagementById)}");
                return InternalServerError(ex);
            }

            return Ok(mngmtList);

        }


        [HttpGet]
        [Route("GetOfficeLocationById")]

        public async Task<IHttpActionResult> GetOfficeLocationById(int orgCode)
        {
            List<OrganisationOfficeLocationDTO> officeList = new List<OrganisationOfficeLocationDTO>();
            try
            {
                officeList = await _organisationOfficeLocationDetialsRepository.GetOrganisationOfficeLocationById(orgCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in OrganisationController / {nameof(OrganisationController.GetOfficeLocationById)}");
                return InternalServerError(ex);
            }

            return Ok(officeList);

        }


        [HttpGet]
        [Route("GetOfficeLocationsOrderByName")]
        public async Task<IHttpActionResult> GetOfficeLocationsOrderByName()
        {
            List<OrganisationOfficeLocationDetailsDto> officeLocationlist = new List<OrganisationOfficeLocationDetailsDto>();
            try
            {
                officeLocationlist = await _organisationOfficeLocationDetialsRepository.GetOrganisationOfficeLocationsNames();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in OrganisationController / {nameof(OrganisationController.GetOfficeLocationsOrderByName)}");
                return InternalServerError(ex);
            }

            return Ok(officeLocationlist);

        }

        [HttpGet]
        [Route("GetMediaProcessDetailsOrderByName")]
        public async Task<IHttpActionResult> GetMediaProcessDetailsOrderByName()
        {
            List<MediaProcessDetailsDTO> result = new List<MediaProcessDetailsDTO>();
            try
            {
                result = await _mediaProcessDetailsService.GetAllMediaProcessDetails();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in OrganisationController / {nameof(OrganisationController.GetOfficeLocationsOrderByName)}");
                return InternalServerError(ex);
            }

            return Ok(result);

        }
    }
}
