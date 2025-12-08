using NotesApp.Contracts.AuthContracts;
using NotesApp.Contracts.UserContracts;

namespace NotesApp.Abstractions;

public interface IAuthService
{
    public Task<LogInResponse> SignUpAsync(CreateUserDto dto);
    public Task<LogInResponse?> LogInAsync(LogInDto dto);
    public Task<bool> LogOutAsync (Guid userId);
    public Task<bool> VerifyTokenAsync (Guid userId, string token);
    public Task<LogInResponse?> RefreshAsync(string refreshToken);
    public Task RevokeAsync (string refreshToken);
}