namespace FamAppAPI.Models
{

    /// <summary>
    /// The groups.
    /// </summary>
    public class Groups
    {
        /// <summary>
        /// Gets or Sets the id.
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Gets or Sets the name.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Gets or Sets the user id.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Gets or Sets a value indicating whether premium.
        /// </summary>
        public bool premium { get; set; }
        /// <summary>
        /// Gets or Sets the user.
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Gets or Sets the users in groups.
        /// </summary>
        public ICollection<UserInGroup>? UsersInGroups { get; set; }
        /// <summary>
        /// Gets or Sets the calendar.
        /// </summary>
        public Calendar? Calendar { get; set; }
    }
}
