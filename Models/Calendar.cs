namespace FamAppAPI.Models
{
    /// <summary>
    /// The calendar.
    /// </summary>
    public class Calendar
    {
        /// <summary>
        /// Gets or Sets the id.
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Gets or Sets the group id.
        /// </summary>
        public int group_id { get; set; }
        /// <summary>
        /// Gets or Sets the groups.
        /// </summary>
        public Groups? Groups { get; set; }
        /// <summary>
        /// Gets or Sets the dates.
        /// </summary>
        public ICollection<Date>? Dates { get; set; }
    }
}
