using NotesApp.enams;

namespace NotesApp.Models;

public class Note
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime EditDate { get; set; } = DateTime.Now;
    public bool IsCompleted { get; set; } = false;
    public int UserId  { get; set; }
    public PriorityOfExecution Priority { get; set; }

    public Note(int id, string? title, string? description, int userId, PriorityOfExecution priority)
    {
        Id = id;
        Title = title;
        Description = description;
        UserId = userId;
        Priority = priority;
    }
}