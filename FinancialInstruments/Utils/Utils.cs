using FinancialInstruments.FinancialProducts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialInstruments.Utils
{
    public static class Utils
    {

        public static List<string> GetFileNames(string path, Enums.InputDataType inputDataType)
        {
            DirectoryInfo d = new DirectoryInfo(path);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles($"*.{inputDataType.ToString().ToLower()}"); //Getting Text files

            List<string> fileNames = new List<string>();

            foreach (FileInfo file in Files)
            {
                fileNames.Add(file.Name);
            }

            return fileNames;
        }

        public static List<double> SetRandomIntegers(int numberOfInts)
        {
            List<double> stockWeights = new List<double>();
            List<double> stockWeightsReturn = new List<double>();

            Random random = new Random();

            for(int i=0;i<numberOfInts; i++)
            {
                stockWeights.Add(random.Next());
            }

            double totalWeight = stockWeights.Sum(x => x);

            //for(int i = 0;i< numberOfInts; i++)
            //{
            //    stockWeights.Return(i) = stockWeights.ElementAt(i) / totalWeight;
            //}

            return stockWeightsReturn;
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
