using Android.Graphics.Pdf;
using Android.Graphics;
using Android.OS;
using AndroidX.RecyclerView.Widget;
using Maui.PDFView.Platforms.Android.Common;
using Microsoft.Maui.Handlers;
using Android.Widget;
using Android.Views;
using Maui.PDFView.Events;
using Maui.PDFView.Helpers;
using Maui.PDFView.Platforms.Android;

namespace Maui.PDFView;

// Droid-specific implementation
partial class PdfView
{
   PdfRenderer _PdfRenderer = null;

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

      _PdfRenderer = new PdfRenderer(ParcelFileDescriptor.Open(new Java.IO.File(pdfPath), ParcelFileMode.ReadOnly));

   }


   public void UnloadPDF()
   {
      if (_PdfRenderer != null)
      {
         // close the renderer
         _PdfRenderer.Close();

         _PdfRenderer = null;
         _PDFInfos = new PDFInfos();
      }

      ClearPages();
   }


   private async Task<PDFInfos> NewPDFInfos(string pdfPath, string url)
   {
      if (_PdfRenderer == null)
      {
         await LoadPDF(pdfPath);
      }

      if (_PdfRenderer != null)
      {
         _PDFInfos = new PDFInfos()
         {
            PageCount = _PdfRenderer.PageCount,
            // IsPasswordProtected = _PdfRenderer.,
            FileName = url,
            FileSizeInBytes = new System.IO.FileInfo(pdfPath).Length,
         };
      }
      else
      {
         _PDFInfos = new PDFInfos();
      }

      return _PDFInfos;
   }


   public async System.Threading.Tasks.Task SavePageAsImageAsync(string outputImagePath, uint pageNumber = 0)
   {
      if (_PdfRenderer == null)
      {
         return;
      }

      var page = _PdfRenderer.OpenPage((int)pageNumber);

      // create bitmap at appropriate size
      var bitmap = Bitmap.CreateBitmap(page.Width, page.Height, Bitmap.Config.Argb8888);

      //  If you need to apply a color to the page
      bitmap.EraseColor(Android.Graphics.Color.White);

      // Crop page
      var matrix = GetCropMatrix(page, bitmap, Thickness.Zero);

      // render PDF page to bitmap
      page.Render(bitmap, null, matrix, PdfRenderMode.ForDisplay);

      string savedPath = BitmapSaver.SaveBitmapToFile(bitmap, outputImagePath, Bitmap.CompressFormat.Png);

      // close the page
      page.Close();
   }


   private Matrix? GetCropMatrix(PdfRenderer.Page page, Bitmap bitmap, Thickness bounds)
   {
      if (bounds.IsEmpty)
         return null;

      int pageWidth = page.Width;
      int pageHeight = page.Height;

      var cropLeft = (int)bounds.Left;
      int cropTop = (int)bounds.Top;
      int cropRight = pageWidth - (int)bounds.Right;
      int cropBottom = pageHeight - (int)bounds.Bottom;

      // Create a matrix for shifting and scaling
      Matrix matrix = new Matrix();

      // Scale the cut area to the entire bitmap
      float scaleX = (float)bitmap.Width / (cropRight - cropLeft);
      float scaleY = (float)bitmap.Height / (cropBottom - cropTop);

      matrix.SetScale(scaleX, scaleY);

      // Shift the rendering area so that only the necessary part of the PDF is drawn
      matrix.PostTranslate(-cropLeft * scaleX, -cropTop * scaleY);

      return matrix;
   }


   public void RenderPages()
   {
      //if (_PdfDocument == null)
      //{
      //   return;
      //}

      //if (Handler is IPlatformViewHandler platformHandler)
      //{
      //   (platformHandler as Maui.PDFView.PdfViewHandler)?.RenderPages(_PdfDocument);
      //}
   }
}
