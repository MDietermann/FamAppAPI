using System.Web.Http.Routing.Constraints;

namespace FamAppAPI.Models
{
    /// <summary>
    /// The date.
    /// </summary>
    public class Date
    {
        /// <summary>
        /// Gets or Sets the id.
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Gets or Sets the calendar id.
        /// </summary>
        public int calendar_id { get; set; }
        /// <summary>
        /// Gets or Sets the user id.
        /// </summary>
        public int user_id { get; set; }
        /// <summary>
        /// Gets or Sets the start date.
        /// </summary>
        public DateTime start_date { get; set; }
        /// <summary>
        /// Gets or Sets the end date.
        /// </summary>
        public DateTime end_date { get; set; }
        /// <summary>
        /// Gets or Sets the calendars.
        /// </summary>
        public Calendar? Calendars { get; set; }
        /// <summary>
        /// Gets or Sets the users.
        /// </summary>
        public ICollection<User>? Users { get; set; }
    }
}
