using _SharedWpfLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.ViewModels
{
    public class HomeViewModel: ViewModelBase
    {
        public string Version
        {
            get 
            {
                var version = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
                return $"Version : {version.Major}.{version.Minor}"; 
            }
        }
    }
}
