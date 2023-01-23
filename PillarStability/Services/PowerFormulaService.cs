using PillarStability.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.Services
{
    public class PowerFormulaService : PillarStrengthService
    {
        private PillarModel _pillarModel;
        private PowerFormulaModel _powerFormulaModel;

        public PowerFormulaService(PillarModel pillarModel) : base(pillarModel)
        {
            _pillarModel = pillarModel;
            _powerFormulaModel = (PowerFormulaModel)pillarModel.PillarStrengthModel;
        }

        public override float calculatePillarStrengthAtWH(float width, float height)
        {
            float Hb = MathF.Pow(height, _powerFormulaModel.Beta);
            float Wa = MathF.Pow(width, _powerFormulaModel.Alpha);
            float PS = _powerFormulaModel.K * (Wa / Hb);

            return PS;
        }

        public override PillarStrengthService generateExcelNormInvPillarStrengthService(Random random)
        {
            PowerFormulaModel basePFModel = (PowerFormulaModel)_pillarModel.PillarStrengthModel;
            
            PillarModel res = new PillarModel(_pillarModel.Name);
            res.PillarStrengthModel = new PowerFormulaModel(basePFModel.Alpha, basePFModel.Beta);
            PowerFormulaModel resL = (PowerFormulaModel)res.PillarStrengthModel;

            // Generate Random values within sample size
            res.Height = getExcelNormInv(_pillarModel.Height, basePFModel.StdHeight, random);
            res.Width = getExcelNormInv(_pillarModel.Width, basePFModel.StdWidth, random);
            res.Length = getExcelNormInv(_pillarModel.Length, basePFModel.StdLength, random);

            resL.K = getExcelNormInv(basePFModel.K, basePFModel.StdK, random);

            return new PowerFormulaService(res);
        }
    }
}
