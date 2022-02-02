using PillarStability.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.Models
{
    public static class SerriesBuilder
    {
        public static List<CoordSerries> whGraph(float UCS)
        {
            float wTh = 0.0f;
            float APC = 0.0f;
            float K = 0.0f;
            float PS = 0.0f;
            float FOS1 = 0.0f;
            float FOS14 = 0.0f;

            CoordSerries coordsFail = new CoordSerries();
            CoordSerries coordsStable = new CoordSerries();

            while (wTh <= 30)
            {
                wTh = wTh + 0.1f;

                if (wTh > 3.99)
                {
                    APC = 0.23f + 0.017f * wTh;
                }
                else
                {
                    APC = (float)(0.34f * Math.Pow(Math.Log10(wTh + 0.75f), (1.4f / wTh)));
                }

                K = (float)(Math.Tan(Math.Acos((1 - APC) / (1 + APC))));

                PS = (float)((0.44 * UCS) * (0.68 + 0.52 * K));

                FOS1 = PS / UCS;
                FOS14 = PS / (1.4f * UCS);

                var coordfail = new Coord { x = wTh, y = FOS1 };

                coordsFail.coords.Add(coordfail);

                var coordStable = new Coord { x = wTh, y = FOS14 };

                coordsStable.coords.Add(coordStable);
            }

            var res = new List<CoordSerries>() { coordsFail, coordsStable };
            return res;
        }
    }
}
