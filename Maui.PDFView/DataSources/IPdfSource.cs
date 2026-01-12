namespace Maui.PDFView.DataSources;

public interface IPdfSource
{
   string LastError { get; }

   Task<string> GetFilePathAsync();

   Task<string> LoadPDF(string url);
   //Task<string> LoadPDF(byte[] pdfBytes);

}
