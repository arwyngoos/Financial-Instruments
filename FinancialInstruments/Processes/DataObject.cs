using FinancialInstruments.FinancialProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialInstruments.Processes
{
    public static class DataObject
    {
        public static SortedDictionary<string, SortedDictionary<DateTime, StockObservation>> ReadExcelProcessOutput;

        public static SortedDictionary<string, SortedDictionary<DateTime, StockObservation>> ReadFromSqlProcessOutput;

        public static SortedDictionary<string, SortedDictionary<DateTime, StockObservation>> ReadJsonProcessOutput;

        public static SortedDictionary<string, Stock> Stocks;
    }
}
