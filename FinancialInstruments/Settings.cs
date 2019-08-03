using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialInstruments.Utils;

namespace FinancialInstruments.SQL
{
    public class Settings
    {
        public static string TableName = "StockData";

        public static string DataBase = "ArwynTest";

        public static string DataBaseEngine = "NLLT108279";

        public static string DataDirectory = @"C:\Users\argoos\Documents\Arwyn\C# Coding\Git Projects\Data";

        public static List<string> ProductCollection = new List<string>
        {
            "MSFT",
            "AAPL",
            "AMZN"
        };

        public static Enums.InputDataType InputDataType = Enums.InputDataType.CSV;

        public static string ApiKey = "1C3WP3BH56DF8J1R";

        public static string TimeSeriesGrid = "TIME_SERIES_DAILY";

        public static Enums.OutputSize OutputSize = Enums.OutputSize.Compact;
    }
}
