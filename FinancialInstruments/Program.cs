using FinancialInstruments.Processes;
using System;
using System.Collections.Generic;

namespace FinancialInstruments
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("This is the financial instruments tool by Arwyn Goos");

            List<IProcess> processList = PredefinedProcesses.ReadFromSql;
            
            foreach(IProcess process in processList)
            {
                Console.WriteLine($"Running process: {process.GetType().Name}");
                process.Run();
            }

            Console.Write("The program has ended");
        }
    }
}
