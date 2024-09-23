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

List<Curso> cursos = [
    new Curso {Id = 1, Año = 6, Division = 7, CicloLectivo = 2024 },
    new Curso {Id = 2, Año = 6, Division = 8, CicloLectivo = 2024 },
];

//lee listado de alumnos
app.MapGet("/alumno", () =>
{
    return Results.Ok(alumnos);
})
    .WithTags("Alumno");

//crea un nuevo alumno en la lista
app.MapPost("/alumno", ([FromBody] Alumno alumno) =>
{
    alumnos.Add(alumno);
    return Results.Ok(alumnos);
})
    .WithTags("Alumno");

app.MapDelete("/alumno", ([FromQuery] int idAlumno) =>
{
    var alumnoAEliminar = alumnos.FirstOrDefault(alumno => alumno.Id == idAlumno);
    if (alumnoAEliminar != null)
    {
        alumnos.Remove(alumnoAEliminar);
        return Results.Ok(alumnos); //Codigo 200
    }
    else
    {
        return Results.NotFound(); //Codigo 404
    }
})
    .WithTags("Alumno");

app.MapPut("/alumno", ([FromQuery] int idAlumno, [FromBody] Alumno alumno) =>
{
    var alumnoAActualizar = alumnos.FirstOrDefault(alumno => alumno.Id == idAlumno);
    if (alumnoAActualizar != null)
    {
        alumnoAActualizar.Nombre = alumno.Nombre;
        return Results.Ok(alumnos); //Codigo 200
    }
    else
    {
        return Results.NotFound(); //Codigo 404
    }
})
    .WithTags("Alumno");

app.MapGet("/curso", () =>
{
    return Results.Ok(cursos);
})
    .WithTags("Curso");

app.MapPost("/curso", ([FromBody] Curso curso) =>
{
    cursos.Add(curso);
    return Results.Ok(cursos);
})
    .WithTags("Curso");

app.MapDelete("/curso", ([FromQuery] int idCurso) =>
{
    var cursoAEliminar = cursos.FirstOrDefault(curso => curso.Id == idCurso);
    if (cursoAEliminar != null)
    {
        cursos.Remove(cursoAEliminar);
        return Results.Ok(cursos); //Codigo 200
    }
    else
    {
        return Results.NotFound(); //Codigo 404
    }
})
    .WithTags("Curso");

app.MapPut("/curso", ([FromQuery] int idCurso, [FromBody] Curso curso) =>
{
    var cursoAActualizar = cursos.FirstOrDefault(curso => curso.Id == idCurso);
    if (cursoAActualizar != null)
    {
        cursoAActualizar.Año = curso.Año;
        cursoAActualizar.Division = curso.Division;
        return Results.Ok(cursos); //Codigo 200
    }
    else
    {
        return Results.NotFound(); //Codigo 404
    }
})
    .WithTags("Curso");

app.MapPost("/curso/{idCurso}/alumno/{idAlumno}", (int idCurso, int idAlumno) =>
{
    var curso = cursos.FirstOrDefault(curso => curso.Id == idCurso);
    var alumno = alumnos.FirstOrDefault(alumno => alumno.Id == idAlumno);

    if (alumno != null && curso != null)
    {
        //alumno.Cursos.Add(curso);
        curso.Alumnos.Add(alumno);
        return Results.Ok();
    }

    return Results.NotFound();
})
    .WithTags("Curso");

app.MapDelete("/curso/{idCurso}/alumno/{idAlumno}", (int idCurso, int idAlumno) =>
{
    var curso = cursos.FirstOrDefault(curso => curso.Id == idCurso);
    var alumno = alumnos.FirstOrDefault(alumno => alumno.Id == idAlumno);

    if (alumno != null && curso != null)
    {
        curso.Alumnos.Remove(alumno);
        return Results.Ok();
    }

    return Results.NotFound();
})
    .WithTags("Curso");

app.Run();
