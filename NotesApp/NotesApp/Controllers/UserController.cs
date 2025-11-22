using System.Data;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Abstractions;
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
    public ActionResult<List<User>> GetAllUsers()
    => Ok(_userRepository.GetAllUsers());
    
    [HttpGet("{id}")]
    public ActionResult<User?> GetUserById(int id)
    => Ok(_userRepository.GetUserById(id));
    
    [HttpPost]
    public ActionResult<int> CreateUser(string login, [FromBody] string password)
    {
        var userId = _userRepository.CreateUser(login. Trim(), password. Trim());
        return Ok(userId);
    }

    [HttpPut]
    public ActionResult UpdateUser(int id, string login, [FromBody] string password)
    {
        _userRepository.UpdateUser(id, login.Trim(), password.Trim());
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteUser(int id)
    {
        _userRepository.DeleteUser(id);
        return NoContent();
    }
}