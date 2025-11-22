using NotesApp.enams;
using NotesApp.Models;

namespace NotesApp.Abstractions;

public interface INoteRepository
{
    public List<Note> GetAllNote();
    public void CreateNote(string? title, string? description, int userId, PriorityOfExecution priority);
    public bool ChangeNote(int noteId, int userId, string? title = null, PriorityOfExecution? priority = null,
        string? description = null);
    public bool DeleteNote(int noteId);
    public List<Note> GetNotesByUserId(int userId);
}