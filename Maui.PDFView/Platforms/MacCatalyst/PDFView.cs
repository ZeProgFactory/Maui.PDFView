using System;
using CoreGraphics;
using Foundation;
using ImageIO;
using MobileCoreServices;
using PdfKit;

namespace Maui.PDFView;

// MacCatalyst-specific implementation
partial class PdfView
{
   PdfDocument _PdfDocument = null;

   public async Task LoadPDF(string pdfPath)
   {
      UnloadPDF();

      // Open the PDF file
      //StorageFile pdfFile = await StorageFile.GetFileFromPathAsync(pdfPath);

      //using (IRandomAccessStream pdfStream = await pdfFile.OpenAsync(FileAccessMode.Read))
      //{
      //   // Load the PDF document
      //   _PdfDocument = await PdfDocument.LoadFromStreamAsync(pdfStream);
      //}
   }


   public void UnloadPDF()
   {
      //if (_PdfDocument != null)
      //{
      //   _PdfDocument = null;
      //   _PDFInfos = new PDFInfos();
      //}

      ClearPages();
   }

   private async Task<PDFInfos> NewPDFInfos(string pdfPath, string url)
   {
      _PDFInfos = new PDFInfos()
      {
         //               PageCount = ,
         FileName = url,
         FileSizeInBytes = new System.IO.FileInfo(pdfPath).Length,
      };

      return _PDFInfos;
   }


   public async System.Threading.Tasks.Task SavePageAsImageAsync(string outputImagePath, uint pageNumber = 0)
   {
      throw new NotImplementedException();
   }

   public async System.Threading.Tasks.Task SavePageAsImageAsync(string pdfPath, string outputImagePath, uint pageNumber = 0)
   {
      if (!File.Exists(pdfPath))
         throw new FileNotFoundException("PDF not found", pdfPath);

      // Load PDF
      var pdfDoc = new PdfDocument(NSUrl.FromFilename(pdfPath));

      if (pdfDoc.PageCount == 0)
         throw new InvalidOperationException("PDF has no pages.");


      // Get first page
      var page = pdfDoc.GetPage((nint)pageNumber);

      var pageBounds = page.GetBoundsForBox(PdfDisplayBox.Media);

      // Create bitmap context
      int width = (int)pageBounds.Width;
      int height = (int)pageBounds.Height;

      using var colorSpace = CGColorSpace.CreateDeviceRGB();
      using var context = new CGBitmapContext(
          data: IntPtr.Zero,
          width: width,
          height: height,
          bitsPerComponent: 8,
          bytesPerRow: width * 4,
          colorSpace: colorSpace,
          bitmapInfo: CGImageAlphaInfo.PremultipliedLast
      );

      // Flip coordinate system (PDFKit uses bottom-left origin)
      context.TranslateCTM(0, height);
      context.ScaleCTM(1, -1);

      // Draw the PDF page
      page.Draw(PdfDisplayBox.Media, context);

      // Create CGImage
      using var cgImage = context.ToImage();

      // Encode PNG using ImageIO
      using var url = NSUrl.FromFilename(outputImagePath);
      using var dest = CGImageDestination.Create(url, UTType.PNG, 1);

      dest.AddImage(cgImage);
      dest.Close();
   }

}
