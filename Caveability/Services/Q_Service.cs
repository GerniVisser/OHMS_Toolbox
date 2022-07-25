using Caveability.Models;
using SharedWpfLibrary.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.Services
{
    public class Q_Service
    {
        private Q_Model q_Model;

        public Q_Service(Q_Model q)
        {
            q_Model = q;
        }

        public Coord CalcCoordFromX()
        {
            float n = (q_Model.rqd / q_Model.jn) * (q_Model.jr / q_Model.ja);
            return new Coord() { x = 0, y = n};
        }
    }
}
