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
    /// Lógica interna para EngSearchView.xaml
    /// </summary>
    public partial class OfferSearchView : Window
    {
        MainWindow _mainWindow;
        CustQuotasService service;

        public OfferSearchView(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            service = new CustQuotasService();
            LoadGrid();
        }

        private void LoadGrid()
        {
            try
            {
                dgOffer.ItemsSource = null;
                dgOffer.ItemsSource = service.GetCustQuota();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados!" + "\n" + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TxtFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtFilter.Text != string.Empty)
            {
                dgOffer.ItemsSource = null;
                dgOffer.ItemsSource = service.GetByQuotationNo(txtFilter.Text);
            }
        }

        private void DgOffer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(dgOffer.SelectedItems.Count > 0)
            {
                var custquota = dgOffer.SelectedItem as MA_CustQuotas;
                if(custquota != null)
                {
                    _mainWindow.lblNoCustQuota.Content = "Oferta Nº: " + custquota.QuotationNo;
                    _mainWindow.lblCustQuotaId.Content = custquota.CustQuotaId.ToString();
                    _mainWindow.LoadCbItemGrid();
                    this.Close();
                }
            }
        }
    }
}
