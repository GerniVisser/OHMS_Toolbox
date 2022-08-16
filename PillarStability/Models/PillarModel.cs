using _SharedWpfLibrary.Service;
using PillarStability.Helper;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace PillarStability.Models
{
    public class PillarModel
    {
        public string Name { get; set; }
        public float Height;
        public float Width;
        public float Length;
        public float APS;
        public float UCS;
        public Brush Color;
        public MonteCarloModel MonteCarloModel;

        public PillarModel(string name)
        {
            Name = name;
            Height = 2.5f;
            Width = 7;
            Length = 7;
            APS = 80.34f;
            UCS = 111.42f;
            Color = BrushService.getRandomBrush();
            MonteCarloModel = new MonteCarloModel();
        }

        public PillarModel(string name, float height, float width, float length, float aps, float ucs)
        {
            Name = name;
            Height = height;
            Width = width;
            Length = length;
            APS = aps;
            UCS = ucs;
            Color = BrushService.getRandomBrush();
            MonteCarloModel = new MonteCarloModel();
        }

    }
}
