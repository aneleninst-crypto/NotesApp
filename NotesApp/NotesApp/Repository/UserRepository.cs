using AutoMapper;
using NotesApp.Abstractions;
using NotesApp.Contracts;
using NotesApp.Exceptions;
using NotesApp.Models;

namespace NotesApp.Repository;

public class UserRepository : IUserRepository
{
    private readonly List<User> _users = new ();
    private readonly IMapper _mapper;
    public UserRepository(IMapper mapper)
    {
        _mapper = mapper;
    }
    public ListOfUsers GetAllUsers()
    => _mapper.Map<ListOfUsers>(_users);

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

    public UserVm GetUserById(int id)
    {        
        var user = _users.FirstOrDefault(n => n.Id == id);
        if (user is null)
        {
            throw new UserNotFoundExceptionId(id);
        }
        return _mapper.Map<UserVm>(user);
    }

    public UserVm GetUserByLogin(string login)
    {
        var user = _users.FirstOrDefault(n => n.Login == login);
        if (user is null)
        {
            throw new UserNotFoundExceptionLogin(login);
        }
        return _mapper.Map<UserVm>(user);
    }

    private User TryGetUserByIdAndThrowIfNotFound(int id)
    {
        var user = _users.FirstOrDefault(n => n.Id == id);
        if (user == null)
        {
            throw new UserNotFoundExceptionId(id);
        }
        return user;
    }
}
       
