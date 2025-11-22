using NotesApp.Models;

namespace NotesApp.Abstractions;

public interface IUserRepository
{
    public List<User> GetAllUsers();
    public int CreateUser(string login, string password);
    public void UpdateUser(int id, string login, string password);
    public void DeleteUser(int id);
    public User? GetUserById(int id);
}