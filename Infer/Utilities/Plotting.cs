using Microsoft.FSharp.Collections;
using Plotly.NET;
using Plotly.NET.LayoutObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infer.Utilities
{
    public static class Plotting
    {
        public static void GetBinomialDistribution(int[] xAxisValues, int[] yAxisValues)
        {
            LinearAxis xAxis = new LinearAxis();
            xAxis.SetValue("title", "P|X=k|");
            xAxis.SetValue("showline", true);
            xAxis.SetValue("gridcolor", "#ffff");
            xAxis.SetValue("zerolinewidth", 2);
            xAxis.SetValue("zerolinecolor", "#ffff");

            LinearAxis yAxis = new LinearAxis();
            yAxis.SetValue("title", "k");
            yAxis.SetValue("showline", true);
            yAxis.SetValue("gridcolor", "#ffff");
            yAxis.SetValue("zerolinewidth", 2);
            yAxis.SetValue("zerolinecolor", "#ffff");

            Layout layout = new Layout();
            layout.SetValue("xaxis", xAxis);
            layout.SetValue("yaxis", yAxis);
            layout.SetValue("title", "A Figure Specified by DynamicObj");
            layout.SetValue("plot_bgcolor", "#e5ecf6");
            layout.SetValue("showlegend", true);

            Trace trace = new Trace("bar");
            trace.SetValue("x", xAxisValues);
            trace.SetValue("y", yAxisValues);


            var fig = GenericChart.Figure.create(ListModule.OfSeq(new[] { trace }), layout);
            GenericChart.fromFigure(fig).Show();
        }

        public static void GenerateHistogram(int[] xAxisValues, string chartTitle = "")
        {
            Chart2D.Chart.Histogram<int, string>(xAxisValues, StyleParam.Orientation.Vertical)
                    .WithTitle(chartTitle)
                    .Show();
        }

        public static void GenerateHistogram(double[] xAxisValues, string chartTitle = "")
        {
            Chart2D.Chart.Histogram<double, string>(xAxisValues, StyleParam.Orientation.Vertical)
                    .WithTitle(chartTitle)
                    .Show();
        }

        public static void GenerateHistogram1(double[] xAxisValues, string chartTitle = "")
        {

            var histogram = Chart2D.Chart.Histogram<double, string>(xAxisValues, StyleParam.Orientation.Vertical);
            histogram.WithTitle(chartTitle);



        }

        public static void GetDistribution(decimal[] xAxisValues, decimal[] yAxisValues)
        {
            Chart2D.Chart.Line<decimal, decimal, string>(xAxisValues.ToList(), yAxisValues.ToList()).Show();

        }


    }
}
