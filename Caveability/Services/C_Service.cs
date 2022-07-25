using Caveability.Models;
using SharedWpfLibrary.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.Services
{
    public class C_Service
    {
        private C_Model c_Model;

        public C_Service(C_Model c)
        {
            c_Model = c;
        }

        public float getX()
        {
            return c_Model.c;
        }

        public Coord CalcCoordFromX(float x)
        {
            if (c_Model.isFootwall)
            {
                return new Coord() { x = x, y = CalcFromXFootwall(x) };
            }
            else
            {
                return new Coord() { x = x, y = CalcFromX(x) };
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

        private float CalcFromX(float x)
        {
            if (x >= 0 && x < 20)
            {
                return 4;
            }
            else if (x >= 20 && x < 50)
            {
                float single = 0.0333f;
                float constant = 3.3333f;

                float result = (float)(single * x + constant);

                return result;
            }
            else if (x >= 50 && x < 90)
            {
                float single = 0.075f;
                float constant = 1.25f;

                float result = (float)(single * x + constant);

                return result;
            }
            else
            {
                return 8;
            }
        }

        private float CalcFromXFootwall(float x)
        {
            if (x >= 0 && x < 30)
            {
                return 7.98f;
            }
            else if (x >= 30 && x < 90)
            {
                float single = -0.0667f;
                float constant = 10f;

                float result = (float)(single * x + constant);

                return result;
            }
            else
            {
                return 4;
            }
        }
    }
}
