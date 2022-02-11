using PillarStability.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.Helper
{
    public class OutputGridObject
    {
        private string _pillarname;

        public string Pillar
        {
            get { return _pillarname; }
            set { _pillarname = value; }
        }

        private float _effPillarWidth;

        public float Width
        {
            get { return _effPillarWidth; }
            set { _effPillarWidth = value; }
        }

        private float _whRatio;

        public float WidthtHeight
        {
            get { return _whRatio; }
            set { _whRatio = value; }
        }

        private float _aveStress;

        public float AveStress
        {
            get { return _aveStress; }
            set { _aveStress = value; }
        }

        private float _aveConfinement;

        public float AveConfinement
        {
            get { return _aveConfinement; }
            set { _aveConfinement = value; }
        }
    }

    public class MCGridObject
    {
        private string _pillar;

        public string Pillar
        {
            get { return _pillar; }
            set { _pillar = value; }
        }

        private float _dsf;

        public float DSF
        {
            get { return _dsf; }
            set { _dsf = value; }
        }

        private float _aveSF;

        public float AveSF
        {
            get { return _aveSF; }
            set { _aveSF = value; }
        }

        private float _standardDev;

        public float StandardDev
        {
            get { return _standardDev; }
            set { _standardDev = value; }
        }

        private float _mfSF;

        public float mfSF
        {
            get { return _mfSF; }
            set { _mfSF = value; }
        }

        private float _probSF;

        public float probSF
        {
            get { return _probSF; }
            set { _probSF = value; }
        }
    }

    public class PillarPrams
    {
        public PillarPrams(PillarModel model)
        {
            Name = model.Name;
            Width = model.Width;
            Height = model.Height;
            Length = model.Length;
            APS = model.APS;
            UCS = model.UCS;
        }

        public string Name { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Length { get; set; }
        public float APS { get; set; }
        public float UCS { get; set; }
    }
}
