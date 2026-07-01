using System;
using System.Collections.Generic;

namespace BlazorApp1.Models;

public partial class Curso
{
    public int IdCurso { get; set; }

    public string? Nombre { get; set; }

    public int? IdMaestro { get; set; }

    public int? IdGrado { get; set; }

    public virtual ICollection<Calificaciones> Calificaciones { get; set; } = new List<Calificaciones>();

    public virtual Grado? IdGradoNavigation { get; set; }

    public virtual Maestro? IdMaestroNavigation { get; set; }
}
