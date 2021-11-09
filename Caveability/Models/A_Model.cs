using Caveability.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.Models
{
    public class A_Model : ICalculator
    {

        public double _oc { get; set; }
        public double _omax { get; set; }


        public A_Model(float oc, float omax)
        {
            _oc = oc;
            _omax = omax;
        }

        public double Calculate()
        {
            double A = graphCalcutation(_oc / _omax);
            return A;
        }

        public double CalculateXAxis()
        {
            return _oc / _omax;
        }

        public List<Coord> GetGraphCoords()
        {
            int range = 10;
            double interval = 0.07;

            double numPoints = range / interval;

            List<Coord> graphPoints = new List<Coord>();

            for (int i = 0; i < numPoints; i++)
            {
                graphPoints.Add(new Coord { x = i * interval, y = graphCalcutation(i * interval) });
            }

            return graphPoints;
        }

        private double graphCalcutation(double point)
        {
            if (point >= 0 && point < 1.8)
            {
                return 0.1;
            }
            else if (point >= 1.8 && point < 5.2)
            {
                float quad = -0.072f;
                float single = 0.7603f;
                float constant = -1.0156f;

                double result = quad * Math.Pow(point, 2) + single * point + constant;

                return result;
            }
            else
            {
                return 0.99;
            }
        }

        public override string ToString()
        {
            return "";
        }
    }
}
