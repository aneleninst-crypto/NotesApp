using NotesApp.Abstractions;
using NotesApp.enams;
using NotesApp.Models;

namespace NotesApp.Repository;

public class NoteRepository : INoteRepository
{
    private readonly List<Note> _notes = new();
    public List<Note> GetAllNote()
    {
        return _notes;
    }

    public void CreateNote(string? title, string? description, int userId, PriorityOfExecution priority)
    {
        var newNote = new Note(_notes.Count+1, title, description, userId, priority );
        _notes.Add(newNote);
    }

    public bool ChangeNote(int noteId, int userId, string? title = null, PriorityOfExecution? priority = null, string? description = null)
    {
        var note = _notes.SingleOrDefault(n => n.Id == noteId);

        if (note == null)
        {
            return false;
        }

        if (note.UserId != userId)
        {
            return false;
        }
        
        if (title != null)
        { 
            note.Title = title;
        }

        if (description != null)
        {
            note.Description = description;
        }

        if (priority != null)
        {
            note.Priority = priority.Value;
        }
        return true;
    }

    public bool DeleteNote(int noteId)
    {
        var note = _notes.SingleOrDefault(n => n.Id == noteId);
        if (note == null)
        {
            
            return false;
        }
        _notes.Remove(note);
        return true;
    }

    public List<Note> GetNotesByUserId(int userId)
    {
        return _notes.Where(n => n.UserId == userId).ToList();
    }
}