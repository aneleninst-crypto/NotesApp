using NotesApp.Models;

namespace NotesApp.Abstractions;

public interface IJwtTokenGenerator
{
    public JwtToken Generate(User user);
    public RefreshToken GenerateRefreshToken(Guid userId);
}