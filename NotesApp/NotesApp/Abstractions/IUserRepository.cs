using NotesApp.Contracts;
using NotesApp.Models;

namespace NotesApp.Abstractions;

public interface IUserRepository
{
    public ListOfUsers GetAllUsers();
    public int CreateUser(string login, string password);
    public string UpdateUserLogin(int id, string login);
    public void DeleteUser(int id);
    public UserVm GetUserById(int id);
    public UserVm GetUserByLogin(string login);
}