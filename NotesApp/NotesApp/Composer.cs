using NotesApp.Services;

namespace NotesApp.Configurations; // пространство имен не то

public static class Composer
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Composer).Assembly);
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