using Maui.PDFView.DataSources;

namespace Maui.PDFView.Sample.Pages;

public partial class MainPage : ContentPage
{
   public MainPage()
   {
      InitializeComponent();

      pdfView.PropertyChanged += (s, e) =>
      {
         if (e.PropertyName == nameof(pdfView.Uri))
         {
            Title = System.IO.Path.GetFileName(pdfView.Uri);
         }
      };

      pdfView.PropertyChanged += (s, e) =>
      {
         if (e.PropertyName == nameof(pdfView.IsBusy))
         {
            Title = (pdfView.IsBusy ? "IsBusy" : pdfView.GetInfos().Title);
         }
      };
   }

   private async void LegacyHTTPLoad_Clicked(object sender, EventArgs e)
   {
      // Sample pdf download 10 MB
      var url = "https://www.learningcontainer.com/wp-content/uploads/2019/09/sample-pdf-download-10-mb.pdf";

      var source = new HttpPdfSource(url);

      pdfView.Uri = await source.GetFilePathAsync();

   }

   private async void HTTPLoad_Clicked(object sender, EventArgs e)
   {
      // Sample pdf download 10 MB
      var url = "https://www.learningcontainer.com/wp-content/uploads/2019/09/sample-pdf-download-10-mb.pdf";

      await pdfView.LoadPDF(new HttpPdfSource(), url);
   }

   private void GetInfo_Clicked(object sender, EventArgs e)
   {
      // Display some info about the PDF
      PDFInfos pdfInfos = pdfView.GetInfos();

      DisplayAlertAsync("PDF Infos", ""
         + $"Title: {pdfInfos.Title}\n"
         + $"File: {pdfInfos.FileName}\n"
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
}
