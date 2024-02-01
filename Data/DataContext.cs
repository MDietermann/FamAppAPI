using FamAppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FamAppAPI.Data
{
    /// <summary>
    /// Stellt den Datenkontext für die FamAppAPI dar.
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="DataContext"/> Klasse.
        /// </summary>
        /// <param name="options">Die Optionen zur Konfiguration des Datenkontexts.</param>
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or Sets the groups.
        /// </summary>
        public DbSet<Groups> Groups { get; set; }

        /// <summary>
        /// Gets or Sets the users.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or Sets the users in groups.
        /// </summary>
        public DbSet<UserInGroup> UsersInGroups { get; set; }

        // Ruft die Kalender in der API ab oder legt sie fest.
        /// <summary>
        /// Gets or Sets the calendar.
        /// </summary>
        public DbSet<Calendar> Calendar { get; set; }

        /// <summary>
        /// Gets or Sets the date.
        /// </summary>
        public DbSet<Date> Date { get; set; }

        /// <summary>
        /// On model creating.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region UserInGroup

            // Konfiguriert den Primärschlüssel für die UserInGroup-Entität
            modelBuilder.Entity<UserInGroup>()
                .HasKey(userInGroup => new { userInGroup.UserId, userInGroup.GroupId });

            // Konfiguriert die Beziehung zwischen UserInGroup und Users-Entitäten
            modelBuilder.Entity<UserInGroup>()
                .HasOne(userInGroup => userInGroup.Users)
                .WithMany(user => user.UsersInGroups)
                .HasForeignKey(userInGroup => userInGroup.UserId);

            // Konfiguriert die Beziehung zwischen UserInGroup und Groups-Entitäten
            modelBuilder.Entity<UserInGroup>()
                .HasOne(userInGroup => userInGroup.Groups)
                .WithMany(group => group.UsersInGroups)
                .HasForeignKey(userInGroup => userInGroup.GroupId);

            #endregion

            #region Groups

            modelBuilder.Entity<User>()
                .HasMany(user => user.Groups) // Konfiguriert die Beziehung zwischen Groups und Users-Entitäten, um eine Gruppe zu erstellen, die nur einem User zugewiesen werden kann
                .WithOne(group => group.User)
                .HasForeignKey(group => group.UserId)
                .IsRequired();

            #endregion
        }
    }
}