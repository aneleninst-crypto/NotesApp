using NotesApp.Abstractions;
using NotesApp.Repository;

namespace NotesApp.Services; // namespace не тот + у папки бы название с окончанием s сделать.

public static class UserRepositoryExtension
{
    public static IServiceCollection AddUserRepository(this IServiceCollection services)
        => services.AddScoped<IUserRepository, UserRepository>();
}