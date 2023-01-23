using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.Models
{
    public class MonteCarloModel
    {
        public int Iterations;
        public int Bins;
        public List<float> StrengthList;
        public List<float> FosList;

        public MonteCarloModel()
        {
            Iterations = 5000;
            Bins = 50;
            StrengthList = new List<float>();
            FosList = new List<float>();
        }
    }
}
