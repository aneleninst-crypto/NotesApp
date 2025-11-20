using NotesApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddExceptionHandler<ExceptionHandler>();

var app = builder.Build();

app.UseExceptionHandler("/Error");

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();