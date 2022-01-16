using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L1
{
    public partial class MainForm : Form
    {
        //TestPlan TestPlan { get; init; } = new TestPlan(new int[] { 2, 3, 4, 5, 6, 8, 10, 12, 15/*, 30*/ });
        TestPlan TestPlan { get; init; } = new TestPlan(new int[] { 2, 3, 4, 6, 8, 10, 15, 20, 30 });

        ApproxPlan ApproxPlan { get; init; } = new ApproxPlan(new int[] { 0, 1, 2, 3, 4 });

        public MainForm()
        {
            InitializeComponent();
            ColdStart();
        }

        #region Methods

        void ColdStart()
        {
            // do dummy calculation at first call (avoid cold start problem)
            var f1 = new Function(TestPlan.N[7]);
            var op1 = new FunctionOptimization(f1) { Method = FunctionOptimization.MinMethod.BFGS };
            op1.FindMin(f1.X0);

            var f2 = new Function(TestPlan.N[1]);
            var op2 = new FunctionOptimization(f2) { Method = FunctionOptimization.MinMethod.Gradient };
            op2.FindMin(f2.X0);
        }

        void Run()
        {
            for (var test = 0; test < TestPlan.N.Length; test++)
            {
                var f = new Function(TestPlan.N[test]);
                var op = new FunctionOptimization(f) { Tolerance = 1e-8, Method = FunctionOptimization.MinMethod.Gradient };

                var sw = new Stopwatch();
                sw.Start();

                TestPlan.Result[test] = op.FindMin(f.X0);

                sw.Stop();

                TestPlan.CalculationPeriodMs[test] = sw.Elapsed.TotalMilliseconds;
            }
            TestPlan.Succeeded = true;

            for (var approx = 0; approx < ApproxPlan.PolinomDegrees.Length; approx++)
            {
                var resultTable = TestPlan.CalculationsTable;
                var aporxDegree = ApproxPlan.PolinomDegrees[approx];
                var approxF = new FunctionApproximation(resultTable, aporxDegree);

                var op = new FunctionOptimization(approxF) { Tolerance = 1e-4, Method = FunctionOptimization.MinMethod.BFGS };
                ApproxPlan.PolinomA[approx] = op.FindMin(approxF.X0);
            }
            ApproxPlan.Succeeded = true;
        }

        void UpdateControls()
        {
            // Test plan list
            {
                m_list.BeginUpdate();
                m_list.Items.Clear();

                if (TestPlan.Succeeded)
                {
                    for (var test = 0; test < TestPlan.N.Length; test++)
                    {
                        var minX = TestPlan.Result[test].Succeded ? string.Join("; ", TestPlan.Result[test].MinX.Select(x => $"{x:F3}").ToArray()) : string.Empty;

                        m_list.Items.Add(new ListViewItem(new string[] { $"{test + 1, 3}",
                                                                         $"{TestPlan.N[test], 3}",
                                                                         $"{TestPlan.CalculationPeriodMs[test]:F3}",
                                                                         $"{TestPlan.Result[test].Steps}",
                                                                         $"{TestPlan.Result[test].MinF:G4}",
                                                                         minX,
                                                                         $"{TestPlan.Result[test].Succeded}" }));
                    }

                    m_columnNum.Width = -2;
                    m_columnDimension.Width = -2;
                    m_columnCalculationPeriod.Width = -2;
                    m_columnCalculationSteps.Width = -2;
                    m_columnMinF.Width = -2;
                    m_columnMinX.Width = -2;
                    m_columnValid.Width = -2;
                }

                m_list.EndUpdate();
            }

            // Approx Plan list
            {
                m_approxList.BeginUpdate();
                m_approxList.Items.Clear();

                if (ApproxPlan.Succeeded)
                {
                    var bestApprox = ApproxPlan.BestApproxPolinom;
                    var aCount = ApproxPlan.PolinomA[bestApprox].MinX.Length;

                    for (var aIndex = 0; aIndex < aCount; aIndex++)
                    {
                        m_approxList.Items.Add(new ListViewItem(new string[] { $"{aIndex, 3}",
                                                                               $"{ApproxPlan.PolinomA[bestApprox].MinX[aIndex]:F3}"}));
                    }
                }
                m_approxList.EndUpdate();
            }
        }

        void BuildGraph()
        {
            var img = new Bitmap(m_graphBox.Width, m_graphBox.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (var graph = Graphics.FromImage(img))
            {
                graph.Clear(Color.White);
                graph.SmoothingMode = SmoothingMode.HighQuality;
                graph.InterpolationMode = InterpolationMode.HighQualityBicubic;

                Func<double, double, PointF> mm2px = (double x, double y) =>
                {
                    var px = new PointF((float)(graph.DpiX / 25.4f * x),
                                        (float)(graph.DpiY / 25.4f * y));
                    return px;
                };

                var margin_px = mm2px(5,5);
                var graphWidth_px = Math.Max(0, img.Width - margin_px.X * 2);
                var graphHeight_px = Math.Max(0, img.Height - margin_px.Y * 2);
                var graphOrign_px = new PointF(margin_px.X, img.Height - margin_px.Y);

                // draw axis
                {
                    var pen = new Pen(Color.Black);
                    pen.EndCap = LineCap.ArrowAnchor;

                    graph.DrawLine(pen, graphOrign_px, new PointF(graphOrign_px.X + graphWidth_px, graphOrign_px.Y));
                    graph.DrawLine(pen, graphOrign_px, new PointF(graphOrign_px.X, graphOrign_px.Y - graphHeight_px));
                }

                if (TestPlan.Succeeded)
                {
                    var table = TestPlan.CalculationsTable;

                    var lowerX = Enumerable.Range(0, table.GetLength(1)).Select(i => table[0, i]).Min();
                    var upperX = Enumerable.Range(0, table.GetLength(1)).Select(i => table[0, i]).Max();
                    var lowerY = Enumerable.Range(0, table.GetLength(1)).Select(i => table[1, i]).Min();
                    var upperY = Enumerable.Range(0, table.GetLength(1)).Select(i => table[1, i]).Max();

                    var minX = 0.0;
                    var maxX = upperX + 1;
                    var minY = 0.0;
                    var maxY = upperY + (upperY - lowerY) * 0.05;

                    Func<double, double, PointF> pt2px = (double x, double y) =>
                    {
                        var px = new PointF((float)((x - minX) * graphWidth_px / (maxX - minX)) + graphOrign_px.X,
                                           -(float)((y - minY) * graphHeight_px / (maxY - minY)) + graphOrign_px.Y);
                        return px;
                    };

                    // draw axis
                    {
                        var font = new Font(FontFamily.GenericSansSerif, 8.0f);
                        var fontBrush = new SolidBrush(Color.Black);

                        var xAxisFormat = new StringFormat() { Alignment = StringAlignment.Center };
                        var yAxisFormat = new StringFormat() { LineAlignment = StringAlignment.Center };

                        var xMarks = Enumerable.Range(0, TestPlan.N.Length).Select(i => Tuple.Create(i + 1, 0)).ToArray();
                        foreach (var mark in xMarks)
                        {
                            var pt = pt2px(mark.Item1, mark.Item2);

                            graph.DrawString($"{mark.Item1:F0}", font, fontBrush, pt, xAxisFormat);
                        }

                        var yMarks = new Tuple<double, double>[] { Tuple.Create(0.0, lowerY), Tuple.Create(0.0, upperY) };
                        foreach (var mark in yMarks)
                        {
                            var pt = pt2px(mark.Item1, mark.Item2);

                            graph.DrawString($"{mark.Item2:F3}", font, fontBrush, pt, yAxisFormat);
                        }
                    }


                    // draw graph
                    {
                        if (ApproxPlan.Succeeded)
                        {
                            var bestApprox = ApproxPlan.BestApproxPolinom;

                            var ptCount = 1000;
                            var stepX = (upperX - lowerX) / ptCount;

                            var pts = new List<PointF>();
                            for (var x = lowerX; x <= upperX; x += stepX)
                            {
                                var y = FunctionApproximation.CalcPolinomValue(ApproxPlan.PolinomA[bestApprox].MinX, x);
                                pts.Add(pt2px(x, y));
                            }

                            var pen = new Pen(Color.BlueViolet, mm2px(0.5f, 0.5f).X);
                            graph.DrawLines(pen, pts.ToArray());
                        }

                        {
                            var ptSize = new SizeF(mm2px(2, 2));
                            var ptHalfSize = new SizeF(ptSize.Width / 2, ptSize.Height / 2);
                            var ptBrush = new SolidBrush(Color.FromArgb(128, Color.Black));
                            var ptPen = new Pen(Color.Black);
                            for (var i = 0; i < table.GetLength(1); i++)
                            {
                                var px = pt2px((float)table[0, i], (float)table[1, i]);

                                var ellipse = new RectangleF(px - ptHalfSize, ptSize);
                                graph.FillEllipse(ptBrush, ellipse);
                                graph.DrawEllipse(ptPen, ellipse);
                            }
                        }
                    }
                }
            }

            m_graphBox.Image?.Dispose();
            m_graphBox.Image = img;
        }

        #endregion

        #region Event Handlers

        private void OnRun(object sender, EventArgs e)
        {
            Run();

            UpdateControls();
            BuildGraph();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            BuildGraph();
        }

        private void OnGraphSize(object sender, EventArgs e)
        {
            BuildGraph();
        }

        #endregion
    }
}
