using FinancialInstruments.SQL;
using FinancialInstruments.Utilities;

namespace FinancialInstruments.Processes
{
    public class ReadFromSqlProcess : IProcess
    {
        public void Run()
        {
            DataObject.ReadStockDataFromSqlOutput = SqlReader
                .ReadStockDataFromDataBase()
                .ParseStockDataTableToSortedDictionary();

            DataObject.ReadInterestRateDataFromSqlOutput = SqlReader
                .ReadInterestRateDataFromDataBase()
                .ParseInterestRateDataTableToSortedDictionary();
        }
    }
}
