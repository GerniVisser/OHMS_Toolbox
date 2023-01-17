using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.Models
{
    public class LunderPakalnisModel : MonteCarloModel
    {
        public float StdLength;
        public float StdWidth;
        public float StdHeight;
        public float Coeff;
        public float StdCoeff;
        public float StdKappa;
        public float C1;
        public float StdC1;
        public float C2;
        public float StdC2;
        public float Psk;
        public float StdPsk;
        public float StdUcs;
        public float StdAps;
        public float Lsf;

        public LunderPakalnisModel()
        {
            StdLength = 0f;
            StdWidth = 0f;
            StdHeight = 0f;
            Coeff = 0.4f;
            StdCoeff = 0f;
            StdKappa = 0f;
            C1 = 0.68f;
            StdC1 = 0f;
            C2 = 0.52f;
            StdC2 = 0f;
            Psk = 0.45f;
            StdPsk = 0f;
            StdUcs = 0f;
            StdAps = 0f;
            Lsf = 1f;
        }
    }
}
