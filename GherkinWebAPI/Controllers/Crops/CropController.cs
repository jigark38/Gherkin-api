namespace GherkinWebAPI.Controllers
{
    using AutoMapper;
    using GherkinWebAPI.Core;
    using GherkinWebAPI.DTO;
    using GherkinWebAPI.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    /// <summary>
    /// Defines the <see cref="CropController" />
    /// </summary>
    [Route("api/V1/[Controller]")]
    public class CropController : ApiController
    {
        /// <summary>
        /// Defines the _service
        /// </summary>
        private readonly ICropService _service;

        /// <summary>
        /// Defines the controller
        /// </summary>
        private readonly string controller = nameof(CropController);

        /// <summary>
        /// Initializes a new instance of the <see cref="CropController"/> class.
        /// </summary>
        /// <param name="cropService">The cropService<see cref="ICropService"/></param>
        public CropController(ICropService cropService)
        {
            _service = cropService;
        }

        /// <summary>
        /// The GetCropGroup
        /// </summary>
        /// <returns>The <see cref="Task{IHttpActionResult}"/></returns>
        [HttpGet]
        [Route("GetCropGroup")]
        public async Task<IHttpActionResult> GetCropGroup()
        {
            try
            {
                var res = Mapper.Map<List<CropGroupDto>>(await _service.GetAllCropGroup());
                return res.Count > 0 ? Ok(res.OrderBy(c => c.Name)) : (IHttpActionResult)NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CropController.GetCropGroup)}");
                return InternalServerError();
            }
        }

        /// <summary>
        /// The AddCropGroup
        /// </summary>
        /// <param name="cropGroup">The cropGroup<see cref="CropGroupDto"/></param>
        /// <returns>The <see cref="Task{IHttpActionResult}"/></returns>
        [Route("AddCropGroup"), HttpPost]
        public async Task<IHttpActionResult> AddCropGroup([FromBody]CropGroupDto cropGroup)
        {
            try
            {
                await _service.AddCropGroup(Mapper.Map<CropGroup>(cropGroup));
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CropController.AddCropGroup)}");
                return InternalServerError();
            }
        }

        /// <summary>
        /// The AddCrop
        /// </summary>
        /// <param name="cropViewModel">The cropViewModel<see cref="CropSchemeDto"/></param>
        /// <returns>The <see cref="Task{IHttpActionResult}"/></returns>
        [Route("AddCrop"), HttpPost]
        public async Task<IHttpActionResult> AddCrop([FromBody]CropSchemeDto cropViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var crop = new Crop()
                    {
                        Name = cropViewModel.CropName,
                        CropGroupCode = cropViewModel.GroupCode
                    };

                    var result = await _service.AddCrop(crop);
                    var cropScheme = Mapper.Map<List<CropScheme>>(cropViewModel.Schemes);
                    cropScheme.ForEach(i =>
                     {
                         i.CropCode = result.CropCode;
                     });

                    await _service.AddCropScheme(cropScheme);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CropController.AddCrop)}");
                    return InternalServerError();
                }

                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// The GetAllCrops
        /// </summary>
        /// <returns>The <see cref="Task{IHttpActionResult}"/></returns>
        [Route("GetAllCrops"), HttpGet]
        public async Task<IHttpActionResult> GetAllCrops()
        {
            try
            {
                var res = Mapper.Map<List<CropDto>>(await _service.GetAllCrops());
                return res.Count > 0 ? Ok(res.OrderBy(c => c.Name)) : (IHttpActionResult)NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CropController.GetAllCrops)}");
                return InternalServerError();
            }
        }

        /// <summary>
        /// The Search
        /// </summary>
        /// <param name="groupCode">The groupCode<see cref="string"/></param>
        /// <param name="cropName">The cropName<see cref="string"/></param>
        /// <returns>The <see cref="Task{IHttpActionResult}"/></returns>
        [Route("Search"), HttpPost]
        public async Task<IHttpActionResult> Search(CropDto crop)
        {
            if (string.IsNullOrEmpty(crop.CropGroupCode) || string.IsNullOrEmpty(crop.Name))
            {
                return BadRequest("Please provide the valid Crop group code and Crop name");
            }

            try
            {
                var result = await _service.SearchCrop(crop.CropGroupCode, crop.Name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CropController.Search)}");
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// The EditCrop
        /// </summary>
        /// <param name="cropViewModel">The cropViewModel<see cref="CropSchemeDto"/></param>
        /// <returns>The <see cref="Task{IHttpActionResult}"/></returns>
        [Route("EditCrop"), HttpPost]
        public async Task<IHttpActionResult> EditCrop([FromBody]CropSchemeDto cropViewModel)
        {
            try
            {
                var crop = new Crop()
                {
                    Name = cropViewModel.CropName,
                    CropGroupCode = cropViewModel.GroupCode,
                    CropCode = cropViewModel.CropCode
                };

                await _service.UpDateCrop(crop);
                var cropScheme = Mapper.Map<List<CropScheme>>(cropViewModel.Schemes);

                await _service.UpdateCropScheme(cropScheme);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CropController.EditCrop)}");
                return InternalServerError();
            }

            return Ok();
        }

        [Route("GetCropSchemes"), HttpGet]
        public async Task<IHttpActionResult> GetCropSchemes(string cropCode = "")
        {
            try
            {
                var res = await _service.GetCropSchemes(cropCode);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CropController.GetCropSchemes)}");
                return InternalServerError();
            }
        }

        [Route("GetCropListByCropGroupCode/{cropGroupCode}"), HttpGet]
        public async Task<IHttpActionResult> GetCropListByCropGroupCode(string cropGroupCode)
        {
            try
            {
                var cropList = await _service.GetCropListByCropGroupCode(cropGroupCode);
                return Ok(cropList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(CropController.GetCropListByCropGroupCode)}");
                return InternalServerError();
            }
        }
    }
}