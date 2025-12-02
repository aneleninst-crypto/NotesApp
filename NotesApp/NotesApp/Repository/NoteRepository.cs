using AutoMapper;
using NotesApp.Abstractions;
using NotesApp.Contracts.NoteContracts;
using NotesApp.Database;
using NotesApp.Exceptions;
using NotesApp.Models;

namespace NotesApp.Repository;

public class NoteRepository(
    IMapper mapper,
    ApplicationDbContext dbContext
    ) : INoteRepository
{
    public ListOfNotes GetAllNote()
    => mapper.Map<ListOfNotes>(dbContext.Notes.ToList());

    public int CreateNote(CreateNoteDto createNoteDto,  Guid userId)
    {
        var note = createNoteDto.ToNote(userId);
        dbContext.Notes.Add(note);
        dbContext.SaveChanges();
        return note.Id;
    }

    public bool UpdateNote (int noteId, UpdateNoteDto updateNoteDto)
    {
        var note = TryGetNoteByIdAndThrowIfNotFound(noteId);
        mapper.Map(updateNoteDto, note);
        dbContext.SaveChanges();
        return true;
    }

    public bool DeleteNote(int noteId,  Guid? userId)
    {
        var note = TryGetNoteByIdAndThrowIfNotFound(noteId);
        dbContext.Notes.Remove(note);
        dbContext.SaveChanges();
        return true;
    }

    private Note TryGetNoteByIdAndThrowIfNotFound(int noteId)
    {
        var note = dbContext.Notes.FirstOrDefault(n => n.Id == noteId);
        if (note is null)
        {
            throw new NoteNotFoundException(noteId);
        }
        return note;
    }
}