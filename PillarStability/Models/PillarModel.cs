using PillarStability.Services;
using SharedWpfLibrary.Service;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Xml;

namespace PillarStability.Models
{
    public class PillarModel
    {
        public string Name { get; set; }
        public float Height;
        public float Width;
        public float Length;
        public float APS { get; set; }
        public float DesiredFOS;
        public Brush Color { get; set; }
        public MonteCarloModel MonteCarloModel;
        public PillarStrengthModel PillarStrengthModel;

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
            MonteCarloModel = new MonteCarloModel();
            PillarStrengthModel = new LunderPakalnisModel();
        }

        public float Weff
        {
            get
            {
                return (4 * (Length * Width)) / (2 * (Length + Width));
            }
        }

        public float Wth
        {
            get
            {
                return Weff / Height;
            }
        }
        public float CurrentFoS
        {
            get
            {
                if (MonteCarloModel.FosList.Count == 0) return float.NaN;
                var _fosBinsService = new BinsService(MonteCarloModel.FosList, MonteCarloModel.Bins);
                return MathF.Round(_fosBinsService.getMostFrequentBin().Min, 2);
            }
        }


    }
}
