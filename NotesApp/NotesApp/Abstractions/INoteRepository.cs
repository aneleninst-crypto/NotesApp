using NotesApp.Contracts;
using NotesApp.Models; // СЛЕДИ за неиспользуемыми using

namespace NotesApp.Abstractions;

public interface INoteRepository
{
    public ListOfNotes GetAllNote();
    public void CreateNote(CreateNoteDto createNoteDto); // по хорошему вернуть id заметки
    public bool UpdateNote(UpdateNoteDto updateNoteDto); 
    public bool DeleteNote(int noteId);
}