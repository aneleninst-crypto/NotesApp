using NotesApp.Enums;

namespace NotesApp.Models;

public class Note
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime EditDate { get; set; } = DateTime.UtcNow;
    public bool IsCompleted { get; set; } = false;
    public int UserId  { get; set; }
    public PriorityOfExecution Priority { get; set; }
    public User User { get; set; }
}