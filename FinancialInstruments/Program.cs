using FinancialInstruments.ExcelTools;
using FinancialInstruments.Helpers;
using System;
using System.Collections.Generic;
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

            string dataSource = @"C:\Users\argoos\Documents\Arwyn\C# Coding\Git Projects\DataUse";

            List<string> fileNames = Utils.Utils.getFileNames(dataSource);

            SortedDictionary<string, SortedDictionary<DateTime, double>> instrumentsObservations = ExcelReader.readExcelFiles(fileNames, dataSource);

            StockContainer stockContainer = new StockContainer(instrumentsObservations);




            Console.Write("The program has ended");
            
        }
    }
}
