using PillarStability.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PillarStability.DataObjects
{
    public class PillarSaveDataObject
    {
        public string Name { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public float Length { get; set; }
        public float APS { get; set; }
        public float DesiredFOS { get; set; }
        public Brush Color { get; set; }
    }
}
