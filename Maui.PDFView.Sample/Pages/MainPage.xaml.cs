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
   }

   private async void LegacyHTTPLoad_Clicked(object sender, EventArgs e)
   {
      // Sample pdf download 10 MB
      var url = "https://www.learningcontainer.com/wp-content/uploads/2019/09/sample-pdf-download-10-mb.pdf";

      var source = new HttpPdfSource(url);

      pdfView.Uri = await source.GetFilePathAsync();

   }

   private void HTTPLoad_Clicked(object sender, EventArgs e)
   {
      // Sample pdf download 10 MB
      var url = "https://www.learningcontainer.com/wp-content/uploads/2019/09/sample-pdf-download-10-mb.pdf";

      //var source = new HttpPdfSource(url);

      //pdfView.LoadPDF(new HttpPdfSource(), url );
      //pdfView.RenderPages();
   }
}
