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

    public int CreateUser(CreateUserDto dto)
    {
        var userId = _users.Count;
        var user = _mapper.Map<User>(dto);
        user.Id = userId;
        _users.Add(user);
        
        return userId;
    }

    public string UpdateUserLogin(int id,UpdateUserDto dto)
    {
        var user = TryGetUserByIdAndThrowIfNotFound(id);

        user.Login = dto.Login;
        return user.Login;
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
            throw new UserNotFoundByLoginException(login);
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
       
