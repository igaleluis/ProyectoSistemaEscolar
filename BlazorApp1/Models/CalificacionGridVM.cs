namespace BlazorApp1.Models
{
    public class CalificacionGridVM
    {
        public int IdEstudiante { get; set; }
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";

        public int? IdCalificacion { get; set; }

        public decimal? Zona { get; set; }
        public decimal? ExamenFinal { get; set; }

        public decimal Total =>
            (Zona ?? 0) + (ExamenFinal ?? 0);

        public bool TieneCalificacion =>
            IdCalificacion.HasValue;
    }
}
