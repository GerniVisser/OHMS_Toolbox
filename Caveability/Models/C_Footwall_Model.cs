using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.Models
{
    class C_Footwall_Model : C_Model
    {
        public C_Footwall_Model(float c) : base(c)
        {
        }

        public override float graphCalcutation(double point)
        {
            if (point >= 0 && point < 30)
            {
                return 7.98f;
            }
            else if (point >= 30 && point < 90)
            {
                float single = -0.0667f;
                float constant = 10f;

                float result = (float)(single * point + constant);

                return result;
            }
            else
            {
                return 4;
            }
        }
    }
}
