using Caveability.Models;
using SharedWpfLibrary.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.Services
{
    public class HR_Service
    {
        private HR_Model HR_Model;

        private A_Service A_Service;
        private B_Service B_Service;
        private C_Service C_Service;
        private Q_Service Q_Service;

        public HR_Service(WallModel wallModel)
        {
            HR_Model = wallModel.HR_Model; 

            A_Service = new A_Service(wallModel.A_Model);
            B_Service = new B_Service(wallModel.B_Model);
            C_Service = new C_Service(wallModel.C_Model);
            Q_Service = new Q_Service(wallModel.Q_Model);
        }

        public float getX()
        {
            // Use N to Calculate
            return HR_Model.Height * HR_Model.Width / (2 * (HR_Model.Width + HR_Model.Height));
        }

        public float CalculateXAxis()
        {
            var n = Calc_N();
            if (n < 0.1) return 0;
            else if (n < 18)
            {
                double a = -0.3593;
                double b = 2.1136;
                double c = -3.2 + n;

                double r1 = Math.Pow(b, 2) - 4 * a * c;

                double x1 = -b - Math.Sqrt(r1);

                x1 = x1 / (2 * a);

                return (float)(x1);
            }
            else
                return (float)(Math.Pow(10, 0.338 * Math.Log10(n) + 0.573));
        }

        public Coord CalcCoordFromX(float x)
        {
            float y = 0.0695f * (float)Math.Pow(x, 2.8198);
            return new Coord { x = x, y = y };
        }

        // Used to draw line graph
        public Coord CalcCoordFromXBot(float x)
        {
            if (x < 3) return new Coord { x = x, y = 0 };
            else if (x < 10)
            {
                var y = (float)(0.3593 * Math.Pow(x, 2) - 2.1136 * x + 3.2);
                return new Coord { x = x, y = y };
            }
            else
            {
                var y = (float)(Math.Pow(10, (Math.Log10(x) - 0.573) / 0.338));
                return new Coord { x = x, y = y };
            }
        }

        public CoordSerries CoordSerriesCalcBot()
        {
            int range = 24;
            float interval = 0.2f;

            float numPoints = range / interval;

            CoordSerries graphPoints = new CoordSerries();

            for (int i = 1; i < numPoints; i++)
            {
                graphPoints.coords.Add(CalcCoordFromXBot(i * interval));
            }

            return graphPoints;
        }

        public CoordSerries CoordSerriesCalc()
        {
            int range = 24;
            float interval = 0.2f;

            float numPoints = range / interval;

            CoordSerries graphPoints = new CoordSerries();

            for (int i = 1; i < numPoints; i++)
            {
                graphPoints.coords.Add(CalcCoordFromX(i * interval));
            }

            return graphPoints;
        }

        public float GetMaxLenght()
        {
            double hr = CalculateXAxis();
            var t = 2 * HR_Model.Width * hr;
            var b = HR_Model.Width - 2 * hr;
            var final = Math.Round(t / b, 2);
            return (float)(final);
        }

        public float Calc_N()
        {
            var q = Q_Service.CalcCoordFromX().y;
            var a = A_Service.CalcCoordFromX(A_Service.getX()).y;
            var b = B_Service.CalcCoordFromX(B_Service.getX()).y;
            var c = C_Service.CalcCoordFromX(C_Service.getX()).y;
            return (float)(q * a * b * c);
        }
    }
}
