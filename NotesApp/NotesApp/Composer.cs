using Microsoft.EntityFrameworkCore;
using NotesApp.Database;
using NotesApp.Extension;
using NotesApp.Services;

namespace NotesApp;

public static class Composer
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Composer).Assembly);
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(
                "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=notes");
        });
        services.AddExceptionHandler<ExceptionHandler>();
        services.AddControllers();
        
        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        return services;
    }

    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddUserRepository();
        services.AddNoteRepository();
        
        return services;
    }
}