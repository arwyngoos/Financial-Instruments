using FinancialInstruments.SQL;
using System;

namespace FinancialInstruments.Processes
{
    public class WriteToSqlProcess : IProcess
    {
        public void Run()
        {
            SqlConnector sqlConnector = new SqlConnector();

            if (DataObject.ReadJsonProcessOutput == null && DataObject.ReadExcelProcessOutput == null)
            {
                throw new Exception("Failed: both the excel and json process output is null.");
            }

            if (DataObject.ReadJsonProcessOutput != null && DataObject.ReadExcelProcessOutput != null)
            {
                throw new Exception("Failed: both the excel and json process output is filled. Unclear which to pick");
            }

            sqlConnector.WriteToDatabase(DataObject.ReadExcelProcessOutput ?? DataObject.ReadJsonProcessOutput);
        }
    }
}
