using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.Models
{
    public class ReportModel
    {
        public Wall footwall { get; set; }
        public ChartStreamModel footwallStream{ get; set; }
        public Wall hangwall { get; set; }
        public ChartStreamModel hangwallStream{ get; set; }
        public Wall stopeback { get; set; }
        public ChartStreamModel stopebackStream { get; set; }
        public Wall strikeend { get; set; }
        public ChartStreamModel strikeendStream { get; set; }
    }

    public class ChartStreamModel
    {
        public Stream A_chartStream { get; set; }
        public Stream B_chartStream { get; set; }
        public Stream C_chartStream { get; set; }
        public Stream HR_chartStream { get; set; }
    }
}
