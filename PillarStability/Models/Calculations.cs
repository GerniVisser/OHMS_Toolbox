using PillarStability.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.Models
{
    public static class Calculations
    {
        public static OutputGridObject calculate(PillarModel pillar)
        {
            var i = new OutputGridObject() {
                Width = pillar.Weff,
                WidthtHeight = pillar.Wth,
                Pillar = pillar.Name,
                AveStress = pillar.APStUCS
            };

            return i;
        }

    }
}
