using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using BlazorApp1.Models;

namespace BlazorApp1.Reportes
{
    public class CuadroDeNotasMaestro : IDocument
    {
        private readonly string _curso;
        private readonly string _grado;
        private readonly List<CalificacionGridVM> _data;

        public CuadroDeNotasMaestro(string curso, string grado, List<CalificacionGridVM> data)
        {
            _curso = curso;
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

                    col.Item().AlignCenter().Text($"Cuadro de Notas Curso: {_curso}  |  Grado: {_grado}")
                        .FontSize(12);

                    col.Item().PaddingBottom(20); 
                });

                page.Content().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.ConstantColumn(50);
                        columns.ConstantColumn(50);
                        columns.ConstantColumn(50);
                        columns.ConstantColumn(80);
                    });

                    // HEADER
                    table.Header(header =>
                    {
                        header.Cell().Text("Estudiante").Bold();
                        header.Cell().Text("Zona").Bold();
                        header.Cell().Text("Examen").Bold();
                        header.Cell().Text("Total").Bold();
                        header.Cell().Text("Estado").Bold();
                    });

                    // DATA
                    foreach (var item in _data)
                    {
                        table.Cell().Text($"{item.Nombre} {item.Apellido}");
                        table.Cell().Text(item.Zona.ToString());
                        table.Cell().Text(item.ExamenFinal.ToString());
                        table.Cell().Text(item.Total.ToString());

                        var estado = item.Total >= 61 ? "Aprobado" : "Reprobado";
                        var color = item.Total >= 61 ? Colors.Green.Medium : Colors.Red.Medium;

                        table.Cell().Text(estado).FontColor(color);
                    }
                });
            });
        }
    }
}
