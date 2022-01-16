using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L1
{
    class FunctionApproximation : IFunction
    {
        public double[,] Table { get; init; }
        public int PolinomDegree { get; init; }
        public double[] X0 => CalcInitialGuess();

        public FunctionApproximation(double[,] table, int polinomDegree)
        {
            Debug.Assert(polinomDegree >= 0 && polinomDegree < table.GetLength(1));

            Table = table;
            PolinomDegree = polinomDegree;
        }

        public double CalcValue(double[] a)
        {
            Debug.Assert(a.Length == (PolinomDegree + 1));

            double ls = 0;
            for (var i = 0; i < Table.GetLength(1); i++)
            {
                var x = Table[0, i];
                var y = Table[1, i];

                var pv = CalcPolinomValue(a, x);
                var sq = Math.Pow(y - pv, 2);

                ls += sq;
            }

            return ls;
        }

        static public double CalcPolinomValue(double[] a, double x)
        {
            double y = 0;
            for (var i=0; i<a.Length; i++)
            {
                y += a[i] * Math.Pow(x, i);
            }

            return y;
        }

        double[] CalcInitialGuess()
        {
            var x0 = new double[PolinomDegree + 1];
            for (var i = 0; i < x0.Length; i++)
            {
                x0[i] = 1;
            }

            return x0;
        }

    }
}
