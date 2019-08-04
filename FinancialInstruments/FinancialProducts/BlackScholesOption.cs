using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Statistics.Distributions.Univariate;
using FinancialInstruments.Utilities;
using FinancialInstruments.Utils;

namespace FinancialInstruments.FinancialProducts
{
    public class BlackScholesOption : Option
    {
        public double BlackScholesValue;

        public BlackScholesOption(double strike, double dailyVolatility, double riskFreeRate, double stockPrice, DateTime maturity, DateTime valuationDate, Enums.OptionType optionType, Func<double, double> payOffFunction)
            : base(strike, dailyVolatility, riskFreeRate, stockPrice, maturity, valuationDate, optionType, payOffFunction)
        {
            SetBlackScholesPrice();
        }

        public void SetBlackScholesPrice()
        {
            double d1 = (1 / (AnnualVolatility * Math.Sqrt(ValuationTimeSpan.Years())))
                        * (Math.Log(StockPrice / Strike) + (RiskFreeRate + 0.5 * Math.Pow(AnnualVolatility, 2)) * ValuationTimeSpan.Years());

            double d2 = d1 - AnnualVolatility * Math.Sqrt(ValuationTimeSpan.Years());

            NormalDistribution normalDistribution = new NormalDistribution();

            if (OptionType == Enums.OptionType.Call)
            {
                BlackScholesValue = normalDistribution.DistributionFunction(d1) * StockPrice
                                    - normalDistribution.DistributionFunction(d2) * Strike * Math.Exp(-RiskFreeRate * ValuationTimeSpan.Years());
            }
            else if (OptionType == Enums.OptionType.Put)
            {
                BlackScholesValue = -normalDistribution.DistributionFunction(-d1) * StockPrice
                                    + normalDistribution.DistributionFunction(-d2) * Strike * Math.Exp(-RiskFreeRate * ValuationTimeSpan.Years());
            }
            else
            {
                throw new Exception($"Failed: OptionType {OptionType.ToString()} is not supported");
            }
        }
    }
}
