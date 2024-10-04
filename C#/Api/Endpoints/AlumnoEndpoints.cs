using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class AlumnoEndpoints
{
    public static RouteGroupBuilder MapAlumnoEndpoints(this RouteGroupBuilder app)
    {
        //lee listado de alumnos
        app.MapGet("/alumno", (EscuelaContext context) =>
        {
            return Results.Ok(context.Alumnos.ToList());
        });

        //crea un nuevo alumno en la lista
        app.MapPost("/alumno", ([FromBody] Alumno alumno, EscuelaContext context) =>
        {
            context.Alumnos.Add(alumno);
            context.SaveChanges();
            return Results.Ok(context.Alumnos);
        });

        app.MapDelete("/alumno", ([FromQuery] int idAlumno, EscuelaContext context) =>
        {
            var alumnoAEliminar = context.Alumnos.FirstOrDefault(alumno => alumno.Idalumno == idAlumno);
            if (alumnoAEliminar != null)
            {
                context.Alumnos.Remove(alumnoAEliminar);
                context.SaveChanges();
                return Results.Ok(context.Alumnos); //Codigo 200
            }
            else
            {
                return Results.NotFound(); //Codigo 404
            }
        });

        app.MapPut("/alumno", ([FromQuery] int idAlumno, [FromBody] Alumno alumno, EscuelaContext context) =>
        {
            var alumnoAActualizar = context.Alumnos.FirstOrDefault(alumno => alumno.Idalumno == idAlumno);
            if (alumnoAActualizar != null)
            {
                alumnoAActualizar.Nombre = alumno.Nombre;
                context.SaveChanges();
                return Results.Ok(context.Alumnos); //Codigo 200
            }
            else
            {
                return Results.NotFound(); //Codigo 404
            }
        });

        return app;
    }
}