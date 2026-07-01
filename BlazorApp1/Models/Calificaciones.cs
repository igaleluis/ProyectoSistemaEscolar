using System;
using System.Collections.Generic;

namespace BlazorApp1.Models;

public partial class Calificaciones
{
    public int IdCalificacion { get; set; }

    public decimal? Zona { get; set; }

    public decimal? ExamenFinal { get; set; }

    public decimal? NotaFinal { get; set; }

    public int? IdEstudiante { get; set; }

    public int? IdCurso { get; set; }

    public virtual Curso? IdCursoNavigation { get; set; }

    public virtual Estudiante? IdEstudianteNavigation { get; set; }
}
