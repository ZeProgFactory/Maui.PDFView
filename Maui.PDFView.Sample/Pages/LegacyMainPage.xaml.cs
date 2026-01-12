using Maui.PDFView.Sample.ViewModels;

namespace Maui.PDFView.Sample.Pages;

public partial class LegacyMainPage : ContentPage
{
    public LegacyMainPage()
    {
        InitializeComponent();
        BindingContext = new LegacyViewModel();
    }
}