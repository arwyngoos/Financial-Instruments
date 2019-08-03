using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialInstruments.Excel.Processes;
using FinancialInstruments.FinancialProducts;

namespace FinancialInstruments.Processes
{
    public class CreateFinancialInstrumentsProcess : IProcess
    {
        public void Run()
        {
            DataObject.Stocks = CreateStocks(DataObject.ReadFromSqlProcessOutput);
        }

        private SortedDictionary<string, Stock> CreateStocks(SortedDictionary<string, SortedDictionary<DateTime, StockObservation>> observations)
        {
            SortedDictionary<string, Stock> stocks = new SortedDictionary<string, Stock>();
            foreach (string stockId in observations.Keys)
            {
                stocks.Add(stockId, new Stock(stockId, observations[stockId]));
            }

            return stocks;
        }
    }
}
