using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.ViewModels
{
    public class TabWallViewModel
    {
        private string _wallTabName;

        public TabWallViewModel(string wallTabName)
        {
            _wallTabName = wallTabName;
        }

        public string WallTabName
        {
            get { return _wallTabName; }
        }

    }
}
