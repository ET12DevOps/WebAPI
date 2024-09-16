namespace Api;

public class Curso
{
    public int Id { get; set; }
    public required int Año { get; set; }
    public required int Division { get; set; }
    public int CicloLectivo { get; set; }
    public List<Alumno> Alumnos = new List<Alumno>();
}