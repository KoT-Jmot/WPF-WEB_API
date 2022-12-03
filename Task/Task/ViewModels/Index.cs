using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Controls;
using System.Windows.Forms.DataVisualization.Charting;
using Task.ViewModels;

namespace Task.Models
{
    internal class Index
    {
        private static string localhost = "http://localhost:5000/index/";//;
        public static void index(string Cur, string Cur_DateStart, string Cur_DateEnd, Chart chart_chart, Label ExpectionText)
        {
            DateTime? start = null, end = null;
            ExpectionText.Content = ""; //You mast enter your localhost
            try
            {

                try
                {
                    start = DateTime.Parse(Cur_DateStart);
                    end = DateTime.Parse(Cur_DateEnd);
                    if (start >= end)
                        throw new Exception();
                }
                catch
                {
                    ExpectionText.Content = DateTime.Now + " Ввод даты осуществлён неверно";
                    return;
                }

                string url = localhost + $"{Cur}/{start.Value.ToString("yyyy/MM/dd")}/{end.Value.ToString("yyyy/MM/dd")}";
                string json = Encoding.UTF8.GetString(new WebClient().DownloadData(url));

                if (json == null)
                    throw new Exception("Server problems");
                var model = JsonConvert.DeserializeObject<List<Currency>>(json).OrderBy(c=>c.Date).ToList();

                DateTime[] axisXData = model.Select(c => c.Date).ToArray();
                double[] axisYData = model.Select(c => c.Cur_OfficialRate).ToArray();
                CreateChart.GetChart(axisXData, axisYData.ToList(), chart_chart);
            }
            catch (Exception Q)
            {
                ExpectionText.Content = DateTime.Now + " " + Q.Message;
                return;
            }

        }
    }
}
