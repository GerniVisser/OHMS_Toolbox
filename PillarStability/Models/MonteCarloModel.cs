using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.Models
{
    public class MonteCarloModel
    {
        public int Iterations;
        public int Bins;
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

        public MonteCarloModel()
        {
            Iterations = 5000;
            Bins = 50;
            StdLength = 0.5f;
            StdWidth = 0.5f;
            StdHeight = 0.5f;
            Coeff = 0.4f;
            StdCoeff = 0.06f;
            StdKappa = 0;
            C1 = 0.68f;
            StdC1 = 0.068f;
            C2 = 0.52f;
            StdC2 = 0.052f;
            Psk = 0.45f;
            StdPsk = 0.05f;
            StdUcs = 0;
            StdAps = 0;
            Lsf = 1f;
        }
    }
}
