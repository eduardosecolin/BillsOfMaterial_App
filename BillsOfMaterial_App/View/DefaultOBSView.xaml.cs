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
    /// Lógica interna para DefaultOBSView.xaml
    /// </summary>
    public partial class DefaultOBSView : Window
    {

        private DefaultObsService dfService;
        private OperationView _window;

        public DefaultOBSView(OperationView window)
        {
            InitializeComponent();
            _window = window;
            dfService = new DefaultObsService();
            LoadGrid();
        }

        private void LoadGrid()
        {
            try
            {
                dgObs.ItemsSource = null;
                dgObs.ItemsSource = dfService.GetAll(txtFilter.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados!" + "\n" + ex.InnerException.Message, "Erro",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TxtFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            LoadGrid();
        }

        private void DgObs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgObs.SelectedItems.Count > 0)
            {
                var obs = dgObs.SelectedItem as CS_DBDefaultOBS;
                if (obs != null)
                {
                    txtOBS.Text = obs.Observation;
                    txtId.Text = obs.Id.ToString();
                }
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _window.txtObs.Text = txtOBS.Text;
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
