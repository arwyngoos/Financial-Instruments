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

            List<IProcess> processList = PredefinedProcesses.ReadExcelAndWriteToSql;
            
            foreach(IProcess process in processList)
            {
                Console.WriteLine($"Running process: {process.GetType().Name}");
                process.Run();
            }

            Console.Write("The program has ended");
            
        }
    }
}
