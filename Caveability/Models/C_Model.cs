using Caveability.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.Models
{
    public class C_Model : ICalculator
    {
        public float _c { get; set; }

        public C_Model(float c)
        {
            _c = c;
        }

        public float Calculate()
        {
            float C = graphCalcutation(_c);
            return C;
        }

        public float CalculateXAxis()
        {
            return _c;
        }

        // Background Graph to be drwn
        public List<Coord> GetGraphCoords()
        {
            int range = 90;
            float interval = 0.5f;

            float numPoints = range / interval;

            List<Coord> graphPoints = new List<Coord>();

            for (int i = 0; i < numPoints; i++)
            {
                graphPoints.Add(new Coord { x = i * interval, y = graphCalcutation(i * interval) });
            }

            return graphPoints;
        }

        // Takes X val and returns Y val
        public virtual float graphCalcutation(double point)
        {
            if (point >= 0 && point < 20)
            {
                return 4;
            }
            else if (point >= 20 && point < 50)
            {
                float single = 0.0333f;
                float constant = 3.3333f;

                float result = (float)(single * point + constant);

                return result;
            }
            else if (point >= 50 && point < 90)
            {
                float single = 0.075f;
                float constant = 1.25f;

                float result = (float)(single * point + constant);

                return result;
            }
            else
            {
                return 8;
            }
        }

        public override string ToString()
        {
            return "";
        }
    }
}
