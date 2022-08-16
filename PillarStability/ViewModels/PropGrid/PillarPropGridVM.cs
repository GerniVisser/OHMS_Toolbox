using _SharedWpfLibrary.ViewModels;
using PillarStability.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PillarStability.ViewModels.PropGrid
{
    public class PillarPropGridVM: ViewModelBase
    {
        private PillarModel _pillarModel;

        public PillarPropGridVM(PillarModel pillarModel)
        {
            _pillarModel = pillarModel;
        }

        [DisplayName("Color"), Description("Pillar Color"), Category("General")]
        public Brush Color
        {
            get { return _pillarModel.Color; }
            set 
            { 
                _pillarModel.Color = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }

        [DisplayName("Height"), Description("Pillar Height (m)"), Category("General")]
        public float Height
        {
            get { return _pillarModel.Height; }
            set
            {
                _pillarModel.Height = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }

        [DisplayName("Width"), Description("Pillar Width (m)"), Category("General")]
        public float Width
        {
            get { return _pillarModel.Width; }
            set
            {
                _pillarModel.Width = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }

        [DisplayName("Length"), Description("Pillar Length (m)"), Category("General")]
        public float Length
        {
            get { return _pillarModel.Length; }
            set
            {
                _pillarModel.Length = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }

        [DisplayName("APS"), Description("Average Pillar Stress (MPa)"), Category("General")]
        public float APS
        {
            get { return _pillarModel.APS; }
            set
            {
                _pillarModel.APS = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }

        [DisplayName("UCS"), Description("Average Uniaxial Compressive strength(MPa)"), Category("General")]
        public float UCS
        {
            get { return _pillarModel.UCS; }
            set
            {
                _pillarModel.UCS = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }
    }
}
