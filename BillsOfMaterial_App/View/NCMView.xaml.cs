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
    /// Lógica interna para NCMView.xaml
    /// </summary>
    public partial class NCMView : Window
    {
        private NCMService ncmService;
        private RegisterItem _window;

        public NCMView(RegisterItem window)
        {
            InitializeComponent();
            _window = window;
            ncmService = new NCMService();
            LoadGrid();
        }

        private void LoadGrid()
        {
            dgNCM.ItemsSource = null;

            if(txtFilter.Text == string.Empty)
            {
                dgNCM.ItemsSource = ncmService.GetAll();
            }
            else
            {
                dgNCM.ItemsSource = ncmService.GetAllByParams(txtFilter.Text);
            }
        }

        private void TxtFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            LoadGrid();
        }

        private void DgNCM_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgNCM.SelectedItems.Count > 0)
            {
                var ncm = dgNCM.SelectedItem as MA_BRNCM;
                if (ncm != null)
                {
                    txtNCM.Text = ncm.NCM;
                }
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _window.txtNCM.Text = txtNCM.Text;
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n" + ex.StackTrace,
                    "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
