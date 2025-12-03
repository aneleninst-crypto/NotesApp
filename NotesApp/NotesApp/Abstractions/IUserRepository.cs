using NotesApp.Contracts.UserContracts;

namespace NotesApp.Abstractions;

public interface IUserRepository
{
    public ListOfUsers GetAllUsers();
    public string UpdateUserLogin(Guid id, UpdateUserDto dto);
    public void DeleteUser(Guid id);
    public UserVm GetUserById(Guid id);
    public UserVm GetUserByLogin(string login);
}