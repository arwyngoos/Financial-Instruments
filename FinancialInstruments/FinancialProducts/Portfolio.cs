using FinancialInstruments.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialInstruments.FinancialProducts
{
    public class Portfolio
    {
        public SortedDictionary<string, Stock> StockList { get; set; }

        public SortedDictionary<string, Option> OptionList { get; set; }

        public List<double> StockAmounts { get; set; }

        public List<double> OptionAmounts { get; set; }





        public Portfolio(StockContainer stockContainer, List<double> stockAmounts, List<double> optionAmounts)
        {
            if (stockAmounts.Count != stockContainer.Stocks.Count || optionAmounts.Count != stockContainer.Options.Count)
            {
                throw new Exception("Failed: the number of weights is not equal to the number of stocks");
            }

            this.StockList = stockContainer.Stocks;
            this.OptionList = stockContainer.Options;
            this.StockAmounts = stockAmounts;
            this.OptionAmounts = optionAmounts;
        }


        public double GetMarketValue(DateTime date)
        {
            double stockValue = StockList.Sum(x => x.Value.GetStockValue(date));

            double optionValue = OptionList.Sum(x => x.Value.GetOptionValue(date));

            return stockValue + optionValue;

        }

    }
}
