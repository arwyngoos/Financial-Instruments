using FinancialInstruments.Excel.Processes;
using FinancialInstruments.SQL;
using FinancialInstruments.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
