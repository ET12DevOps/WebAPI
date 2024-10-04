using Api;
using Api.Endpoints;
using Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("iliana_db");

builder.Services.AddDbContext<EscuelaContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.EnableTryItOutByDefault());
}

app.MapGroup("/api")
    .MapAlumnoEndpoints()
    .WithTags("Alumno");

app.MapGroup("/api")
    .MapCursoEndpoints()
    .WithTags("Curso");

app.Run();
