using Microsoft.AspNetCore.Mvc;
using NotesApp.Abstractions;
using NotesApp.Contracts;
using NotesApp.Models;

namespace NotesApp.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
        => _userRepository = userRepository;

    [HttpGet]
    public ActionResult<ListOfUsers> GetAllUsers()
    => Ok(_userRepository.GetAllUsers());
    
    [HttpGet("{id}")]
    public ActionResult<User?> GetUserById(int id)
    => Ok(_userRepository.GetUserById(id));

    [HttpGet("by_login")]
    public ActionResult<UserVm> GetUser(string login)
    {
        var user = _userRepository.GetUserByLogin(login);
        return Ok(user);
    }

    [HttpPost]
    public ActionResult<int> CreateUser(CreateUserDto dto)
        => Ok(_userRepository.CreateUser(dto));

    [HttpPut("{id}")]
    public ActionResult UpdateUserLogin(int id, UpdateUserDto dto)
    {
        var login = _userRepository.UpdateUserLogin(id, dto);
        return Ok(login);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteUser(int id)
    {
        _userRepository.DeleteUser(id);
        return NoContent();
    }
}