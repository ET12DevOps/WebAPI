using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class CursoEndpoints
{
    public static RouteGroupBuilder MapCursoEndpoints(this RouteGroupBuilder app)
    {
        app.MapGet("/curso", (EscuelaContext context) =>
        {
            return Results.Ok(context.Cursos);
        });

        app.MapPost("/curso", ([FromBody] Curso curso, EscuelaContext context) =>
        {
            context.Cursos.Add(curso);
            context.SaveChanges();
            return Results.Ok(context.Cursos);
        });

        app.MapDelete("/curso", ([FromQuery] Guid idCurso, EscuelaContext context) =>
        {
            var cursoAEliminar = context.Cursos.FirstOrDefault(curso => curso.Idcurso == idCurso);
            if (cursoAEliminar != null)
            {
                context.Cursos.Remove(cursoAEliminar);
                context.SaveChanges();
                return Results.Ok(context.Cursos); //Codigo 200
            }
            else
            {
                return Results.NotFound(); //Codigo 404
            }
        });

        app.MapPut("/curso", ([FromQuery] Guid idCurso, [FromBody] Curso curso, EscuelaContext context) =>
        {
            var cursoAActualizar = context.Cursos.FirstOrDefault(curso => curso.Idcurso == idCurso);
            if (cursoAActualizar != null)
            {
                cursoAActualizar.Anio = curso.Anio;
                cursoAActualizar.Division = curso.Division;
                context.SaveChanges();
                return Results.Ok(context.Cursos); //Codigo 200
            }
            else
            {
                return Results.NotFound(); //Codigo 404
            }
        });

        app.MapPost("/curso/{idCurso}/alumno/{idAlumno}", (Guid idCurso, int idAlumno, EscuelaContext context) =>
        {
            var curso = context.Cursos.FirstOrDefault(curso => curso.Idcurso == idCurso);
            var alumno = context.Alumnos.FirstOrDefault(alumno => alumno.Idalumno == idAlumno);

            if (alumno != null && curso != null)
            {
                //alumno.Cursos.Add(curso);
                context.Alumnocursos.Add(new Alumnocurso { Idalumnocurso = 0, IdalumnoNavigation = alumno, IdcursoNavigation = curso });
                context.SaveChanges();
                return Results.Ok();
            }

            return Results.NotFound();
        });

        app.MapDelete("/curso/{idCurso}/alumno/{idAlumno}", (Guid idCurso, int idAlumno, EscuelaContext context) =>
        {
            var alumnoCurso = context.Alumnocursos.FirstOrDefault(x => x.Idcurso == idCurso && x.Idalumno == idAlumno);

            if (alumnoCurso is not null)
            {
                context.Alumnocursos.Remove(alumnoCurso);
                context.SaveChanges();
                return Results.Ok();
            }

            return Results.NotFound();
        });

        return app;
    }
}