//namespace Maui.PDFView.DataSources;

//public class ByteArrayPdfSource : IPdfSource
//{
//   byte[] _pdfBytes;


//   public ByteArrayPdfSource()
//   {
//      _pdfBytes = null;
//   }


//   public ByteArrayPdfSource(byte[] pdfBytes)
//   {
//      _pdfBytes = pdfBytes;
//   }


//   public string LastError { get; private set; }


//   public async Task<string> GetFilePathAsync()
//   {
//      LastError = "";

//      try
//      {
//         var tempFile = PdfTempFileHelper.CreateTempPdfFilePath();
//         await File.WriteAllBytesAsync(tempFile, _pdfBytes);
//         return tempFile;
//      }
//      catch (Exception ex)
//      {
//         LastError = ex.Message;
//         return null;
//      }
//   }


//   public Task<string> LoadPDF(byte[] pdfBytes)
//   {
//      _pdfBytes = pdfBytes;
      
//      return GetFilePathAsync();
//   }
//}
