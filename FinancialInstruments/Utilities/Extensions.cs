using FinancialInstruments.FinancialProducts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FinancialInstruments.Utilities
{
    public static class Extensions
    {
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
