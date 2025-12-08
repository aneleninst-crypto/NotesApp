using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NotesApp.Abstractions;
using NotesApp.Contracts.NoteContracts;
using NotesApp.Database;
using NotesApp.Enums;
using NotesApp.Exceptions;
using NotesApp.Models;

namespace NotesApp.Repository;

public class NoteRepository(
    IMapper mapper,
    ApplicationDbContext dbContext
    ) : INoteRepository
{
    public async Task<ListOfNotes> GetAllNoteAsync(Guid? userId, bool? isCompleted, PriorityOfExecution? priorityOfExecution, 
        string? title, SortNotesBy? sortNotesBy)
    {
        IQueryable<Note> notes = dbContext.Notes.Where(n => n.UserId == userId);
        
        if (isCompleted.HasValue)
            notes = notes.Where(n => n.IsCompleted == isCompleted);
        
        if (priorityOfExecution.HasValue)
            notes = notes.Where(n => n.Priority == priorityOfExecution);
        
        if (!string.IsNullOrWhiteSpace(title))
            notes = notes.Where(n => n.Title.Contains(title));

        notes = sortNotesBy switch
        {
            SortNotesBy.TitleByAscending => notes.OrderBy(n => n.Title),
            SortNotesBy.TitleByDescending => notes.OrderByDescending(n => n.Title),

            SortNotesBy.CreatedByAscending => notes.OrderBy(n => n.Created),
            SortNotesBy.CreatedByDescending => notes.OrderByDescending(n => n.Created),

            SortNotesBy.EditDateByAscending => notes.OrderBy(n => n.EditDate),
            SortNotesBy.EditDateByDescending => notes.OrderByDescending(n => n.EditDate),

            SortNotesBy.IsCompletedByAscending => notes.OrderBy(n => n.IsCompleted),
            SortNotesBy.IsCompletedByDescending => notes.OrderByDescending(n => n.IsCompleted),

            SortNotesBy.PriorityByAscending => notes.OrderBy(n => n.Priority),
            SortNotesBy.PriorityByDescending => notes.OrderByDescending(n => n.Description),
            
            _ => notes
        };
        var result = await notes.ToListAsync();
        
        return mapper.Map<ListOfNotes>(result);
    }

    public async Task<int> CreateNoteAsync(CreateNoteDto createNoteDto,  Guid userId)
    {
        var note = createNoteDto.ToNote(userId);
        await dbContext.Notes.AddAsync(note);
        
        await dbContext.SaveChangesAsync();
        
        return note.Id;
    }

    public async Task<bool> UpdateNoteAsync (int noteId, UpdateNoteDto updateNoteDto)
    {
        var note = TryGetNoteByIdAndThrowIfNotFound(noteId);
        mapper.Map(updateNoteDto, note);
        
        await dbContext.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> DeleteNoteAsync(int noteId,  Guid? userId)
    {
        var note = TryGetNoteByIdAndThrowIfNotFound(noteId);
        dbContext.Notes.Remove(note);
        
        await dbContext.SaveChangesAsync();
        
        return true;
    }

    private Note TryGetNoteByIdAndThrowIfNotFound(int noteId)
    {
        var note = dbContext.Notes.FirstOrDefault(n => n.Id == noteId);
        
        if (note is null)
            throw new NoteNotFoundException(noteId);
        
        return note;
    }
}