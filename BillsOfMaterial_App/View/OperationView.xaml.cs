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
    /// Lógica interna para OperationView.xaml
    /// </summary>
    public partial class OperationView : Window
    {
        MainWindow _mainWindow;
        OperationService operationService;
        private string description = string.Empty;
        public bool isEditmode = false;
        private bool isException = false;

        public OperationView(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            operationService = new OperationService();
            LoadGrid();
        }

        private void LoadGrid()
        {
            try
            {
                dgOperation.ItemsSource = operationService.GetAll();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " - Erro ao consultar dados", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                isEditmode = false;
            }
        }

        private void TxtFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtFilter.Text != string.Empty)
            {
                dgOperation.ItemsSource = null;
                dgOperation.ItemsSource = operationService.GetAll(txtFilter.Text);
            }
            else
            {
                LoadGrid();
            }
        }

        private void DgOperation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgOperation.SelectedItems.Count > 0)
            {
                var operations = dgOperation.SelectedItem as MA_Operations;
                if (operations != null)
                {
                    txtOperation.Text = operations.Operation;
                    description = operations.Description;
                }

                string obs = operationService.GetOperationNotes(operations.Operation);
                if (!string.IsNullOrEmpty(obs))
                {
                    txtObs.Text = obs;
                }
                else
                {
                    txtObs.Text = string.Empty;
                }
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateFields())
                {
                    if (isEditmode)
                    {
                        var treeview = _mainWindow.tvBOM.SelectedItem as TreeViewItem;
                        if(treeview != null)
                        {
                            treeview.Header = txtOperation.Text + " - " + operationService.GetDescriptionOp(txtOperation.Text) + "- " + "Tempo de Processamento = " + Convert.ToDateTime(dpTimeProcess.SelectedTime.ToString()).ToString("HH:mm") + "- OBS: " + txtObs.Text;
                            this.Close();
                        }
                    }
                    else
                    {
                        if (_mainWindow.level1 == true)
                        {
                            FillLevel1();
                        }
                        else if (_mainWindow.level2 == true)
                        {
                            FillLevel2();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if(isEditmode == true)
                {
                    isException = true;
                }
            }
            finally
            {
                if (isException)
                {
                    isEditmode = true;
                    isException = false;
                }
                else
                {
                    isEditmode = false;
                }
            }
        }

        private void FillLevel1()
        {
            TreeViewItem chilItem = new TreeViewItem();
            chilItem.Name = "tvOpLevel1";
            chilItem.Header = txtOperation.Text + " - " + description.Replace("-", "") + "- " + "Tempo de Processamento = " + Convert.ToDateTime(dpTimeProcess.SelectedTime.ToString()).ToString("HH:mm") + " - OBS: " + txtObs.Text;
            _mainWindow.tvOperation.Items.Add(chilItem);
            this.Close();
        }

        private void FillLevel2()
        {
            TreeViewItem viewItem = _mainWindow.tvBOM.SelectedItem as TreeViewItem;
            TreeViewItem chilItem = new TreeViewItem();
            chilItem.Name = "tvOpLevel2";
            chilItem.Header = txtOperation.Text + " - " + description.Replace("-", "") + "- " + "Tempo de Processamento = " + Convert.ToDateTime(dpTimeProcess.SelectedTime.ToString()).ToString("HH:mm") + " - OBS: " + txtObs.Text;
            viewItem.Items.Add(chilItem);
            this.Close();
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrEmpty(txtOperation.Text))
            {
                MessageBox.Show("Escolha uma operação!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(dpTimeProcess.Text))
            {
                MessageBox.Show("Escolha um tempo de processamento!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
    }
}
