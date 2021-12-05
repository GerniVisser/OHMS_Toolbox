using Caveability.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.Models
{
    public class B_Model : ICalculator
    {
        public float _b { get; set; }


        public B_Model(float b)
        {
            _b = b;
        }

        public float Calculate()
        {
            float B = graphCalcutation(_b);
            return B;
        }

        public float CalculateXAxis()
        {
            return _b;
        }


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

        private float graphCalcutation(float point)
        {
            if (point >= 0 && point < 20)
            {
                return 0.4f;
            }
            else if (point >= 20 && point < 40)
            {
                float single = 0.005f;
                float constant = 0.3f;

                float result = single * point + constant;

                return result;
            }
            else if (point >= 40 && point < 65)
            {
                float single = 0.02f;
                float constant = -0.3f;

                float result = single * point + constant;

                return result;
            }
            else
            {
                return 0.99f;
            }
        }

        public override string ToString()
        {
            return "";
        }
    }
}
