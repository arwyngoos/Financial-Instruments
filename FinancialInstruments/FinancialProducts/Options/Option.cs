using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using FinancialInstruments.Utilities;

namespace FinancialInstruments.FinancialProducts
{
    public abstract class Option
    {
        public double MonteCarloValue { get; private set; }

        public double MonteCarloValueParallel { get; private set; }

        public double Strike { get; }

        public Enums.OptionType OptionType { get; }

        public double AnnualVolatility => Math.Sqrt(365) * DailyVolatility;

        public double DailyVolatility { get; }

        public double RiskFreeRate { get; }

        public double StockPrice { get; }

        public DateTime Maturity { get; }

        public MonteCarloSimulation MonteCarloSimulation { get; private set; }

        public MonteCarloSimulation MonteCarloSimulationParallel { get; private set; }

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
        }

        private void ValueOptionMonteCarloParallel(int numberOfSimulations)
        {
            MonteCarloSimulation monteCarloSimulation = new MonteCarloSimulation(AnnualVolatility, RiskFreeRate, StockPrice, ValuationDate, Maturity);
            Stopwatch watch = new Stopwatch();

            watch.Start();
            var paths = new ConcurrentBag<SortedDictionary<DateTime, double>>();
            Parallel.For(0, numberOfSimulations, i =>
            {
                paths.Add(monteCarloSimulation.SimulatePath());
            });
            watch.Stop();

            monteCarloSimulation.MonteCarloPaths = paths.ToList();

            Console.WriteLine($"Monte carlo simulation parallel took {watch.Elapsed}");
            MonteCarloSimulationParallel = monteCarloSimulation;
            MonteCarloValueParallel = monteCarloSimulation.GetValueAtDate(Maturity, PayOffFunction);
        }

        public void ValueOptionMonteCarloIterative(int numberOfSimulations)
        {
            MonteCarloSimulation monteCarloSimulation = new MonteCarloSimulation(AnnualVolatility, RiskFreeRate, StockPrice, ValuationDate, Maturity);
            Stopwatch watch = new Stopwatch();

            watch.Start();
            for (int i = 0; i < numberOfSimulations; i++)
            {
                monteCarloSimulation.AddMonteCarloPath();
            }
            watch.Stop();

            Console.WriteLine($"Monte carlo simulation took {watch.Elapsed}");

            MonteCarloSimulation = monteCarloSimulation;
            MonteCarloValue = monteCarloSimulation.GetValueAtDate(Maturity, PayOffFunction);
        }
    }
}
