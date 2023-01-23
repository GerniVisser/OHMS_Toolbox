using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.Services
{
    // Bins Service class does all calculations for setting up the bins for the MOnte Carlo Sim 
    public class BinsService
    {
        private List<Bin> _bins;

        public BinsService(List<float> value, int numberOfBins)
        {
            _bins = new List<Bin>();
            populateBins(value, numberOfBins);
        }

        public List<Bin> BinsList
        {
            get { return _bins; }
        }

        private void populateBins(List<float> value, int numberOfBins)
        {
            value.Sort();

            float min = value[0];
            float max = value[value.Count - 1];

            float interval = (max - min) / numberOfBins;

            for (int i = 0; i < numberOfBins; i++)
            {
                float start = min + (i * interval);
                float end = min + ((i + 1) * interval);

                Bin bin = new Bin(start, end);

                for (int x = 0; x < value.Count - 1; x++)
                {
                    if (value[x] >= start && value[x] < end)
                    {
                        bin.Frequency++;
                    }
                }
                BinsList.Add(bin);
            }
        }

        // Returns the bin with the most items
        public Bin getMostFrequentBin()
        {
            Bin bin = BinsList[0];
            for (int i = 1; i < BinsList.Count - 1; i++)
            {
                if (BinsList[i].Frequency > bin.Frequency)
                {
                    bin = BinsList[i];
                }
            }

            return bin;
        }

        public float getPercentageOfBinsBelowLimit(float limit)
        {
            var newList = BinsList.Where(x => x.Mid < limit).ToList();

            float x = 0;
            for(int i = 0; i < newList.Count; i++)
            {
                x += newList[i].Frequency;
            }

            float y = 0;
            for (int i = 0; i < BinsList.Count; i++)
            {
                y += BinsList[i].Frequency;
            }

            var res = x / y;
            return res;
        }

        public int getSumOffrequencies()
        {
            int res = 0;
            for (int i = 0; i < BinsList.Count - 1; i++)
            {
                res = res + BinsList[i].Frequency;
            }
            return res;
        }
    }

    // Single bin holling a the min, max and amount of items in that bin 
    public class Bin
    {
        private int _frecuancy;
        private float _max;
        private float _min;

        public Bin(float min, float max)
        {
            _min = min;
            _max = max;
        }

        public int Frequency
        {
            get { return _frecuancy; }
            set { _frecuancy = value; }
        }

        public float Min
        {
            get { return _min; }
            set { _min = value; }
        }

        public float Mid
        {
            get { return (_max + _min) / 2; }
        }

        public float Max
        {
            get { return _max; }
            set { _max = value; }
        }
    }
}
