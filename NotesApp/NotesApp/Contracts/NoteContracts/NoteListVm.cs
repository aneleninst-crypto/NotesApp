using NotesApp.Enums;

namespace NotesApp.Contracts.NoteContracts;

public record NoteListVm(string Title, string Description, PriorityOfExecution Priority);