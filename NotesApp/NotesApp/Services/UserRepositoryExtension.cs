using NotesApp.Abstractions;
using NotesApp.Repository;

namespace NotesApp.Services;

public static class UserRepositoryExtension
{
    public static IServiceCollection AddUserRepository(this IServiceCollection services)
        => services.AddSingleton<IUserRepository, UserRepository>();
}