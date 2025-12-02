using NotesApp.Enums;

namespace NotesApp.Contracts.NoteContracts;

public record NoteVm(int Id, string Title, string Description, PriorityOfExecution Priority);