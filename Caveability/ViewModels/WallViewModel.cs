using Caveability.Models;
using Caveability.ViewModels.Graphs;
using SharedWpfLibrary.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.ViewModels
{
    public class WallViewModel : ViewModelBase
    {
        private WallModel _wallModel;
        private HR_GraphViewModel _hrViewModel;

        public string WallName
        {
            get { return _wallModel.Name; }
        }

        // Private List to keep track of all Graphs 
        // This List is what is quaried when a new Graph is set
        private List<GraphViewModelBase> _graphViewModelList;

        // Current wall and what should be interacted with
        private GraphViewModelBase _graphViewModel;
        public GraphViewModelBase GraphViewModel
        {
            get { return _graphViewModel; }
            set
            {
                if (_graphViewModel == value) return;

                _graphViewModel = value;
                OnPropertyChanged(nameof(GraphViewModel));
            }
        }

        private int _tabGraphIndex;

        public int TabGraphIndex
        {
            get { return _tabGraphIndex; }
            set
            {
                _tabGraphIndex = value;
                // When Tab Changes it updates GraphModel to stay in sync 
                GraphViewModel = _graphViewModelList[value];
                OnPropertyChanged(nameof(TabGraphIndex));
            }
        }

        private PropGridViewModel _propGridViewModel;

        public PropGridViewModel PropGridViewModel
        {
            get { return _propGridViewModel; }
        }

        public float CurrentHR 
        { 
            get { return MathF.Round(_hrViewModel.getX,2); } 
        }

        public float MaxHR
        {
            get { return MathF.Round((float)_hrViewModel.GraphPoint[0].x, 2); }
        }

        public float MaxLength
        {
            get { return MathF.Round(_hrViewModel.getMaxLength, 2); }
        }

        public WallViewModel(WallModel wallModel)
        {
            _wallModel = wallModel;
            _hrViewModel = new HR_GraphViewModel(_wallModel);

            _graphViewModelList = new List<GraphViewModelBase>
            {
                new A_GraphViewModel(_wallModel.A_Model),
                new B_GraphViewModel(_wallModel.B_Model),
                new C_GraphViewModel(_wallModel.C_Model),
                _hrViewModel
            };
            TabGraphIndex = 0;

            _propGridViewModel = new PropGridViewModel(_wallModel);
            // Subscribe to PropGridModel Property Changed 
            _propGridViewModel.PropertyChanged += HandlePropGridChange;
        }
        // Method gets called when prop in PropGrid gets changed
        // Gelegates update requiests to all thats needs to be updated.
        public void HandlePropGridChange(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(GraphViewModel));
        }

    }
}
