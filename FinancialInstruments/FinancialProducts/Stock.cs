using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialInstruments.Utils;

namespace FinancialInstruments.FinancialProducts
{
    public class Stock
    {
        public string Name { get; set; }

        public SortedDictionary<DateTime, double> Returns { get; set; }

        public SortedDictionary<DateTime, double> Levels { get; set; }

        public double CurrentVolatility
        {
            get
            {
                return VolatilityPath.Last().Value;
            }
        }
        public SortedDictionary<DateTime, double> VolatilityPath { get; set; }

        public Stock(string name, SortedDictionary<DateTime, double> levels, Enums.VolatilityModels volatilityModel= Enums.VolatilityModels.EWMA)
        {
            this.Name = name;
            this.Levels = levels;

            Returns = SetReturns();
            SetCurrentVolatility(volatilityModel);
        }

        private SortedDictionary<DateTime, double> SetReturns()
        {
            SortedDictionary<DateTime, double> returns = new SortedDictionary<DateTime, double>();

            for(int i =0;i <Levels.Keys.Count-1;i++)
            {
                DateTime date = Levels.ElementAt(i).Key;
                returns.Add(date, Math.Log(Levels.ElementAt(i).Value) - Math.Log(Levels.ElementAt(i + 1).Value));
            }

            return returns;
        }

        public void SetCurrentVolatility(Enums.VolatilityModels volatilityModels)
        {
            switch (volatilityModels)
            {
                case Enums.VolatilityModels.EWMA:
                    SetEWMAVolatility();
                    break;

                case Enums.VolatilityModels.Garch:
                    SetGarchVolatility();
                    break;
                case Enums.VolatilityModels.Historical:
                    SetHistoricalVolatility();
                    break;
            }

            
        }

        private void SetEWMAVolatility()
        {

            SortedDictionary<DateTime, double> volatilityPath = new SortedDictionary<DateTime, double>();
            double volatility = 0;

            foreach (DateTime date in Returns.Keys)
            {
                volatility = 0.94 * volatility + 0.06 * Math.Pow(Returns[date], 2);
                volatilityPath.Add(date, volatility);

            }

            this.VolatilityPath = volatilityPath;
        }

        private void SetGarchVolatility()
        {

        }

        private void SetHistoricalVolatility()
        {
            SortedDictionary<DateTime, double> volatilityPath = new SortedDictionary<DateTime, double>();

            foreach (DateTime date in Returns.Keys)
            {
                volatilityPath.Add(date, Returns.Where(x => x.Key <= date).Sum(x => Math.Pow(x.Value, 2))/Returns.Where (x => x.Key<= date).Count());
            }

            this.VolatilityPath = volatilityPath;
        }

        public double GetStockValue(DateTime date)
        {
            return Levels.Single(x => x.Key == date).Value;
        }
        

        
    }
}
