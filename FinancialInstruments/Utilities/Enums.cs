namespace FinancialInstruments.Utilities
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
