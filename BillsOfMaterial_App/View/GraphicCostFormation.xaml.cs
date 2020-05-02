using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BillsOfMaterial_App.View
{
    /// <summary>
    /// Lógica interna para GraphicCostFormation.xaml
    /// </summary>
    public partial class GraphicCostFormation : Window
    {

        CostFormationView window;

        public GraphicCostFormation(CostFormationView _window)
        {
            InitializeComponent();
            window = _window;
            FillChart();
        }

        private void FillChart()
        {
            try
            {
                double? totalMarkup = string.IsNullOrEmpty(window.txtTotalMarkup.Text) ? 0 : Convert.ToDouble(window.txtTotalMarkup.Text);
                double? margin = string.IsNullOrEmpty(window.txtMargin.Text) ? 0 : Convert.ToDouble(window.txtMargin.Text);
                double? df = string.IsNullOrEmpty(window.txtFixedExpenses.Text) ? 0 : Convert.ToDouble(window.txtFixedExpenses.Text);
                double? dv = window.CalculateFieldsViewToMarkupGraphic(Convert.ToDouble(window.unitValue));
                double? freight = string.IsNullOrEmpty(window.txtFreightValue.Text) ? 0 : Convert.ToDouble(window.txtFreightValue.Text);
                double? cost = ((window.unitValue + freight) / totalMarkup) * 100;

                double? df_value = (df != 0) ? (totalMarkup / 100) * df : 0;
                //dv = (dv - cost);
                double? margin_value = 100 - (Math.Round(Convert.ToDouble(df), 2) + Math.Round(Convert.ToDouble(dv), 2) + Math.Round(Convert.ToDouble(cost), 2));

                SeriesCollection series = new SeriesCollection();
                PieSeries serieDF = new PieSeries();
                serieDF.Title = "Despesas Fixas %";
                serieDF.Values = new ChartValues<ObservableValue> { new ObservableValue(Math.Round(Convert.ToDouble(df), 2)) };
                serieDF.DataLabels = true;
                serieDF.Foreground = Brushes.Black;
                //serieDF.LabelPoint = chartPoint =>
                //string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
                serieDF.PushOut = 5;
                series.Add(serieDF);

                PieSeries serieDV = new PieSeries();
                serieDV.Title = "Despesas Variaveis %";
                serieDV.Values = new ChartValues<ObservableValue> { new ObservableValue(Math.Round(Convert.ToDouble(dv), 2)) };
                serieDV.DataLabels = true;
                serieDV.Foreground = Brushes.Black;
                //serieDV.LabelPoint = chartPoint =>
                //string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
                serieDV.PushOut = 5;
                series.Add(serieDV);

                PieSeries serieCost = new PieSeries();
                serieCost.Title = "Custo %";
                serieCost.Values = new ChartValues<ObservableValue> { new ObservableValue(Math.Round(Convert.ToDouble(cost), 2)) };
                serieCost.DataLabels = true;
                serieCost.Foreground = Brushes.Black;
                //serieCost.LabelPoint = chartPoint =>
                //string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
                serieCost.PushOut = 5;
                series.Add(serieCost);

                PieSeries serieMargin = new PieSeries();
                serieMargin.Title = "Lucro %";
                serieMargin.Values = new ChartValues<ObservableValue> { new ObservableValue(Math.Round(Convert.ToDouble(margin_value), 2)) };
                serieMargin.DataLabels = true;
                serieMargin.Foreground = Brushes.Black;
                //serieMargin.LabelPoint = chartPoint =>
                //string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
                serieMargin.PushOut = 5;
                series.Add(serieMargin);

                Pizza.Series = series;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao realizar grafico!" + "\n" + ex.Message, "Erro",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
