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
    public abstract class Option
    {
        public double MonteCarloValue;

        public double Strike;

        public Enums.OptionType OptionType;

        public double AnnualVolatility => Math.Sqrt(365) * DailyVolatility;

        public double DailyVolatility;

        public double RiskFreeRate;

        public double StockPrice;

        public DateTime Maturity;

        public MonteCarloSimulation MonteCarloSimulation;

        public DateTime ValuationDate;

        public TimeSpan ValuationTimeSpan => Maturity - ValuationDate;

        public Func<double, double> PayOffFunction;

        public Option(
            double strike, 
            double dailyVolatility, 
            double riskFreeRate, 
            double stockPrice, 
            DateTime maturity, 
            DateTime valuationDate, 
            Enums.OptionType optionType, 
            Func<double, double> payOffFunction)
        {
            Strike = strike;
            DailyVolatility = dailyVolatility;
            RiskFreeRate = riskFreeRate;
            StockPrice = stockPrice;
            Maturity = maturity;
            ValuationDate = valuationDate;
            OptionType = optionType;
            PayOffFunction = payOffFunction;

            SetMonteCarloPrice(100000);
        }

        public void SetMonteCarloPrice(int numberOfSimulations)
        {
            MonteCarloSimulation monteCarloSimulation = new MonteCarloSimulation(AnnualVolatility, RiskFreeRate, StockPrice, ValuationDate, Maturity);
            for (int i = 0; i < numberOfSimulations; i++)
            {
                monteCarloSimulation.AddMonteCarloPath();
            }

            MonteCarloSimulation = monteCarloSimulation;
            MonteCarloValue = monteCarloSimulation.GetValueAtDate(Maturity, PayOffFunction);
        }

        
    }
}
