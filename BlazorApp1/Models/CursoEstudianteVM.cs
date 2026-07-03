namespace BlazorApp1.Models
{
    public class CursoEstudianteVM
    {
        public int IdCurso { get; set; }
        public string Nombre { get; set; }
        public string Grado { get; set; }
        public string Nivel { get; set; }
        public decimal? NotaFinal { get; set; }

        public string Estado => NotaFinal >= 61 ? "Aprobado" : "Reprobado";
    }
}
