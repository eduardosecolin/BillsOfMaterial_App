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
    /// Lógica interna para ItemsSelectedView.xaml
    /// </summary>
    public partial class ItemsSelectedView : Window
    {
        RegisterItem _window;
        ItemsService itemsService;
        NCMService ncmService;

        public ItemsSelectedView(RegisterItem window)
        {
            InitializeComponent();
            _window = window;
            itemsService = new ItemsService();
            ncmService = new NCMService();
            LoadGrid();
        }

        private void LoadGrid()
        {
            try
            {
                dgItems.ItemsSource = itemsService.GetAll();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " - Erro ao consultar dados", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DgItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgItems.SelectedItems.Count > 0)
            {
                var items = dgItems.SelectedItem as MA_Items;
                if (items != null)
                {
                    txtItem.Text = items.Item;
                }
            }
        }

        private void TxtFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtFilter.Text != string.Empty)
            {
                dgItems.ItemsSource = null;
                dgItems.ItemsSource = itemsService.GetAll(txtFilter.Text);
            }
            else
            {
                LoadGrid();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtItem.Text != string.Empty)
                {
                    var item = itemsService.GetItem(txtItem.Text);
                    if (item != null)
                    {
                        _window.txtItem.Text = item.Item;
                        _window.txtUoM.Text = item.BaseUoM;
                        _window.txtDescription.Text = item.Description;
                        _window.txtNCM.Text = ncmService.GetNCM(item.Item);
                        _window.txtStandardCost.Text = itemsService.GetStandardCost(item.Item).ToString();
                        int? nature = itemsService.GetNatureItem(item.Item);
                        if (nature == 22413314)
                        {
                            _window.cbNature.SelectedIndex = 0;
                        }
                        else if (nature == 22413313)
                        {
                            _window.cbNature.SelectedIndex = 1;
                        }
                        else if(nature == 22413312)
                        {
                            _window.cbNature.SelectedIndex = 2;
                        }

                        _window.EnabledFields();
                        _window.isEdit = true;

                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
