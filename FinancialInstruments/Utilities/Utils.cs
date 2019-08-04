using System;
using System.Collections.Generic;
using System.IO;

namespace FinancialInstruments.Utilities
{
    public static class Utils
    {
        public static List<string> GetFileNames(string path, Enums.InputDataType inputDataType)
        {
            DirectoryInfo d = new DirectoryInfo(path);//Assuming Test is your Folder
            FileInfo[] files = d.GetFiles($"*.{inputDataType.ToString().ToLower()}"); //Getting Text files

            List<string> fileNames = new List<string>();

            foreach (FileInfo file in files)
            {
                fileNames.Add(file.Name);
            }

            return fileNames;
        }

        public static List<DateTime> CreateDailyDateTimeGrid(DateTime startDate, DateTime endDate)
        {
            if (!(startDate < endDate))
            {
                throw new Exception($"Failed: the startDate {startDate} should be before the endDate {endDate}");
            }

            List<DateTime> dates = new List<DateTime>();
            DateTime date = startDate;

            while (date <= endDate)
            {
                dates.Add(date);
                date = date.AddDays(1);
            }

            return dates;
        }
    }
}
