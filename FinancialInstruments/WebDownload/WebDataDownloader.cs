using FinancialInstruments.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FinancialInstruments.WebDownload
{
    public class WebDataDownloader
    {
        private string function;
        private string apiKey;
        private string dataType;
        public WebDataDownloader()
        {
            function = "TIME_SERIES_DAILY";
            apiKey = "1C3WP3BH56DF8J1R";
            dataType = "csv";

              
        }
        public void DownloadFromWeb()
        {
            using (WebClient client = new WebClient())
            {
                foreach(string ProductId in Settings.ProductCollection)
                {
                    client.DownloadFile(GetURL(ProductId), $"{Settings.DataDirectory}/{ProductId}.csv");
                }
                
            }
        }

        private string GetURL(string symbol)
        {
            return $"https://www.alphavantage.co/query?function={function}&symbol={ symbol}&apikey={apiKey}&datatype={dataType}";
        }

    }
}
