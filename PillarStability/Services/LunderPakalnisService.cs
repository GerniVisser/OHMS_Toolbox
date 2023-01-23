using PillarStability.DataObjects;
using PillarStability.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.Services
{
    public class LunderPakalnisService: PillarStrengthService
    {
        private PillarModel _pillarModel;
        private LunderPakalnisModel _lunderPakalnisModel;
        public LunderPakalnisService(PillarModel pillarModel) : base(pillarModel)
        {
            _pillarModel = pillarModel;
            _lunderPakalnisModel = (LunderPakalnisModel)pillarModel.PillarStrengthModel;
        }

        public override float calculatePillarStrengthAtWH(float width, float height)
        {
            float wh = width / height;

            if (wh > 3.99f)
            {
                _lunderPakalnisModel.Coeff = 0.23f + 0.017f * wh;
            }
            else
            {
                _lunderPakalnisModel.Coeff = 0.34f * MathF.Pow(MathF.Log10(wh + 0.75f), (1.4f / wh));
            }

            _lunderPakalnisModel.Kappa = MathF.Tan(MathF.Acos((1 - _lunderPakalnisModel.Coeff) / (1 + _lunderPakalnisModel.Coeff)));

            float PS = _lunderPakalnisModel.Psk * _lunderPakalnisModel.UCS * (_lunderPakalnisModel.C1 + _lunderPakalnisModel.C2 * _lunderPakalnisModel.Kappa);

            return PS;
        }

        public override PillarStrengthService generateExcelNormInvPillarStrengthService(Random random)
        {
            PillarModel res = new PillarModel(_pillarModel.Name);
            LunderPakalnisModel resL = (LunderPakalnisModel)res.PillarStrengthModel;

            LunderPakalnisModel lunderPakalnisModel = (LunderPakalnisModel)_pillarModel.PillarStrengthModel;
            // Generate deviation of values to account for hetrogenious nature of rock
            res.Height = getExcelNormInv(_pillarModel.Height, lunderPakalnisModel.StdHeight, random);
            res.Width = getExcelNormInv(_pillarModel.Width, lunderPakalnisModel.StdWidth, random);
            res.Length = getExcelNormInv(_pillarModel.Length, lunderPakalnisModel.StdLength, random);

            resL.C1 = getExcelNormInv(lunderPakalnisModel.C1, lunderPakalnisModel.StdC1, random);
            resL.C2 = getExcelNormInv(lunderPakalnisModel.C2, lunderPakalnisModel.StdC2, random);

            resL.UCS = getExcelNormInv(lunderPakalnisModel.UCS, lunderPakalnisModel.StdUcs, random);
            res.APS = getExcelNormInv(_pillarModel.APS, lunderPakalnisModel.StdAps, random);

            resL.Psk = getExcelNormInv(lunderPakalnisModel.Psk, lunderPakalnisModel.StdPsk, random);

            return new LunderPakalnisService(res);
        }
    }
}
