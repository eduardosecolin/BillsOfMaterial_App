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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BillsOfMaterial_App.Model;
using BillsOfMaterial_App.Service;

namespace BillsOfMaterial_App.AuxView
{
    /// <summary>
    /// Interação lógica para SettingsView.xam
    /// </summary>
    public partial class SettingsView : UserControl
    {

        private readonly OnOffService onOffService;

        public SettingsView()
        {
            InitializeComponent();
            onOffService = new OnOffService();
            FillGrid();
        }

        private void FillGrid()
        {
            try
            {
                dgOnOff.ItemsSource = null;
                dgOnOff.ItemsSource = onOffService.GetDataFiltered();
            }catch(Exception ex)
            {
                MessageBox.Show("Erro ao carregar grid! \n" + ex.Message , "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btUnlock_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var obj = dgOnOff.SelectedItem as CS_OnOffValidate;
                if(obj != null)
                {
                    OnOffService.Update((int)obj.Id_Offer, obj.OfferNo, obj.Item, false);
                    FillGrid();
                }
                else
                {
                    MessageBox.Show("Escolha um registro!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro \n" + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
