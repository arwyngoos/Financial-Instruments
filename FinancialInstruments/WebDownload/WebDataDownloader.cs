using FinancialInstruments.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FinancialInstruments.WebDownload
{
    public static class WebDataDownloader
    {
        public static void DownloadFromWeb()
        {
            using (WebClient client = new WebClient())
            {
                foreach(string ProductId in Settings.ProductCollection)
                {
                    client.DownloadFile(GetURL(ProductId), $"{Settings.DataDirectory}/{ProductId}.{Settings.InputDataType.ToString().ToLower()}");
                }
                
            }
        }

        private static string GetURL(string symbol)
        {
            return $"https://www.alphavantage.co/query?function={Settings.TimeSeriesGrid}&symbol={symbol}" +
                   $"&apikey={Settings.ApiKey}" +
                   $"&datatype={Settings.InputDataType.ToString().ToLower()}" +
                   $"&outputsize={Settings.OutputSize.ToString().ToLower()}";
        }

    }
}
