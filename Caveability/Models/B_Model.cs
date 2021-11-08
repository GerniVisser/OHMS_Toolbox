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
        public double _b { get; set; }


        public B_Model(float b)
        {
            _b = b;
        }

        public double Calculate()
        {
            double B = graphCalcutation(_b);
            return B;
        }

        public double CalculateXAxis()
        {
            return _b;
        }


        public List<Coord> GetGraphCoords()
        {
            int range = 90;
            double interval = 0.5;

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
            if (point >= 0 && point < 20)
            {
                return 0.4;
            }
            else if (point >= 20 && point < 40)
            {
                float single = 0.005f;
                float constant = 0.3f;

                double result = single * point + constant;

                return result;
            }
            else if (point >= 40 && point < 65)
            {
                float single = 0.02f;
                float constant = -0.3f;

                double result = single * point + constant;

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
