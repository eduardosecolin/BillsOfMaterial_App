using BillsOfMaterial_App.Model;
using BillsOfMaterial_App.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Lógica interna para RegisterItem.xaml
    /// </summary>
    public partial class RegisterItem : Window
    {

        private bool isNew = false;
        public bool isEdit = false;
        private ItemsService itemService;

        public RegisterItem()
        {
            InitializeComponent();
            itemService = new ItemsService();
            BlockFields();
        }

        private void BlockFields()
        {
            txtItem.IsEnabled = false;
            txtDescription.IsEnabled = false;
            txtNCM.IsEnabled = false;
            txtUoM.IsEnabled = false;
            txtStandardCost.IsEnabled = false;
            cbNature.IsEnabled = false;
            btnSerchUoM.IsEnabled = false;
            btnSerchNCM.IsEnabled = false;
        }

        public void EnabledFields()
        {
            txtItem.IsEnabled = true;
            txtDescription.IsEnabled = true;
            txtStandardCost.IsEnabled = true;
            cbNature.IsEnabled = true;
            btnSerchNCM.IsEnabled = true;
            btnSerchUoM.IsEnabled = true;
        }

        private void TxtStandardCost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BtnSerchUoM_Click(object sender, RoutedEventArgs e)
        {
            UoMView window = new UoMView(this);
            window.Show();
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            EnabledFields();
            isNew = true;
            isEdit = false;
        }

        private void BtnSerchNCM_Click(object sender, RoutedEventArgs e)
        {
            NCMView window = new NCMView(this);
            window.Show();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            ItemsSelectedView window = new ItemsSelectedView(this);
            window.Show();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Validate())
                {
                    if (isNew)
                    {
                        if (itemService.ExistItem(txtItem.Text))
                        {
                            MessageBox.Show($"O item: { txtItem.Text } já existe!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                        else
                        {
                            if (txtItem.Text != string.Empty)
                            {
                                MA_Items item = new MA_Items();
                                item.Item = txtItem.Text;
                                item.Description = txtDescription.Text;
                                item.BaseUoM = txtUoM.Text;
                                string nature = cbNature.Text;
                                if (nature.Equals("Compra", StringComparison.OrdinalIgnoreCase))
                                {
                                    item.Nature = 22413314;
                                }
                                else if (nature.Equals("Produto Acabado", StringComparison.OrdinalIgnoreCase))
                                {
                                    item.Nature = 22413313;
                                }
                                else if (nature.Equals("Semiacabado", StringComparison.OrdinalIgnoreCase))
                                {
                                    item.Nature = 22413312;
                                }

                                item.Disabled = "0";
                                item.TBCreated = DateTime.Now;
                                item.TBModified = DateTime.Now;

                                if (itemService.AddItem(item))
                                {
                                    MA_ItemsFiscalYearData itemFYD = new MA_ItemsFiscalYearData();
                                    itemFYD.Item = txtItem.Text;
                                    itemFYD.StandardCost = Convert.ToDouble(txtStandardCost.Text);
                                    itemFYD.FiscalYear = (short)DateTime.Now.Year;
                                    itemFYD.TBCreated = DateTime.Now;
                                    itemFYD.TBModified = DateTime.Now;

                                    if (itemService.AddItemStandardCost(itemFYD))
                                    {
                                        MA_ItemsBRTaxes itemBR = new MA_ItemsBRTaxes();
                                        itemBR.Item = txtItem.Text;
                                        itemBR.NCM = txtNCM.Text;
                                        itemBR.TBCreated = DateTime.Now;
                                        itemBR.TBModified = DateTime.Now;

                                        if (itemService.AddItemNCM(itemBR))
                                        {
                                            MessageBox.Show($"Item { txtItem.Text } salvo com sucesso!", "Informação",
                                                MessageBoxButton.OK, MessageBoxImage.Information);

                                            ClearFields();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Insira um item!", "Aviso",
                                            MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                    }
                    else if (isEdit)
                    {
                        if (txtItem.Text != string.Empty)
                        {
                            MA_Items item = itemService.GetItem(txtItem.Text);
                            if (item != null)
                            {
                                item.Item = txtItem.Text;
                                item.Description = txtDescription.Text;
                                item.BaseUoM = txtUoM.Text;
                                string nature = cbNature.Text;
                                if (nature.Equals("Compra", StringComparison.OrdinalIgnoreCase))
                                {
                                    item.Nature = 22413314;
                                }
                                else if (nature.Equals("Produto Acabado", StringComparison.OrdinalIgnoreCase))
                                {
                                    item.Nature = 22413313;
                                }
                                else if (nature.Equals("Semiacabado", StringComparison.OrdinalIgnoreCase))
                                {
                                    item.Nature = 22413312;
                                }

                                item.Disabled = "0";
                                item.TBModified = DateTime.Now;

                                if (itemService.EditItem(item))
                                {
                                    var itemFYD = itemService.GetItemFiscalYearData(txtItem.Text);
                                    itemFYD.Item = txtItem.Text;
                                    itemFYD.StandardCost = Convert.ToDouble(txtStandardCost.Text);
                                    itemFYD.TBModified = DateTime.Now;

                                    if (itemService.EditItemStandardCost(itemFYD))
                                    {
                                        var itemBR = itemService.GetItemBRTaxes(txtItem.Text);
                                        itemBR.Item = txtItem.Text;
                                        itemBR.NCM = txtNCM.Text;
                                        itemBR.TBModified = DateTime.Now;

                                        if (itemService.EditItemNCM(itemBR))
                                        {
                                            MessageBox.Show($"Item { txtItem.Text } atualizado com sucesso!", "Informação",
                                                MessageBoxButton.OK, MessageBoxImage.Information);

                                            ClearFields();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao salvar!" + "\n" + ex.Message, "Erro",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearFields()
        {
            txtItem.Clear();
            txtNCM.Clear();
            txtDescription.Clear();
            txtUoM.Clear();
            txtStandardCost.Clear();
            cbNature.Text = string.Empty;
            BlockFields();
        }

        private bool Validate()
        {
            if(txtItem.Text == string.Empty)
            {
                MessageBox.Show("Insira um item!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (txtUoM.Text == string.Empty)
            {
                MessageBox.Show("Insira uma unidade de medida!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (txtNCM.Text == string.Empty)
            {
                MessageBox.Show("Insira um NCM!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (cbNature.Text == string.Empty)
            {
                MessageBox.Show("Insira uma espécie!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
    }
}
