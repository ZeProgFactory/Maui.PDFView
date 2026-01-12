using System.Windows.Input;
using Maui.PDFView.DataSources;

namespace Maui.PDFView;

public partial class PdfView : View, IPdfView
{
   public static readonly BindableProperty IsHorizontalProperty = BindableProperty.Create(
           propertyName: nameof(IsHorizontal),
           returnType: typeof(bool),
           declaringType: typeof(PdfView),
           defaultValue: false);

   public static readonly BindableProperty MaxZoomProperty = BindableProperty.Create(
           propertyName: nameof(MaxZoom),
           returnType: typeof(float),
           declaringType: typeof(PdfView),
           defaultValue: 4f,
           propertyChanged: OnMaxZoomPropertyChanged);

   public static readonly BindableProperty PageAppearanceProperty = BindableProperty.Create(
           propertyName: nameof(PageAppearance),
           returnType: typeof(PageAppearance),
           declaringType: typeof(PdfView),
           defaultValue: null);

   public static readonly BindableProperty PageChangedCommandProperty = BindableProperty.Create(
           propertyName: nameof(PageChangedCommand),
           returnType: typeof(ICommand),
           declaringType: typeof(PdfView),
           defaultValue: default(ICommand));

   public static readonly BindableProperty PageIndexProperty = BindableProperty.Create(
           propertyName: nameof(PageIndex),
           returnType: typeof(uint),
           declaringType: typeof(PdfView),
           defaultValue: (uint)0, defaultBindingMode: BindingMode.TwoWay);


   public bool IsHorizontal
   {
      get => (bool)GetValue(IsHorizontalProperty);
      set => SetValue(IsHorizontalProperty, value);
   }

   public float MaxZoom
   {
      get => (float)GetValue(MaxZoomProperty);
      set => SetValue(MaxZoomProperty, value);
   }

   public PageAppearance? PageAppearance
   {
      get => (PageAppearance?)GetValue(PageAppearanceProperty);
      set => SetValue(PageAppearanceProperty, value);
   }

   public ICommand PageChangedCommand
   {
      get => (ICommand)GetValue(PageChangedCommandProperty);
      set => SetValue(PageChangedCommandProperty, value);
   }

   public uint PageIndex
   {
      get => (uint)GetValue(PageIndexProperty);
      set => SetValue(PageIndexProperty, value);
   }

   private static void OnMaxZoomPropertyChanged(BindableObject bindable, object oldValue, object newValue)
   {
      if ((float)newValue < 1f)
         throw new ArgumentException("PdfView: MaxZoom cannot be less than 1");
   }

   // - - -  - - - 


   /// <summary>
   /// Gets a value indicating whether the component is currently performing a background operation.
   /// </summary>
   public bool IsBusy
   {
      get => _IsBusy;
      internal set
      {
         //ToDo: SetValue
         _IsBusy = value;

         OnPropertyChanged("IsBusy");
      }
   }
   bool _IsBusy = false;


   private PDFInfos _PDFInfos = new PDFInfos();


   /// <summary>
   /// Load PDF from URL wo rendering it.
   /// </summary>
   /// <param name="pdfSource"></param>
   /// <param name="url"></param>
   /// <returns></returns>
   public async Task<bool> LoadPDF(IPdfSource pdfSource, string url)
   {
      bool Result = false;

      IsBusy = true;

      _PDFInfos = new PDFInfos();
      _PDFInfos.FileName = await pdfSource.LoadPDF(url);

      if (System.IO.File.Exists(_PDFInfos.FileName))
      {
         _PDFInfos = await NewPDFInfos(_PDFInfos.FileName, url);

         Result = true;
      }
      else
      {
         _PDFInfos = new PDFInfos();
      }

      IsBusy = false;

      return Result;
   }

   public PDFInfos GetInfos()
   {
      return _PDFInfos;
   }

   public async System.Threading.Tasks.Task SaveFirstPageAsImageAsync(string outputImagePath)
   {
      await SavePageAsImageAsync(outputImagePath, 0);
   }

   public void RenderPages()
   {
      // PlatformRenderPages();
   }
}
