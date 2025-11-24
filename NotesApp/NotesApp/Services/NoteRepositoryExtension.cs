using NotesApp.Abstractions;
using NotesApp.Repository;

namespace NotesApp.Services;

public static class NoteRepositoryExtension // я бы не сказал, что эти Extension должны быть в папке Services, создай для них папку Extensions
{
    public static IServiceCollection AddNoteRepository(this IServiceCollection services)
        => services.AddSingleton<INoteRepository, NoteRepository>();
}