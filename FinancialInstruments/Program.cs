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

            string dataSource = @"C:\Users\argoos\Documents\Arwyn\C# Coding\Git Projects\Data";

            List<string> fileNames = Utils.Utils.getFileNames(dataSource);



        }
    }
}
