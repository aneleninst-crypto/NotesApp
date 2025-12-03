using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Abstractions;
using NotesApp.Contracts;
using NotesApp.Contracts.AuthContracts;
using NotesApp.Contracts.UserContracts;
using NotesApp.Models;

namespace NotesApp.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserController(IAuthService authService, IUserRepository userRepository) : BaseController
{

    [AllowAnonymous]
    [HttpPost("signup")]
    public ActionResult<LogInResponse> SignUp([FromBody] CreateUserDto dto)
    {
        var token = authService.SignUp(dto);
        return Ok(token);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public ActionResult<LogInResponse> LogIn([FromBody] LogInDto dto)
    {
        var result = authService.LogIn(dto);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost("logout")]
    public ActionResult<bool> LogOut([FromBody] Guid userId)
    {
        var result = authService.LogOut(userId);
        if (!result)
            return NotFound();
        return Ok(result);
    }

    [HttpPost("refresh")]
    public ActionResult<LogInResponse> Refresh([FromBody] string refreshToken)
    {
        var result = authService.Refresh(refreshToken);
        if (result is null)
            return NotFound();
        return Ok(result);
    }
    
    [HttpGet]
    public ActionResult<ListOfUsers> GetAllUsers()
    => Ok(userRepository.GetAllUsers());
    
    [HttpGet("{id:guid}")]
    public ActionResult<User?> GetUserById(Guid id)
    => Ok(userRepository.GetUserById(id));

    [HttpGet("by_login")]
    public ActionResult<UserVm> GetUser(string login)
    {
        var user = userRepository.GetUserByLogin(login);
        return Ok(user);
    }
    
    [HttpPut("{id:guid}")]
    public ActionResult UpdateUserLogin(Guid id, UpdateUserDto dto)
    {
        var login = userRepository.UpdateUserLogin(id, dto);
        return Ok(login);
    }

    [HttpDelete("{id:guid}")]
    public ActionResult DeleteUser(Guid id)
    {
        userRepository.DeleteUser(id);
        return NoContent();
    }
}