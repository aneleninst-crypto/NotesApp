using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NotesApp.Abstractions;
using NotesApp.Contracts.UserContracts;
using NotesApp.Database;
using NotesApp.Exceptions;
using NotesApp.Models;

namespace NotesApp.Repository;

public class UserRepository(
    IMapper mapper,
    ApplicationDbContext dbContext) : IUserRepository
{
    public async Task<ListOfUsers> GetAllUsersAsync()
    {
        var users = await dbContext.Users.ToListAsync();
        
        return mapper.Map<ListOfUsers>(users);
    }

    public async Task<string> UpdateUserLoginAsync(Guid id,UpdateUserDto dto)
    {
        var user = await TryGetUserByIdAndThrowIfNotFoundAsync(id);
        user.Login = dto.Login;
        
        await dbContext.SaveChangesAsync();
        
        return user.Login;
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var  user = await TryGetUserByIdAndThrowIfNotFoundAsync(id);
        
        dbContext.Remove(user);
        
        await dbContext.SaveChangesAsync();
    }

    public async Task<UserVm> GetUserByIdAsync(Guid id)
    {
        var user = await TryGetUserByIdAndThrowIfNotFoundAsync(id);
        
        return mapper.Map<UserVm>(user);
    }

    public async Task<UserVm> GetUserByLoginAsync(string login)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(n => n.Login == login);
        
        if (user is null)
            throw new UserNotFoundByLoginException(login);
        
        return mapper.Map<UserVm>(user);
    }

    private async Task<User> TryGetUserByIdAndThrowIfNotFoundAsync(Guid id)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(n => n.Id == id);
        
        if (user is null)
            throw new UserNotFoundExceptionId(id);
        
        return user;
    }
}
       
