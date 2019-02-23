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
    }
}
