using _SharedWpfLibrary.Service;
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
        public float DesiredFOS;
        public Brush Color;
        public MonteCarloModel MonteCarloModel;

        public PillarModel(string name)
        {
            Name = name;
            Height = 2.5f;
            Width = 7;
            Length = 7;
            APS = 80.34f;
            DesiredFOS = 1f;
            Color = BrushService.getRandomBrush();
            // LunderPakalnis Extendes MonteCarloModel
            MonteCarloModel = new LunderPakalnisModel();
        }

        public PillarModel(string name, float height, float width, float length, float aps)
        {
            Name = name;
            Height = height;
            Width = width;
            Length = length;
            APS = aps;
            Color = BrushService.getRandomBrush();
            MonteCarloModel = new LunderPakalnisModel();
        }

    }
}
