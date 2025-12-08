using Microsoft.EntityFrameworkCore;
using NotesApp.Abstractions;
using NotesApp.Contracts.AuthContracts;
using NotesApp.Contracts.UserContracts;
using NotesApp.Database;
using NotesApp.Models;

namespace NotesApp.Services;

public class AuthService(
    ApplicationDbContext  dbContext,
    IJwtTokenGenerator jwtTokenGenerator
        ) : IAuthService
{
    public async Task<LogInResponse> SignUpAsync(CreateUserDto dto)
    {
        var user = new User
        {
            Login = dto.Login,
            Password = dto.Password
        };
        
        await dbContext.Users.AddAsync(user);
       
        await dbContext.SaveChangesAsync();

        var (jwt, refresh) = UpdateToken(user);
        
        await dbContext.SaveChangesAsync();
        
        return CreateResponse(jwt, refresh);
    }

    public async Task<LogInResponse?> LogInAsync(LogInDto dto)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Login == dto.Login);
        if (user is null)
            return null;
        
        if(user.Password != dto.Password)
            return null;
        
        var (jwt, refresh) = UpdateToken(user);
        await dbContext.SaveChangesAsync(); 
        
        return CreateResponse(jwt, refresh);
    }

    public async Task<bool> LogOutAsync(Guid userId)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null)
            return false;
        
        var token = await dbContext.JwtTokens.FirstOrDefaultAsync(t => t.UserId == userId);
        if (token is null)
            return false;
        
        dbContext.Remove(token);
        await dbContext.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> VerifyTokenAsync(Guid userId, string token)
    {
        var jwtToken = await dbContext.JwtTokens.FirstOrDefaultAsync(t => t.UserId == userId);
        if (jwtToken is null)
            return false;
        
        return jwtToken.Token == token && jwtToken.ExpiresAt > DateTime.UtcNow;
    }

    public async Task<LogInResponse?> RefreshAsync(string refreshToken)
    {
        var existingRefreshToken = await dbContext.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == refreshToken && x.ExpiresAt > DateTime.UtcNow);
        
        if (existingRefreshToken is null)
            return null;
        
        var (jwt, refresh) = UpdateToken(existingRefreshToken.User);
        
        await dbContext.SaveChangesAsync();
        
        return CreateResponse(jwt, refresh);
    }

    public async Task RevokeAsync(string refreshToken)
    {
        var existingRefreshToken  = await dbContext.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == refreshToken && x.ExpiresAt > DateTime.UtcNow);
        
        if (existingRefreshToken is null)
            return;
        
        dbContext.Remove(existingRefreshToken);
        await dbContext.SaveChangesAsync();
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