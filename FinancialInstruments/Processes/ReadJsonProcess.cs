using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialInstruments.Excel.Processes;
using FinancialInstruments.Json;

namespace FinancialInstruments.Processes
{
    public class ReadJsonProcess : IProcess
    {
        public void Run()
        {
            DataObject.ReadJsonProcessOutput = JsonReader.ReadJsonFiles();
        }
    }
}
