namespace Maui.PDFView.DataSources;

public class HttpPdfSource : IPdfSource
{
    private readonly string _url;

    public HttpPdfSource(string url)
    {
        _url = url;
    }

    public async Task<string> GetFilePathAsync()
    {
        var tempFile = PdfTempFileHelper.CreateTempPdfFilePath();

        try
        {
#if WINDOWS
            using HttpClient client = new HttpClient(); 
            client.Timeout = Time

            using HttpResponseMessage response = await client.GetAsync(_url); 
            response.EnsureSuccessStatusCode(); 
            await using FileStream fs = new FileStream(tempFile, FileMode.Create); 
            await response.Content.CopyToAsync(fs);
#else
            using var client = new HttpClient();
            var stream = await client.GetStreamAsync(_url);
            await using var fileStream = File.Create(tempFile);
            await stream.CopyToAsync(fileStream);
#endif
        }
        catch (Exception ex)
        {

        }

        return tempFile;
    }
}