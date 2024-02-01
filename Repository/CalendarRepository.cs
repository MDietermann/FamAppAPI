using FamAppAPI.Data;
using FamAppAPI.Interfaces;
using FamAppAPI.Models;

namespace FamAppAPI.Repository
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly DataContext dataContext;

        public CalendarRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public ICollection<Calendar> GetCalendars() => dataContext.Calendar.OrderBy(c => c.id).ToList();

        public Calendar? GetCalendarOfGroup(int groupId) => dataContext.Calendar.SingleOrDefault(c => c.group_id == groupId);

        public Calendar? GetCalendar(int id) => dataContext.Calendar.SingleOrDefault(c => c.id == id);

        public bool CalendarExists(int calendarId) => dataContext.Calendar.Any(c => c.id == calendarId);

        public bool CreateCalendar(Calendar calendar)
        {
            dataContext.Add(calendar);
            return Save();
        }

        public bool DeleteCalendar(Calendar calendar)
        {
            dataContext.Remove(calendar);
            return Save();
        }

        public bool Save()
            => dataContext.SaveChanges() > 0
            ? true
            : false;
    }
}
