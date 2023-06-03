using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using GherkinWebAPI.Core;
using GherkinWebAPI.DTO.BankAccount;
using GherkinWebAPI.Models;

namespace GherkinWebAPI.Controllers
{
    [Route("api/V1/[Controller]")]
    public class BankAccountDetailsController : ApiController
    {
        private IBankAccountDetailsService _bankAccountDetailsService;
        public BankAccountDetailsController(IBankAccountDetailsService bankAccountDetailsService)
        {
            _bankAccountDetailsService = bankAccountDetailsService;
        }

        [HttpPost,Route("AddBankAccountDetails")]
        public async Task<HttpResponseMessage> AddBankAccountDetails([FromBody]BankAccountDetails addBankAccountDetails)
        {
            BankAccountDetails bankAccountDetails = new BankAccountDetails();
            try
            {
                if (addBankAccountDetails != null || ModelState.IsValid)
                {
                    bankAccountDetails = await _bankAccountDetailsService.CreateBankAccount(addBankAccountDetails);
                    return Request.CreateResponse(HttpStatusCode.OK,bankAccountDetails);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
                catch(Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
                
        }
        [HttpPost, Route("UpdateBankAccountDetails")]
        public async Task<HttpResponseMessage> UpdateBankAccountDetails(string bankCode,[FromBody]BankAccountDetails addBankAccountDetails)
        {
            BankAccountDetails bankAccountDetails = new BankAccountDetails();
            try
            {
                if (addBankAccountDetails != null || ModelState.IsValid)
                {
                    bankAccountDetails = await _bankAccountDetailsService.UpdateBankAccount(bankCode, addBankAccountDetails);
                    return Request.CreateResponse(HttpStatusCode.OK, bankAccountDetails);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpPost, Route("SuspendBankAccount")]
        public async Task<HttpResponseMessage> CloseBankAccount([FromBody]BankAccountClose bankAccountClose)
        {
            BankAccountClose bankAccountDetails = new BankAccountClose();
            try
            {
                if (bankAccountClose != null || ModelState.IsValid)
                {
                    bankAccountDetails = await _bankAccountDetailsService.CloseBankAccount(bankAccountClose);
                    return Request.CreateResponse(HttpStatusCode.OK, bankAccountClose);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound,"Invalid Details Provided!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPost, Route("UpdateSuspendBankAccount")]
        public async Task<HttpResponseMessage> UpdateSuspendBankAccount(string bankCode,[FromBody]BankAccountClose bankAccountClose)
        {
            BankAccountClose bankAccountDetails = new BankAccountClose();
            try
            {
                if (bankAccountClose != null || ModelState.IsValid)
                {
                    bankAccountDetails = await _bankAccountDetailsService.UpdateClosedAccount(bankCode,bankAccountClose);
                    return Request.CreateResponse(HttpStatusCode.OK, bankAccountClose);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Invalid Details Provided!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpGet, Route("GetBankAccountDetails")]
        public async Task<HttpResponseMessage> GetBankAccountDetails()
        {
            try
            {
              var  _bankAccountDetails = await _bankAccountDetailsService.GetAccountDetails();
                if(_bankAccountDetails.Count()>0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _bankAccountDetails); 
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpGet, Route("GetAccountDetailsByBankCode")]
        public async Task<HttpResponseMessage> GetAccountDetailsByBankCode(string bankCode)
        {
          BankAccountDetails _bankAccountDetails = new BankAccountDetails();
            try
            {
                _bankAccountDetails = await _bankAccountDetailsService.GetAccountDetailsByBankCode(bankCode);
                    return Request.CreateResponse(HttpStatusCode.OK, _bankAccountDetails);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet, Route("GetAccountStatusByBankCode")]
        public async Task<HttpResponseMessage> GetAccountStatusByBankCode(string bankCode)
        {
            BankAccountClose _bankAccountStatus = new BankAccountClose();
            try
            {
                _bankAccountStatus = await _bankAccountDetailsService.GetAccountStatus(bankCode);
                return Request.CreateResponse(HttpStatusCode.OK, _bankAccountStatus);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}
