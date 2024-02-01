using FamAppAPI.Data;
using FamAppAPI.Interfaces;
using FamAppAPI.Models;

namespace FamAppAPI.Repository
{
    public class DateRepository : IDateRepository
    {
        private readonly DataContext dataContext;

        public DateRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public ICollection<Date> GetDates() 
            => dataContext.Date.OrderBy(d => d.id).ToList();

        public ICollection<Date>? GetDatesByCalendar(int calendarId) 
            => dataContext.Date.Where(d => d.calendar_id == calendarId).ToList();

        public ICollection<Date>? GetDatesByUser(int userId)
            => dataContext.Date.Where(d => d.user_id == userId).ToList();

        public ICollection<Date>? GetDatesByUserAndCalendar(int userId, int calendarId)
            => dataContext.Date.Where(d => d.user_id == userId && d.calendar_id == calendarId).ToList();

        public Date GetDate(int id)
            => dataContext.Date.SingleOrDefault(d => d.id == id);

        public bool DateExists(int id)
            => dataContext.Date.Any(d => d.id == id);

        public bool DateExistsUser(int userId)
            => dataContext.Date.Any(d => d.user_id == userId);

        public bool DateExistsCalendar(int calendarId)
            => dataContext.Date.Any(d => d.calendar_id == calendarId);

        public bool CreateDate(Date date)
        {
            dataContext.Add(date);
            return Save();
        }

        public bool UpdateDate(Date date)
        {
            dataContext.Update(date);
            return Save();
        }

        public bool DeleteDate(Date date)
        {
            dataContext.Remove(date);
            return Save();
        }

        public bool Save()
            => dataContext.SaveChanges() > 0
            ? true
            : false;
    }
}
