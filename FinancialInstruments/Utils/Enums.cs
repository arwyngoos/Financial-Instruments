using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialInstruments.Utils
{
    public static class Enums
    {
        public enum VolatilityModels
        {
            Garch,
            EWMA,
            Historical
        }

        public enum OptionType
        {
            Call, 
            Put,
            Custom
        }

        public enum InputDataType
        {
            CSV,
            JSON
        }

        public enum OutputSize
        {
            Compact, 
            Full
        }
    }
}
