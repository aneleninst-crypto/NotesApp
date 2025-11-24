using AutoMapper;
using NotesApp.Abstractions;
using NotesApp.Contracts;
using NotesApp.enams;
using NotesApp.Models;

namespace NotesApp.Repository;

public class NoteRepository : INoteRepository
{
    private readonly List<Note> _notes = new();
    private readonly IMapper _mapper;

    public NoteRepository(IMapper mapper)
    {
        _mapper = mapper;
    }
    public ListOfNotes GetAllNote()
    => _mapper.Map<ListOfNotes>(_notes);

    public void CreateNote(CreateNoteDto createNoteDto)
    {
        var noteId = _notes.Count + 1;
        var note = _mapper.Map<Note>(createNoteDto); // не передаешь id, убрать конструктор
        note.Id = noteId;
        _notes.Add(note);
    }

    public bool ChangeNote(int noteId, int userId, string? title = null, PriorityOfExecution? priority = null,
        string? description = null) // будем принимать dto полноценную
    {
        GetNotesByUserId(userId); // а почему мы не используем список этот, не запишем его в переменную? Зачем вообще две операции тут, поиска всех заметок пользователя (не используем к
                                  // тому же, и потому поиск среди всех заметок, конкретной заметки по id. Вот код ниже можно было бы и вынести в отдельный метод, вместо твоего GetNotesByUserId
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