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
        public static SortedDictionary<string, SortedDictionary<DateTime, StockObservation>> ReadExcelProcessOutput = new SortedDictionary<string, SortedDictionary<DateTime, StockObservation>>();

        public static SortedDictionary<string, SortedDictionary<DateTime, StockObservation>> ReadFromSqlProcessOutput = new SortedDictionary<string, SortedDictionary<DateTime, StockObservation>>();

    }
}
