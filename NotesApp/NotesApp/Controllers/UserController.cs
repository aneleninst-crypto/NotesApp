using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Abstractions;
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
    public async Task<ActionResult<LogInResponse>> SignUp([FromBody] CreateUserDto dto)
    {
        var token = await authService.SignUpAsync(dto);
        return Ok(token);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<LogInResponse>> LogIn([FromBody] LogInDto dto)
    {
        var result = await authService.LogInAsync(dto);
        if (result is null)
            return NotFound();
        
        return Ok(result);
    }

    [HttpPost("logout")]
    public async Task<ActionResult<bool>> LogOut([FromBody] Guid userId)
    {
        var result = await authService.LogOutAsync(userId);
        if (!result)
            return NotFound();
        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<LogInResponse>> Refresh([FromBody] string refreshToken)
    {
        var result = await authService.RefreshAsync(refreshToken);
        if (result is null)
            return NotFound();
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<ActionResult<ListOfUsers>> GetAllUsers()
    => Ok(await userRepository.GetAllUsersAsync());
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<User?>> GetUserById(Guid id)
    => Ok(await userRepository.GetUserByIdAsync(id));

    [HttpGet("by_login")]
    public async Task<ActionResult<UserVm>> GetUser(string login)
    {
        var user = await userRepository.GetUserByLoginAsync(login);
        return Ok(user);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateUserLogin(Guid id, UpdateUserDto dto)
    {
        var login = await userRepository.UpdateUserLoginAsync(id, dto);
        return Ok(login);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteUser(Guid id)
    {
        await userRepository.DeleteUserAsync(id);
        return NoContent();
    }
}