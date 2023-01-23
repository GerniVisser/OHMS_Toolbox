using PillarStability.Models;
using SharedWpfLibrary.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.Services
{
    public class Wh_Service
    {
        private PillarModel _pillarModel;
        private PillarStrengthService _pillarStrengthService;

        private const float STEP_SIZE = 0.5f;
        public Wh_Service(PillarModel pillarModel)
        {
            _pillarModel = pillarModel;
        }

        public PillarStrengthService PillarStrengthService 
        {
            set 
            { 
                _pillarStrengthService = value; 
            } 
        }
        
        public List<Coord> graphStable()
        {
            List<Coord> Res = new List<Coord>();
            float PS;

            for (float x = 0; x < 6; x += STEP_SIZE)
            {
                float wth = x * _pillarModel.Height;
                PS = _pillarStrengthService.calculatePillarStrengthAtWH(wth, _pillarModel.Height);

                float Stress = PS / _pillarModel.DesiredFOS;

                var coord = new Coord { x = x, y = Stress };

                Res.Add(coord);
            }

            return Res;

        }

        public List<Coord> graphPoint()
        {
            List<Coord> Res = new List<Coord>() 
            {
                new Coord()
                {
                    x = _pillarModel.Wth, y = _pillarModel.APS
                }
            };

            return Res;
        }
    }
}
