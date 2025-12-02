using NotesApp.Contracts.NoteContracts;

namespace NotesApp.Abstractions;

public interface INoteRepository
{
    public ListOfNotes GetAllNote();
    public int CreateNote(CreateNoteDto createNoteDto, Guid userId);
    public bool UpdateNote(int noteId, UpdateNoteDto updateNoteDto); 
    public bool DeleteNote(int noteId,  Guid? userId);
}