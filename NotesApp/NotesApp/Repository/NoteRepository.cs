using AutoMapper;
using NotesApp.Abstractions;
using NotesApp.Contracts;
using NotesApp.Database;
using NotesApp.Models;

namespace NotesApp.Repository;

public class NoteRepository(
    IMapper mapper,
    ApplicationDbContext dbContext
    ) : INoteRepository
{
    private readonly IMapper _mapper = mapper;
    public ListOfNotes GetAllNote()
    => _mapper.Map<ListOfNotes>(dbContext.Notes.ToList());

    public void CreateNote(CreateNoteDto createNoteDto)
    {
        var note = _mapper.Map<Note>(createNoteDto);
        dbContext.Notes.Add(note);
        dbContext.SaveChanges();
    }

    public bool UpdateNote (UpdateNoteDto updateNoteDto)
    {
        var note = dbContext.Notes.SingleOrDefault(n => n.Id == updateNoteDto.Id);

        if (note is null)
        {
            return false;
        } 
        _mapper.Map(updateNoteDto, note);
        dbContext.SaveChanges();
        return true;
    }

    public bool DeleteNote(int noteId)
    {
        var note = dbContext.Notes.SingleOrDefault(n => n.Id == noteId);
        if (note is null)
        {
            
            return false;
        }
        dbContext.Notes.Remove(note);
        dbContext.SaveChanges();
        return true;
    }
}