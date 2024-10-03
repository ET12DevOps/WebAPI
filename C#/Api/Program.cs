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

// List<Alumno> alumnos = [
//     new Alumno {Id = 1, Nombre = "Lucas"},
//     new Alumno {Id = 2, Nombre = "Nahuel"},
//     new Alumno {Id = 3, Nombre = "Joel"}
// ];

// List<Curso> cursos = [
//     new Curso {Id = 1, Año = 6, Division = 7, CicloLectivo = 2024 },
//     new Curso {Id = 2, Año = 6, Division = 8, CicloLectivo = 2024 },
// ];

// app.MapPost("/curso/{idCurso}/alumno/{idAlumno}", (int idCurso, int idAlumno) =>
// {
//     var curso = cursos.FirstOrDefault(curso => curso.Id == idCurso);
//     var alumno = alumnos.FirstOrDefault(alumno => alumno.Id == idAlumno);

//     if (alumno != null && curso != null)
//     {
//         //alumno.Cursos.Add(curso);
//         curso.Alumnos.Add(alumno);
//         return Results.Ok();
//     }

//     return Results.NotFound();
// })
//     .WithTags("Curso");

// app.MapDelete("/curso/{idCurso}/alumno/{idAlumno}", (int idCurso, int idAlumno) =>
// {
//     var curso = cursos.FirstOrDefault(curso => curso.Id == idCurso);
//     var alumno = alumnos.FirstOrDefault(alumno => alumno.Id == idAlumno);

//     if (alumno != null && curso != null)
//     {
//         curso.Alumnos.Remove(alumno);
//         return Results.Ok();
//     }

//     return Results.NotFound();
// })
//     .WithTags("Curso");

app.MapGroup("/api")
    .MapAlumnoEndpoints()
    .WithTags("Alumno");

app.MapGroup("/api")
    .MapCursoEndpoints()
    .WithTags("Curso");

app.Run();
