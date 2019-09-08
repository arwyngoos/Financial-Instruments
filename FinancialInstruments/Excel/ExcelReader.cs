using System;
using System.Collections.Generic;
using FinancialInstruments.FinancialProducts;
using FinancialInstruments.Utilities;

namespace FinancialInstruments.Excel
{
    public static class ExcelReader
    {

        public static SortedDictionary<string, SortedDictionary<DateTime, StockObservation>> ReadStockExcelFiles()
        {
            string folder = Settings.StockDataDirectory;

            if (Settings.InputDataType != Enums.InputDataType.Csv)
            {
                throw  new Exception($"The inputdate type {Settings.InputDataType.ToString()} is not csv, but the excel reader is called.");
            }

            List<string> fileNames = Utils.GetFileNames(folder, Settings.InputDataType);

            SortedDictionary<string, SortedDictionary<DateTime, StockObservation>> instrumentsObservations = new SortedDictionary<string, SortedDictionary<DateTime, StockObservation>>();

            foreach(string fileName in fileNames)
            {

                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(folder + "//" + fileName);
                Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;


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

        public static SortedDictionary<string, SortedDictionary<DateTime, double?>> ReadInterestRateExcelFiles()
        {
            string folder = Settings.InterestRateDataDirectory;

            if (Settings.InputDataType != Enums.InputDataType.Csv)
            {
                throw new Exception($"The inputdate type {Settings.InputDataType.ToString()} is not csv, but the excel reader is called.");
            }

            List<string> fileNames = Utils.GetFileNames(folder, Settings.InputDataType);

            SortedDictionary<string, SortedDictionary<DateTime, double?>> instrumentsObservations = new SortedDictionary<string, SortedDictionary<DateTime, double?>>();

            foreach (string fileName in fileNames)
            {

                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(folder + "//" + fileName);
                Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;


                int rowCount = xlRange.Rows.Count;

                SortedDictionary<DateTime, double?> currentInstrumentObservations = new SortedDictionary<DateTime, double?>();
                for (int i = 2; i < rowCount; i++)
                {
                    DateTime date = DateTime.FromOADate((double)xlWorksheet.Range["A" + i.ToString()].Value2);

                    double? rate;
                    if(xlWorksheet.Range["B" + i].Value2 is string
                       && xlWorksheet.Range["B" + i].Value2 == ".")
                    {
                        rate = null;
                    }
                    else
                    {
                        rate = xlWorksheet.Range["B" + i].Value2;
                    }

                    currentInstrumentObservations.Add(date, rate);
                }

                instrumentsObservations.Add(fileName.Replace(".csv", ""), currentInstrumentObservations);

                xlApp.Quit();
                xlWorkbook.Close();
            }

            return instrumentsObservations;
        }
    }
}
