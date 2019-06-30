using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialInstruments.FinancialProducts
{
    public class StockObservation
    {
        public DateTime Date { get; private set; }

        public double Open { get; private set; }

        public double High{ get; private set; }

        public double Low { get; private set; }

        public double Close { get; private set; }

        public int Volume { get; private set; }


        public StockObservation(DateTime date, double open, double high, double low, double close, int volume)
        {
            Date = date;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
        }
    }
}
