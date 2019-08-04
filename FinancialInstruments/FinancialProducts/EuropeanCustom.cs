using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialInstruments.Utilities;

namespace FinancialInstruments.FinancialProducts
{
    public class EuropeanCustom : Option
    {
        public EuropeanCustom(double strike, double dailyVolatility, double riskFreeRate, double stockPrice, DateTime maturity, DateTime valuationDate, Func<double, double> payOffFunction)
            : base(strike, dailyVolatility, riskFreeRate, stockPrice, maturity, valuationDate, Enums.OptionType.Custom, payOffFunction)
        {
        }
    }
}