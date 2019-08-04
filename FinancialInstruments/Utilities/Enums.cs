namespace FinancialInstruments.Utilities
{
    public static class Enums
    {
        public enum VolatilityModels
        {
            Garch,
            Ewma,
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
            Csv,
            Json
        }

        public enum OutputSize
        {
            Compact, 
            Full
        }
    }
}
