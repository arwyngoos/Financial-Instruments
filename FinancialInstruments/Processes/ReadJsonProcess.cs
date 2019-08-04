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
