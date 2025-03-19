using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.IO;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace MediCare_.Services
{
    public interface IPdfService
    {
        Task<byte[]> GenerateDoctorNotePdfAsync(string doctorName, string noteContent);
    }
    public class PdfService : IPdfService
    {
        public async Task<byte[]> GenerateDoctorNotePdfAsync(string doctorName, string noteContent)
        {
            QuestPDF.Settings.License = LicenseType.Community; // Add this line

            var pdfDocument = QuestPDF.Fluent.Document.Create(container =>  // Explicit reference
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.Header().Text($"Doctor's Note for {doctorName}").Bold().FontSize(18).AlignCenter();
                    page.Content().PaddingVertical(10).Text(noteContent).FontSize(12);
                    page.Footer().AlignRight().Text($"Generated on {DateTime.Now:yyyy-MM-dd HH:mm}");
                });
            });

            using var stream = new MemoryStream();
            pdfDocument.GeneratePdf(stream);
            return stream.ToArray();
        }
    }
}
