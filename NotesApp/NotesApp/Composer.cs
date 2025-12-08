using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NotesApp.Abstractions;
using NotesApp.Database;
using NotesApp.Extensions;
using NotesApp.Options;
using NotesApp.Politics;
using NotesApp.Services;
using Swashbuckle.AspNetCore.SwaggerGen;

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
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();
        
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
        services.AddScoped<IAuthService, AuthService>();
        
        return services;
    }

    public static IServiceCollection AddCustomAuthorization(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddSwaggerGen()
            .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtOptions = configuration
                    .GetRequiredSection(nameof(JwtOptions))
                    .Get<JwtOptions>()!;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new
                        SymmetricSecurityKey(Convert.FromBase64String(jwtOptions.Secret))
                };
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var authService =
                            context.HttpContext
                                .RequestServices
                                .GetRequiredService<IAuthService>();

                        var userId = context.Principal?.FindFirstValue(ClaimTypes.NameIdentifier);
                        var isValid = false;
                        if (userId is not null)
                        {
                            isValid = await authService.VerifyTokenAsync(
                                Guid.Parse(userId),
                                context.SecurityToken.UnsafeToString()
                            );
                        }

                        if (
                            userId is null
                            || context.SecurityToken.ValidTo < DateTime.UtcNow
                            || !isValid
                        )
                        {
                            context.Fail("Unauthorized");
                        }
                    }
                };
            });
        services.AddScoped<IAuthorizationHandler, PostOwnerRequirementHandler>();
        services.AddHttpContextAccessor();
        services.AddAuthorization(options =>
        {
            var defaultAuthorizationPolicyBuilder = 
                new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
            defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
            options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            
            options.AddPolicy("NotesOwner", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.AddRequirements(new PostOwnerRequirement());
            });
        });
        
        services.AddOptions<JwtOptions>()
            .Bind(configuration.GetRequiredSection(nameof(JwtOptions)))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddTransient<IJwtTokenGenerator,  JwtTokenGenerator>();
        
        return services;
    }
}