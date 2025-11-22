using NotesApp.Models;

namespace NotesApp.Abstractions;

public interface IUserRepository
{
    public List<User> GetAllUsers();
    public int CreateUser(string login, string password);
    public string UpdateUserLogin(int id, string login);
    public void DeleteUser(int id);
    public User? GetUserById(int id);
    public User? GetUserByLogin(string login);
}