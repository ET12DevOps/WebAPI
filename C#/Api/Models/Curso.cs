using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Curso
{
    public Guid Idcurso { get; set; }

    public int Anio { get; set; }

    public int Division { get; set; }

    public int? Ciclolectivo { get; set; }
}
