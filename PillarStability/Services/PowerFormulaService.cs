using PillarStability.DataObjects;
using PillarStability.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.Services
{
    public class PowerFormulaService : FoSAlgoritm
    {
        public float Calculate(Func<float, float, float> getExcelNormInv, PillarModel pillarModel)
        {
            PowerFormulaModel powerFormulaModel = (PowerFormulaModel)pillarModel.MonteCarloModel;

            float H = getExcelNormInv(pillarModel.Height, powerFormulaModel.StdHeight);
            float W = getExcelNormInv(pillarModel.Width, powerFormulaModel.StdWidth);

            float K = getExcelNormInv(powerFormulaModel.K, powerFormulaModel.StdK);

            float PS = K * MathF.Pow(W, powerFormulaModel.Alpha) / MathF.Pow(H, powerFormulaModel.Beta);

            float FOS = PS / pillarModel.APS;

            return FOS;
        }

        public MonteCarloDataObject GenerateSummaryObject(PillarModel pillarModel, List<float> fosList)
        {
            return null;
        }
    }
}
