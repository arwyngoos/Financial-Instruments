using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Statistics.Distributions.Univariate;

namespace FinancialInstruments.Utils
{
    public static class Statistics
    {
        public static List<double> GenerateStandardNormalRandomNumbers(int numberOfDraws)
        {
            NormalDistribution normalDistribution = new NormalDistribution();
            List<double> draws = new List<double>();
            for (int i = 0; i < numberOfDraws; i++)
            {
                draws.Add(normalDistribution.Generate());
            }

            return draws;
        }
    }
}
