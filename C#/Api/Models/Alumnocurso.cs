using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Alumnocurso
{
    public int Idalumnocurso { get; set; }

    public int Idalumno { get; set; }

    public Guid Idcurso { get; set; }

    public virtual Alumno IdalumnoNavigation { get; set; } = null!;

    public virtual Curso IdcursoNavigation { get; set; } = null!;
}
