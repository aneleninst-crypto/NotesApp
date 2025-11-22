using NotesApp.Abstractions;
using NotesApp.Exceptions;
using NotesApp.Models;

namespace NotesApp.Repository;

public class UserRepository : IUserRepository
{
    private readonly List<User> _users = new ();
    public List<User> GetAllUsers()
    {
        return _users;
    }

    public int CreateUser(string login, string password)
    {
        var userId = _users.Count;
        _users.Add(new User(userId, login, password));
        return userId;
    }

    public string UpdateUserLogin(int id, string login)
    {
        var user = TryGetUserByIdAndThrowIfNotFound(id);
        
        user.Login = login;
        return login;
    }

    public void DeleteUser(int id)
    {
        var  user = TryGetUserByIdAndThrowIfNotFound(id);
        _users.Remove(user);
    }

    public User? GetUserById(int id)
    {        
        return _users.FirstOrDefault(n => n.Id == id);
    }

    public User? GetUserByLogin(string login)
    {
        return _users.FirstOrDefault(n => n.Login.Equals(StringComparison.OrdinalIgnoreCase));
    }

    private User TryGetUserByIdAndThrowIfNotFound(int id)
    {
        var user = _users.FirstOrDefault(n => n.Id == id);
        if (user == null)
        {
            throw new UserNotFoundException(id);
        }
        return user;
    }
}
       
