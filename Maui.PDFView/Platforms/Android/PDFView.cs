using System;
using System.Collections.Generic;
using System.Text;

namespace Maui.PDFView;

// Droid-specific implementation
partial class PdfView  
{
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
