using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialInstruments.Utilities;

namespace FinancialInstruments.FinancialProducts
{
    public class EuropeanPut : BlackScholesOption
    {
        public EuropeanPut(double strike, double dailyVolatility, double riskFreeRate, double stockPrice, DateTime maturity, DateTime valuationDate)
            : base(strike, dailyVolatility, riskFreeRate, stockPrice, maturity, valuationDate, Enums.OptionType.Put, input => Math.Max(input - strike, 0))
        {
        }
    }
}