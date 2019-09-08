using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialInstruments.FinancialProducts.InterestRates
{
    public class LiborRateObservation
    {
        public DateTime Date;

        public double Rate;

        public LiborRateObservation (DateTime date, double rate)
        {
            Date = date;
            Rate = rate;
        }
    }
}
