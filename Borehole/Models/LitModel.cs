using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borehole.Models
{
    public class LitModel
    {
        public string filepath { get; set; }
        public List<string> colomns { get; set; }
        public int IdColumn { get; set; }
        public int fromColumn { get; set; }
        public int toColumn { get; set; }
        public int typeColumn { get; set; }
    }
}
