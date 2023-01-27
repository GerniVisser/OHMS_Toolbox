using SharedWpfLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.ViewModels.Graphs
{
    public abstract class GraphBaseViewModel: ViewModelBase
    {
        public abstract void Update();
    }
}
