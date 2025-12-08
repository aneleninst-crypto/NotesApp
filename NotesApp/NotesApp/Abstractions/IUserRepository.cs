using NotesApp.Contracts.UserContracts;

namespace NotesApp.Abstractions;

public interface IUserRepository
{
    public Task<ListOfUsers> GetAllUsersAsync();
    public Task<string> UpdateUserLoginAsync(Guid id, UpdateUserDto dto);
    public Task DeleteUserAsync(Guid id);
    public Task<UserVm> GetUserByIdAsync(Guid id);
    public Task<UserVm> GetUserByLoginAsync(string login);
}