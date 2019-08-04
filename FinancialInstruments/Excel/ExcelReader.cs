using FinancialInstruments.FinancialProducts;
using FinancialInstruments.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialInstruments.Utilities;
using Excel = Microsoft.Office.Interop.Excel;

namespace FinancialInstruments.ExcelTools
{
    public static class ExcelReader
    {

        public static SortedDictionary<string, SortedDictionary<DateTime, StockObservation>> ReadExcelFiles()
        {
            string folder = Settings.DataDirectory;

            if (Settings.InputDataType != Enums.InputDataType.CSV)
            {
                throw  new Exception($"The inputdate type {Settings.InputDataType.ToString()} is not csv, but the excel reader is called.");
            }

            List<string> fileNames = Utilities.Utils.GetFileNames(folder, Settings.InputDataType);
            SortedDictionary<string, SortedDictionary<DateTime, StockObservation>> instrumentsObservations = new SortedDictionary<string, SortedDictionary<DateTime, StockObservation>>();

            foreach(string fileName in fileNames)
            {

                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(folder + "//" + fileName);
                Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;


                //RemoveNullObservations(xlRange);

                int rowCount = xlRange.Rows.Count;

                SortedDictionary<DateTime, StockObservation> currentObservation = new SortedDictionary<DateTime, StockObservation>();

                for (int i = 2; i < rowCount; i++)
                {
                    DateTime date = DateTime.FromOADate((double) xlWorksheet.Range["A" + i.ToString()].Value2);
                    double open = xlWorksheet.Range["B" + i.ToString()].Value2;
                    double high = xlWorksheet.Range["C" + i.ToString()].Value2;
                    double low = xlWorksheet.Range["D" + i.ToString()].Value2;
                    double close = xlWorksheet.Range["E" + i.ToString()].Value2;
                    int volume = Convert.ToInt32(xlWorksheet.Range["F" + i.ToString()].Value2);


                    currentObservation.Add(date, new StockObservation(date, open, high, low, close, volume));
                }

                instrumentsObservations.Add(fileName.Replace(".csv", ""), currentObservation);

                xlApp.Quit();
                xlWorkbook.Close();

            }
            

            return instrumentsObservations;
        }
    }
}
