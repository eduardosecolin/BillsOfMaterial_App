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
        MainWindow _window;
        CustQuotaCompOpService serviceCompOp;
        CustQuotasService serviceCQ;
        OperationService serviceOp;
        ItemsService serviceItem;
        WorkersService serviceWorkers;
        OPFamilyService familyService;
        private string pathFile1;
        private string pathFile2;
        private string pathFile3;

        public CostFormationView(MainWindow mainWindow)
        {
            InitializeComponent();
            _window = mainWindow;
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
            if(result == true)
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
                //if (_window.SaveSimulation())
                //{
                // calculo da formação de custo               
                int id = Convert.ToInt32(_window.lblCustQuotaId.Content.ToString());
                string item = _window.cbItemGrid.Text;
                double? unitValue = serviceCQ.GetUnitValueItem(id, _window.positionLine);
                double?[] vet = CalculateOperations(id, item);
                unitValue += CalculateComponents(id, item) + vet[0] + vet[1];
                MessageBox.Show("Resiltado: " + unitValue);
                //}
                //else
                //{
                //    Environment.Exit(0);
                //}
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message + " - " + ex.StackTrace, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private double? CalculateComponents(int id, string itemParam)
        {
            try
            {
                double? result = 0;
                List<CS_CustQuotasComponent> listComp = serviceCompOp.GetSimulationComponents(id, itemParam);
                if(listComp.Count > 0)
                {
                    foreach (var item in listComp)
                    {
                        result += serviceItem.GetStandardCost(item.Component);
                    }
                }

                return result;
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
                if(listOp.Count > 0)
                {
                    foreach (var item in listOp)
                    {
                        resultFamily += CalculateHorlyCostFamilyOperation(item.Operation);
                        resultWorkers += CalculateHorlyCostWorkerOperation(item.Operation);
                    }
                }

                return new double?[] { resultFamily, resultWorkers };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private double? CalculateHorlyCostWorkerOperation(string op)
        {
            try
            {
                // calculo da mao de obra
                double? totalWHourlyCost = 0;
                string timeStringFormat = string.Empty;
                string timeString = serviceOp.GetWorkTimeOperation(op).ToString() + "000";
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
                totalWHourlyCost = (GetHourlyCostWorkers(op));

                return totalWHourlyCost;
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private double? CalculateHorlyCostFamilyOperation(string op)
        {
            try
            {
                // calculo da mao de obra
                double? totalWCHourlyCost = 0;
                string timeStringFormat = string.Empty;
                string timeString = serviceOp.GetWorkTimeOperation(op).ToString() + "000";
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
                totalWCHourlyCost = (GetHourlyCostFamily(op) * timeDouble);

                return totalWCHourlyCost;

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
                foreach (var item in serviceOp.GetWorkersOperation(op))
                {
                    result += serviceWorkers.GetHorlyCostWorker(item.WorkerID);
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
    }
}
