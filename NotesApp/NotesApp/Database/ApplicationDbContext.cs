using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NotesApp.Models;

namespace NotesApp.Database;

public class ApplicationDbContext(
    IOptions<ApplicationDbContextSettings> options,
    IWebHostEnvironment environment,
    ILogger<ApplicationDbContext> logger
    ) : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<JwtToken> JwtTokens { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        
        optionsBuilder.UseNpgsql(options.Value.ConnectionString);
        if (environment.IsDevelopment())
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.LogTo(message => logger.LogInformation(message),
                LogLevel.Information);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);
    }
}