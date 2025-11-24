using AutoMapper;
using NotesApp.Abstractions;
using NotesApp.Contracts;
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
        var note = _mapper.Map<Note>(createNoteDto);
        note.Id = noteId;
        _notes.Add(note);
    }

    public bool UpdateNote (UpdateNoteDto updateNoteDto)
    {
        var note = _notes.SingleOrDefault(n => n.Id == updateNoteDto.Id);

        if (note == null)
        {
            return false;
        } 
        _mapper.Map(updateNoteDto, note);
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
}