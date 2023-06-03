using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.ValidateModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace GherkinWebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ConsigneeBuyersController : ApiController
    {
        private readonly IConsgineeBuyersService _service;
        private readonly string controller = nameof(ConsigneeBuyersController);
        private readonly RepositoryContext _repositoryContext;
        private readonly IRepositoryWrapper _repository;
        private readonly ILogger<ConsigneeBuyersDetails> _logger;
        public ConsigneeBuyersController(IConsgineeBuyersService service, IRepositoryWrapper repository, RepositoryContext repositoryContext)
        {
            this._service = service;
            //   _logger = logger;
            this._repository = repository;
            this._repositoryContext = repositoryContext;
        }

        [HttpGet]
        [Route("GetConsigneeBuyers")]
        public async Task<IHttpActionResult> GetConsigneeBuyers()
        {

            List<ConsgineeBuyersDto> details = new List<ConsgineeBuyersDto>();
            try
            {
                var res = await _service.GetAllConsgineeBuyersDetails();

                return Ok(res);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(ConsigneeBuyersController.GetConsigneeBuyersById)}");
                return InternalServerError();
            }

        }
        [Route("GetConsigneeBuyerNameByConsgType")]
        public async Task<IHttpActionResult> GetConsgineeNameByConsType(string consgType)
        {

            List<ConsgineeBuyersDto> details = new List<ConsgineeBuyersDto>();
            try
            {
                var res = await _service.GetConsgineeNameByConsType(consgType);
                return Ok(res);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(ConsigneeBuyersController.GetConsgineeNameByConsType)}");
                return InternalServerError();
            }

        }

        [HttpGet]
        [Route("GetConsigneeBuyersById")]
        public async Task<IHttpActionResult> GetConsigneeBuyersById(string consgType, string CBCode)
        {
            try
            {
                var res = await _service.GetConsgineeBuyersDetailsId(consgType, CBCode);
                return Ok(res);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(ConsigneeBuyersController.GetConsigneeBuyersById)}");
                return InternalServerError();
            }
        }

        [ValidateModelState]
        [HttpPost]
        [Route("AddConsgineeDeatils")]
        public async Task<IHttpActionResult> AddConsgineeBuyersDetails([FromBody]ConsgineeBuyersDto consigneeBuyersViewModel)
        {
            if (ModelState.IsValid)
            {


                try
                {
                    var Consignee = new ConsigneeBuyersDetails()
                    {


                        Cosng_Buyer_Type = consigneeBuyersViewModel.Cosng_Buyer_Type,
                        C_B_Name = consigneeBuyersViewModel.C_B_Name,
                        C_B_Address = consigneeBuyersViewModel.C_B_Address,
                        W_Country_Id = consigneeBuyersViewModel.W_Country_Id,
                        W_State_Id = consigneeBuyersViewModel.W_State_Id,
                        W_City_Id = consigneeBuyersViewModel.W_City_Id,
                        W_Pincode = consigneeBuyersViewModel.W_Pincode,
                        Country_Area_Code = consigneeBuyersViewModel.Country_Area_Code,
                        Managment_Name = consigneeBuyersViewModel.Managment_Name,
                        Mang_Mobile_No = consigneeBuyersViewModel.Mang_Mobile_No,
                        Office_No = consigneeBuyersViewModel.Office_No,
                        Alternate_No = consigneeBuyersViewModel.Alternate_No,
                        Mail_id = consigneeBuyersViewModel.Mail_id,
                        Alt_Mail_id = consigneeBuyersViewModel.Alt_Mail_id,
                        License_No = consigneeBuyersViewModel.License_No,
                        Credit_Limited = consigneeBuyersViewModel.Credit_Limited,
                        Currency_Code = consigneeBuyersViewModel.Currency_Code,
                        Created_Date = consigneeBuyersViewModel.Created_Date,
                        Modified_Date = consigneeBuyersViewModel.Modified_Date,


                    };
                    var result = await _service.AddConsgineeBuyersDetails(Consignee);

                    if (consigneeBuyersViewModel.documentUploadeds != null && consigneeBuyersViewModel.documentUploadeds.Count > 0)
                    {

                        foreach (var item in consigneeBuyersViewModel.documentUploadeds)
                        {
                            byte[] docImage = null;
                            if (!string.IsNullOrEmpty(item.DocDetails))
                            {
                                string ImgStr = Regex.Replace(item.DocDetails, "^data:(image|pdf|application)\\/[a-zA-Z]*-?[a-zA-Z]+;base64,", string.Empty, RegexOptions.IgnoreCase);
                                docImage = Convert.FromBase64String(ImgStr);

                                var regexMatches = Regex.Matches(item.DocDetails, "(^data:(image|pdf|application)\\/[a-zA-Z]*-?[a-zA-Z]+;base64,)", RegexOptions.IgnoreCase);
                                if (regexMatches != null)
                                {
                                    var tt = regexMatches[0].Groups[0].Value;
                                    item.ImagePreappendText = tt;
                                }
                            }

                            var DocDetails = new DocumentUpload();
                            DocDetails.Document_Name = item.Document_Name;
                            DocDetails.C_B_Code = result.C_B_Code;
                            DocDetails.Document_Details = docImage;
                            DocDetails.ImagePreappendText = item.ImagePreappendText;
                            var details = _repositoryContext.DocumentUploads.Add(DocDetails);
                            //  await _repositoryContext.SaveChangesAsync();
                            await _service.AddDocumentDetails(details);
                        }

                    }




                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(ConsigneeBuyersController.AddConsgineeBuyersDetails)}");
                    return InternalServerError();
                }

                return Ok(consigneeBuyersViewModel);
            }

            return BadRequest();
        }

        [CheckModelForNull]
        [ValidateModelState]
        [HttpPut]
        [Route("UpdateConsgineeDeatils")]
        public async Task<IHttpActionResult> UpdateConsgineeDetails(string id, [FromBody]ConsgineeBuyersDto consigneeBuyersViewModel)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    if (consigneeBuyersViewModel != null)
                    {

                        var details = await _service.UpdateConsgineeDetails(id, consigneeBuyersViewModel);
                        if (details != null)
                            return Ok(details);
                        else
                            return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " " + $"Exception in {controller}/{nameof(ConsigneeBuyersController.UpdateConsgineeDetails)}");
                    return InternalServerError();
                }

                return Ok();
            }

            return BadRequest();
        }



    }
}
