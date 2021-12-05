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
        public float _rqd { get; set; }
        public float _jn { get; set; }
        public float _jr { get; set; }
        public float _ja { get; set; }

        public Q_Model(float rQD, float jn, float jr, float ja)
        {
            _rqd = rQD;
            _jn = jn;
            _jr = jr;
            _ja = ja;
        }

        public float Calculate()
        {
            float N = (_rqd / _jn) * (_jr / _ja);
            return N;
        }

        public float CalculateXAxis()
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
