using FinancialInstruments.Excel.Processes;
using FinancialInstruments.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialInstruments.Processes
{
    public class WriteToSqlProcess : IProcess
    {
        public void Run()
        {
            SqlConnector sqlConnector = new SqlConnector();
            sqlConnector.WriteToDatabase(DataObject.ReadExcelProcessOutput);
        }
    }
}
