using BillsOfMaterial_App.Model;
using BillsOfMaterial_App.Service;
using Microsoft.Win32;
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
    /// Lógica interna para CostFormationView.xaml
    /// </summary>
    public partial class CostFormationView : Window
    {
        CustQuotaCompOpService serviceCompOp;
        CustQuotasService serviceCQ;
        OperationService serviceOp;
        ItemsService serviceItem;
        WorkersService serviceWorkers;
        OPFamilyService familyService;
        private string pathFile1 = string.Empty;
        private string pathFile2 = string.Empty;
        private string pathFile3 = string.Empty;
        public int positionLine;

        public CostFormationView()
        {
            InitializeComponent();
            serviceCompOp = new CustQuotaCompOpService();
            serviceItem = new ItemsService();
            serviceCQ = new CustQuotasService();
            serviceOp = new OperationService();
            serviceWorkers = new WorkersService();
            familyService = new OPFamilyService();
        }

        #region Preview text input regex

        private void TxtFreightValue_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TxtPis_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TxtCofins_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TxtIcms_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TxtIpi_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TxtFixedExpenses_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TxtVariableExpenses_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        #endregion

        #region Select File

        private void BtnFile1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Todos os arquivos (*.*)|*.*";
            dialog.Title = "Selecione um arquivo";

            Nullable<bool> result = dialog.ShowDialog();
            if (result == true)
            {
                txtPathFile1.Text = System.IO.Path.GetFileName(dialog.FileName);
                pathFile1 = dialog.FileName;
            }
        }

        private void BtnSFile2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Todos os arquivos (*.*)|*.*";
            dialog.Title = "Selecione um arquivo";

            Nullable<bool> result = dialog.ShowDialog();
            if (result == true)
            {
                txtPathFile2.Text = System.IO.Path.GetFileName(dialog.FileName);
                pathFile2 = dialog.FileName;
            }
        }

        private void BtnFile3_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Todos os arquivos (*.*)|*.*";
            dialog.Title = "Selecione um arquivo";

            Nullable<bool> result = dialog.ShowDialog();
            if (result == true)
            {
                txtPathFile3.Text = System.IO.Path.GetFileName(dialog.FileName);
                pathFile3 = dialog.FileName;
            }
        }

        #endregion

        private void ClearFields()
        {
            txtFreightValue.Clear();
            txtPis.Clear();
            txtCofins.Clear();
            txtIcms.Clear();
            txtIpi.Clear();
            txtFixedExpenses.Clear();
            txtVariableExpenses.Clear();
            txtPathFile1.Clear();
            txtPathFile2.Clear();
            txtPathFile3.Clear();
        }

        private void BtnSaveAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // calculo da formação de custo               
                int id = Convert.ToInt32(txtCustQuotaId.Text);
                string item = cbItemGrid.Text;
                double? unitValue = serviceCQ.GetUnitValueItem(id, positionLine);
                double?[] vet = CalculateOperations(id, item);
                unitValue += CalculateComponents(id, item) + vet[0] + vet[1];
                double? costValue = CalculateFieldsView(Convert.ToDouble(unitValue));
                serviceCQ.UpdateCostFormationCustQuatas(id, positionLine, costValue);
                MessageBox.Show($"Formação de Custo (R$ { Math.Round(Convert.ToDouble(costValue), 2) }) e Simulação de Engenharia de Produtos salva com sucesso!",
                    "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                MessageBoxResult resultDialog = MessageBox.Show("Deseja realizar outra simulação?", "Pergunta",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultDialog == MessageBoxResult.Yes)
                {
                    ClearFields();
                    this.Close();
                }
                else
                {
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " - " + ex.StackTrace,
                    "Erro", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private double? CalculateComponents(int id, string itemParam)
        {
            try
            {
                double? result = 0;
                List<CS_CustQuotasComponent> listComp = serviceCompOp.GetSimulationComponents(id, itemParam);
                if (listComp.Count > 0)
                {
                    foreach (var item in listComp)
                    {
                        if (item.R1Costvalue > 0)
                        {
                            result += (item.R1Costvalue * item.Qty);
                        }
                        else
                        {
                            result += (serviceItem.GetStandardCost(item.Component) * item.Qty);
                        }
                    }
                }

                result = (double.IsNaN(Convert.ToDouble(result))) ? 0 : result;

                return result ?? 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private double?[] CalculateOperations(int id, string itemParam)
        {
            try
            {
                double? resultFamily = 0;
                double? resultWorkers = 0;
                List<CS_CustQuotasOperation> listOp = serviceCompOp.GetSimulationOperations(id, itemParam);
                if (listOp.Count > 0)
                {
                    foreach (var item in listOp)
                    {
                        resultFamily += (GetHourlyCostFamily(item.Operation) * CalculateHorlyCostFamilyOperation(item.TimeProcess));
                        resultWorkers += GetHourlyCostWorkers(item.Operation);
                    }
                }

                resultFamily = (double.IsNaN(Convert.ToDouble(resultFamily))) ? 0 : resultFamily;
                resultWorkers = (double.IsNaN(Convert.ToDouble(resultWorkers))) ? 0 : resultWorkers;

                return new double?[] { resultFamily, resultWorkers };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private double? CalculateHorlyCostFamilyOperation(DateTime? timeProcess)
        {
            try
            {
                // calculo da mao de obra
                string timeStringFormat = string.Empty;
                double timeDouble = 0;
                string[] timeSplit = Convert.ToDateTime(timeProcess).ToShortTimeString().Split(':');
                if (timeSplit.Length > 1)
                {
                    if (timeSplit[1] != "00")
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

                return timeDouble;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private double? CalculateHorlyCostWorkerOperation(int? workTime)
        {
            try
            {
                // calculo da mao de obra
                string timeStringFormat = string.Empty;
                string timeString = workTime + "000";
                double timeDouble = Convert.ToDouble(timeString);
                double time = TimeSpan.FromMilliseconds(timeDouble).TotalHours;
                string timeStr = TimeSpan.FromHours(time).ToString("h\\:mm");
                string[] timeSplit = timeStr.Split(':');
                if (timeSplit.Length > 1)
                {
                    if (timeSplit[1] != "00")
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

                return timeDouble;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private double? GetHourlyCostWorkers(string op)
        {
            try
            {
                double? result = 0;
                List<MA_OperationsLabour> ListWorkersOp = serviceOp.GetWorkersOperation(op);
                foreach (var item in ListWorkersOp)
                {
                    result += ((serviceWorkers.GetHorlyCostWorker(item.WorkerID) / ListWorkersOp.Count) * CalculateHorlyCostWorkerOperation(item.WorkingTime));
                }

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private double? GetHourlyCostFamily(string op)
        {
            try
            {
                string WC = familyService.GetOperationFamily(op);
                if (!string.IsNullOrEmpty(WC))
                {
                    return familyService.GetHourlyCostOpFamily(WC);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private double CalculateFieldsView(double costValue)
        {
            try
            {
                double freight = (!string.IsNullOrEmpty(txtFreightValue.Text)) ? Convert.ToDouble(txtFreightValue.Text) : 0;
                double pis = (!string.IsNullOrEmpty(txtPis.Text)) ? (costValue / 100) * Convert.ToDouble(txtPis.Text) : 0;
                double cofins = (!string.IsNullOrEmpty(txtCofins.Text)) ? (costValue / 100) * Convert.ToDouble(txtCofins.Text) : 0;
                double icms = (!string.IsNullOrEmpty(txtIcms.Text)) ? (costValue / 100) * Convert.ToDouble(txtIcms.Text) : 0;
                double ipi = (!string.IsNullOrEmpty(txtIpi.Text)) ? (costValue / 100) * Convert.ToDouble(txtIpi.Text) : 0;
                double df = (!string.IsNullOrEmpty(txtFixedExpenses.Text)) ? (costValue / 100) * Convert.ToDouble(txtFixedExpenses.Text) : 0;
                double dv = (!string.IsNullOrEmpty(txtVariableExpenses.Text)) ? (costValue / 100) * Convert.ToDouble(txtVariableExpenses.Text) : 0;
                double margin = (!string.IsNullOrEmpty(txtMargin.Text)) ? (costValue / 100) * Convert.ToDouble(txtMargin.Text) : 0;
                double variableMargin = (!string.IsNullOrEmpty(txtVariableMargin.Text)) ? (costValue / 100) * Convert.ToDouble(txtVariableMargin.Text) : 0;

                return costValue + (
                    freight +
                    pis +
                    cofins +
                    icms +
                    ipi +
                    df +
                    dv +
                    margin +
                    variableMargin
                    );
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void TxtMargin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TxtVariableMargin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public void LoadCbItemGrid()
        {
            try
            {
                var custQuota = serviceCQ.GetById(Convert.ToInt32(txtCustQuotaId.Text));

                if (custQuota != null)
                {
                    cbItemGrid.ItemsSource = null;
                    List<string> items = new List<string>();
                    foreach (var item in serviceCQ.GetAll(custQuota.CustQuotaId))
                    {
                        items.Add(item.Item);
                    }
                    cbItemGrid.ItemsSource = items;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CbItemGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            if (serviceCQ.ExistCostFormation(int.Parse(txtCustQuotaId.Text), cbItemGrid.Text))
            {
                MessageBox.Show("Já existe formação de custo para a simulãção selecionada!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                BlockFields();
                return;
            }
            EnabledFields();
            double?[] vet = serviceItem.GetTaxValues(cbItemGrid.Text);
            if(vet != null && vet.Length > 0)
            {
                txtPis.Text = vet[0].ToString();
                txtCofins.Text = vet[1].ToString();
                txtIcms.Text = vet[2].ToString();
                txtIpi.Text = vet[3].ToString();
            }
        }

        private void CbItemGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            positionLine = cbItemGrid.SelectedIndex + 1;
        }

        private void EnabledFields()
        {
            txtFreightValue.IsEnabled = true;
            txtPis.IsEnabled = true;
            txtCofins.IsEnabled = true;
            txtIcms.IsEnabled = true;
            txtIpi.IsEnabled = true;
            txtFixedExpenses.IsEnabled = true;
            txtVariableExpenses.IsEnabled = true;
            txtMargin.IsEnabled = true;
            txtVariableMargin.IsEnabled = true;
        }

        private void BlockFields()
        {
            txtFreightValue.IsEnabled = false;
            txtPis.IsEnabled = false;
            txtCofins.IsEnabled = false;
            txtIcms.IsEnabled = false;
            txtIpi.IsEnabled = false;
            txtFixedExpenses.IsEnabled = false;
            txtVariableExpenses.IsEnabled = false;
            txtMargin.IsEnabled = false;
            txtVariableMargin.IsEnabled = false;
        }

        private void BtnSerchOffer_Click(object sender, RoutedEventArgs e)
        {
            OfferSearchView window = new OfferSearchView(this);
            window.Show();
        }
    }
}
