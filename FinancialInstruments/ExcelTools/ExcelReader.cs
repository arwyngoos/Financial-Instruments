using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace FinancialInstruments.ExcelTools
{
    public class ExcelReader
    {

        public SortedDictionary<string, SortedDictionary<DateTime, double>> readExcelFiles(List<string> fileNames, string folder)
        {

            SortedDictionary<string, SortedDictionary<DateTime, double>> instrumentsObservations = new SortedDictionary<string, SortedDictionary<DateTime, double>>();

            foreach(string fileName in fileNames)
            {
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(folder +"//"+fileName);
                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Excel.Range xlRange = xlWorksheet.UsedRange;


            }


            return instrumentsObservations;
        }
    }
}
