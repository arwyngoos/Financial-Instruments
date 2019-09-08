using System.Collections.Generic;
using System.Linq;
using Accord.Statistics.Distributions.Univariate;

namespace FinancialInstruments.Utilities
{
    public static class Statistics
    {
        public static List<double> GenerateStandardNormalRandomNumbers(int numberOfDraws)
        {
            NormalDistribution normalDistribution = new NormalDistribution();

            return normalDistribution.Generate(numberOfDraws).ToList();
        }
    }
}
