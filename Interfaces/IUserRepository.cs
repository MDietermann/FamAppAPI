using FamAppAPI.Models;

namespace FamAppAPI.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();

        User? GetUserById(int id);

        User? GetUserByMail(string email);

        int GetUserGroupCount(int userId);

        bool UserExistsById(int id);

        bool UserExistsByMail(string email);

        bool CreateUser(User user);

        bool UpdateUser(User user);

        bool DeleteUser(User user);

        bool Save();
    }
}