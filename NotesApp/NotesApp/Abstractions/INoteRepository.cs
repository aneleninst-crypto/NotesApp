using NotesApp.Contracts.NoteContracts;
using NotesApp.Enums;

namespace NotesApp.Abstractions;

public interface INoteRepository
{
    public Task<ListOfNotes> GetAllNoteAsync(Guid? userId, bool? isCompleted, PriorityOfExecution? priorityOfExecution, 
        string? title, SortNotesBy? sortNotesBy);
    public Task<int> CreateNoteAsync(CreateNoteDto createNoteDto, Guid userId);
    public Task<bool> UpdateNoteAsync(int noteId, UpdateNoteDto updateNoteDto); 
    public Task<bool> DeleteNoteAsync(int noteId,  Guid? userId);
}