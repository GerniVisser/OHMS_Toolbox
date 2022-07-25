using Caveability.Models;
using SharedWpfLibrary.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.Services
{
    public class B_Service
    {
        private B_Model b_Model;

        public B_Service(B_Model b)
        {
            b_Model = b;
        }

        public float getX()
        {
            return b_Model.b;
        }

        public Coord CalcCoordFromX(float x)
        {
            if (x >= 0 && x < 20)
            {
                return new Coord() { x = x, y = 0.4f };
            }
            else if (x >= 20 && x < 40)
            {
                float single = 0.005f;
                float constant = 0.3f;

                float result = single * x + constant;

                return new Coord() { x = x, y = result };
            }
            else if (x >= 40 && x < 65)
            {
                float single = 0.02f;
                float constant = -0.3f;

                float result = single * x + constant;

                return new Coord() { x = x, y = result };
            }
            else
            {
                return new Coord() { x = x, y = 0.99f };
            }
        }

        public CoordSerries CoordSerriesCalc()
        {
            int range = 90;
            float interval = 0.5f;

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
