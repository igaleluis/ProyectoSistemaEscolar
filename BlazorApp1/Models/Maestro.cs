using System;
using System.Collections.Generic;

namespace BlazorApp1.Models;

public partial class Maestro
{
    public int IdMaestro { get; set; }

    public int? IdUsuario { get; set; }

    public virtual ICollection<Curso> Cursos { get; set; } = new List<Curso>();

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
