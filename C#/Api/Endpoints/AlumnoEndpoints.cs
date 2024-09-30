using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class AlumnoEndpoints
{
    public static RouteGroupBuilder MapAlumnoEndpoints(this RouteGroupBuilder app)
    {
        List<Alumno> alumnos = [
                new Alumno {Id = 1, Nombre = "Lucas"},
                new Alumno {Id = 2, Nombre = "Nahuel"},
                new Alumno {Id = 3, Nombre = "Joel"}
        ];

        app.MapGet("/alumno", () =>
        {
            return Results.Ok(alumnos);
        });

        app.MapPost("/alumno", ([FromBody] Alumno alumno) =>
        {
            alumnos.Add(alumno);
            return Results.Ok(alumnos);
        });

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
        });

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
        });

        return app;
    }
}