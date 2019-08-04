using FinancialInstruments.FinancialProducts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FinancialInstruments.Utilities
{
    public static class Extensions
    {
        public static SortedDictionary<string, SortedDictionary<DateTime, StockObservation>> ParseDataTableToSortedDictionary(this DataTable dataTable)
        {
            SortedDictionary<string, SortedDictionary<DateTime, StockObservation>> result = new SortedDictionary<string, SortedDictionary<DateTime, StockObservation>>();

            foreach(DataRow row in dataTable.AsEnumerable())
            {
                string productId = row.Field<string>("Product");
                DateTime date = row.Field<DateTime>("Date");
                double open = (double)row.Field<decimal>("Open");
                double high = (double)row.Field<decimal>("High");
                double low = (double)row.Field<decimal>("Low");
                double close = (double)row.Field<decimal>("close");
                int volume = Convert.ToInt32(row.Field<decimal>("Volume"));

                if (!result.ContainsKey(productId))
                {
                    result[productId] = new SortedDictionary<DateTime, StockObservation>();
                }

                result[productId].Add(date, new StockObservation(date, open, high, low, close, volume));
            }

            return result;
        }

        public static double Years(this TimeSpan timeSpan)
        {
            return timeSpan.TotalDays / 365;
        }

        public static double Mean(this List<double> numbers)
        {
            return numbers.Sum() / numbers.Count;
        }
    }
}
