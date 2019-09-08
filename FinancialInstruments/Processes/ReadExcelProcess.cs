using FinancialInstruments.Excel;

namespace FinancialInstruments.Processes
{
    public class ReadExcelProcess : IProcess
    {
        public void Run()
        {
            DataObject.StockObservationsFromExcel = ExcelReader.ReadStockExcelFiles();
            DataObject.InterestRateObservationsFromExcel = ExcelReader.ReadInterestRateExcelFiles();
        }
    }
}
