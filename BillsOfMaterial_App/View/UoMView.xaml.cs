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
using BillsOfMaterial_App.Model;

namespace BillsOfMaterial_App.View
{
    /// <summary>
    /// Lógica interna para UoMView.xaml
    /// </summary>
    public partial class UoMView : Window
    {

        private UoMService uomService;
        private RegisterItem _window;
        private OperationView opWindow;

        public UoMView(RegisterItem window)
        {
            InitializeComponent();
            _window = window;
            uomService = new UoMService();
            LoadGrid();
        }

        public UoMView(OperationView window)
        {
            InitializeComponent();
            opWindow = window;
            uomService = new UoMService();
            LoadGrid();
        }

        private void LoadGrid()
        {
            dgUoM.ItemsSource = null;

            if(txtFilter.Text == string.Empty)
            {
                dgUoM.ItemsSource = uomService.GetAll();
            }
            else
            {
                dgUoM.ItemsSource = uomService.GetAllByParams(txtFilter.Text);
            }
        }

        private void TxtFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            LoadGrid();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_window != null && opWindow == null)
                {
                    _window.txtUoM.Text = txtUoM.Text;
                }
                else
                {
                    opWindow.txtUoM.Text = txtUoM.Text;
                }
                    this.Close();               
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n" + ex.StackTrace,
                    "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DgUoM_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgUoM.SelectedItems.Count > 0)
            {
                var uom = dgUoM.SelectedItem as MA_UnitsOfMeasure;
                if (uom != null)
                {
                    txtUoM.Text = uom.BaseUoM;
                }
            }
        }
    }
}
