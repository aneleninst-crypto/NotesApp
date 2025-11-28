using NotesApp;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddSwagger()
    .AddApplicationServices();

var app = builder.Build();

app.UseExceptionHandler("/Error");

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();