using FinancialInstruments.SQL;
using FinancialInstruments.Utilities;

namespace FinancialInstruments.Processes
{
    public class ReadFromSqlProcess : IProcess
    {
        public void Run()
        {
            SqlConnector sqlConnector = new SqlConnector();
            DataObject.ReadFromSqlProcessOutput = sqlConnector.ReadFromDataBase().ParseDataTableToSortedDictionary();
        }
    }
}
