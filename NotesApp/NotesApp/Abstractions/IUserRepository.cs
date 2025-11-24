using NotesApp.Contracts;

namespace NotesApp.Abstractions;

public interface IUserRepository
{
    public ListOfUsers GetAllUsers();
    public int CreateUser(CreateUserDto dto);
    public string UpdateUserLogin(int id, UpdateUserDto dto);
    public void DeleteUser(int id);
    public UserVm GetUserById(int id);
    public UserVm GetUserByLogin(string login);
}