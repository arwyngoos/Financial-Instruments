using FinancialInstruments.Excel.Processes;
using FinancialInstruments.ExcelTools;
using FinancialInstruments.FinancialProducts;
using FinancialInstruments.Helpers;
using FinancialInstruments.Processes;
using FinancialInstruments.SQL;
using FinancialInstruments.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialInstruments
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is the financial instuments tool by Arwyn Goos");

            List<IProcess> processList = PredefinedProcesses.DownloadFromWebAndWriteToAndReadFromSQL;
            
            foreach(IProcess process in processList)
            {
                process.Run();
            }


            //StockContainer stockContainer = new StockContainer(instrumentsObservations);
            //Portfolio portFolio = new Portfolio(stockContainer, Utils.Utils.SetRandomIntegers(stockContainer.Stocks.Count), Utils.Utils.SetRandomIntegers(stockContainer.Options.Count));

            Console.Write("The program has ended");
            
        }
    }
}
