using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Statistics.Distributions.Univariate;
using FinancialInstruments.Utils;

namespace FinancialInstruments.FinancialProducts
{
    public class MonteCarloSimulation
    {
        public SortedDictionary<int, SortedDictionary<DateTime, double>> MonteCarloPaths = new SortedDictionary<int, SortedDictionary<DateTime, double>>();

        public double DailyVolatility;

        public double RiskFreeRate;

        public double StockPrice;

        public DateTime ValuationDate;

        public DateTime MaturityDate;

        public MonteCarloSimulation(double dailyVolatility, double riskFreeRate, double stockPrice, DateTime valuationDate, DateTime maturityDate)
        {
            DailyVolatility = dailyVolatility;
            RiskFreeRate = riskFreeRate;
            StockPrice = stockPrice;
            MaturityDate = maturityDate;
            ValuationDate = valuationDate;
        }

        public void AddMonteCarloPath()
        {
            MonteCarloPaths.Add(MonteCarloPaths.Keys.Count + 1, SimulatePath());
        }

        private SortedDictionary<DateTime, double> SimulatePath()
        {
            SortedDictionary<DateTime, double> simulatedPath = new SortedDictionary<DateTime, double>();

            List<DateTime> dates = Utils.Utils.CreateDailyDateTimeGrid(ValuationDate, MaturityDate);

            NormalDistribution normalDistribution = new NormalDistribution();
            List<double> randomNumbers = Utils.Statistics.GenerateStandardNormalRandomNumbers(dates.Count - 1);

            simulatedPath.Add(ValuationDate, StockPrice);

            for(int i = 1; i < dates.Count; i++)
            {
                simulatedPath.Add(dates[i], IncrementSimulationToNextStep(simulatedPath[dates[i-1]], randomNumbers[i-1]));
            }

            return simulatedPath;
        }

        private double IncrementSimulationToNextStep(double previousValue, double innovation)
        {

            return previousValue + previousValue * RiskFreeRate * 1 / 365 +
                   previousValue * DailyVolatility * innovation;
        }

        public double GetValueAtDate(DateTime valuationDate, Func<double, double> payOffFunction)
        {
            List<double> payOffs = new List<double>();

            foreach(int simulationNumber in MonteCarloPaths.Keys)
            {
                payOffs.Add(payOffFunction(MonteCarloPaths[simulationNumber][valuationDate]));
            }

            return payOffs.Mean();
        }
    }
}
