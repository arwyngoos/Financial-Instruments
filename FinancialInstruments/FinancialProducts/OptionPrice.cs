using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Statistics.Distributions.Univariate;
using FinancialInstruments.Utils;

namespace FinancialInstruments.FinancialProducts
{
    public class OptionPrice
    {
        public double BlackScholesValue;

        public double MonteCarloValue;

        public double Strike;

        public double AnnualVolatility => Math.Sqrt(252) * DailyVolatility;

        public double DailyVolatility;

        public double RiskFreeRate;

        public double StockPrice;

        public DateTime Maturity;

        public Enums.OptionType OptionType;

        public MonteCarloSimulation MonteCarloSimulation;

        public DateTime ValuationDate;

        public TimeSpan ValuationTimeSpan => Maturity - ValuationDate;

        public Func<double, double> PayOffFunction;

        public OptionPrice(double strike, double dailyVolatility, double riskFreeRate, double stockPrice, DateTime maturity, DateTime ValudationDate, Enums.OptionType optionType)
        {
            Strike = strike;
            DailyVolatility = dailyVolatility;
            RiskFreeRate = riskFreeRate;
            StockPrice = stockPrice;
            Maturity = maturity;
            OptionType = optionType;
            ValuationDate = ValudationDate;

            SetBlackScholesPrice();
            SetMonteCarloPrice(1000);
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

        public void SetMonteCarloPrice(int numberOfSimulations)
        {
            MonteCarloSimulation monteCarloSimulation = new MonteCarloSimulation(DailyVolatility, RiskFreeRate, StockPrice, ValuationDate, Maturity);
            for (int i = 0; i < numberOfSimulations; i++)
            {
                monteCarloSimulation.AddMonteCarloPath();
            }

            BlackScholesValue = monteCarloSimulation.GetValueAtDate(Maturity, PayOffFunction);
        }

        
    }
}
