using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialInstruments.Utils;

namespace FinancialInstruments.FinancialProducts
{
    public class EuropeanPut : BlackScholesOption
    {
        public EuropeanPut(double strike, double dailyVolatility, double riskFreeRate, double stockPrice, DateTime maturity, DateTime valuationDate)
            : base(strike, dailyVolatility, riskFreeRate, stockPrice, maturity, valuationDate, Enums.OptionType.Put)
        {
            PayOffFunction = EuropeanPutPayOff;
        }

        public readonly Func<double, double> EuropeanPutPayOff = delegate (double stockPrice)
        {
            return Math.Max(stockPrice - Strike, 0);
        };
    }
}