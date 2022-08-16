using PillarStability.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.Services
{
    public class PillarDataService
    {
        private PillarModel _pillarModel;

        public PillarDataService(PillarModel pillarModel)
        {
            _pillarModel = pillarModel;
        }

        private float calcAPC()
        {
            float apc;
            if (Wth > 3.99)
            {
                apc = 0.23f + 0.017f * Wth;
            }
            else
            {
                apc = (float)(0.34f * Math.Pow(Math.Log10(Wth + 0.75f), (1.4f / Wth)));
            }
            return apc;
        }
        public float Weff
        {
            get { return 4 * (_pillarModel.Length * _pillarModel.Width) / (2 * (_pillarModel.Length + _pillarModel.Width)); }
        }

        public float Wth
        {
            get { return Weff / _pillarModel.Height; }
        }

        public float APStUCS
        {
            get { return _pillarModel.APS / _pillarModel.UCS; }
        }

        public float APC
        {
            get { return calcAPC(); }
        }
    }
}
