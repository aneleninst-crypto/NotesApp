using NotesApp.Enums;
using NotesApp.Models;

namespace NotesApp.Contracts.NoteContracts;

public record CreateNoteDto(string Title, string Description, PriorityOfExecution Priority)
{
    public Note ToNote(Guid userId) => new()
    {
        UserId = userId,
        Title = Title,
        Description = Description,
        Priority = Priority
    };
}