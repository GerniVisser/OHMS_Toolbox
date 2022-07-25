using SharedWpfLibrary.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.ViewModels.Graphs
{
    public abstract class GraphViewModelBase : ViewModelBase
    {
        public abstract string GraphName
        {
            get;
        }
        public abstract float getX
        {
            get;
        }


        public virtual List<Coord> GraphLine
        {
            get;
        }

        public virtual List<Coord> HRGraphLineTop
        {
            get;
        }

        public virtual List<Coord> HRGraphLineBot
        {
            get;
        }

        public abstract List<Coord> GraphPoint
        {
            get;
        }

        public virtual List<Coord> HRGraphPointMax
        {
            get;
        }
    }
}
