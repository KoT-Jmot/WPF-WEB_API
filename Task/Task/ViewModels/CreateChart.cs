using System;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace Task.ViewModels
{
    public class CreateChart
    {
        public static void GetChart(DateTime[] axisXData, double[] axisYData, Chart chart_chart)
        {


            chart_chart.Series.Clear();
            chart_chart.Series.Add(new Series("Series1"));
            chart_chart.Series["Series1"].ChartArea = "Default";
            chart_chart.Series["Series1"].ChartType = SeriesChartType.Line;
            chart_chart.Series["Series1"].BorderWidth = 3;

            chart_chart.ChartAreas[0].AxisY.Maximum = axisYData.Max();
            chart_chart.ChartAreas[0].AxisY.Minimum = axisYData.Min();
            chart_chart.Series["Series1"].Points.DataBindXY(axisXData, axisYData);
        }
    }
}
