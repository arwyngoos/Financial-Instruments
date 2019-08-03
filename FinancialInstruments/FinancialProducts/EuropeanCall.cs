using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialInstruments.Utils;

namespace FinancialInstruments.FinancialProducts
{
    public class EuropeanCall : BlackScholesOption
    {
        public Enums.OptionType OptionType = Enums.OptionType.Call;

        public EuropeanCall(double strike, double dailyVolatility, double riskFreeRate, double stockPrice, DateTime maturity, DateTime valuationDate)
            : base(strike, dailyVolatility, riskFreeRate, stockPrice, maturity, valuationDate, Enums.OptionType.Call)
        {
            PayOffFunction = EuropeanCallPayOff;
        }

        public readonly Func<double, double> EuropeanCallPayOff = delegate (double stockPrice)
        {
            return Math.Max(stockPrice - Strike, 0);
        };
    }
}
