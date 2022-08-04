using _SharedWpfLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.ViewModels
{
    public class WIPViewModel: ViewModelBase
    {
        private string _text;

        public WIPViewModel(string text)
        {
            _text = text;
        }

        public string Text
        {
            get { return _text + " Comming Soon !!"; }
            set { _text = value; }
        }

    }
}
