using System;
using System.Collections.Generic;

namespace BlazorApp1.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Apellido { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public string? Contraseña { get; set; }

    public string? Rol { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();

    public virtual ICollection<Maestro> Maestros { get; set; } = new List<Maestro>();
}
