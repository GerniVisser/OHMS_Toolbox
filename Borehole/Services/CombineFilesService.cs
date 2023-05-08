using Borehole.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borehole.Services
{
    internal class CombineFilesService
    {
        private LitModel _litModel;
        private GeoTechModel _geoModel;

        public CombineFilesService(LitModel litFilePath, GeoTechModel geoFilePath)
        {
            _litModel = litFilePath;
            _geoModel = geoFilePath;
        }

        public void Combine(string outFilePath)
        {
            // read Lit file
            var litRecords = new List<string[]>();
            using (var reader = new StreamReader(_litModel.filepath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    litRecords.Add(values);
                }
            }

            // read Geo file
            var geoRecords = new List<string[]>();
            using (var reader = new StreamReader(_geoModel.filepath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    geoRecords.Add(values);
                }
            }

            // Combine files 
            var result = from d1 in litRecords
                         join d2 in geoRecords
                         on d1[_litModel.IdColumn] equals d2[_geoModel.IdColumn]
                         where Decimal.Parse(d1[_litModel.toColumn], CultureInfo.InvariantCulture) >= Decimal.Parse(d2[_geoModel.distColumn], CultureInfo.InvariantCulture)
                         && Decimal.Parse(d1[_litModel.fromColumn], CultureInfo.InvariantCulture) <= Decimal.Parse(d2[_geoModel.distColumn], CultureInfo.InvariantCulture)
                         select new { ID = d1[_litModel.IdColumn], Value = d2[_geoModel.distColumn], Cate = d1[_litModel.typeColumn] };

            // Write to file
            using (StreamWriter writer = new StreamWriter(outFilePath))
            {
                // Write the header row
                writer.WriteLine("ID,Value,Rock Type");

                // Write the data rows
                foreach (var item in result)
                {
                    writer.WriteLine($"{item.ID},{item.Value},{item.Cate}");
                }
            }
        }
    }
}
