﻿using BillsOfMaterial_App.AuxView;
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
        SimulationEDP _mainWindow;
        CostFormationView _viewCostFormation;
        CustQuotasService service;

        public OfferSearchView(SimulationEDP mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            service = new CustQuotasService();
            LoadGrid();
        }

        public OfferSearchView(CostFormationView viewCostFormation)
        {
            InitializeComponent();
            _viewCostFormation = viewCostFormation;
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
            if (dgOffer.SelectedItems.Count > 0)
            {
                var custquota = dgOffer.SelectedItem as MA_CustQuotas;
                if (custquota != null)
                {
                    if (_mainWindow != null)
                    {
                        _mainWindow.lblNoCustQuota.Content = "Oferta Nº: " + custquota.QuotationNo;
                        _mainWindow.lblCustQuotaId.Content = custquota.CustQuotaId.ToString();
                        _mainWindow.LoadCbItemGrid();
                        this.Close();
                    }
                    else if (_viewCostFormation != null)
                    {
                        _viewCostFormation.txtSelectItem.Text = custquota.QuotationNo;
                        _viewCostFormation.txtCustQuotaId.Text = custquota.CustQuotaId.ToString();
                        _viewCostFormation.LoadCbItemGrid();
                        this.Close();
                    }
                }
            }
        }
    }
}
