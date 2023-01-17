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
    public class MonteCarloService
    {
        private PillarModel _pillarModel;
        private Random _random;

        public MonteCarloModel MonteCarloModel
        {
            get { return _pillarModel.MonteCarloModel; }
            set { _pillarModel.MonteCarloModel = value; }
        }


        private List<float> _fosList;

        public List<float> Foslist
        {
            get 
            {
                if (_fosList == null)
                {
                    _fosList = generateFOSList();
                }
                return _fosList;
            }
        }

        public FoSAlgoritm FoSAlgoritm
        {
            get 
            {
                if (MonteCarloModel is LunderPakalnisModel)
                {
                    return new LunderPakalnisService();
                }
                else if (MonteCarloModel is PowerFormulaModel)
                {
                    return new PowerFormulaService();
                }
                else return null;
            }
        }

        public MonteCarloService(PillarModel pillarModel)
        {
            _pillarModel = pillarModel;
            // Gernerate random seed
            _random = new Random(Guid.NewGuid().GetHashCode());
        }

        public void CalculateMonteCarlo()
        {
            _fosList = generateFOSList();
        }

        private List<float> generateFOSList()
        {
            List<float> fosList = new List<float>();

            for (int i = 0; i < MonteCarloModel.Iterations; i++)
            {
                float FOS = FoSAlgoritm.Calculate(getExcelNormInv, _pillarModel);

                if (!float.IsNaN(FOS))
                {
                    fosList.Add(FOS);
                }
            }
            return fosList;
        }
        public List<Coord> generateMonteCarloChartLine()
        {
            List<Coord> res = new List<Coord>();
            BinsService binsService = new BinsService(Foslist, MonteCarloModel.Bins);

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

        public List<Coord> generateMonteCarloCumulativeChartLine()
        {
            List<Coord> res = new List<Coord>();
            BinsService binsService = new BinsService(Foslist, MonteCarloModel.Bins);

            int freqSum = binsService.getSumOffrequencies();

            float cumNormFreq = 0f;

            for (int i = 0; i < binsService.BinsList.Count; i++)
            {
                float x = binsService.BinsList[i].Min;
                float y = (float)binsService.BinsList[i].Frequency / (float)freqSum;

                cumNormFreq += y;

                Coord coord = new Coord()
                {
                    x = x,
                    y = cumNormFreq
                };

                res.Add(coord);
            }

            return res;
        }

        public List<Coord> generateFos1ChartLine()
        {
            List<Coord> res = new List<Coord>()
            {
                new Coord(){ x = 1, y = 0 },
                new Coord(){ x = 1, y = 1 },
            };

            return res;
        }

        public List<Coord> generateFos14ChartLine()
        {
            List<Coord> res = new List<Coord>()
            {
                new Coord(){ x = 1.4, y = 0 },
                new Coord(){ x = 1.4, y = 1 },
            };

            return res;
        }

        public MonteCarloDataObject generateMonteCarloDataObject()
        {
            return FoSAlgoritm.GenerateSummaryObject(_pillarModel, Foslist);
        }

        private float getExcelNormInv(float mean, float stdev)
        {
            float normInv = 0.0f;

            if (stdev == 0.0f)
            {
                return mean;
            }
            else
            {
                try
                {
                    NormalDistribution n = new NormalDistribution(mean, stdev);
                    normInv = (float)n.InverseLeftProbability(_random.NextDouble());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                return normInv;
            }
        }
    }
}
