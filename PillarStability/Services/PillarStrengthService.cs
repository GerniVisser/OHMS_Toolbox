using Meta.Numerics.Statistics.Distributions;
using PillarStability.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace PillarStability.Services
{
    public abstract class PillarStrengthService
    {
        private PillarModel _pillarModel;
        private float _strength;

        protected PillarStrengthService(PillarModel pillarModel)
        {
            _pillarModel = pillarModel;
        }

        public abstract float calculatePillarStrengthAtWH(float width, float height);

        public abstract PillarStrengthService generateExcelNormInvPillarStrengthService(Random random);

        public float calculateCurrentStrength()
        {
            _strength = calculatePillarStrengthAtWH(_pillarModel.Width, _pillarModel.Height);
            return _strength;
        }

        public float PillarStrength
        {
            get { return _strength; }
        }

        public float PillarFos
        {
            get { return _strength / _pillarModel.APS; }
        }

        protected float getExcelNormInv(float mean, float stdev, Random random)
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
                    normInv = (float)n.InverseLeftProbability(random.NextDouble());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                return normInv;
            }
        }

    }
}
