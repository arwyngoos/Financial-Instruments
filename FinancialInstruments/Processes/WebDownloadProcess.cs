using FinancialInstruments.Excel.Processes;
using FinancialInstruments.WebDownload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
