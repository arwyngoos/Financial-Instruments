using FinancialInstruments.ExcelTools;
using FinancialInstruments.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialInstruments.Excel.Processes
{
    public class ReadExcelProcess : IProcess
    {
        public void Run()
        {
            DataObject.ReadExcelProcessOutput = ExcelReader.ReadExcelFiles();
        }
    }
}
