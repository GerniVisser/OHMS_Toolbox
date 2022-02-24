using PillarStability.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace PillarStability.Services
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

        public static bool SavePillarModel(string path, List<PillarModel> pillars)
        {
            string csv = "Pillar,Height [m],Width [m],Length [m],APS [MPa],UCS [MPa]\n";

            for (int i = 0; i <= pillars.Count - 1; i++)
            {
                string nl = pillars[i].Name + ",";
                nl += pillars[i].Height + ",";
                nl += pillars[i].Width + ",";
                nl += pillars[i].Length + ",";
                nl += pillars[i].APS + ",";
                nl += pillars[i].UCS + "\n";

                csv += nl;
            }

            File.WriteAllText(path, csv);

            return true;
        }
    }
}
