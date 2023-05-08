using Borehole.Models;
using SharedWpfLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borehole.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
		private BoreholeViewModel _boreholeViewModel;

		public BoreholeViewModel BoreholeViewModel
        {
			get { return _boreholeViewModel; }
			set { _boreholeViewModel = value; }
		}

		public MainViewModel(BoreholeViewModel boreholeViewModel)
		{
			_boreholeViewModel = new BoreholeViewModel(new BoreholeModel());
		}
	}
}
