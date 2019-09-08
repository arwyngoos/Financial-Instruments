using System.Collections.Generic;

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
            new ReadFromSqlProcess()
        };

        public static List<IProcess> ReadFromSql => new List<IProcess>
        {
            new ReadFromSqlProcess()
        };

        public static List<IProcess> DownloadFromWeb => new List<IProcess>
        {
            new WebDownloadProcess()
        };

        public static List<IProcess> DownloadFromWebAndWriteToAndReadFromSql => new List<IProcess>
        {
            new WebDownloadProcess(),
            new ReadExcelProcess(),
            new WriteToSqlProcess(),
            new ReadExcelProcess()
        };

        public static List<IProcess> DownloadFromWebAndReadJson => new List<IProcess>
        {
            new WebDownloadProcess(),
            new ReadJsonProcess()
        };

        public static List<IProcess> DownloadFromWebReadJsonAndWriteToSql => new List<IProcess>
        {
            new WebDownloadProcess(),
            new ReadJsonProcess(),
            new WriteToSqlProcess()
        };

        public static List<IProcess> ReadFromSqlAndCreateFinancialInstruments => new List<IProcess>
        {
            new ReadFromSqlProcess(),
            new CreateFinancialInstrumentsProcess()
        };
    }
}
