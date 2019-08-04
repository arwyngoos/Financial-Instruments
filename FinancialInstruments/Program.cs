using FinancialInstruments.Excel.Processes;
using FinancialInstruments.Processes;
using System;
using System.Collections.Generic;

namespace FinancialInstruments
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is the financial instuments tool by Arwyn Goos");

            List<IProcess> processList = PredefinedProcesses.ReadFromSqlAndCreateFinancialInstruments;
            
            foreach(IProcess process in processList)
            {
                Console.WriteLine($"Running process: {process.GetType().Name}");
                process.Run();
            }

            Console.Write("The program has ended");
        }
    }
}
