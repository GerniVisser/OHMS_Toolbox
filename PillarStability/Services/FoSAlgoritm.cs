using PillarStability.DataObjects;
using PillarStability.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.Services
{
    public interface FoSAlgoritm
    {
        public float Calculate(Func<float, float, float> getExcelNormInv, PillarModel pillarModel);
        public MonteCarloDataObject GenerateSummaryObject(PillarModel pillarModel, List<float> fosList);
    }
}
