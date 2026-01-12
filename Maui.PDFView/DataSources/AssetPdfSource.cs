using System.Reflection;

namespace Maui.PDFView.DataSources;

public class AssetPdfSource : IPdfSource
{
   string _resourcePath;

   public AssetPdfSource()
   {
      _resourcePath = string.Empty;
   }
   public AssetPdfSource(string resourcePath)
   {
      _resourcePath = resourcePath;
   }

   public string LastError { get; private set; }


   public async Task<string> GetFilePathAsync()
   {
      LastError = "";

      try
      {
         var assembly = Assembly.GetEntryAssembly();
         byte[] bytes;
         await using (Stream stream = assembly.GetManifestResourceStream(_resourcePath))
         {
            bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
         }

         var tempFile = PdfTempFileHelper.CreateTempPdfFilePath();
         await File.WriteAllBytesAsync(tempFile, bytes);

         return tempFile;
      }
      catch (Exception ex)
      {
         LastError = ex.ToString();
         System.Diagnostics.Debug.WriteLine(ex.ToString());

         return "";
      }
   }


   public Task<string> LoadPDF(string resourcePath)
   {
      _resourcePath = resourcePath;

      return GetFilePathAsync();
   }
}
