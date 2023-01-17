using PillarStability.DataObjects;
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
        public float Calculate(Func<float, float, float> getExcelNormInv, PillarModel pillarModel)
        {
            LunderPakalnisModel lunderPakalnisModel  = (LunderPakalnisModel)pillarModel.MonteCarloModel;
            // Generate Random values within sample size
            float H = getExcelNormInv(pillarModel.Height, lunderPakalnisModel.StdHeight);
            float W = getExcelNormInv(pillarModel.Width, lunderPakalnisModel.StdWidth);
            float L = getExcelNormInv(pillarModel.Length, lunderPakalnisModel.StdLength);

            //This C value Im unsure about wheteher it should be a constant 
            // If so it does not make sence to simulate
            float C1 = getExcelNormInv(lunderPakalnisModel.C1, lunderPakalnisModel.StdC1);
            float C2 = getExcelNormInv(lunderPakalnisModel.C2, lunderPakalnisModel.StdC2);

            float UCS = getExcelNormInv(lunderPakalnisModel.UCS, lunderPakalnisModel.StdUcs);
            float APS = getExcelNormInv(pillarModel.APS, lunderPakalnisModel.StdAps);

            float PSK = getExcelNormInv(lunderPakalnisModel.Psk, lunderPakalnisModel.StdPsk);

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
                APC = 0.46f * MathF.Pow(MathF.Log10(WTH + 0.75f), (1.4f / WTH));
            }

            float K = MathF.Tan(MathF.Acos((1 - APC) / (1 + APC)));

            float PS = PSK * UCS * (C1 + C2 * K);

            float FOS = PS / APS;

            return FOS;

        }

        public MonteCarloDataObject GenerateSummaryObject(PillarModel pillarModel, List<float> fosList)
        {
            PillarDataService pillarDataService = new Wh_Service(pillarModel);
            LunderPakalnisModel lunderPakalnisModel = (LunderPakalnisModel)pillarModel.MonteCarloModel;
            BinsService binsService = new BinsService(fosList, lunderPakalnisModel.Bins);

            float K = MathF.Tan(MathF.Acos((1 - pillarDataService.APC) / (1 + pillarDataService.APC)));
            float dsf = lunderPakalnisModel.Psk * lunderPakalnisModel.UCS * (lunderPakalnisModel.C1 + lunderPakalnisModel.C2 * K) / pillarModel.APS;

            return new MonteCarloDataObject()
            {
                Pillar = pillarModel.Name,
                DSF = dsf,
                AveSF = fosList.Average(),
                StandardDev = getStandardDev(fosList),
                mfSF = MathF.Round(binsService.getMostFrequentBin().Min, 2),
                probSF = binsService.getPercentageOfBinsBelowLimit(lunderPakalnisModel.Lsf)
            };
        }

        public float getStandardDev(List<float> fosList)
        {
            float ave = fosList.Average();
            float sumOfSquaresOfDiff = fosList.Select(val => (val - ave) * (val - ave)).Sum();
            float sd = MathF.Sqrt(sumOfSquaresOfDiff / fosList.Count);

            return sd;
        }
    }
}
