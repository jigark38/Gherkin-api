using AutoMapper;
using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.DTO.Product_GradeDto;
using GherkinWebAPI.Models;
using GherkinWebAPI.ValidateModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GherkinWebAPI.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProductDetailsController : ApiController
    {
        /// <summary>
        /// Defines the _service
        /// </summary>
        private readonly IProductDetailsService _service;

        /// <summary>
        /// Defines the controller
        /// </summary>
        private readonly string controller = nameof(ProductDetailsController);

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDetailsController"/> class.
        /// </summary>
        /// <param name="cropService">The ProductDetailsService<see cref="IProductDetailsService"/></param>
        public ProductDetailsController(IProductDetailsService productgrpService)
        {
            _service = productgrpService;
        }
        [HttpGet, Route("GetallProductGroup")]
        public async Task<IHttpActionResult> GetAllProductGroup()
        {
            try
            {
                var details = await _service.GetAllProductGroupAsync();
                return Ok(details);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(ProductDetailsController.GetAllProductGroup)}");
                return InternalServerError();
            }
        }

        [HttpGet, Route("GetallProductGroupDetailsGrid")]
        public async Task<IHttpActionResult> GetAllproductAsync()
        {
            try
            {
                var details = await _service.GetAllproductAsync();
                return Ok(details);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(ProductDetailsController.GetAllproductAsync)}");
                return InternalServerError();
            }
        }
        [CheckModelForNull]
        [ValidateModelState]
        [HttpPost, Route("AddProductGroup")]
        public async Task<IHttpActionResult> AddProductGroup([FromBody]ProductGroup productGroup)
        {
            try
            {
                var res = await _service.AddProductGroup(productGroup);
                return Ok(productGroup);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(ProductDetailsController.AddProductGroup)}");
                return InternalServerError();
            }
        }

        [HttpPost, Route("AddVeraiety")]
        public async Task<IHttpActionResult> AddVeraiety([FromBody]ProductDetails productDetails)
        {
            try
            {
                await _service.AddVariety(productDetails);
                return Ok(productDetails);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(ProductDetailsController.AddVeraiety)}");
                return InternalServerError();
            }
        }

        [HttpGet, Route("GetVareityByprodgrpcode")]
        public async Task<IHttpActionResult> GetVareityByprodgrpcode(string prodgroupcode)
        {
            try
            {
                var details = await _service.GetAllVarietyAsync(prodgroupcode);
                return Ok(details);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(ProductDetailsController.GetVareityByprodgrpcode)}");
                return InternalServerError();
            }
        }

        [HttpPost, Route("Addgrade")]
        public async Task<IHttpActionResult> AddGrade([FromBody]GradeDetails gradeDetails)
        {
            try
            {
                await _service.AddGrade(gradeDetails);
                return Ok(gradeDetails);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(ProductDetailsController.AddGrade)}");
                return InternalServerError();
            }
        }

        [HttpGet, Route("GetAllGradeByVariety")]
        public async Task<IHttpActionResult> GetAllGradeByVariety(string Varietycode)
        {
            try
            {
                var details = await _service.GetAllGradeByVarietyAsync(Varietycode);
                return Ok(details);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(ProductDetailsController.GetAllGradeByVariety)}");
                return InternalServerError();
            }
        }

    }
}
