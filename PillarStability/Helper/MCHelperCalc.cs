using Meta.Numerics.Statistics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PillarStability.Helper
{
    public class MCHelperCalc
    {
        public static float getExcelNormInv(float random, float mean, float stdev)
        {
            float normInv = 0.0f;

            if (stdev == 0.0f)
            {
                return mean;
            }
            else
            {
                try
                {
                    NormalDistribution n = new NormalDistribution(mean, stdev);
                    normInv = (float)(n.InverseLeftProbability(random));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                return normInv;
            }
        }

        public static float calcStandardDev(List<float> list)
        {
            float ave = list.Average();
            float sumOfSquaresOfDiff = list.Select(val => (val - ave) * (val - ave)).Sum();
            float sd = (float)(Math.Sqrt(sumOfSquaresOfDiff / list.Count));

            return sd;
        }

        public static float getMostFrequentBin(List<float> list)
        {
            float ave = list.Average();
            float sumOfSquaresOfDiff = list.Select(val => (val - ave) * (val - ave)).Sum();
            float sd = (float)(Math.Sqrt(sumOfSquaresOfDiff / list.Count));

            return sd;
        }
    }
}
