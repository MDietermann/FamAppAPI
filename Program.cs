using FamAppAPI.Data;
using FamAppAPI.Interfaces;
using FamAppAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace FamAppAPI
{
    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Der Einstiegspunkt der Anwendung.
        /// </summary>
        /// <param name="args">Befehlszeilenargumente.</param>
        public static void Main(string[] args)
        {
            // Erstellen des Webanwendungserstellers
            var webAppBuilder = WebApplication.CreateBuilder(args);

            // Konfiguration der Dienste
            webAppBuilder.Services.AddControllers();
            webAppBuilder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            webAppBuilder.Services.AddScoped<IUserRepository, UserRepository>();
            webAppBuilder.Services.AddScoped<IGroupsRepository, GroupsRepository>();
            webAppBuilder.Services.AddScoped<IUserInGroupRepository, UserInGroupRepository>();
            webAppBuilder.Services.AddScoped<ICalendarRepository, CalendarRepository>();

            // Konfiguration von Swagger/OpenAPI
            webAppBuilder.Services.AddEndpointsApiExplorer();
            webAppBuilder.Services.AddSwaggerGen();
            webAppBuilder.Services.AddDbContext<DataContext>(options =>
            {
                // Konfiguration des DbContext
                options.UseMySQL(webAppBuilder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Erstellen der Anwendung
            var app = webAppBuilder.Build();

            // Aktivieren von Swagger und Swagger UI in der Entwicklungs-Umgebung
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Konfiguration der Middleware
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}