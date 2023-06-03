using AutoMapper;
using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.ValidateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace GherkinWebAPI.Controllers
{
    [Route("api/V1/[Controller]")]
    public class YearlyHolidayDetailsController : ApiController
    {
        private readonly IYearlyHolidayDetailsService _yearlyHolidayDetailsService;

        public YearlyHolidayDetailsController(IYearlyHolidayDetailsService yearlyHolidayDetailsService)
        {
            _yearlyHolidayDetailsService = yearlyHolidayDetailsService;
        }

        [HttpGet]
        [Route("GetAllWeekDays")]
        public async Task<IHttpActionResult> GetAllWeekDays()
        {
            try
            {
                var weekDays = Mapper.Map<List<WeekDay>>(await _yearlyHolidayDetailsService.GetAllWeekDays());
                return weekDays.Count > 0 ? Ok(weekDays.OrderBy(c => c.Weekly_Weekdays_No)) : (IHttpActionResult)NotFound();
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("GetYearlyCalendarDetailsByEmpId")]
        public async Task<IHttpActionResult> GetYearlyCalendarDetailsByEmpId(string employeeId)
        {
            try
            {
                var YearlyCalendarDetails = Mapper.Map<List<YearlyCalendarDTO>>(await _yearlyHolidayDetailsService.GetYearlyCalendarDetailsByEmpId(employeeId));
                return Ok(YearlyCalendarDetails);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [ValidateModelState]
        [Route("AddYearlyCalendarDetails")]
        public async Task<IHttpActionResult> AddYearlyCalendarDetails(YearlyCalendar yearlyCalendar)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            try
            {
                await _yearlyHolidayDetailsService.AddYearlyCalendarDetails(Mapper.Map<YearlyCalendar>(yearlyCalendar));
                return Ok();
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [ValidateModelState]
        [Route("AddWeeklyHolidays")]
        public async Task<IHttpActionResult> AddWeeklyHolidays(List<WeeklyHoliday> weeklyHolidaysList)
        {
            try
            {
                await _yearlyHolidayDetailsService.AddWeeklyHolidays(Mapper.Map<List<WeeklyHoliday>>(weeklyHolidaysList));
                return Ok();
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [ValidateModelState]
        [Route("AddStatutoryHolidays")]
        public async Task<IHttpActionResult> AddStatutoryHolidays(List<StatutoryHoliday> statutoryHolidays)
        {
            try
            {
                await _yearlyHolidayDetailsService.AddStatutoryHolidays(Mapper.Map<List<StatutoryHoliday>>(statutoryHolidays));
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
