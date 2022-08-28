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
        private FoSAlgoritm _fosAlgoritm;

        //Algorithm thaty is used to Calcutale Factor of safety
        public  FoSAlgoritm FoSAlgoritm
        {
            get { return _fosAlgoritm; }
            set { _fosAlgoritm = value; }
        }

        private PillarModel _pillarModel;
        private MonteCarloModel _monteCarloModel;
        private Random _random;

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

        public MonteCarloService(FoSAlgoritm foSAlgoritm, PillarModel pillarModel, MonteCarloModel monteCarloModel)
        {
            _fosAlgoritm = foSAlgoritm;
            _pillarModel = pillarModel;
            _monteCarloModel = monteCarloModel;
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

            for (int i = 0; i < _monteCarloModel.Iterations; i++)
            {
                float FOS = FoSAlgoritm.Calculate(getExcelNormInv, _pillarModel, _monteCarloModel);

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
            BinsService binsService = new BinsService(Foslist, _monteCarloModel.Bins);

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
            BinsService binsService = new BinsService(Foslist, _monteCarloModel.Bins);

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
            PillarDataService pillarDataService = new Wh_Service(_pillarModel);
            BinsService binsService = new BinsService(Foslist, _monteCarloModel.Bins);
            float K = MathF.Tan(MathF.Acos((1 - pillarDataService.APC) / (1 + pillarDataService.APC)));
            float dsf = _monteCarloModel.Psk * _pillarModel.UCS * (_monteCarloModel.C1 + _monteCarloModel.C2 * K) / _pillarModel.APS;

            return new MonteCarloDataObject()
            {
                Pillar = _pillarModel.Name,
                DSF = dsf,
                AveSF = Foslist.Average(),
                StandardDev = getStandardDev(),
                mfSF = MathF.Round(binsService.getMostFrequentBin().Min, 2),
                probSF = binsService.getPercentageOfBinsBelowLimit(_monteCarloModel.Lsf)
            };
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

        public float getStandardDev()
        {
            float ave = Foslist.Average();
            float sumOfSquaresOfDiff = Foslist.Select(val => (val - ave) * (val - ave)).Sum();
            float sd = MathF.Sqrt(sumOfSquaresOfDiff / Foslist.Count);

            return sd;
        }
    }
}
