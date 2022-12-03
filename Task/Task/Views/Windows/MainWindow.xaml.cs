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

            Cur_DateStart.Text = Properties.Settings.Default.DateStart;
            Cur_DateEnd.Text = Properties.Settings.Default.DateEnd;
            SelectedCurrency.SelectedIndex = Properties.Settings.Default.Cur;
            Windows.Width = Properties.Settings.Default.SizeWindowWidth;
            Windows.Height = Properties.Settings.Default.SizeWindowHeight;
            Windows.Left = Properties.Settings.Default.WidnowLeft;
            Windows.Top = Properties.Settings.Default.WindowTop;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Index.index(SelectedCurrency.Text, Cur_DateStart.Text, Cur_DateEnd.Text, chart_chart, ExpectionText);
        }
        private void Windows_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.DateStart = Cur_DateStart.Text;
            Properties.Settings.Default.DateEnd = Cur_DateEnd.Text;
            Properties.Settings.Default.Cur = SelectedCurrency.SelectedIndex;
            Properties.Settings.Default.SizeWindowWidth = Windows.Width;
            Properties.Settings.Default.SizeWindowHeight = Windows.Height;
            Properties.Settings.Default.WidnowLeft = Windows.Left;
            Properties.Settings.Default.WindowTop = Windows.Top;
            Properties.Settings.Default.Save();

        }
    }
}

