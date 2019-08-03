using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialInstruments.Utils;

namespace FinancialInstruments.FinancialProducts
{
    public class EuropeanCustom : Option
    {
        public Enums.OptionType OptionType = Enums.OptionType.Custom;

        public EuropeanCustom(double strike, double dailyVolatility, double riskFreeRate, double stockPrice, DateTime maturity, DateTime valuationDate, Func<double, double, double> payOffFunction)
            : base(strike, dailyVolatility, riskFreeRate, stockPrice, maturity, valuationDate)
        {
            PayOffFunction = payOffFunction;
        }

    }
}