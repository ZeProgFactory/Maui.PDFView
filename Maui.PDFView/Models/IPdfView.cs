using System.Windows.Input;

namespace Maui.PDFView
{
   public interface IPdfView : IView
   {
      bool IsHorizontal { get; set; }
      float MaxZoom { get; set; }
      PageAppearance? PageAppearance { get; set; }
      uint PageIndex { get; set; }

      ICommand PageChangedCommand { get; set; }
   }
}
