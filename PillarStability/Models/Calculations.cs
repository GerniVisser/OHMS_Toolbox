using PillarStability.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.Models
{
    public static class Calculations
    {
        public static OutputGridObject calculate(PillarModel pillar)
        {
            var i = new OutputGridObject() {
                Width = pillar.Weff,
                WidthtHeight = pillar.Wth,
                Pillar = pillar.Name,
                AveStress = pillar.APStUCS
            };

            return i;
        }

        public static MCGridObject calculateMC(PillarModel pillar, int iterations)
        {
            List<float> fosList = new List<float>();
            Random random = new Random();

            for (int i = 0; i < iterations; i++)
            {

                float H = MCHelperCalc.getExcelNormInv((float)(random.NextDouble()), pillar.Height, pillar.stdHeight);
                float W = MCHelperCalc.getExcelNormInv((float)(random.NextDouble()), pillar.Width, pillar.stdWidth);
                float L = MCHelperCalc.getExcelNormInv((float)(random.NextDouble()), pillar.Length, pillar.stdLength);

                float COEFF = MCHelperCalc.getExcelNormInv((float)(random.NextDouble()), pillar.Coeff, pillar.stdCoeff);

                float area = W * L;
                float area4 = area * 4;
                float perimeter = (2 * W) + (2 * L);
                float WEFF = area4 / perimeter;

                float WTH = WEFF / H;

                float APC = 0.0f;

                if (WTH > 3.99)
                {
                    APC = 0.23f + 0.017f * WTH;
                }
                else
                {
                    APC = (float)(0.40f * Math.Pow(Math.Log10(WTH + 0.75f), (1.4f / WTH)));
                }

                float K = (float)(Math.Tan(Math.Acos((1 - APC) / (1 + APC))));

                float C1 = MCHelperCalc.getExcelNormInv((float)(random.NextDouble()), pillar.C1, pillar.stdC1);
                float C2 = MCHelperCalc.getExcelNormInv((float)(random.NextDouble()), pillar.C2, pillar.stdC2);

                float UCS = MCHelperCalc.getExcelNormInv((float)(random.NextDouble()), pillar.UCS, pillar.stdUcs);
                float APS = MCHelperCalc.getExcelNormInv((float)(random.NextDouble()), pillar.APS, pillar.stdAps);

                float PSK = MCHelperCalc.getExcelNormInv((float)(random.NextDouble()), pillar.Psk, pillar.stdPsk);

                float PS = PSK * UCS * (C1 + C2 * K);

                float FOS = PS / APS;

                if (!float.IsNaN(FOS))
                {
                    fosList.Add(FOS);
                }
            }

            float dsf = float.NaN;
            float asf = float.NaN;
            float stdevsf = float.NaN;

            float mfFOS = float.NaN;
            float plFOS = float.NaN;

            if(fosList.Count > 0)
            {
                float K = MathF.Tan(MathF.Acos((1 - pillar.APC) / (1 + pillar.APC)));
                dsf = ((pillar.Psk * pillar.UCS) * (pillar.C1 + pillar.C2 * K)) / pillar.APS;
                stdevsf = MCHelperCalc.calcStandardDev(fosList);
                asf = fosList.Average();

                Bins bins = new Bins(fosList, 1000);
                mfFOS = MathF.Round(bins.getMostFrequentBin().Min, 2);
                plFOS = bins.GetLimitFOS(pillar.Lsf);
            }

            MCGridObject gridObject = new MCGridObject()
            {
                Pillar = pillar.Name,
                DSF = dsf,
                AveSF = asf,
                StandardDev = stdevsf,
                mfSF = mfFOS,
                probSF = plFOS
            };

            return gridObject;
        }

    }
}
