using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FinancialInstruments.Utils;

namespace FinancialInstruments.FinancialProducts
{
    public class Stock
    {
        public string Name { get; set; }

        public SortedDictionary<DateTime, StockObservation> Observations { get; set; }

        public SortedDictionary<DateTime, double> Returns { get; set; }

        public SortedDictionary<DateTime, double> EWMAVolatilityPath { get; set; }

        public Option OptionPrice;

        public double CurrentValue => Observations.Last().Value.Close;

        public DateTime LastObservation => Observations.Last().Key;

        public double CurrentVolatility => EWMAVolatilityPath.Last().Value;

        public Stock(string name, SortedDictionary<DateTime, StockObservation> observations)
        {
            Name = name;
            Observations = observations;

            Returns = GetReturns();
            EWMAVolatilityPath = GetEWMAVolatility();
        }

        private SortedDictionary<DateTime, double> GetReturns()
        {
            SortedDictionary<DateTime, double> returns = new SortedDictionary<DateTime, double>();

            foreach (DateTime date in Observations.Keys)
            {
                returns.Add(date, (Math.Log(Observations[date].Close) - Math.Log(Observations[date].Open)));
            }

            return returns;
        }

        private SortedDictionary<DateTime, double> GetEWMAVolatility()
        {
            SortedDictionary<DateTime, double> volatilityPath = new SortedDictionary<DateTime, double>();
            List<DateTime> dates = Returns.Keys.ToList();

            volatilityPath.Add(Returns.Keys.First(), Math.Pow(Returns.Values.First(), 2));

            for (int i = 1; i < dates.Count; i++)
            {
                volatilityPath[dates[i]] = 0.94 * volatilityPath[dates[i - 1]] + 0.06 * Math.Pow(Returns[dates[i]], 2);
            }

            return new SortedDictionary<DateTime, double>(volatilityPath.ToDictionary(x => x.Key, y => Math.Sqrt(y.Value)));
        }
    }
}
