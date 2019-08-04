using System;
using System.Dynamic;
using FinancialInstruments.Utilities;

namespace FinancialInstruments.FinancialProducts
{
    public abstract class Option
    {
        public double MonteCarloValue { get; private set; }

        public double Strike { get; }

        public Enums.OptionType OptionType { get; }

        public double AnnualVolatility => Math.Sqrt(365) * DailyVolatility;

        public double DailyVolatility { get; }

        public double RiskFreeRate { get; }

        public double StockPrice { get; }

        public DateTime Maturity { get; }

        public MonteCarloSimulation MonteCarloSimulation { get; private set; }

        public DateTime ValuationDate { get; }

        public TimeSpan ValuationTimeSpan => Maturity - ValuationDate;

        public Func<double, double> PayOffFunction { get; }

        protected Option(
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
