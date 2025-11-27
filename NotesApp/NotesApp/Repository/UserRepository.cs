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
    private readonly IMapper _mapper = mapper; 
    // убрать строчки
    public ListOfUsers GetAllUsers()
    => _mapper.Map<ListOfUsers>(dbContext.Users.ToList());

    public int CreateUser(CreateUserDto dto)
    {
        var user = _mapper.Map<User>(dto);
        dbContext.Add(user);
        dbContext.SaveChanges();
        return user.Id;
    }

    public string UpdateUserLogin(int id,UpdateUserDto dto)
    {
        var user = TryGetUserByIdAndThrowIfNotFound(id);
            // зачем тут пустая строчка, если нигде их не делаешь между операциями. Хотя по хорошему разделять всё же операции по смыслу
        user.Login = dto.Login;
        dbContext.SaveChanges();
        return user.Login;
    }

    public void DeleteUser(int id)
    {
        var  user = TryGetUserByIdAndThrowIfNotFound(id);
        dbContext.RemoveRange(dbContext.Notes.Where(n => n.UserId == id)); // лишняя строчка, надо понять почему)
        dbContext.Remove(user);
        dbContext.SaveChanges();
    }

    public UserVm GetUserById(int id)
    {
        var user = TryGetUserByIdAndThrowIfNotFound(id);
        return _mapper.Map<UserVm>(user);
    }

    public UserVm GetUserByLogin(string login)
    {
        var user = dbContext.Users.FirstOrDefault(n => n.Login == login);
        if (user is null)
        {
            throw new UserNotFoundByLoginException(login);
        }
        return _mapper.Map<UserVm>(user);
    }

    private User TryGetUserByIdAndThrowIfNotFound(int id)
    {
        var user = dbContext.Users.FirstOrDefault(n => n.Id == id);
        if (user is null)
        {
            throw new UserNotFoundExceptionId(id);
        }
        return user;
    }
}
       
