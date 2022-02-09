using Meta.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.Helper
{
    public class Bins
    {
        private List<Bin> _bins;

        public Bins(List<float> value, int numberOfBins)
        {
            _bins = new List<Bin>();
            populateBins(value, numberOfBins);
        }

        public List<Bin> BinsList
        {
            get { return _bins; }
            set { _bins = value; }
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
                    if (value[x] > start && value[x] < end)
                    {
                        bin.Frequency++;
                    }
                }
                BinsList.Add(bin);
            }
        }

        public Bin getMostFrequentBin()
        {
            Bin bin = BinsList[0];
            for (int i = 1; i < BinsList.Count - 1; i++)
            {
                if(BinsList[i].Frequency > bin.Frequency)
                {
                    bin = BinsList[i];
                }
            }

            return bin;
        }

        public float GetLimitFOS(float limit)
        {
            var newList = BinsList.Where(x => x.Mid < limit).ToList();

            float x = newList.Count;
            float y = BinsList.Count;

            var res = (x / y) * 100f;
            return res;
        }

    }
}
