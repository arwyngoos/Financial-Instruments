using FinancialInstruments.Excel.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialInstruments.Processes
{
    public static class PredefinedProcesses
    {

        public static List<IProcess> ReadExcel => new List<IProcess>
        {
            new ReadExcelProcess(),
        };

        public static List<IProcess> ReadExcelAndWriteToSql => new List<IProcess>
        {
            new ReadExcelProcess(),
            new WriteToSqlProcess()
        };

        public static List<IProcess> ReadExcelAndWriteAndReadFromSql => new List<IProcess>
        {
            new ReadExcelProcess(),
            new WriteToSqlProcess(),
            new ReadExcelProcess()
        };

        public static List<IProcess> ReadFromSql => new List<IProcess>
        {
            new ReadFromSqlProcess()
        };

        public static List<IProcess> DownloadFromWeb => new List<IProcess>
        {
            new WebDownloadProcess()
        };

        public static List<IProcess> DownloadFromWebAndWriteToAndReadFromSQL => new List<IProcess>
        {
            new WebDownloadProcess(),
            new ReadExcelProcess(),
            new WriteToSqlProcess(),
            new ReadExcelProcess()
        };
    }
}
