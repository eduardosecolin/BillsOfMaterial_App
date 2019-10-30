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
        ItemsView _window;
        ItemsService service;
        DrawingService dwService;

        public DrawingView(ItemsView window)
        {
            InitializeComponent();
            _window = window;
            service = new ItemsService();
            dwService = new DrawingService();
            LoadGrid();
        }

        private void LoadGrid()
        {
            try
            {
                dgDrawing.ItemsSource = service.GetAllOldItem();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " - Erro ao consultar dados", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TxtFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtFilter.Text != string.Empty)
            {
                dgDrawing.ItemsSource = null;
                dgDrawing.ItemsSource = service.GetAllOldItemByParam(txtFilter.Text);
            }
            else
            {
                LoadGrid();
            }
        }

        private void DgDrawing_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgDrawing.SelectedItems.Count > 0)
            {
                var items = dgDrawing.SelectedItem as MA_Items;
                if (items != null)
                {
                    txtDrawing.Text = items.OldItem;
                }
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(txtDrawing.Text != string.Empty)
            {
                _window.txtDrawing.Text = txtDrawing.Text;
                this.Close();
            }
            else
            {
                MessageBox.Show("Selecione um desenho!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
