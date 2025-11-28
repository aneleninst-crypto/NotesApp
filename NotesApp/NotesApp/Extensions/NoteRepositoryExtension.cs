using NotesApp.Abstractions;
using NotesApp.Repository;

namespace NotesApp.Extensions;

public static class NoteRepositoryExtension
{
    public static IServiceCollection AddNoteRepository(this IServiceCollection services)
        => services.AddScoped<INoteRepository, NoteRepository>();
}