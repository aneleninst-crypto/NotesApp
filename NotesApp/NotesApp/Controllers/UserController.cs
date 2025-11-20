using System.Data;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Models;

namespace NotesApp.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{
    private static readonly List<User> Users = new();

    [HttpPost]
    public ActionResult<int> Register(string login, [FromBody] string password)
    {
        var userId = Users.Count;
        Users.Add(new User(userId, login, password));
        return Ok(userId);
    }
    
    [HttpGet]
    public ActionResult<List<User>> GetUsers() => Ok(Users);
}