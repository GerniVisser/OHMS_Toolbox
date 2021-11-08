﻿using Caveability.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.Models
{
    public class HR_Model
    {
        [Category("HR"), DisplayName("length"), Description("")]
        public double _length { get; set; }
        [Category("HR"), DisplayName("width"), Description("")]
        public double _width { get; set; }

        public HR_Model(double length, double width)
        {
            _length = length;
            _width = width;
        }

        public double Calculate()
        {
            // Use N to Calculate
            return _length * _width / (2 * (_width + _length));
        }

        // calculate x axis of N
        public double CalculateXAxis(double n)
        {
            if (n < 0.1) return 0;
            else if (n < 18)
            {
                double a = -0.3593;
                double b = 2.1136;
                double c = -3.2 + n;

                double r1 = (Math.Pow(b, 2) - 4 * a * c);

                double x1 = (-b - Math.Sqrt(r1));

                x1 = x1 / (2 * a);

                return x1;
            }
            else
                return Math.Pow(10, 0.338 * Math.Log10(n) + 0.573);
        }

        // Used to draw line graph
        private double CalculateYAxis(double hr)
        {
            if (hr < 3) return 0;
            else if (hr < 10)
            {
                return 0.3593 * Math.Pow(hr, 2) - 2.1136 * hr + 3.2;
            }
            else
                return Math.Pow(10, (Math.Log10(hr) - 0.573) / 0.338);
        }

        // Used to draw line graph
        private double CalculateTopYAxis(double hr)
        {
            return 0.0695*Math.Pow(hr,2.8198);
        }

        // Draw guide lines on graph
        public List<Coord> GetBottomGraphCoords()
        {
            int range = 24;
            double interval = 0.2;

            double numPoints = range / interval;

            List<Coord> graphPoints = new List<Coord>();

            for (int i = 1; i < numPoints; i++)
            {
                graphPoints.Add(new Coord { x = i * interval, y = CalculateYAxis(i * interval) });
            }

            return graphPoints;
        }

        public List<Coord> GetTopGraphCoords()
        {
            int range = 24;
            double interval = 0.2;

            double numPoints = range / interval;

            List<Coord> graphPoints = new List<Coord>();

            for (int i = 1; i < numPoints; i++)
            {
                graphPoints.Add(new Coord { x = i * interval, y = CalculateTopYAxis(i * interval) });
            }

            return graphPoints;
        }

        public double GetMaxLenght(double n)
        {
            double hr = CalculateXAxis(n);
            var t = 2 * _width * hr;
            var b = _width - 2 * hr;
            var final = Math.Round(t / b, 2);
            return final;
        }

        public override string ToString()
        {
            return "";
        }
    }
}
