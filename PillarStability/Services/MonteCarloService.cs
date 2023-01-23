using Meta.Numerics.Statistics.Distributions;
using PillarStability.DataObjects;
using PillarStability.Models;
using SharedWpfLibrary.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.Services
{
    public static class MonteCarloService
    {
        public static void updateMCLists(PillarModel pillarModel, PillarStrengthService pillarStrengthService)
        {
            Random random = new Random();   

            MonteCarloModel monteCarloModel = pillarModel.MonteCarloModel;
            monteCarloModel.FosList.Clear();
            monteCarloModel.StrengthList.Clear();

            for (int i = 0; i < monteCarloModel.Iterations; i++)
            {
                var variablePillarModelService = pillarStrengthService.generateExcelNormInvPillarStrengthService(random);
                variablePillarModelService.calculateCurrentStrength();   

                float Strength = variablePillarModelService.PillarStrength;
                float FOS = variablePillarModelService.PillarFos;

                if (!float.IsNaN(FOS))
                {
                    monteCarloModel.FosList.Add(FOS);
                    monteCarloModel.StrengthList.Add(Strength);
                }
            }
        }
        public static List<Coord> generateMonteCarloChartLine(PillarModel pillarModel)
        {
            List<Coord> res = new List<Coord>();
            BinsService binsService = new BinsService(pillarModel.MonteCarloModel.FosList, pillarModel.MonteCarloModel.Bins);

            int freqSum = binsService.getSumOffrequencies();

            for (int i = 0; i < binsService.BinsList.Count; i++)
            {
                float x = binsService.BinsList[i].Min;
                float y = (float)binsService.BinsList[i].Frequency / (float)freqSum;

                Coord coord = new Coord()
                {
                    x = x,
                    y = y
                };

                res.Add(coord);
            }

            return res;
        }

        public static List<Coord> generateMonteCarloCumulativeChartLine(PillarModel pillarModel)
        {
            List<Coord> res = new List<Coord>();
            BinsService binsService = new BinsService(pillarModel.MonteCarloModel.FosList, pillarModel.MonteCarloModel.Bins);

            int freqSum = binsService.getSumOffrequencies();

            float cumnormfreq = 0f;

            for (int i = 0; i < binsService.BinsList.Count; i++)
            {
                float x = binsService.BinsList[i].Min;
                float y = (float)binsService.BinsList[i].Frequency / (float)freqSum;

                cumnormfreq += y;

                Coord coord = new Coord()
                {
                    x = x,
                    y = cumnormfreq
                };

                res.Add(coord);
            }

            return res;
        }

        public static List<Coord> generateFosChartLine(float Fos)
        {
            List<Coord> res = new List<Coord>()
            {
                new Coord(){ x = Fos, y = 0 },
                new Coord(){ x = Fos, y = 1 },
            };

            return res;
        }

        public static List<Coord> generateFos1ChartLine()
        {
            List<Coord> res = new List<Coord>()
            {
                new Coord(){ x = 1, y = 0 },
                new Coord(){ x = 1, y = 1 },
            };

            return res;
        }
    }
}
