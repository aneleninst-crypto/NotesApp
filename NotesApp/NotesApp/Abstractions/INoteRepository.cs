using NotesApp.Contracts;
using NotesApp.Models;

namespace NotesApp.Abstractions;

public interface INoteRepository
{
    public ListOfNotes GetAllNote();
    public void CreateNote(CreateNoteDto createNoteDto);
    public bool UpdateNote(UpdateNoteDto updateNoteDto); 
    public bool DeleteNote(int noteId);
}