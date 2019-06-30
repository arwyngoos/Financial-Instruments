using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialInstruments.Utils
{
    public static class Extensions
    {
        public static SortedDictionary<string, SortedDictionary<DateTime, double>> ParseDataTableToSortedDictionary(this DataTable dataTable)
        {
            SortedDictionary<string, SortedDictionary<DateTime, double>> result = new SortedDictionary<string, SortedDictionary<DateTime, double>>();

            foreach(DataRow row in dataTable.AsEnumerable())
            {
                string productId = row.Field<string>("Product");
                DateTime date = row.Field<DateTime>("Date");
                double value = (double)row.Field<decimal>("Value");

                if (!result.ContainsKey(productId))
                {
                    result[productId] = new SortedDictionary<DateTime, double>();
                }

                result[productId].Add(date, value);
            }

            return result;
        }
    }
}
