using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
    public interface IYearlyHolidayDetailsRepository
    {
        Task<YearlyCalendar> InsertYearlyCalendarDetails(YearlyCalendar yearlyCalendar);
        Task<IEnumerable<WeeklyHoliday>> InsertWeeklyHolidaysList(List<WeeklyHoliday> weeklyHolidaysList);
        Task<IEnumerable<StatutoryHoliday>> InsertStatutoryHolidaysList(List<StatutoryHoliday> statutoryHolidays);
        Task<List<YearlyCalendarDTO>> GetAllYearlyCalendarDetails(string employeeId);
        Task<List<WeekDay>> GetAllWeekDays();
    }
}
