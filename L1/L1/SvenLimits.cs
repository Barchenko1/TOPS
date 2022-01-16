using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L1
{
    class SvenLimits
    {
        public IFunction F { get; init; } = default(IFunction);

        public SvenLimits(IFunction f)
        {
            F = f;
        }

        public void FindLimits(double[] x0, double initialDelta, out double[] lowerBound, out double[] upperBound)
        {
            lowerBound = new double[x0.Length];
            upperBound = new double[x0.Length];

            var x = x0.Clone() as double[];

            for (var i = 0; i < x0.Length; i++)
            {
                double firstPart = 0.01 * Math.Pow(x[1] - x.Length, 6);
                var step = 0;
                var delta = initialDelta;
                var prev_x = x[i];

                var f_current = firstPart + F.CalcValue(x);
                x[i] = prev_x + delta;
                var f_next = firstPart + F.CalcValue(x);

                if (f_next > f_current)
                {
                    delta *= -1;
                    x[i] = prev_x + delta;
                    f_next = firstPart + F.CalcValue(x);
                }

                while (f_next < f_current)
                {
                    step++;
                    f_current = f_next;

                    prev_x = x[i];
                    delta = initialDelta * Math.Pow(2, step);
                    x[i] = prev_x + delta;

                    f_next = firstPart + F.CalcValue(x);
                }

                upperBound[i] = Math.Max(x[i], prev_x);
                lowerBound[i] = Math.Min(x[i], prev_x);
                x[i] = (upperBound[i] + lowerBound[i]) / 2;
            }
        }
    }
}
