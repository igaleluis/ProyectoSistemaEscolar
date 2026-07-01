using System;
using System.Collections.Generic;

namespace BlazorApp1.Models;

public partial class Grado
{
    public int IdGrado { get; set; }

    public string? Nombre { get; set; }

    public string? Nivel { get; set; }

    public virtual ICollection<Curso> Cursos { get; set; } = new List<Curso>();

    public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
}
