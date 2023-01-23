using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.DataObjects
{
    public static class PillarStrengthOptions
    {
        public static List<string> Options { 
            get 
            {
                return new List<string>()
                {
                    "Lunder and Pakalnis",
                    "Salamon and Munro",
                    "Hedley and Grant",
                    "Stacey and Page",
                    "PlatMine Merensky",
                    "PlatMine UG2"
                };
            }
        }
    }
}
