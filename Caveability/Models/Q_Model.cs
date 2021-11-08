using Caveability.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.Models
{
    public class Q_Model : ICalculator
    {
        public double _rqd { get; set; }
        public double _jn { get; set; }
        public double _jr { get; set; }
        public double _ja { get; set; }

        public Q_Model(float rQD, float jn, float jr, float ja)
        {
            _rqd = rQD;
            _jn = jn;
            _jr = jr;
            _ja = ja;
        }

        public double Calculate()
        {
            double N = (_rqd / _jn) * (_jr / _ja);
            return N;
        }

        public double CalculateXAxis()
        {
            throw new NotImplementedException();
        }

        public List<Coord> GetGraphCoords()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "";
        }
    }
}
