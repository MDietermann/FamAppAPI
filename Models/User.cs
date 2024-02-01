namespace FamAppAPI.Models
{
    /// <summary>
    /// The user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or Sets the id.
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Gets or Sets the first name.
        /// </summary>
        public string first_name { get; set; }
        /// <summary>
        /// Gets or Sets the last name.
        /// </summary>
        public string last_name { get; set; }
        /// <summary>
        /// Gets or Sets the email.
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// Gets or Sets the password.
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// Gets or Sets the groups.
        /// </summary>
        public ICollection<Groups>? Groups { get; set; }
        /// <summary>
        /// Gets or Sets the users in groups.
        /// </summary>
        public ICollection<UserInGroup>? UsersInGroups { get; set; }
    }
}
