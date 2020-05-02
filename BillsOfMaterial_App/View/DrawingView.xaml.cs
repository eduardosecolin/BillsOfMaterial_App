using BillsOfMaterial_App.AuxView;
using BillsOfMaterial_App.Model;
using BillsOfMaterial_App.Service;
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
    /// Lógica interna para DrawingView.xaml
    /// </summary>
    public partial class DrawingView : Window
    {
        SimulationEDP _window;
        ItemsView _windowItem;
        BOMService bomService;
        DrawingService dwService;

        public DrawingView(SimulationEDP window)
        {
            InitializeComponent();
            _window = window;
            bomService = new BOMService();
            dwService = new DrawingService();
            LoadGrid();
        }

        public DrawingView(ItemsView window)
        {
            InitializeComponent();
            _windowItem = window;
            bomService = new BOMService();
            dwService = new DrawingService();
            LoadGrid();
        }

        private void LoadGrid()
        {
            try
            {
                dgDrawing.ItemsSource = null;

                if (txtFilter.Text != string.Empty)
                {
                    dgDrawing.ItemsSource = dwService.GetAll(txtFilter.Text);
                }
                else
                {
                    dgDrawing.ItemsSource = dwService.GetAll();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " - Erro ao consultar dados", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TxtFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            LoadGrid();
        }

        private void DgDrawing_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgDrawing.SelectedItems.Count > 0)
            {
                var items = dgDrawing.SelectedItem as MA_Drawings;
                if (items != null)
                {
                    txtDrawing.Text = items.Drawing;
                }
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtDrawing.Text != string.Empty)
            {
                if (_window != null)
                {
                    _window.txtDrawing.Text = txtDrawing.Text;
                    this.Close();
                }
                else if (_windowItem != null)
                {
                    _windowItem.txtDrawingComponent.Text = txtDrawing.Text;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Selecione um desenho!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
