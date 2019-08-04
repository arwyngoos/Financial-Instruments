using System;

namespace FinancialInstruments.FinancialProducts
{
    public class StockObservation
    {
        public DateTime Date { get; }

        public double Open { get; }

        public double High{ get; }

        public double Low { get; }

        public double Close { get; }

        public int Volume { get; }


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
