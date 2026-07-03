using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using BlazorApp1.Models;

namespace BlazorApp1.Reportes
{
    public class BoletaDeNotasAlumno : IDocument
    {
        private readonly string _nombreEstudiante;
        private readonly string _grado;
        private readonly List<CursoEstudianteVM> _data;

        public BoletaDeNotasAlumno(string nombreEstudiante, string grado, List<CursoEstudianteVM> data)
        {
            _nombreEstudiante = nombreEstudiante;
            _grado = grado;
            _data = data;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(30);

                page.Header().Column(col =>
                {
                    col.Item().AlignCenter().Height(80).Image("wwwroot/Imagenes/logoColegio.png");

                    col.Item().AlignCenter().Text("COLEGIO NUEVO HORIZONTE")
                        .FontSize(18).Bold();

                    col.Item().AlignCenter().Text($"Boleta de Calificaciones: {_nombreEstudiante}  |  Grado: {_grado}")
                        .FontSize(12);

                    col.Item().AlignCenter().Text($"Fecha de emisión: {DateTime.Now:dd/MM/yyyy}")
                        .FontSize(10)
                        .FontColor(Colors.Grey.Darken2);

                    col.Item().PaddingBottom(20);
                });

                page.Content().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.ConstantColumn(70);
                        columns.ConstantColumn(90);
                        
                    });

                    // HEADER
                    table.Header(header =>
                    {
                        header.Cell().Text("Curso").Bold();
                        header.Cell().Text("Nota Final").Bold();
                        header.Cell().Text("Estado").Bold();
                    });

                    // DATA
                    foreach (var item in _data)
                    {
                        table.Cell().Text(item.Nombre);
                        table.Cell().Text(item.NotaFinal.ToString());

                        var estado = item.NotaFinal >= 61 ? "Aprobado" : "Reprobado";
                        var color = item.NotaFinal >= 61 ? Colors.Green.Medium : Colors.Red.Medium;

                        table.Cell().Text(estado).FontColor(color);
                    }
                });
            });
        }
    }
}
