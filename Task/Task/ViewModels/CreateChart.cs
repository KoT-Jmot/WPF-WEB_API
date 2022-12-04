using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Media;

namespace Task.ViewModels
{
    public class CreateChart
    {
        public static void GetChart(DateTime[] axisXData, List<double> axisYData, Chart chart_chart)
        {


            chart_chart.Series.Clear();
            chart_chart.Series.Add(new Series("Series1"));
            chart_chart.Series["Series1"].ChartArea = "Default";
            chart_chart.Series["Series1"].ChartType = SeriesChartType.Line;
            chart_chart.Series["Series1"].BorderWidth = 3;
            axisYData = axisYData.Select(a => Math.Round(a, 4)).ToList();
            chart_chart.ChartAreas[0].AxisY.Maximum = axisYData.Max() + 0.1;
            chart_chart.ChartAreas[0].AxisY.Minimum = axisYData.Min() - 0.1;
            chart_chart.Series["Series1"].Points.DataBindXY(axisXData, axisYData.ToArray());
            chart_chart.Series["Series1"].Points.FindMinByValue().LabelForeColor = System.Drawing.Color.Red;
            chart_chart.Series["Series1"].Points.FindMinByValue().LabelBackColor = System.Drawing.Color.White;
            chart_chart.Series["Series1"].Points.FindMinByValue().Label = $"min\n{axisYData.Min()}";
            chart_chart.Series["Series1"].Points.FindMaxByValue().LabelForeColor = System.Drawing.Color.Red;
            chart_chart.Series["Series1"].Points.FindMaxByValue().LabelBackColor = System.Drawing.Color.White;
            chart_chart.Series["Series1"].Points.FindMaxByValue().Label = $"max\n{axisYData.Max()}";
        }
    }
}
