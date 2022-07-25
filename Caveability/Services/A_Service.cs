using Caveability.Models;
using SharedWpfLibrary.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.Services
{
    public class A_Service
    {
        private A_Model a_Model;

        public A_Service(A_Model a)
        {
            a_Model = a;
        }

        public float getX()
        {
            return a_Model.oc / a_Model.omax;
        }

        public Coord CalcCoordFromX(float x)
        {
            if (x >= 0 && x < 1.8)
            {
                return new Coord { x = x, y = 0.1f };
            }
            else if (x >= 1.8 && x < 5.2)
            {
                float quad = -0.072f;
                float single = 0.7603f;
                float constant = -1.0156f;

                float result = (float)(quad * Math.Pow(x, 2) + single * x + constant);

                return new Coord { x = x, y = result };
            }
            else
            {
                return new Coord { x = x, y = 0.99f };
            }
        }

        public CoordSerries CoordSerriesCalc()
        {
            int range = 10;
            float interval = 0.07f;

            float numPoints = range / interval;

            CoordSerries graphPoints = new CoordSerries();

            for (int i = 0; i < numPoints; i++)
            {
                graphPoints.coords.Add(CalcCoordFromX(i * interval));
            }

            return graphPoints;
        }
    }
}
