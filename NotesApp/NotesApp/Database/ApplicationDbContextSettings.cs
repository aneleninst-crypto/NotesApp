using System.ComponentModel.DataAnnotations;

namespace NotesApp.Database;

public class ApplicationDbContextSettings
{
    [Required]
    public required string Host { get; init; }
    [Required]
    public int Port { get; init; }
    [Required]
    public required string UserName { get; init; }
    [Required]
    public required string Password { get; init; }
    [Required]
    public required string Database { get; init; }

    public string ConnectionString
        => $"Host={Host};" +
           $"Port={Port};" +
           $"Username={UserName};" +
           $"Password={Password};" +
           $"Database={Database}";
}