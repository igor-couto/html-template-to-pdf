using System.IO;

namespace HtmlTemplateToPdf
{
    public class PdfDocument
    {
        public byte[] Bytes { get; }

        public Stream Stream
        {
            get => new MemoryStream(Bytes);
        }

        public PdfDocument(byte[] bytes)
            => Bytes = bytes;
    }
}