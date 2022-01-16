using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L1
{
    class TestPlan
    {
        public int[] N { get; init; }
        public FunctionOptimization.MinResult[] Result { get; init; }
        public double[] CalculationPeriodMs { get; init; }
        public double[,] CalculationsTable => BuildCalculationsTable();
        public bool Succeeded { get; set; } = false;

        public TestPlan(int[] n)
        {
            Debug.Assert(n.Length > 0);

            N = n;
            Result = new FunctionOptimization.MinResult[n.Length];
            CalculationPeriodMs = new double[n.Length];
        }

        double[,] BuildCalculationsTable()
        {
            var count = Result.Where(r => r.Succeded).Count();

            var calculationTable = new double[2, count];
            for (int test = 0, tableIndex = 0; test < N.Length; test++, tableIndex++)
            {
                if (Result[test].Succeded)
                {
                    calculationTable[0, tableIndex] = test + 1;
                    calculationTable[1, tableIndex] = CalculationPeriodMs[test];
                }
            }

            return calculationTable;
        }
    }

    class ApproxPlan
    {
        public int[] PolinomDegrees { get; init; }
        public FunctionOptimization.MinResult[] PolinomA { get; init; }
        public int BestApproxPolinom => CalcBestPolinom();
        public bool Succeeded { get; set; } = false;

        public ApproxPlan(int[] polinomDegrees)
        {
            Debug.Assert(polinomDegrees.Length > 0);

            PolinomDegrees = polinomDegrees;
            PolinomA = new FunctionOptimization.MinResult[polinomDegrees.Length];
        }
        
        int CalcBestPolinom()
        {
            Debug.Assert(PolinomA.Length > 0);

            var min = double.MaxValue;
            var minIndex = -1;

            if (Succeeded)
            {
                for (var i = 0; i < PolinomA.Length; i++)
                {
                    if (PolinomA[i].Succeded && 
                        PolinomA[i].MinF < min)
                    {
                        min = PolinomA[i].MinF;
                        minIndex = i;
                    }
                }
            }

            return minIndex;
        }
    }
}
