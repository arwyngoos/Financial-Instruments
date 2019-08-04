using System.Net;

namespace FinancialInstruments.WebDownload
{
    public static class WebDataDownloader
    {
        public static void DownloadFromWeb()
        {
            using (WebClient client = new WebClient())
            {
                foreach(string productId in Settings.ProductCollection)
                {
                    client.DownloadFile(GetUrl(productId), $"{Settings.DataDirectory}/{productId}.{Settings.InputDataType.ToString().ToLower()}");
                }
                
            }
        }

        private static string GetUrl(string symbol)
        {
            return $"https://www.alphavantage.co/query?function={Settings.TimeSeriesGrid}&symbol={symbol}" +
                   $"&apikey={Settings.ApiKey}" +
                   $"&datatype={Settings.InputDataType.ToString().ToLower()}" +
                   $"&outputsize={Settings.OutputSize.ToString().ToLower()}";
        }

    }
}
