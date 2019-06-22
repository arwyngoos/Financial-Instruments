using FinancialInstruments.Data;
using FinancialInstruments.ExcelTools;
using FinancialInstruments.FinancialProducts;
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

            string dataDirectory = @"C:\Users\argoos\Documents\Arwyn\C# Coding\Git Projects\DataUse";
            string htmlSource = "https://query1.finance.yahoo.com/v7/finance/download/%5EGSPC?period1=1558515820&period2=1561194220&interval=1d&events=history&crumb=P9Nfvrt1G2p";

            WebDownloader.DownloadData(htmlSource, dataDirectory);

            SortedDictionary<string, SortedDictionary<DateTime, double>> instrumentsObservations = ExcelReader.ReadExcelFiles(dataDirectory);

            StockContainer stockContainer = new StockContainer(instrumentsObservations);



            Portfolio portFolio = new Portfolio(stockContainer, Utils.Utils.SetRandomIntegers(stockContainer.Stocks.Count), Utils.Utils.SetRandomIntegers(stockContainer.Options.Count));



            Console.Write("The program has ended");
            
        }
    }
}
