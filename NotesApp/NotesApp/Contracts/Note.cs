using NotesApp.enams;

namespace NotesApp.Contracts;

public record NoteVm(int Id, string Title, string Description, PriorityOfExecution Priority); // Мне не нравятся названия классов, что они тоже Note - User, как и наше модели. Создает путанницу
public record NoteListVm(string Title, string Description, PriorityOfExecution Priority);
public record NoteTitleViewModel(string Title);
public record NoteDescriptionViewModel(string Description);
public record ListOfNotes(List<NoteListVm> Notes);
public record CreateNoteDto(string Title, string Description, int UserId, PriorityOfExecution Priority);
public record UpdateNoteDto(int UserId, string? Title, string? Description, PriorityOfExecution? Priority);