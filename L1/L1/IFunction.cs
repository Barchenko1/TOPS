using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L1
{
    interface IFunction
    {
        double CalcValue(double[] x);
    }

    interface IFunctionWithGradient : IFunction
    {
        double[] CalcGradient(double[] x);
    }
}
