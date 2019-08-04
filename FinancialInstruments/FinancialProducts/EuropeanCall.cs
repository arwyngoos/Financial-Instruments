using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialInstruments.Utilities;

namespace FinancialInstruments.FinancialProducts
{
    public class EuropeanCall : BlackScholesOption
    {
        public EuropeanCall(double strike, double dailyVolatility, double riskFreeRate, double stockPrice, DateTime maturity, DateTime valuationDate)
            : base(strike, dailyVolatility, riskFreeRate, stockPrice, maturity, valuationDate, Enums.OptionType.Call, input => Math.Max(input - strike, 0))
        {
        }
    }
}
