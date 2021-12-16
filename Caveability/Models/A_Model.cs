using Caveability.Helper;
using System;
using System.Collections.Generic;

namespace Caveability.Models
{
    public class A_Model : ICalculator
    {

        public float _oc { get; set; }
        public float _omax { get; set; }


        public A_Model(float oc, float omax)
        {
            _oc = oc;
            _omax = omax;
        }

        public float Calculate()
        {
            float A = graphCalcutation(_oc / _omax);
            return A;
        }

        public float CalculateXAxis()
        {
            return _oc / _omax;
        }

        public List<Coord> GetGraphCoords()
        {
            int range = 10;
            float interval = 0.07f;

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
            if (point >= 0 && point < 1.8)
            {
                return 0.1f;
            }
            else if (point >= 1.8 && point < 5.2)
            {
                float quad = -0.072f;
                float single = 0.7603f;
                float constant = -1.0156f;

                float result = (float)(quad * Math.Pow(point, 2) + single * point + constant);

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
