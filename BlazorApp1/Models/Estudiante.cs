using System;
using System.Collections.Generic;

namespace BlazorApp1.Models;

public partial class Estudiante
{
    public int IdEstudiante { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdGrado { get; set; }

    public virtual ICollection<Calificaciones> Calificaciones { get; set; } = new List<Calificaciones>();

    public virtual Grado? IdGradoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
