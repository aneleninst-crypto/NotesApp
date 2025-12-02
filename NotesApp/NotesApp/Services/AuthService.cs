using Microsoft.EntityFrameworkCore;
using NotesApp.Abstractions;
using NotesApp.Contracts;
using NotesApp.Database;
using NotesApp.Models;

namespace NotesApp.Services;

public class AuthService(
    ApplicationDbContext  dbContext,
    IJwtTokenGenerator jwtTokenGenerator
        ) : IAuthService
{
    public LogInResponse SignUp(CreateUserDto dto)
    {
        var user = new User
        {
            Login = dto.Login,
            Password = dto.Password
        };
        dbContext.Users.Add(user);
        dbContext.SaveChanges();

        var (jwt, refresh) = UpdateToken(user);
        dbContext.SaveChanges();
        return CreateResponse(jwt, refresh);
    }

    public LogInResponse? LogIn(LogInDto dto)
    {
        var user = dbContext.Users.FirstOrDefault(u => u.Login == dto.Login);
        if (user is null)
            return null;
        if(user.Password != dto.Password)
            return null;
        var (jwt, refresh) = UpdateToken(user);
        dbContext.SaveChanges();
        return CreateResponse(jwt, refresh);
    }

    public bool LogOut(Guid userId)
    {
        var user = dbContext.Users.FirstOrDefault(u => u.Id == userId);
        if (user is null)
            return false;
        var token = dbContext.JwtTokens.FirstOrDefault(t => t.UserId == userId);
        if (token is null)
            return false;
        
        dbContext.Remove(token);
        dbContext.SaveChanges();
        return true;
    }

    public bool VerifyToken(Guid userId, string token)
    {
        var jwtToken = dbContext.JwtTokens.FirstOrDefault(t => t.UserId == userId);
        if (jwtToken is null)
            return false;
        
        return jwtToken.Token == token && jwtToken.ExpiresAt > DateTime.UtcNow;
    }

    public LogInResponse? Refresh(string refreshToken)
    {
        var existingRefreshToken = dbContext.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefault(x => x.Token == refreshToken && x.ExpiresAt > DateTime.UtcNow);
        if (existingRefreshToken is null)
            return null;
        var (jwt, refresh) = UpdateToken(existingRefreshToken.User);
        
        dbContext.SaveChanges();
        return CreateResponse(jwt, refresh);
    }

    private (JwtToken jwt, RefreshToken Refresh) UpdateToken(User user)
    {
        var token = jwtTokenGenerator.Generate(user);
        var oldToken = dbContext.JwtTokens.FirstOrDefault(t => t.UserId == user.Id);
        if (oldToken is not null)
            dbContext.Remove(oldToken);
        dbContext.JwtTokens.Add(token);

        var refreshToken = jwtTokenGenerator.GenerateRefreshToken(user.Id);
        dbContext.Add(refreshToken);
        
        return (token, refreshToken);
    }

    private static LogInResponse CreateResponse(JwtToken jwt, RefreshToken refresh)
        => new(jwt.UserId, jwt.Token, refresh.Token);
}