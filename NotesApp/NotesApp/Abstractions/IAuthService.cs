using NotesApp.Contracts;

namespace NotesApp.Abstractions;

public interface IAuthService
{
    public LogInResponse SignUp(CreateUserDto dto);
    public LogInResponse? LogIn(LogInDto dto);
    public bool LogOut (Guid userId);
    public bool VerifyToken (Guid userId, string token);
    public LogInResponse? Refresh(string refreshToken);
}