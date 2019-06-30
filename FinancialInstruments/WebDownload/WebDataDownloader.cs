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
                client.DownloadFile(Settings.WebDownloadString, "data.csv");
            }
        }

    }
}
