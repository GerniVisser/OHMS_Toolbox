using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.Models
{
    public class LunderPakalnisModel : PillarStrengthModel
    {
        public float UCS;
        public float StdLength;
        public float StdWidth;
        public float StdHeight;
        public float Coeff;
        public float Kappa;
        public float C1;
        public float StdC1;
        public float C2;
        public float StdC2;
        public float Psk;
        public float StdPsk;
        public float StdUcs;
        public float StdAps;

        public LunderPakalnisModel()
        {
            UCS = 111.42f;
            StdLength = 0.5f;
            StdWidth = 0.5f;
            StdHeight = 0.5f;
            Kappa = 0f;
            C1 = 0.68f;
            StdC1 = 0.1f;
            C2 = 0.52f;
            StdC2 = 0.1f;
            Psk = 0.44f;
            StdPsk = 0.1f;
            StdUcs = 0.5f;
            StdAps = 0.5f; 
        }
    }
}
