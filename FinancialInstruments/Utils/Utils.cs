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

        public static List<string> getFileNames(string path)
        {
            DirectoryInfo d = new DirectoryInfo(path);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.csv"); //Getting Text files

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

            for(int i = 0;i< numberOfInts; i++)
            {
                stockWeights.Return(i) = stockWeights.ElementAt(i) / totalWeight;
            }

            return stockWeightsReturn;

        }
    }
}
