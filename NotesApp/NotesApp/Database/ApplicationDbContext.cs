using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NotesApp.Models;

namespace NotesApp.Database;

public class ApplicationDbContext : DbContext
{
    private readonly ApplicationDbContextSettings _dbContextSettings;
    
    public DbSet<User> Users { get; set; }
    public DbSet<Note> Notes { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        IOptions<ApplicationDbContextSettings> dbContextSettings) : base(options)
    {
        _dbContextSettings = dbContextSettings.Value;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        
        optionsBuilder.UseNpgsql(_dbContextSettings.ConnectionString);
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Debug);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}