using AutoMapper;
using NotesApp.Abstractions;
using NotesApp.Contracts;
using NotesApp.Database;
using NotesApp.Exceptions;
using NotesApp.Models;

namespace NotesApp.Repository;

public class UserRepository(
    IMapper mapper,
    ApplicationDbContext dbContext) : IUserRepository
{
    public ListOfUsers GetAllUsers()
    => mapper.Map<ListOfUsers>(dbContext.Users.ToList());

    public string UpdateUserLogin(Guid id,UpdateUserDto dto)
    {
        var user = TryGetUserByIdAndThrowIfNotFound(id);
        user.Login = dto.Login;
        dbContext.SaveChanges();
        return user.Login;
    }

    public void DeleteUser(Guid id)
    {
        var  user = TryGetUserByIdAndThrowIfNotFound(id);
        dbContext.Remove(user);
        dbContext.SaveChanges();
    }

    public UserVm GetUserById(Guid id)
    {
        var user = TryGetUserByIdAndThrowIfNotFound(id);
        return mapper.Map<UserVm>(user);
    }

    public UserVm GetUserByLogin(string login)
    {
        var user = dbContext.Users.FirstOrDefault(n => n.Login == login);
        if (user is null)
        {
            throw new UserNotFoundByLoginException(login);
        }
        return mapper.Map<UserVm>(user);
    }

    private User TryGetUserByIdAndThrowIfNotFound(Guid id)
    {
        var user = dbContext.Users.FirstOrDefault(n => n.Id == id);
        if (user is null)
        {
            throw new UserNotFoundExceptionId(id);
        }
        return user;
    }
}
       
