using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialInstruments.SQL
{
    public class Settings
    {
        public static string TableName = "FinancialProductData";

        public static string DataBase = "ArwynTest";

        public static string DataBaseEngine = "NLLT108279";

        public static string DataDirectory = @"C:\Users\argoos\Documents\Arwyn\C# Coding\Git Projects\DataUse";

        public static string WebDownloadString = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=MSFT&apikey=demo&datatype=csv";
    }
}
