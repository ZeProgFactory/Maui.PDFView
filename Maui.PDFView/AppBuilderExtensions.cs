namespace Maui.PDFView
{
    public static class AppBuilderExtensions
    {
        public static MauiAppBuilder UseMauiPdfView(this MauiAppBuilder builder)
        {
            builder.ConfigureMauiHandlers((handlers) =>
            {
#if ANDROID
                handlers.AddHandler(typeof(PdfView), typeof(PdfViewHandler));
#elif IOS
                handlers.AddHandler(typeof(PdfView), typeof(PdfViewHandler));
#elif MACCATALYST
                handlers.AddHandler(typeof(PdfView), typeof(PdfViewHandler));
#elif WINDOWS
                handlers.AddHandler(typeof(PdfView), typeof(PdfViewHandler));
#endif
            });

            return builder;
        }
    }
}
