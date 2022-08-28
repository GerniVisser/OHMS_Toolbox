using PillarStability.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.Services
{
    public class LunderPakalnisService : FoSAlgoritm
    {
        public float Calculate(Func<float, float, float> getExcelNormInv, PillarModel pillarModel, MonteCarloModel monteCarloModel)
        {
            // Generate Random values within sample size
            float H = getExcelNormInv(pillarModel.Height, monteCarloModel.StdHeight);
            float W = getExcelNormInv(pillarModel.Width, monteCarloModel.StdWidth);
            float L = getExcelNormInv(pillarModel.Length, monteCarloModel.StdLength);

            //This C value Im unsure about wheteher it should be a constant 
            // If so it does not make sence to simulate
            float C1 = getExcelNormInv(monteCarloModel.C1, monteCarloModel.StdC1);
            float C2 = getExcelNormInv(monteCarloModel.C2, monteCarloModel.StdC2);

            float UCS = getExcelNormInv(pillarModel.UCS, monteCarloModel.StdUcs);
            float APS = getExcelNormInv(pillarModel.APS, monteCarloModel.StdAps);

            float PSK = getExcelNormInv(monteCarloModel.Psk, monteCarloModel.StdPsk);

            float area4 = W * L * 4;
            float perimeter = (2 * W) + (2 * L);
            float WEFF = area4 / perimeter;

            float WTH = WEFF / H;

            float APC;

            if (WTH > 3.99)
            {
                APC = 0.23f + 0.017f * WTH;
            }
            else
            {
                APC = 0.40f * MathF.Pow(MathF.Log10(WTH + 0.75f), (1.4f / WTH));
            }

            float K = MathF.Tan(MathF.Acos((1 - APC) / (1 + APC)));

            float PS = PSK * UCS * (C1 + C2 * K);

            float FOS = PS / APS;

            return FOS;

        }
    }
}
