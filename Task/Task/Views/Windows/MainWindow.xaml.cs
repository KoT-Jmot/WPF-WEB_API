using System;
using System.Windows;
using System.Windows.Forms.DataVisualization.Charting;
using Task.Models;

namespace Task
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Cur_DateStart.DisplayDateEnd = DateTime.Now.Date;
            Cur_DateStart.DisplayDateStart = DateTime.Now.AddYears(-5);

            Cur_DateEnd.DisplayDateEnd = DateTime.Now.Date;
            Cur_DateEnd.DisplayDateStart = DateTime.Now.AddYears(-5);
            chartWindow.Opacity = 1;
            chartWindow.IsEnabled = true;
            System.Windows.Controls.Panel.SetZIndex(chartWindow, 10);
            chart_chart.ChartAreas.Add(new ChartArea("Default"));
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Index.index(SelectedCurrency.Text, Cur_DateStart.Text, Cur_DateEnd.Text, chart_chart, ExpectionText);
        }

    }
}

