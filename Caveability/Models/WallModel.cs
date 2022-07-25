using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.Models
{
    public class WallModel
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Q_Model Q_Model { get; set; }
        public A_Model A_Model { get; set; }
        public B_Model B_Model { get; set; }
        public C_Model C_Model { get; set; }
        public HR_Model HR_Model { get; set; }
        public WallModel(string name, bool isFootwall = false)
        {
            _name = name;
            Q_Model = new Q_Model(78, 8, 1, 1);
            A_Model = new A_Model(150, 50);
            B_Model = new B_Model(20);
            C_Model = new C_Model(0, isFootwall);
            HR_Model = new HR_Model(28, 35);
        }

    }
}
