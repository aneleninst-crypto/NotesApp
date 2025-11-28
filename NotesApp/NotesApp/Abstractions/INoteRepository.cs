using NotesApp.Contracts;

namespace NotesApp.Abstractions;

public interface INoteRepository
{
    public ListOfNotes GetAllNote();
    public int CreateNote(CreateNoteDto createNoteDto);
    public bool UpdateNote(int noteId, UpdateNoteDto updateNoteDto); 
    public bool DeleteNote(int noteId);
}