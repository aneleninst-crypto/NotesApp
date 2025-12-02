using NotesApp.Enums;

namespace NotesApp.Contracts.NoteContracts;

public record UpdateNoteDto(string? Title, string? Description, PriorityOfExecution? Priority);