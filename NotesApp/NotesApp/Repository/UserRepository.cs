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

    public void UpdateUser(int id, string login, string password)
    {
        var user = TryGetUserByIdAndThrowIfNotFound(id);
        
        user.Login = login;
        user.Password = password;
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
       
