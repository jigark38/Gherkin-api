using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Service
{
    public class YearlyHolidayDetailsService : IYearlyHolidayDetailsService
    {
        /// <summary>
        /// Defines the _repositoryWrapper
        /// </summary>
        private readonly IRepositoryWrapper _repositoryWrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CropService"/> class.
        /// </summary>
        /// <param name="repositoryWrapper">The repositoryWrapper<see cref="IRepositoryWrapper"/></param>
        public YearlyHolidayDetailsService(IRepositoryWrapper repositoryWrapper)
        {
            this._repositoryWrapper = repositoryWrapper;
        }

        /// <summary>
        /// AddYearlyCalendarDetails
        /// </summary>
        /// <param name="yearlyCalendar">The group<see cref="YearlyCalendar"/></param>
        /// <returns>The <see cref="Task{YearlyCalendar}"/></returns>
        public async Task<YearlyCalendar> AddYearlyCalendarDetails(YearlyCalendar yearlyCalendar)
        {
            yearlyCalendar.Entry_Date = DateTime.UtcNow;
            return await _repositoryWrapper.YearlyHolidayDetailsRepository.InsertYearlyCalendarDetails(yearlyCalendar);
        }

        /// <summary>
        /// AddWeeklyHolidays
        /// </summary>
        /// <param name="weeklyHolidaysList">The group<see cref="List<WeeklyHolidays>"/></param>
        /// <returns>The <see cref="Task{IEnumerable<WeeklyHolidays>}"/></returns>
        public async Task<IEnumerable<WeeklyHoliday>> AddWeeklyHolidays(List<WeeklyHoliday> weeklyHolidaysList)
        {
            return await _repositoryWrapper.YearlyHolidayDetailsRepository.InsertWeeklyHolidaysList(weeklyHolidaysList);
        }

        /// <summary>
        /// AddStatutoryHolidays
        /// </summary>
        /// <param name="statutoryHolidays">The group<see cref="List<StatutoryHolidays>"/></param>
        /// <returns>The <see cref="Task{IEnumerable<StatutoryHolidays>}"/></returns>
        public async Task<IEnumerable<StatutoryHoliday>> AddStatutoryHolidays(List<StatutoryHoliday> statutoryHolidays)
        {
            return await _repositoryWrapper.YearlyHolidayDetailsRepository.InsertStatutoryHolidaysList(statutoryHolidays);
        }

        /// <summary>
        /// GetYearlyCalendarDetailsByEmpId
        /// </summary>
        /// <param name="employeeId">The group<see cref=""/></param>
        /// <returns>The <see cref="List<YearlyCalendarDTO>"/></returns>
        public async Task<List<YearlyCalendarDTO>> GetYearlyCalendarDetailsByEmpId(string employeeId)
        {
            return await _repositoryWrapper.YearlyHolidayDetailsRepository.GetAllYearlyCalendarDetails(employeeId);
        }

        /// <summary>
        /// GetAllWeekDays
        /// </summary>
        /// <param ></param>
        /// <returns>The IEnumerable<WeekDays><see cref="WeekDays"/></returns>
        public async Task<IEnumerable<WeekDay>> GetAllWeekDays()
        {
            return await _repositoryWrapper.YearlyHolidayDetailsRepository.GetAllWeekDays();
        }

    }
}