using PillarStability.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.Helper
{
    public static class csvLoader
    {
        public static List<PillarModel> LoadCsvToPillarModels(string file)
        {
            List<PillarModel> res = new List<PillarModel>();

            string[] csvLines = File.ReadAllLines(file);

            for (int i = 1; i < csvLines.Length; i++)
            {
                string[] line = csvLines[i].Split(',');

                try
                {
                    PillarModel model = new PillarModel(line[0], float.Parse(line[1]), float.Parse(line[2]), float.Parse(line[3]), float.Parse(line[4]), float.Parse(line[5]));
                    res.Add(model);
                }
                catch
                {
                    throw new Exception("CSV file is not formatted correctly");
                }
            }

            return res;
        }
    }
}
