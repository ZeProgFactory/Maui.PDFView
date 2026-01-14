using System.Reflection;
using Maui.PDFView.DataSources;

namespace Maui.PDFView.Sample.Pages;

public partial class MainPage : ContentPage
{
   public MainPage()
   {
      InitializeComponent();

      //pdfView.PropertyChanged += (s, e) =>
      //{
      //   if (e.PropertyName == nameof(pdfView.fUri))
      //   {
      //      Title = System.IO.Path.GetFileName(pdfView.Uri);
      //   }
      //};

      pdfView.PropertyChanged += (s, e) =>
      {
         if (e.PropertyName == nameof(pdfView.IsBusy))
         {
            Title = (pdfView.IsBusy ? "IsBusy" : pdfView.GetInfos().Title);
         }
      };
   }


   private async void HTTPLoad_Clicked(object sender, EventArgs e)
   {
      // Sample pdf download 10 MB
      var url = "https://www.learningcontainer.com/wp-content/uploads/2019/09/sample-pdf-download-10-mb.pdf";

      await pdfView.LoadPDF(new HttpPdfSource(), url);

      Load1rstPage();
   }


   private async void Load1rstPage()
   {
      var tmpFile = System.IO.Path.GetTempFileName();
      await pdfView.SaveFirstPageAsImageAsync(tmpFile);

      imageCover.Source = tmpFile;
      imagePreview.Source = tmpFile;
   }


   private void Unload_Clicked(object sender, EventArgs e)
   {
      pdfView.UnloadPDF();

      imageCover.Source = null;
      imagePreview.Source = null;
   }


   private async void GetInfo_Clicked(object sender, EventArgs e)
   {
      // Display some info about the PDF
      PDFInfos pdfInfos = pdfView.GetInfos();

      await DisplayAlertAsync("PDF Infos", ""
         + $"Title: {pdfInfos.Title}\n"
         + $"File: {pdfInfos.FileName}\n"
         + $"PWD: {pdfInfos.IsPasswordProtected}\n"
         + $"Pages: {pdfInfos.PageCount}\n"
         + $"Size: {pdfInfos.FileSizeInBytes / 1024} KB\n"
         //+ $"IsEncrypted: {pdfInfos.IsEncrypted}\n" 
         //+ $"IsLinearized: {pdfInfos.IsLinearized}\n"
         , "OK"
         );
   }


   private void RenderPages_Clicked(object sender, EventArgs e)
   {
      pdfView.RenderPages();
   }


   private async void Resx1Load_Clicked(object sender, EventArgs e)
   {
      var tmp = GetPdfFileContent("pdf1.pdf");

      await pdfView.LoadPDF(new FilePdfSource(), tmp);

      Load1rstPage();
   }


   private async void Resx2Load_Clicked(object sender, EventArgs e)
   {
      var tmp = GetPdfFileContent("pdf2.pdf");

      await pdfView.LoadPDF(new FilePdfSource(), tmp);

      Load1rstPage();
   }


   public string GetPdfFileContent(string name)
   {
      var assembly = Assembly.GetExecutingAssembly();
      string resourceName = assembly
          .GetManifestResourceNames()
          .Single(str => str.EndsWith(name));

      byte[] bytes;
      using (Stream stream = assembly.GetManifestResourceStream(resourceName))
      {
         bytes = new byte[stream.Length];
         stream.Read(bytes, 0, bytes.Length);
      }

      //  Save the data to a file to get the path to the file.
      //  You can then pass the file path to the library.
      var fileName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
      File.WriteAllBytes(fileName, bytes);

      //  Return path to PDF file
      return fileName;
   }
}
