using FinancialInstruments.Excel.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialInstruments.Processes
{
    public class PredefinedProcesses
    {

        public List<IProcess> ReadExcel => new List<IProcess>
        {
            new ReadExcelProcess(),
        };

        public List<IProcess> ReadExcelAndWriteToSql => new List<IProcess>
        {
            new ReadExcelProcess(),
            new WriteToSqlProcess()
        };

        public List<IProcess> ReadExcelAndWriteAndReadFromSql => new List<IProcess>
        {
            new ReadExcelProcess(),
            new WriteToSqlProcess(),
            new ReadExcelProcess()
        };

        public List<IProcess> ReadFromSql => new List<IProcess>
        {
            new ReadFromSqlProcess()
        };

        public List<IProcess> DownloadFromWeb => new List<IProcess>
        {
            new WebDownloadProcess()
        };
    }
}
