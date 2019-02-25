﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialInstruments.Utils;

namespace FinancialInstruments.FinancialProducts
{
    public class Option
    {
        public double Strike { get; set; }

        public Stock Stock { get; set; }

        public double RiskFreeRate { get; set; }

        public Enums.OptionType OptionType { get; set; }

        public DateTime Maturity { get; set; }

        public DateTime ValuationDate { get; set; }



        public Option(Stock stock, DateTime valuationDate, DateTime maturity, double strike = 100, double riskFreeRate = 2, Enums.OptionType optionType=Enums.OptionType.Call)
        {
            this.Strike = strike;
            this.Stock = stock;
            this.RiskFreeRate = riskFreeRate;
            this.OptionType = optionType;
            this.ValuationDate = valuationDate;
            this.Maturity = maturity;
        }

        public double GetOptionValue(DateTime date)
        {
            double value = 0;

            switch (OptionType)
            {
                case Enums.OptionType.Call:
                    value = SetCallValue(date);
                    break;

                case Enums.OptionType.Put:
                    value =SetPutValue(date);
                    break;
            }

            return value;
        }

        public double SetCallValue(DateTime date)
        {
            double d1 = 1 / (Stock.CurrentVolatility * Math.Sqrt(Maturity.Ticks - ValuationDate.Ticks));


            return d1;
        }

        public double SetPutValue(DateTime date)
        {
            double d1 = 1 / (Stock.CurrentVolatility * Math.Sqrt(Maturity.Ticks - ValuationDate.Ticks));

            return d1;

        }
        
    }
}
