using Api;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

List<Alumno> alumnos = [
    new Alumno {Id = 1, Nombre = "Lucas"},
    new Alumno {Id = 2, Nombre = "Nahuel"},
    new Alumno {Id = 3, Nombre = "Joel"}
];

//endpoints
app.MapGet("/", () =>
{
    return Results.Ok("hola mundo");
});

app.MapPost("/", ([FromBody] string nombre) =>
{
    return Results.Ok(nombre);
});

app.MapGet("/alumno", () =>
{
    return Results.Ok(alumnos);
});

app.MapPost("/alumno", ([FromBody] Alumno alumno) =>
{
    alumnos.Add(alumno);
    return Results.Ok(alumnos);
});

app.Run();
