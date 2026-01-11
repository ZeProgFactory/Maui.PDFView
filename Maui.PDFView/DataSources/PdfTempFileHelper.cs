namespace Maui.PDFView.DataSources;

public class PdfTempFileHelper
{
    /// <summary>
    /// Creates a unique temporary file path for a PDF file.
    /// </summary>
    public static string CreateTempPdfFilePath()
    {
        //ToDo: place temp files in subfolder in order to be able to dlete previously created temp files on changed PDFFile
        return Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".pdf");
    }
}