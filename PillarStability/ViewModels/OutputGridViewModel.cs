using _SharedWpfLibrary.ViewModels;
using PillarStability.Models;
using PillarStability.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.ViewModels
{
    public class OutputGridViewModel : ViewModelBase
    {
        private PillarModel _pillarModel;
        private BinsService _fosBinsService;

        public OutputGridViewModel(PillarModel pillarModel)
        {
            _pillarModel = pillarModel;
        }

        [DisplayName("Name")]
        public string Name
        {
            get
            {
                return _pillarModel.Name;
            }
        }

        [DisplayName("Effective Width")]
        public string Width {
            get
            {
                return MathF.Round(_pillarModel.Weff, 2).ToString() + " m";
            }
        }

        [DisplayName("Width / Height")]
        public float WHRatio
        {
            get
            {
                return MathF.Round(_pillarModel.Wth, 2);
            }
        }

        [DisplayName("Average Pillar Strength")]
        public string AveStrength
        {
            get
            {
                return MathF.Round(_pillarModel.MonteCarloModel.StrengthList.Average(), 2).ToString() + " MPa";
            }
        }

        [DisplayName("Average Stress")]
        public string AveStress
        {
            get
            {
                return MathF.Round(_pillarModel.APS, 2).ToString() + " MPa";
            }
        }

        [DisplayName("Average FOS")]
        public float AveFOS
        {
            get
            {
                _fosBinsService = new BinsService(_pillarModel.MonteCarloModel.FosList, _pillarModel.MonteCarloModel.Bins);
                return MathF.Round(_fosBinsService.getMostFrequentBin().Min, 2);
            }
        }

        [DisplayName("Probability of Failure")]
        public string Reliability
        {
            get
            {
                return MathF.Round(_fosBinsService.getPercentageOfBinsBelowLimit(_pillarModel.DesiredFOS) * 100,2).ToString() + " %";
            }
        }
    }
}
