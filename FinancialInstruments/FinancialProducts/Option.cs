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
        public double BlackScholesValue;

        public double MonteCarloValue;

        public static double Strike;

        public double AnnualVolatility => Math.Sqrt(252) * DailyVolatility;

        public double DailyVolatility;

        public double RiskFreeRate;

        public double StockPrice;

        public DateTime Maturity;

        public MonteCarloSimulation MonteCarloSimulation;

        public DateTime ValuationDate;

        public TimeSpan ValuationTimeSpan => Maturity - ValuationDate;

        public Func<double, double> PayOffFunction;

        public Option(double strike, double dailyVolatility, double riskFreeRate, double stockPrice, DateTime maturity, DateTime ValudationDate)
        {
            Strike = strike;
            DailyVolatility = dailyVolatility;
            RiskFreeRate = riskFreeRate;
            StockPrice = stockPrice;
            Maturity = maturity;
            ValuationDate = ValudationDate;

            SetMonteCarloPrice(1000);
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
