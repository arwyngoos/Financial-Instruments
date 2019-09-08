using System;
using System.Collections.Generic;
using FinancialInstruments.Utilities;

namespace FinancialInstruments.FinancialProducts
{
    public class MonteCarloSimulation
    {
        public List<SortedDictionary<DateTime, double>> MonteCarloPaths = new List<SortedDictionary<DateTime, double>>();

        public double AnnualVolatility;

        public double RiskFreeRate;

        public double StockPrice;

        public DateTime ValuationDate;

        public DateTime MaturityDate;

        public MonteCarloSimulation(double annualVolatility, double riskFreeRate, double stockPrice, DateTime valuationDate, DateTime maturityDate)
        {
            AnnualVolatility = annualVolatility;
            RiskFreeRate = riskFreeRate;
            StockPrice = stockPrice;
            MaturityDate = maturityDate;
            ValuationDate = valuationDate;
        }

        internal SortedDictionary<DateTime, double> SimulatePath()
        {
            SortedDictionary<DateTime, double> simulatedPath = new SortedDictionary<DateTime, double>();

            List<DateTime> dates = Utils.CreateDailyDateTimeGrid(ValuationDate, MaturityDate);
            List<double> randomNumbers = Statistics.GenerateStandardNormalRandomNumbers(dates.Count - 1);

            simulatedPath.Add(ValuationDate, StockPrice);
            for (int i = 1; i < dates.Count; i++)
            {
                simulatedPath.Add(dates[i], IncrementSimulationToNextStep(simulatedPath[dates[i-1]], randomNumbers[i-1]));
            }

            return simulatedPath;
        }

        private double IncrementSimulationToNextStep(double previousValue, double innovation)
        {
            return previousValue * Math.Exp(((RiskFreeRate - 0.5 * Math.Pow(AnnualVolatility, 2)) * (1d / 365d)) 
                                            + AnnualVolatility * Math.Sqrt(1d / 365d) * innovation);

            //return previousValue + previousValue * RiskFreeRate * 1 / 365 +
            //       previousValue * AnnualVolatility * innovation;
        }

        public double GetValueAtDate(DateTime valuationDate, Func<double, double> payOffFunction)
        {
            List<double> payOffs = new List<double>();

            foreach(SortedDictionary <DateTime, double> simulation in MonteCarloPaths)
            {
                payOffs.Add(payOffFunction(simulation[valuationDate]));
            }

            return payOffs.Mean();
        }

        public void AddMonteCarloPath()
        {
            MonteCarloPaths.Add(SimulatePath());
        }
    }
}
