using FinancialInstruments.SQL;
using System;

namespace FinancialInstruments.Processes
{
    public class WriteToSqlProcess : IProcess
    {
        public void Run()
        {
            SqlWriter.WriteStockDataToDatabase(DataObject.StockObservationsFromExcel ?? DataObject.ReadJsonProcessOutput);
            SqlWriter.WriteInterestRateDataToDatabase(DataObject.InterestRateObservationsFromExcel);
        }
    }
}
