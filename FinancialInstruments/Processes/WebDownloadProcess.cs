using FinancialInstruments.WebDownload;

namespace FinancialInstruments.Processes
{
    public class WebDownloadProcess : IProcess
    {
        public void Run()
        {
            WebDataDownloader.DownloadFromWeb();
        }
    }
}
