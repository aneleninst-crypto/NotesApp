using NotesApp.Contracts.NoteContracts;
using NotesApp.Enums;

namespace NotesApp.Abstractions;

public interface INoteRepository
{
    public ListOfNotes GetAllNote(Guid? userId, bool? isCompleted, PriorityOfExecution? priorityOfExecution, 
        string? title, SortNotesBy? sortNotesBy);
    public int CreateNote(CreateNoteDto createNoteDto, Guid userId);
    public bool UpdateNote(int noteId, UpdateNoteDto updateNoteDto); 
    public bool DeleteNote(int noteId,  Guid? userId);
}