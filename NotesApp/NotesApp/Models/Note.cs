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

    public Note(int id, string? title, string? description, int userId, PriorityOfExecution priority) // убери конструкторы, они тебе пока ни к чему.
                                                                                                      // Ты создаешь сущности только в 1 месте своего приложения - репозитории
                                                                                                      // при маппинге. А значит в теории нам конструктор (инструкция по созданию)
                                                                                                      // не нужна. Как только понадобятся, ты поймешь и используешь.
    {
        Id = id;
        Title = title;
        Description = description;
        UserId = userId;
        Priority = priority;
    }
}