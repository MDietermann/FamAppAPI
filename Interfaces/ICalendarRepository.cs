using FamAppAPI.Models;

namespace FamAppAPI.Interfaces
{
    public interface ICalendarRepository
    {
        ICollection<Calendar> GetCalendars();
        Calendar? GetCalendarOfGroup(int groupId);
        Calendar? GetCalendar(int id);
        bool CalendarExists(int calendarId);
        bool CreateCalendar(Calendar calendar);
        bool DeleteCalendar(Calendar calendar);
        bool Save();
    }
}
