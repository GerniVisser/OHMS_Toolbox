using _SharedWpfLibrary.ViewModels;
using PillarStability.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.ViewModels.PropGrid
{
    public class PowerFormulaPropGridVM : ViewModelBase
    {
        private PowerFormulaModel _powerFormulaModel;

        public PowerFormulaPropGridVM(PowerFormulaModel powerFormulaModel)
        {
            _powerFormulaModel = powerFormulaModel;
        }

        [DisplayName("Alpha"), ReadOnly(true), Description("Alpha value"), Category("Parameters"), Display(Order = 0)]
        public float Alpha
        {
            get { return _powerFormulaModel.Alpha; }
            set
            {
                _powerFormulaModel.Alpha = value;
            }
        }

        [DisplayName("Beta"), ReadOnly(true), Description("Beta value"), Category("Parameters"), Display(Order = 0)]
        public float Beta
        {
            get { return _powerFormulaModel.Beta; }
            set
            {
                _powerFormulaModel.Beta = value;
            }
        }

        [DisplayName("K"), Description("Back-fit ubit-strength"), Category("Parameters"), Display(Order = 0)]
        public float K
        {
            get { return _powerFormulaModel.K; }
            set
            {
                _powerFormulaModel.K = value;
            }
        }

        [DisplayName("Std of K"), Description("Standard Deviation of K"), Category("Parameters"), Display(Order = 0)]
        public float stdK
        {
            get { return _powerFormulaModel.StdK; }
            set
            {
                _powerFormulaModel.StdK = value;
            }
        }

        [DisplayName("Std of Width"), Description("Standard Deviation of Width"), Category("Parameters"), Display(Order = 0)]
        public float stdWidth
        {
            get { return _powerFormulaModel.StdWidth; }
            set
            {
                _powerFormulaModel.StdWidth = value;
            }
        }

        [DisplayName("Std of Height"), Description("Standard Deviation of Height"), Category("Parameters"), Display(Order = 0)]
        public float stdHeight
        {
            get { return _powerFormulaModel.StdHeight; }
            set
            {
                _powerFormulaModel.StdHeight = value;
            }
        }
    }
}
