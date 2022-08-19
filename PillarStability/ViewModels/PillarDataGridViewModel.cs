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
    public class PillarDataGridViewModel : ViewModelBase
    {
        private PillarModel _pillarModel;
        private PillarDataService _pillarDataService;

        public PillarDataGridViewModel(PillarModel pillarModel)
        {
            _pillarModel = pillarModel;
            _pillarDataService = new Wh_Service(pillarModel);
        }

        [DisplayName("Name")]
        public string Name
        {
            get
            {
                return _pillarModel.Name;
            }
        }

        [DisplayName("Width")]
        public float Width {
            get
            {
                return _pillarModel.Width;
            }
        }

        [DisplayName("Width / Height")]
        public float WHRatio
        {
            get
            {
                return _pillarModel.Width;
            }
        }

        [DisplayName("Average Stress")]
        public float AveStress
        {
            get
            {
                return _pillarDataService.APStUCS;
            }
        }

        [DisplayName("Average Confinement")]
        public float AveConfienment
        {
            get
            {
                return _pillarDataService.APC;
            }
        }
    }
}
