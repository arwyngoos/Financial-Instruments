using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialInstruments.Processes
{
    public static class DataObject
    {
        public static SortedDictionary<string, SortedDictionary<DateTime, double>> ReadExcelProcessOutput = new SortedDictionary<string, SortedDictionary<DateTime, double>>();

        public static SortedDictionary<string, SortedDictionary<DateTime, double>> ReadFromSqlProcessOutput = new SortedDictionary<string, SortedDictionary<DateTime, double>>();

    }
}
