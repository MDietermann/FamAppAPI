using FamAppAPI.Data;
using FamAppAPI.Interfaces;
using FamAppAPI.Models;

public class UserRepository : IUserRepository
{
    private readonly DataContext dataContext;

    public UserRepository(DataContext context)
    {
        dataContext = context;
    }

    public ICollection<User> GetUsers() => dataContext.Users.OrderBy(u => u.id).ToList();

    public User? GetUserById(int id) => dataContext.Users.SingleOrDefault(u => u.id == id);

    // Einen Benutzer anhand der E-Mail-Adresse abrufen
    public User? GetUserByMail(string email) => dataContext.Users.SingleOrDefault(u => u.email == email);

    // Überprüfen, ob ein Benutzer anhand der ID existiert
    public bool UserExistsById(int id) => dataContext.Users.Any(u => u.id == id);

    // Überprüfen, ob ein Benutzer anhand der E-Mail-Adresse existiert
    public bool UserExistsByMail(string email) => dataContext.Users.Any(u => u.email == email);

    // Die Anzahl der Gruppen eines Benutzers abrufen
    public int GetUserGroupCount(int userId)
    {
        var groupCount = dataContext.UsersInGroups.Where(ug => ug.UserId == userId);
        return groupCount.Any() ? groupCount.Count() : 0;
    }
    // Erstelle einen neuen Benutzer
    public bool CreateUser(User user)
    {
        dataContext.Add(user);
        return Save();
    }

    // Daten des Benutzers aktualisieren
    public bool UpdateUser(User user)
    {
        dataContext.Update(user);
        return Save();
    }

    // Benutzer löschen
    public bool DeleteUser(User user)
    {
        dataContext.Remove(user);
        return Save();
    }

    public bool Save() 
        => dataContext.SaveChanges() > 0
        ? true
        : false;
}