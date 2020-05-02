using BillsOfMaterial_App.AuxView;
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
    /// Lógica interna para OperationView.xaml
    /// </summary>
    public partial class OperationView : Window
    {
        SimulationEDP _mainWindow;
        OperationService operationService;
        private string description = string.Empty;
        public bool isEditmode = false;
        private bool isException = false;

        public OperationView(SimulationEDP mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            operationService = new OperationService();
            DisabledFields();
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
            try
            {
                if (dgOperation.SelectedItems.Count > 0)
                {
                    var operations = dgOperation.SelectedItem as MA_Operations;
                    if (operations != null)
                    {
                        txtOperation.Text = operations.Operation;
                        description = operations.Description;
                        txtCostOperation.Text = Math.Round(Convert.ToDouble(operationService.GetOpTotalCost(operations.Operation)), 2).ToString();
                        ShowFieldsHidden(operations.Operation, operations.WC);
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
            }catch(Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message + "\n " + ex.StackTrace, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        double valor = 0;
                        if (txtUoM.Visibility == Visibility.Visible && txtQuantity.Visibility == Visibility.Visible) 
                        {
                            valor = (Convert.ToDouble(txtCostOperation.Text) * Convert.ToDouble(txtQuantity.Text));
                        }
                        else 
                        {
                            valor = CalculateCostOperation(dpTimeProcess.Text, Convert.ToDouble(txtCostOperation.Text));
                        }

                        txtCostOperation.Text = Math.Round(valor, 2).ToString();
                        var treeview = _mainWindow.tvBOM.SelectedItem as TreeViewItem;
                        if(treeview != null)
                        {
                            treeview.Header = txtOperation.Text.Replace("|", "") + " | " + operationService.GetDescriptionOp(txtOperation.Text) + "| " + "Tempo de Processamento = " + dpTimeProcess.Text + " | Custo: " + txtCostOperation.Text + "| OBS: " + txtObs.Text.Replace("|", "") + " | U.Medida: " + txtUoM.Text + " | Quantidade: " + txtQuantity.Text;
                            this.Close();
                        }
                    }
                    else
                    {
                        double valor = 0;
                        if (txtUoM.Visibility == Visibility.Visible && txtQuantity.Visibility == Visibility.Visible)
                        {
                            valor = (Convert.ToDouble(txtCostOperation.Text) * Convert.ToDouble(txtQuantity.Text));
                        }
                        else
                        {
                            valor = CalculateCostOperation(dpTimeProcess.Text, Convert.ToDouble(txtCostOperation.Text));
                        }

                        txtCostOperation.Text = Math.Round(valor, 2).ToString();
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
            chilItem.Header = txtOperation.Text.Replace("|", "") + " | " + description.Replace("|", "") + "| " + "Tempo de Processamento = " + dpTimeProcess.Text + " | Custo: " + txtCostOperation.Text + " | OBS: " + txtObs.Text.Replace("|", "") + " | U.Medida: " + txtUoM.Text + " | Quantidade: " + txtQuantity.Text;
            _mainWindow.tvOperation.Items.Add(chilItem);
            this.Close();
        }

        private void FillLevel2()
        {
            TreeViewItem viewItem = _mainWindow.tvBOM.SelectedItem as TreeViewItem;
            TreeViewItem chilItem = new TreeViewItem();
            chilItem.Name = "tvOpLevel2";
            chilItem.Header = txtOperation.Text.Replace("|", "") + " | " + description.Replace("|", "") + "| " + "Tempo de Processamento = " + dpTimeProcess.Text + " | Custo: " + txtCostOperation.Text + " | OBS: " + txtObs.Text.Replace("|", "") + " | U.Medida: " + txtUoM.Text + " | Quantidade: " + txtQuantity.Text;
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
                dpTimeProcess.Text = "00:00";
                return true;
            }
            if (txtUoM.Visibility == Visibility.Visible && string.IsNullOrEmpty(txtUoM.Text))
            {
                MessageBox.Show("Escolha uma unidade de medida!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void BtnAddOBS_Click(object sender, RoutedEventArgs e)
        {
            DefaultOBSView window = new DefaultOBSView(this);
            window.Show();
        }

        public static double CalculateCostOperation(string timeProcessTxt, double valor)
        {
            try
            {
                // calculo da mao de obra
                string timeStringFormat = string.Empty;
                double timeDouble = 0;
                string[] timeSplit = timeProcessTxt.Split(':');
                if (timeSplit.Length > 1)
                {
                    double time = Convert.ToDouble(timeSplit[1]);
                    if (time > 0)
                    {
                        double vTemp = Convert.ToDouble(timeSplit[1]);
                        double vValue = Math.Round((vTemp / 60), 2);
                        timeStringFormat = timeSplit[0] + "," + vValue.ToString().Substring(2);
                    }
                    else
                    {
                        timeStringFormat = timeSplit[0] + "," + timeSplit[1];
                    }
                }
                else
                {
                    timeStringFormat = timeSplit[0] + "," + timeSplit[1];
                }

                timeDouble = Convert.ToDouble(timeStringFormat);

                if (timeDouble == 0)
                {
                    return valor;
                }
                else
                {
                    return valor * timeDouble;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void FillCostValueOperation()
        {
            if(txtOperation.Text != string.Empty)
            {
                txtCostOperation.Text = Math.Round(Convert.ToDouble(operationService.GetOpTotalCost(txtOperation.Text)), 2).ToString();
            }
        }

        private void dpTimeProcess_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9:]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void DisabledFields()
        {
            txtUoM.Visibility = Visibility.Hidden;
            btnSerchUoM.Visibility = Visibility.Hidden;
            txtQuantity.Visibility = Visibility.Hidden;
        }

        private void EnabledFields()
        {
            txtUoM.Visibility = Visibility.Visible;
            btnSerchUoM.Visibility = Visibility.Visible;
            txtQuantity.Visibility = Visibility.Visible;
            txtCostOperation.IsReadOnly = false;
        }

        private void btnSerchUoM_Click(object sender, RoutedEventArgs e)
        {
            UoMView window = new UoMView(this);
            window.Show();
        }

        private void ShowFieldsHidden(string op, string WC)
        {
            try
            {
                if (operationService.IsCdt(op))
                {
                    // CDT
                    if (operationService.isTercExteranlCDT(WC))
                    {
                        EnabledFields();
                    }
                }
                else
                {
                    // FAMILIA CDT
                    if (operationService.isTercExteranlFamily(WC))
                    {
                        EnabledFields();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
