using FamAppAPI.Models;

namespace FamAppAPI.Interfaces
{
    public interface IDateRepository
    {
        ICollection<Date> GetDates();
        ICollection<Date>? GetDatesByCalendar(int calendarId);
        ICollection<Date>? GetDatesByUser(int userId);
        ICollection<Date>? GetDatesByUserAndCalendar(int userId, int calendarId);
        Date GetDate(int id);
        bool DateExists(int id);
        bool DateExistsUser(int userId);
        bool DateExistsCalendar(int calendarId);
        bool CreateDate(Date date);
        bool UpdateDate(Date date);
        bool DeleteDate(Date date);
        bool Save();
    }
}
