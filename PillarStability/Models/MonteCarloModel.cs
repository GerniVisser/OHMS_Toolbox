using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.Models
{
    public abstract class MonteCarloModel
    {
        public int Iterations;
        public int Bins;

        protected MonteCarloModel()
        {
            Iterations = 5000;
            Bins = 50;
        }
    }
}
