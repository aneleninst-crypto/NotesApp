using NotesApp.Contracts;
using NotesApp.Models;

namespace NotesApp.Abstractions;

public interface IUserRepository
{
    public ListOfUsers GetAllUsers();
    public int CreateUser(string login, string password); // передавать DTO
    public string UpdateUserLogin(int id, string login); // Передавать DTO
    public void DeleteUser(int id);
    public UserVm GetUserById(int id);
    public UserVm GetUserByLogin(string login);
}