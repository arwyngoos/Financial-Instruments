using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialInstruments.FinancialProducts;

namespace FinancialInstruments.Helpers
{
    public class StockContainer
    {
        public SortedDictionary<string, Stock> Stocks { get; set; }

        public SortedDictionary<string, Option> Options { get; set; }

        public StockContainer(SortedDictionary<string, SortedDictionary<DateTime, double>> instrumentObservations)
        {

            SortedDictionary<string, Stock> stocks = new SortedDictionary<string, Stock>();
            SortedDictionary<string, Option> options = new SortedDictionary<string, Option>();

            foreach (string instrumentName in instrumentObservations.Keys)
            {
                stocks.Add(instrumentName, new Stock(instrumentName, instrumentObservations[instrumentName]));
                options.Add(instrumentName, new Option(stocks.Values.Last(), new DateTime(2018, 1, 1), new DateTime(2019, 1, 1)));
                
            }

            this.Stocks = stocks;
            this.Options = options;
        }
    }
}
