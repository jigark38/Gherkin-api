using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using GherkinWebAPI.ValidateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GherkinWebAPI.Controllers.Overseas
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    //[RoutePrefix("Place")]
    public class PlaceController : ApiController
    {
        private IPlaceRepository _placeService;

        public PlaceController(IPlaceRepository placeService)
        {
            _placeService = placeService;
        }

        [HttpPost]
        [Route("CreatePlace")]
        [ValidateModelState]
        public async Task<IHttpActionResult> CreatePlace([FromBody] Place place)
        {
            try
            {
                var placeDetail = await _placeService.CreatePlace(place);
                return Ok(placeDetail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in PlaceController / {nameof(PlaceController.CreatePlace)}");
                return InternalServerError();
            }

        }

        [HttpGet]
        [Route("GetPlaces")]
        public async Task<IHttpActionResult> GetPlaces()
        {
            List<Place> placesList = new List<Place>();
            try
            {
                placesList = await _placeService.GetAllPlaces();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in PlaceController / {nameof(PlaceController.GetPlaces)}");
                return InternalServerError();
            }

            return Ok(placesList);

        }

        [HttpPost]
        [Route("UpdatePlace")]
        public async Task<IHttpActionResult> UpdatePlace(int placeCode, [FromBody] Place place)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _placeService.UpdatePlace(placeCode, place);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " " + $"Exception in PlaceController / {nameof(PlaceController.UpdatePlace)}");
                    return InternalServerError();
                }

                return Ok();
            }

            return BadRequest(ModelState.Values.First().Errors[0].ErrorMessage);

        }

        [HttpDelete]
        [Route("DeletePlace")]

        public async Task<IHttpActionResult> DeletePlace(int placeCode)
        {

            try
            {
                await _placeService.DeletePlace(placeCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in PlaceController / {nameof(PlaceController.DeletePlace)}");
                return InternalServerError();
            }

            return Ok();

        }

        [HttpGet]
        [Route("GetPlacesByStateCode")]
        public async Task<IHttpActionResult> GetPlacesByStateCode(int stateCode)
        {
            List<Place> placesList = new List<Place>();
            try
            {
                placesList = await _placeService.GetPlacesByStateCode(stateCode);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in PlaceController / {nameof(PlaceController.GetPlacesByStateCode)}");
                return InternalServerError();
            }

            return Ok(placesList);

        }

        [HttpGet]
        [Route("GetPlacesByDistrictCode")]
        public async Task<IHttpActionResult> GetPlacesByDistrictCode(int distCode)
        {
            List<Place> placesList = new List<Place>();
            try
            {
                placesList = await _placeService.GetPlacesByDistrictCode(distCode);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in PlaceController / {nameof(PlaceController.GetPlacesByDistrictCode)}");
                return InternalServerError();
            }

            return Ok(placesList);

        }
    }
}