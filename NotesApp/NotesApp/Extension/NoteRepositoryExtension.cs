using NotesApp.Abstractions;
using NotesApp.Repository;

namespace NotesApp.Extension;

public static class NoteRepositoryExtension
{
    public static IServiceCollection AddNoteRepository(this IServiceCollection services)
        => services.AddScoped<INoteRepository, NoteRepository>();
}