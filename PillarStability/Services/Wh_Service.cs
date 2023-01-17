using PillarStability.Models;
using SharedWpfLibrary.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.Services
{
    public class Wh_Service : PillarDataService
    {
        private PillarModel _pillarModel;
        private LunderPakalnisModel _lunderPakalnisModel;
        public Wh_Service(PillarModel pillarModel) : base(pillarModel)
        {
            _pillarModel = pillarModel;
            _lunderPakalnisModel = (LunderPakalnisModel)pillarModel.MonteCarloModel;
        }

        public override List<Coord> graphFail()
        {
            List<Coord> Res = new List<Coord>();
            float APC, K, PS, FOS1;

            for (float wTh = 0; wTh < 30; wTh += GraphStepSize)
            {
                if (wTh > 3.99)
                {
                    APC = 0.23f + 0.017f * wTh;
                }
                else
                {
                    APC = (float)(0.34f * Math.Pow(Math.Log10(wTh + 0.75f), (1.4f / wTh)));
                }

                K = (float)(Math.Tan(Math.Acos((1 - APC) / (1 + APC))));

                PS = (float)((0.44 * _lunderPakalnisModel.UCS) * (0.68 + 0.52 * K));

                FOS1 = PS / _lunderPakalnisModel.UCS;

                var coord = new Coord { x = wTh, y = FOS1 };

                Res.Add(coord);
            }

            return Res;
        }

        
        public override List<Coord> graphStable()
        {
            List<Coord> Res = new List<Coord>();
            float APC, K, PS, FOS14;

            for (float wTh = 0; wTh < 30; wTh += GraphStepSize)
            {
                if (wTh > 3.99)
                {
                    APC = 0.23f + 0.017f * wTh;
                }
                else
                {
                    APC = (float)(0.34f * Math.Pow(Math.Log10(wTh + 0.75f), (1.4f / wTh)));
                }

                K = (float)(Math.Tan(Math.Acos((1 - APC) / (1 + APC))));

                PS = (float)((0.44 * _lunderPakalnisModel.UCS) * (0.68 + 0.52 * K));

                FOS14 = PS / (1.4f * _lunderPakalnisModel.UCS);

                var coord = new Coord { x = wTh, y = FOS14 };

                Res.Add(coord);
            }

            return Res;

        }

        public override List<Coord> graphPoint()
        {
            List<Coord> Res = new List<Coord>() 
            {
                new Coord()
                {
                    x = Wth, y = APStUCS
                }
            };

            return Res;
        }
    }
}
