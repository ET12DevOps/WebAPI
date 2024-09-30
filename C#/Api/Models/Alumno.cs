using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Alumno
{
    public int Idalumno { get; set; }

    public string Nombre { get; set; } = null!;

    public int Edad { get; set; }
}
