using FinancialInstruments.Excel;

namespace FinancialInstruments.Processes
{
    public class ReadExcelProcess : IProcess
    {
        public void Run()
        {
            DataObject.ReadExcelProcessOutput = ExcelReader.ReadExcelFiles();
        }
    }
}
