using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Optimization;
using MathNet.Numerics.Optimization.ObjectiveFunctions;

namespace L1
{
    class FunctionOptimization
    {
        public struct MinResult
        {
            public double MinF;
            public double[] MinX;

            public int Steps;
            public bool Succeded;
        }

        public enum MinMethod
        {
            Gradient,
            BFGS,
            BFGS_B,
            Simplex
        }

        public IFunction F { get; init; } = default(IFunction);
        public double Tolerance { get; set; } = 1e-8;
        public int MaxIterations { get; set; } = 1000000;
        public bool UseFGradient { get; set; } = true;
        public MinMethod Method { get; set; } = MinMethod.Gradient;

        public FunctionOptimization(IFunction f)
        {
            F = f;
        }

        public MinResult FindMin(double[] x0, double[] lowerBound = default(double[]), double[] upperBound = default(double[]))
        {
            MinResult min;

             var loB = new Lazy<double[]>(() =>
             {
                if (lowerBound == default(double[]))
                {
                    lowerBound = new double[x0.Length];
                    for (var i = 0; i < lowerBound.Length; i++)
                    {
                        lowerBound[i] = (double.MaxValue - 1) * (-1);
                    }
                }

                return lowerBound;
            });

            var upB = new Lazy<double[]>(() =>
            {
                if (upperBound == default(double[]))
                {
                    upperBound = new double[x0.Length];
                    for (var i = 0; i < upperBound.Length; i++)
                    {
                        upperBound[i] = (double.MaxValue - 1);
                    }
                }

                return upperBound;
            });

            var objective = default(IObjectiveFunction);

            if ((F is IFunctionWithGradient) && UseFGradient)
            {
                var gf = F as IFunctionWithGradient;
                
                objective = ObjectiveFunction.Gradient(x => gf.CalcValue(x.ToArray()),
                                                       x => Vector<double>.Build.Dense(gf.CalcGradient(x.ToArray())));
            }
            else
            {
                objective = ObjectiveFunction.Value(x => F.CalcValue(x.ToArray()));
                objective = new ForwardDifferenceGradientObjectiveFunction(objective,
                                                                           Vector<double>.Build.Dense(loB.Value),
                                                                           Vector<double>.Build.Dense(upB.Value));
            }

            try
            {
                switch (Method)
                {
                    case MinMethod.Gradient:
                        {
                            var alg = new ConjugateGradientMinimizer(Tolerance, MaxIterations);
                            var result = alg.FindMinimum(objective, Vector<double>.Build.Dense(x0));

                            min.MinX = result.MinimizingPoint.ToArray();
                            min.MinF = F.CalcValue(min.MinX);
                            min.Steps = result.Iterations;
                            min.Succeded = result.ReasonForExit != ExitCondition.None &&
                                           result.ReasonForExit != ExitCondition.InvalidValues &&
                                           result.ReasonForExit != ExitCondition.ExceedIterations;
                            break;
                        }
                    case MinMethod.BFGS:
                        {
                            var alg = new BfgsMinimizer(Tolerance, Tolerance, Tolerance, MaxIterations);
                            var result = alg.FindMinimum(objective,
                                                         Vector<double>.Build.Dense(x0));


                            min.MinX = result.MinimizingPoint.ToArray();
                            min.MinF = F.CalcValue(min.MinX);
                            min.Steps = result.Iterations;
                            min.Succeded = result.ReasonForExit != ExitCondition.None &&
                                           result.ReasonForExit != ExitCondition.InvalidValues &&
                                           result.ReasonForExit != ExitCondition.ExceedIterations;
                            break;
                        }
                    case MinMethod.BFGS_B:
                        {
                            var alg = new BfgsBMinimizer(Tolerance, Tolerance, Tolerance, MaxIterations);
                            var result = alg.FindMinimum(objective,
                                                         Vector<double>.Build.Dense(loB.Value),
                                                         Vector<double>.Build.Dense(upB.Value),
                                                         Vector<double>.Build.Dense(x0));


                            min.MinX = result.MinimizingPoint.ToArray();
                            min.MinF = F.CalcValue(min.MinX);
                            min.Steps = result.Iterations;
                            min.Succeded = result.ReasonForExit != ExitCondition.None &&
                                           result.ReasonForExit != ExitCondition.InvalidValues &&
                                           result.ReasonForExit != ExitCondition.ExceedIterations;
                            break;
                        }
                    case MinMethod.Simplex:
                        {
                            var result = NelderMeadSimplex.Minimum(objective, Vector<double>.Build.Dense(x0), Tolerance, MaxIterations);

                            min.MinX = result.MinimizingPoint.ToArray();
                            min.MinF = F.CalcValue(min.MinX);
                            min.Steps = result.Iterations;
                            min.Succeded = result.ReasonForExit != ExitCondition.None &&
                                           result.ReasonForExit != ExitCondition.InvalidValues &&
                                           result.ReasonForExit != ExitCondition.ExceedIterations;
                            break;
                        }

                    default:
                        min = default(MinResult);
                        min.Succeded = false;
                        break;
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Exception: {e.Message}");

                min = default(MinResult);
                min.Succeded = false;
            }

            return min;
        }
    }
}
