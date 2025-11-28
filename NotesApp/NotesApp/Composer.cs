using NotesApp.Database;
using NotesApp.Extension;
using NotesApp.Extensions;

namespace NotesApp;

public static class Composer
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(Composer).Assembly);
        services.AddOptions<ApplicationDbContextSettings>()
            .Bind(configuration.GetRequiredSection(nameof(ApplicationDbContextSettings)))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services.Configure<ApplicationDbContextSettings>(
            configuration.GetRequiredSection(nameof(ApplicationDbContextSettings)));
        services.AddDbContext<ApplicationDbContext>();
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