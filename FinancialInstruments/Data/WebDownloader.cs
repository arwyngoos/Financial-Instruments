using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FinancialInstruments.Data
{
    public class WebDownloader
    {
        internal static void DownloadData(string htmlSource, string outputDirectory)
        {
            WebClient Client = new WebClient();
            Client.DownloadFile(htmlSource, outputDirectory);
        }
    }
}
