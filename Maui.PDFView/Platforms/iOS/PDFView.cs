using System;
using System.Collections.Generic;
using System.Text;

namespace Maui.PDFView;

// iOS-specific implementation 
partial class PdfView
{
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

}
