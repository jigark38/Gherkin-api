using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GherkinWebAPI.Repository
{
    public class YearlyHolidayDetailsRepository : RepositoryBase<YearlyCalendar>, IYearlyHolidayDetailsRepository
    {
        private readonly RepositoryContext _context;

        public YearlyHolidayDetailsRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task<YearlyCalendar> InsertYearlyCalendarDetails(YearlyCalendar yearlyCalendar)
        {
            try
            {
                yearlyCalendar.Entry_Date = yearlyCalendar.Entry_Date.AddDays(1);
                yearlyCalendar.Yearly_Calender_Date_From = yearlyCalendar.Yearly_Calender_Date_From.AddDays(1);
                yearlyCalendar.Yearly_Calender_Date_To = yearlyCalendar.Yearly_Calender_Date_To.AddDays(1);
                var result = this._context.YearlyCalendar.Add(yearlyCalendar);
                await _context.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<IEnumerable<WeeklyHoliday>> InsertWeeklyHolidaysList(List<WeeklyHoliday> weeklyHolidaysList)
        {
            try
            {
                var result = this._context.WeeklyHolidays.AddRange(weeklyHolidaysList);
                await _context.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<StatutoryHoliday>> InsertStatutoryHolidaysList(List<StatutoryHoliday> statutoryHolidays)
        {
            try
            {
                foreach (var item in statutoryHolidays)
                {
                    item.Holiday_Date = item.Holiday_Date.AddDays(1);
                }
                var result = this._context.StatutoryHolidays.AddRange(statutoryHolidays);
                await _context.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<WeekDay>> GetAllWeekDays()
        {
            try
            {
                var result = this._context.WeekDays;
                return await result.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch week days");
            }
        }

        public async Task<List<YearlyCalendarDTO>> GetAllYearlyCalendarDetails(string employeeId)
        {
            try
            {

                var yearlyCalendars = await (from ycal in _context.YearlyCalendar
                                             select new YearlyCalendarDTO
                                             {
                                                 Entered_Emp_ID = ycal.Entered_Emp_ID,
                                                 Entry_Date = ycal.Entry_Date,
                                                 Yearly_Calender_Date_From = ycal.Yearly_Calender_Date_From,
                                                 Yearly_Calender_Date_To = ycal.Yearly_Calender_Date_To,
                                                 Yearly_Holidays_Passing_No = ycal.Yearly_Holidays_Passing_No,
                                                 Yearly_Statutory_Holidays = ycal.Yearly_Statutory_Holidays,
                                             }).OrderByDescending(c => c.Yearly_Holidays_Passing_No).ToListAsync();

                foreach (var item in yearlyCalendars)
                {
                    var weeklyHolidaysDTOs = new List<WeeklyHolidaysDTO>();
                    var statutoryHolidaysDTOs = new List<StatutoryHolidaysDTO>();

                    weeklyHolidaysDTOs = await (from wh in _context.WeeklyHolidays
                                                where wh.Entered_Emp_ID == item.Entered_Emp_ID && wh.Yearly_Holidays_Passing_No == item.Yearly_Holidays_Passing_No
                                                select new WeeklyHolidaysDTO
                                                {
                                                    ID = wh.ID,
                                                    Yearly_Holidays_Passing_No = wh.Yearly_Holidays_Passing_No,
                                                    Weekly_Weekdays_No = wh.Weekly_Weekdays_No,
                                                    Entered_Emp_ID = wh.Entered_Emp_ID
                                                }).OrderBy(c => c.Weekly_Weekdays_No).ToListAsync();

                    statutoryHolidaysDTOs = await (from sh in _context.StatutoryHolidays
                                                   where sh.Entered_Emp_ID == item.Entered_Emp_ID && sh.Yearly_Holidays_Passing_No == item.Yearly_Holidays_Passing_No
                                                   select new StatutoryHolidaysDTO
                                                   {
                                                       Holiday_Occasion = sh.Holiday_Occasion,
                                                       Holiday_Date = sh.Holiday_Date,
                                                       Statutory_Holiday_No = sh.Statutory_Holiday_No,
                                                       Yearly_Holidays_Passing_No = sh.Yearly_Holidays_Passing_No,
                                                       Weekly_Weekdays_No = sh.Weekly_Weekdays_No,
                                                       Entered_Emp_ID = sh.Entered_Emp_ID
                                                   }).OrderBy(c => c.Weekly_Weekdays_No).ToListAsync();

                    item.Weekly_Holidays = weeklyHolidaysDTOs;
                    item.Statutory_Holidays = statutoryHolidaysDTOs;
                }

                return yearlyCalendars;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}