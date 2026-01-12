using System;
using System.Collections.Generic;
using System.Text;

namespace Maui.PDFView;

public class PDFInfos
{
   public string FileName
   {
      get { return _FileName; }
      set { _FileName = value; }
   }  
   string _FileName = string.Empty;


   public int PageCount { get; set; } = -1;
   public long FileSizeInBytes { get; set; } = 0;


   public string Title
   {
      get
      {
         return (string.IsNullOrEmpty(_Title) ? System.IO.Path.GetFileNameWithoutExtension(_FileName) : _Title); 
       }
   }
   string _Title = string.Empty;
}
