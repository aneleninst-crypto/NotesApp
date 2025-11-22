using NotesApp.Abstractions;
using NotesApp.Repository;

namespace NotesApp.Services;

public static class NoteRepositoryExtension
{
    public static IServiceCollection AddNoteRepository(this IServiceCollection services)
        => services.AddSingleton<INoteRepository, NoteRepository>();
}