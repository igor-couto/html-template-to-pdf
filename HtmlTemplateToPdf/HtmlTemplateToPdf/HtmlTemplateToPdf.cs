using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HtmlTemplateToPdf
{
    public class HtmlTemplateToPdf
    {
        public string Identifyer { private get; set; } = "@";

        public PdfDocument GetPdf<T>(T model, string pathToHtml)
        {
            var properties = new Dictionary<string, string>();

            foreach (var prop in model.GetType().GetProperties())
                properties.Add(prop.Name, prop.GetValue(model, null).ToString());

            var stringBuilder = new StringBuilder(File.ReadAllText(pathToHtml));

            foreach (var property in properties)
                stringBuilder.Replace(Identifyer + property.Key, property.Value);
            
            var bytes = OpenHtmlToPdf.Pdf.From(stringBuilder.ToString()).Content();

            return new PdfDocument(bytes);
        }

        public void CreatePdf<T>(T model, string pathToHtml, string pathToPdf)
        {
            var pdf = GetPdf(model, pathToHtml);

            using (var file = new FileStream(pathToPdf, FileMode.Create, FileAccess.Write))
            {
                file.Write(pdf.Bytes, 0, pdf.Bytes.Length);
            }
        }
    }
}
