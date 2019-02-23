using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace FinancialInstruments.ExcelTools
{
    public static class ExcelReader
    {

        public static SortedDictionary<string, SortedDictionary<DateTime, double>> readExcelFiles(List<string> fileNames, string folder)
        {

            SortedDictionary<string, SortedDictionary<DateTime, double>> instrumentsObservations = new SortedDictionary<string, SortedDictionary<DateTime, double>>();

            foreach(string fileName in fileNames)
            {
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(folder +"//"+fileName);
                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Excel.Range xlRange = xlWorksheet.UsedRange;


                //RemoveNullObservations(xlRange);

                int rowCount = xlRange.Rows.Count;

                SortedDictionary<DateTime, double> currentObservation = new SortedDictionary<DateTime, double>();

                for (int i = 2; i < Math.Min(200,rowCount); i++)
                {
                    DateTime date = DateTime.FromOADate((double) xlWorksheet.Range["A" + i.ToString()].Value2);
                    double value = xlWorksheet.Range["B" + i.ToString()].Value2;

                    currentObservation.Add(date, value);
                }

                instrumentsObservations.Add(fileName.Replace(".csv", ""), currentObservation);
            }

            return instrumentsObservations;
        }

        public static void RemoveNullObservations(Excel.Range xlRange)
        {

        }
    }
}
