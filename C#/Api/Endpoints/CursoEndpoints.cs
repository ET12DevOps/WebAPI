using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class CursoEndpoints
{
    public static RouteGroupBuilder MapCursoEndpoints(this RouteGroupBuilder app)
    {
        List<Curso> cursos = [
            new Curso {Id = 1, Año = 6, Division = 7, CicloLectivo = 2024 },
            new Curso {Id = 2, Año = 6, Division = 8, CicloLectivo = 2024 },
        ];

        app.MapGet("/curso", () =>
        {
            return Results.Ok(cursos);
        });

        app.MapPost("/curso", ([FromBody] Curso curso) =>
        {
            cursos.Add(curso);
            return Results.Ok(cursos);
        });

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
        });

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
        });

        return app;
    }

}