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
        private string _pillarname = "Hi";

        public string Pillar
        {
            get { return _pillarname; }
            set { _pillarname = value; }
        }

        
        private float _effPillarWidth = 23;

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
}
