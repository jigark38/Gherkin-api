using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
    public interface IYearlyHolidayDetailsService
    {
        Task<YearlyCalendar> AddYearlyCalendarDetails(YearlyCalendar yearlyCalendar);
        Task<IEnumerable<WeeklyHoliday>> AddWeeklyHolidays(List<WeeklyHoliday> weeklyHolidaysList);
        Task<IEnumerable<StatutoryHoliday>> AddStatutoryHolidays(List<StatutoryHoliday> statutoryHolidays);
        Task<List<YearlyCalendarDTO>> GetYearlyCalendarDetailsByEmpId(string employeeId);
        Task<IEnumerable<WeekDay>> GetAllWeekDays();
    }
}
