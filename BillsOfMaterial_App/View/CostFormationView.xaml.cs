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
        CompanyService companyService;
        TBCostFormationService tcsService;
        private string pathFile1 = string.Empty;
        private string pathFile2 = string.Empty;
        private string pathFile3 = string.Empty;
        public int? positionLine;
        double? totalComponents;
        double IPIICMS = 0;
        public double? unitValue;

        public CostFormationView()
        {
            InitializeComponent();
            serviceCompOp = new CustQuotaCompOpService();
            serviceItem = new ItemsService();
            serviceCQ = new CustQuotasService();
            serviceOp = new OperationService();
            serviceWorkers = new WorkersService();
            familyService = new OPFamilyService();
            companyService = new CompanyService();
            tcsService = new TBCostFormationService();
            rb100PercentServices.IsChecked = true;
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
            txtCustQuotaId.Clear();
            txtISS.Clear();
            txtIR.Clear();
            txtCSLL.Clear();
            txtSelectItem.Clear();
            txtFixedExpenses.Clear();
            txtVariableExpenses.Clear();
            txtPathFile1.Clear();
            txtPathFile2.Clear();
            txtPathFile3.Clear();
            txtComissions.Clear();
            txtMarkup.Clear();
            txtTotalMarkup.Clear();
            txtTotalSimulation.Clear();
            cbItemGrid.Text = "";
            cbItemGrid.ItemsSource = null;
            cbItemGrid.Items.Clear();
            BlockFields();
        }

        private void BtnSaveAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (serviceCQ.ExistCostFormation(int.Parse(txtCustQuotaId.Text), cbItemGrid.Text))
                //{
                //    MessageBox.Show("Já existe formação de custo para a simulãção selecionada!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                //    ClearFields();
                //    return;
                //}

                if (!serviceCompOp.ExistData(Convert.ToInt32(txtCustQuotaId.Text), cbItemGrid.Text))
                {
                    MessageBox.Show("Não existe simulação para o item da oferta selecionada!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ClearFields();
                    return;
                }


                // calculo da formação de custo      
                int id = Convert.ToInt32(txtCustQuotaId.Text);
                string item = cbItemGrid.Text;
                CalculateFieldsView(Convert.ToDouble(unitValue));
                double? costValue = Convert.ToDouble(txtTotalMarkup.Text);
                if (serviceCQ.UpdateCostFormationCustQuatas(id, costValue))
                {
                    try
                    {
                        if (tcsService.GetAllWithParams(Convert.ToInt32(txtCustQuotaId.Text), txtSelectItem.Text, cbItemGrid.Text) != null)
                        {
                            MessageBoxResult resultMessage = MessageBox.Show("Já existem dados salvos para essa formação de custo! Deseja sobrescrever os dados?", "Pergunta",
                                 MessageBoxButton.YesNo, MessageBoxImage.Question);
                            if (resultMessage == MessageBoxResult.Yes)
                            {
                                tcsService.DeleteData(Convert.ToInt32(txtCustQuotaId.Text), txtSelectItem.Text, cbItemGrid.Text);

                                CS_TBCostFormation tcs = new CS_TBCostFormation();
                                tcs.Id = tcsService.GetMaxId();
                                tcs.Id_Offer = Convert.ToInt32(txtCustQuotaId.Text);
                                tcs.OfferNo = txtSelectItem.Text;
                                tcs.Item = cbItemGrid.Text;
                                if (ckSumIcmsIpi.IsChecked == true)
                                {
                                    tcs.SumIpiIcms = "1";
                                }
                                else
                                {
                                    tcs.SumIpiIcms = "0";
                                }
                                tcs.PIS = string.IsNullOrEmpty(txtPis.Text) ? 0 : Convert.ToDouble(txtPis.Text);
                                tcs.COFINS = string.IsNullOrEmpty(txtCofins.Text) ? 0 : Convert.ToDouble(txtCofins.Text);
                                tcs.ICMS = string.IsNullOrEmpty(txtIcms.Text) ? 0 : Convert.ToDouble(txtIcms.Text);
                                tcs.Freight = string.IsNullOrEmpty(txtFreightValue.Text) ? 0 : Convert.ToDouble(txtFreightValue.Text);
                                tcs.Comission = string.IsNullOrEmpty(txtComissions.Text) ? 0 : Convert.ToDouble(txtComissions.Text);
                                tcs.IPI = string.IsNullOrEmpty(txtIpi.Text) ? 0 : Convert.ToDouble(txtIpi.Text);
                                tcs.DF_Percent = string.IsNullOrEmpty(txtFixedExpenses.Text) ? 0 : Convert.ToDouble(txtFixedExpenses.Text);
                                tcs.DV_Percent = string.IsNullOrEmpty(txtVariableExpenses.Text) ? 0 : Convert.ToDouble(txtVariableExpenses.Text);
                                tcs.Margin = string.IsNullOrEmpty(txtMargin.Text) ? 0 : Convert.ToDouble(txtMargin.Text);
                                tcs.MarginVariable = string.IsNullOrEmpty(txtVariableMargin.Text) ? 0 : Convert.ToDouble(txtVariableMargin.Text);
                                tcs.ISS = string.IsNullOrEmpty(txtISS.Text) ? 0 : Convert.ToDouble(txtISS.Text);
                                tcs.IR = string.IsNullOrEmpty(txtIR.Text) ? 0 : Convert.ToDouble(txtIR.Text);
                                tcs.CSLL = string.IsNullOrEmpty(txtCSLL.Text) ? 0 : Convert.ToDouble(txtCSLL.Text);
                                tcs.Markup = string.IsNullOrEmpty(txtMarkup.Text) ? 0 : Convert.ToDouble(txtMarkup.Text);
                                tcs.TotalMarkup = string.IsNullOrEmpty(txtTotalMarkup.Text) ? 0 : Convert.ToDouble(txtTotalMarkup.Text);
                                tcs.TotalSimulation = string.IsNullOrEmpty(txtTotalSimulation.Text) ? 0 : Convert.ToDouble(txtTotalSimulation.Text);
                                tcs.TBCreated = DateTime.Now;
                                tcs.TBModified = DateTime.Now;
                                tcs.TBCreatedID = 1;

                                tcsService.Insert(tcs);
                            }
                            else
                            {
                               
                            }
                        }
                        else
                        {
                            CS_TBCostFormation tcs = new CS_TBCostFormation();
                            tcs.Id = tcsService.GetMaxId();
                            tcs.Id_Offer = Convert.ToInt32(txtCustQuotaId.Text);
                            tcs.OfferNo = txtSelectItem.Text;
                            tcs.Item = cbItemGrid.Text;
                            if (ckSumIcmsIpi.IsChecked == true)
                            {
                                tcs.SumIpiIcms = "1";
                            }
                            else
                            {
                                tcs.SumIpiIcms = "0";
                            }
                            tcs.PIS = string.IsNullOrEmpty(txtPis.Text) ? 0 : Convert.ToDouble(txtPis.Text);
                            tcs.COFINS = string.IsNullOrEmpty(txtCofins.Text) ? 0 : Convert.ToDouble(txtCofins.Text);
                            tcs.ICMS = string.IsNullOrEmpty(txtIcms.Text) ? 0 : Convert.ToDouble(txtIcms.Text);
                            tcs.Freight = string.IsNullOrEmpty(txtFreightValue.Text) ? 0 : Convert.ToDouble(txtFreightValue.Text);
                            tcs.Comission = string.IsNullOrEmpty(txtComissions.Text) ? 0 : Convert.ToDouble(txtComissions.Text);
                            tcs.IPI = string.IsNullOrEmpty(txtIpi.Text) ? 0 : Convert.ToDouble(txtIpi.Text);
                            tcs.DF_Percent = string.IsNullOrEmpty(txtFixedExpenses.Text) ? 0 : Convert.ToDouble(txtFixedExpenses.Text);
                            tcs.DV_Percent = string.IsNullOrEmpty(txtVariableExpenses.Text) ? 0 : Convert.ToDouble(txtVariableExpenses.Text);
                            tcs.Margin = string.IsNullOrEmpty(txtMargin.Text) ? 0 : Convert.ToDouble(txtMargin.Text);
                            tcs.MarginVariable = string.IsNullOrEmpty(txtVariableMargin.Text) ? 0 : Convert.ToDouble(txtVariableMargin.Text);
                            tcs.ISS = string.IsNullOrEmpty(txtISS.Text) ? 0 : Convert.ToDouble(txtISS.Text);
                            tcs.IR = string.IsNullOrEmpty(txtIR.Text) ? 0 : Convert.ToDouble(txtIR.Text);
                            tcs.CSLL = string.IsNullOrEmpty(txtCSLL.Text) ? 0 : Convert.ToDouble(txtCSLL.Text);
                            tcs.Markup = string.IsNullOrEmpty(txtMarkup.Text) ? 0 : Convert.ToDouble(txtMarkup.Text);
                            tcs.TotalMarkup = string.IsNullOrEmpty(txtTotalMarkup.Text) ? 0 : Convert.ToDouble(txtTotalMarkup.Text);
                            tcs.TotalSimulation = string.IsNullOrEmpty(txtTotalSimulation.Text) ? 0 : Convert.ToDouble(txtTotalSimulation.Text);
                            tcs.TBCreated = DateTime.Now;
                            tcs.TBModified = DateTime.Now;
                            tcs.TBCreatedID = 1;

                            tcsService.Insert(tcs);
                        }           
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Erro ao salvar dados completos da formação de custo" + "\n" + exc.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    MessageBox.Show($"Formação de Custo (R$ { Math.Round(Convert.ToDouble(costValue), 2) }) e Simulação de Engenharia de Produtos salva com sucesso!",
                          "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                    MessageBoxResult resultDialog = MessageBox.Show("Deseja realizar outra simulação?", "Pergunta",
                        MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resultDialog == MessageBoxResult.Yes)
                    {
                        ClearFields();

                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                }
                else
                {
                    MessageBox.Show("Não existe dados!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
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
                double? qtd = 0;
                List<CS_CustQuotasComponent> listComp = serviceCompOp.GetSimulationComponents(id, itemParam);
                if (listComp.Count > 0)
                {
                    List<string> BOMSList = serviceCompOp.GetBOMComponents(id, itemParam);
                    foreach (var bom in BOMSList)
                    {
                        bool isNV1 = false;
                        foreach (var item in listComp)
                        {

                            if (item.Component.Trim().Equals(bom.Trim()))
                            {
                                isNV1 = true;

                                qtd = item.Qty;
                                result += serviceCompOp.GetTotalComponents(id, itemParam, bom) + serviceCompOp.GetTotalOperations(id, itemParam, bom) + serviceCompOp.GetTotalComponentsR1(id, itemParam, bom);
                                result *= qtd;
                            }
                            //if (item.R1Costvalue > 0)
                            //{
                            //    result += (item.R1Costvalue * item.Qty);
                            //}
                            //else
                            //{
                            //    result += (item.Costvalue);
                            //}

                        }

                        if (!isNV1)
                        {
                            result += serviceCompOp.GetTotalComponents(id, itemParam, bom) + serviceCompOp.GetTotalOperations(id, itemParam, bom) + serviceCompOp.GetTotalComponentsR1(id, itemParam, bom);
                        }
                    }
                }

                result = (double.IsNaN(Convert.ToDouble(result))) ? 0 : result;

                double total = Convert.ToDouble(result);

                return total;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private double? CalculateOperations(int id, string itemParam)
        {
            try
            {
                double? result = 0;
                List<CS_CustQuotasOperation> listOp = serviceCompOp.GetSimulationOperations(id, itemParam);
                if (listOp.Count > 0)
                {
                    foreach (var item in listOp)
                    {
                        result += item.CostOperation;
                    }
                }
                return result;
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

        private void CalculateFieldsView(double costValue)
        {
            try
            {
                double freightStr = (!string.IsNullOrEmpty(txtFreightValue.Text)) ? Convert.ToDouble(txtFreightValue.Text) : 0;
                double freightPercent = (freightStr * 100) / costValue;
                double freight = (!string.IsNullOrEmpty(txtFreightValue.Text)) ? (costValue / 100) * freightPercent : 0;
                double pis = (!string.IsNullOrEmpty(txtPis.Text)) ? Convert.ToDouble(txtPis.Text) : 0;
                double cofins = (!string.IsNullOrEmpty(txtCofins.Text)) ? Convert.ToDouble(txtCofins.Text) : 0;
                double icms = (!string.IsNullOrEmpty(txtIcms.Text)) ? Convert.ToDouble(txtIcms.Text) : 0;
                double ipi = (!string.IsNullOrEmpty(txtIpi.Text)) ? Convert.ToDouble(txtIpi.Text) : 0;
                double df = (!string.IsNullOrEmpty(txtFixedExpenses.Text)) ? Convert.ToDouble(txtFixedExpenses.Text) : 0;
                double commision = (!string.IsNullOrEmpty(txtComissions.Text)) ? Convert.ToDouble(txtComissions.Text) : 0;
                double margin = (!string.IsNullOrEmpty(txtMargin.Text)) ? Convert.ToDouble(txtMargin.Text) : 0;
                double variableMargin = (!string.IsNullOrEmpty(txtVariableMargin.Text)) ? Convert.ToDouble(txtVariableMargin.Text) : 0;
                double iss = (!string.IsNullOrEmpty(txtISS.Text)) ? Convert.ToDouble(txtISS.Text) : 0;
                double ir = (!string.IsNullOrEmpty(txtIR.Text)) ? Convert.ToDouble(txtIR.Text) : 0;
                double csll = (!string.IsNullOrEmpty(txtCSLL.Text)) ?/* (costValue / 100) * */Convert.ToDouble(txtCSLL.Text) : 0;

                double result = (
                    pis +
                    cofins +
                    icms +
                    ipi +
                    df +
                    commision +
                    margin +
                    variableMargin +
                    iss +
                    ir +
                    csll
                    );

                txtVariableExpenses.Text = Math.Round(result, 2).ToString();
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
            txtISS.IsEnabled = true;
            txtIR.IsEnabled = true;
            txtCSLL.IsEnabled = true;
            txtComissions.IsEnabled = true;
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
            txtISS.IsEnabled = false;
            txtIR.IsEnabled = false;
            txtCSLL.IsEnabled = false;
            txtComissions.IsEnabled = false;
        }

        private void BtnSerchOffer_Click(object sender, RoutedEventArgs e)
        {
            OfferSearchView window = new OfferSearchView(this);
            window.Show();
        }

        private void TxtISS_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TxtIR_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TxtCSLL_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void LoadDFDV()
        {
            try
            {
                txtFixedExpenses.Text = companyService.GetFixedExpenses().ToString();
            }
            catch (Exception)
            {

                MessageBox.Show("Erro ao consultar despesas fixas e variaveis", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public double CalculateFieldsViewToMarkup(double costValue)
        {
            try
            {
                //double freightStr = (!string.IsNullOrEmpty(txtFreightValue.Text)) ? Convert.ToDouble(txtFreightValue.Text) : 0;
                //double freightPercent = (freightStr * 100) / costValue;
                //double freight = (!string.IsNullOrEmpty(txtFreightValue.Text)) ? Math.Round(freightPercent, 2) : 0;
                double pis = (!string.IsNullOrEmpty(txtPis.Text)) ? Convert.ToDouble(txtPis.Text) : 0;
                double cofins = (!string.IsNullOrEmpty(txtCofins.Text)) ? Convert.ToDouble(txtCofins.Text) : 0;
                double icms = (!string.IsNullOrEmpty(txtIcms.Text)) ? Convert.ToDouble(txtIcms.Text) : 0;
                double ipi = (!string.IsNullOrEmpty(txtIpi.Text)) ? Convert.ToDouble(txtIpi.Text) : 0;
                double df = (!string.IsNullOrEmpty(txtFixedExpenses.Text)) ? Convert.ToDouble(txtFixedExpenses.Text) : 0;
                double commision = (!string.IsNullOrEmpty(txtComissions.Text)) ? Convert.ToDouble(txtComissions.Text) : 0;
                double margin = (!string.IsNullOrEmpty(txtMargin.Text)) ? Convert.ToDouble(txtMargin.Text) : 0;
                double variableMargin = (!string.IsNullOrEmpty(txtVariableMargin.Text)) ? Convert.ToDouble(txtVariableMargin.Text) : 0;
                double iss = (!string.IsNullOrEmpty(txtISS.Text)) ? Convert.ToDouble(txtISS.Text) : 0;
                double ir = (!string.IsNullOrEmpty(txtIR.Text)) ? Convert.ToDouble(txtIR.Text) : 0;
                double csll = (!string.IsNullOrEmpty(txtCSLL.Text)) ? Convert.ToDouble(txtCSLL.Text) : 0;

                double result = (
                    //freight +
                    pis +
                    cofins +
                    icms +
                    ipi +
                    df +
                    commision +
                    margin +
                    variableMargin +
                    iss +
                    ir +
                    csll
                    );

                return Math.Round(result, 2);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public double CalculateFieldsViewToMarkupGraphic(double costValue)
        {
            try
            {
                double pis = (!string.IsNullOrEmpty(txtPis.Text)) ? Convert.ToDouble(txtPis.Text) : 0;
                double cofins = (!string.IsNullOrEmpty(txtCofins.Text)) ? Convert.ToDouble(txtCofins.Text) : 0;
                double icms = (!string.IsNullOrEmpty(txtIcms.Text)) ? Convert.ToDouble(txtIcms.Text) : 0;
                double ipi = (!string.IsNullOrEmpty(txtIpi.Text)) ? Convert.ToDouble(txtIpi.Text) : 0;
                double commision = (!string.IsNullOrEmpty(txtComissions.Text)) ? Convert.ToDouble(txtComissions.Text) : 0;
                double variableMargin = (!string.IsNullOrEmpty(txtVariableMargin.Text)) ? Convert.ToDouble(txtVariableMargin.Text) : 0;
                double iss = (!string.IsNullOrEmpty(txtISS.Text)) ? Convert.ToDouble(txtISS.Text) : 0;
                double ir = (!string.IsNullOrEmpty(txtIR.Text)) ? Convert.ToDouble(txtIR.Text) : 0;
                double csll = (!string.IsNullOrEmpty(txtCSLL.Text)) ? Convert.ToDouble(txtCSLL.Text) : 0;

                double result = (
                    pis +
                    cofins +
                    icms +
                    ipi +
                    commision +
                    variableMargin +
                    iss +
                    ir +
                    csll
                    );

                return Math.Round(result, 2);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void CalculateMarkup()
        {
            try
            {
                double? freight = (!string.IsNullOrEmpty(txtFreightValue.Text)) ? Convert.ToDouble(txtFreightValue.Text) : 0;
                //double? margin = (!string.IsNullOrEmpty(txtMargin.Text)) ? Convert.ToDouble(txtMargin.Text) : 0;
                //double? df = (!string.IsNullOrEmpty(txtFixedExpenses.Text)) ? Convert.ToDouble(txtFixedExpenses.Text) : 0;
                double? dv = /*(unitValue / 100) * */CalculateFieldsViewToMarkup(Convert.ToDouble(unitValue));
                //double? sum = dv;
                //double? sub = (100 - sum);
                double? markup = 100 / (100 - (dv));

                txtMarkup.Text = Math.Round(Convert.ToDouble(markup), 4).ToString();

                double? totalMarkup = ((Convert.ToDouble(unitValue) + freight) * markup);

                txtTotalMarkup.Text = Math.Round(Convert.ToDouble(totalMarkup), 2).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region FIELDS LOST FOCUS

        private void TxtMargin_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                CalculateMarkup();
                CalculateFieldsView(Convert.ToDouble(unitValue));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao calcular Markup" + "\n" + ex.InnerException.Message,
                    "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TxtFreightValue_LostFocus(object sender, RoutedEventArgs e)
        {
            CalculateFieldsView(Convert.ToDouble(unitValue));
            CalculateMarkup();
        }

        private void TxtPis_LostFocus(object sender, RoutedEventArgs e)
        {
            CalculateFieldsView(Convert.ToDouble(unitValue));
            CalculateMarkup();
        }

        private void TxtCofins_LostFocus(object sender, RoutedEventArgs e)
        {
            CalculateFieldsView(Convert.ToDouble(unitValue));
            CalculateMarkup();
        }

        private void TxtIcms_LostFocus(object sender, RoutedEventArgs e)
        {
            CalculateFieldsView(Convert.ToDouble(unitValue));
            CalculateMarkup();
        }

        private void TxtIpi_LostFocus(object sender, RoutedEventArgs e)
        {
            CalculateFieldsView(Convert.ToDouble(unitValue));
            CalculateMarkup();
        }

        private void TxtComissions_LostFocus(object sender, RoutedEventArgs e)
        {
            CalculateFieldsView(Convert.ToDouble(unitValue));
            CalculateMarkup();
        }

        private void TxtFixedExpenses_LostFocus(object sender, RoutedEventArgs e)
        {
            CalculateFieldsView(Convert.ToDouble(unitValue));
            CalculateMarkup();
        }

        private void TxtISS_LostFocus(object sender, RoutedEventArgs e)
        {
            CalculateFieldsView(Convert.ToDouble(unitValue));
            CalculateMarkup();
        }

        private void TxtIR_LostFocus(object sender, RoutedEventArgs e)
        {
            CalculateFieldsView(Convert.ToDouble(unitValue));
            CalculateMarkup();
        }

        private void TxtCSLL_LostFocus(object sender, RoutedEventArgs e)
        {
            CalculateFieldsView(Convert.ToDouble(unitValue));
            CalculateMarkup();
        }

        #endregion

        private void btnChart_Click(object sender, RoutedEventArgs e)
        {
            GraphicCostFormation window = new GraphicCostFormation(this);
            window.Show();
        }

        private void ckSumIcmsIpi_Checked(object sender, RoutedEventArgs e)
        {
            if (ckSumIcmsIpi.IsChecked == true)
            {
                if ((txtIcms.Text != string.Empty) && (txtIpi.Text != string.Empty))
                {
                    double? ipi = string.IsNullOrEmpty(txtIpi.Text) ? 0 : Convert.ToDouble(txtIpi.Text);
                    double? icms = string.IsNullOrEmpty(txtIcms.Text) ? 0 : Convert.ToDouble(txtIcms.Text);
                    if (ipi > 0 && icms > 0)
                    {
                        double? result = (icms / 100) * ipi;
                        icms += Math.Round(Convert.ToDouble(result), 2);
                        txtIcms.Text = icms.ToString();
                        IPIICMS = Math.Round(Convert.ToDouble(result), 2);
                    }
                }
            }
        }

        private void ckSumIcmsIpi_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ckSumIcmsIpi.IsChecked == false)
            {
                if ((txtIcms.Text != string.Empty) && (txtIpi.Text != string.Empty))
                {
                    double? ipi = string.IsNullOrEmpty(txtIpi.Text) ? 0 : Convert.ToDouble(txtIpi.Text);
                    double? icms = string.IsNullOrEmpty(txtIcms.Text) ? 0 : Convert.ToDouble(txtIcms.Text);
                    if (ipi > 0 && icms > 0)
                    {
                        double? result = (icms / 100) * ipi;
                        icms -= IPIICMS;
                        txtIcms.Text = icms.ToString();
                    }
                }
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void cbItemGrid_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if (cbItemGrid.ItemsSource == null) return;
                EnabledFields();
                int id = string.IsNullOrEmpty(txtCustQuotaId.Text) ? 0 : Convert.ToInt32(txtCustQuotaId.Text);
                string item = cbItemGrid.SelectedItem.ToString();
                //totalOperations = CalculateOperations(id, item);
                if (rb100PercentServices.IsChecked == true)
                {
                    totalComponents = CalculateComponents(id, item);
                    unitValue = totalComponents;//(totalOperations + totalComponents);
                    var tsc = tcsService.GetAllWithParams(id, txtSelectItem.Text, item);
                    if (tsc != null)
                    {
                        ckSumIcmsIpi.IsChecked = tsc.SumIpiIcms == "1" ? true : false;
                        txtFreightValue.Text = tsc.Freight.ToString();
                        txtPis.Text = tsc.PIS.ToString();
                        txtCofins.Text = tsc.COFINS.ToString();
                        txtIcms.Text = tsc.ICMS.ToString();
                        txtIpi.Text = tsc.IPI.ToString();
                        txtComissions.Text = tsc.Comission.ToString();
                        txtFixedExpenses.Text = tsc.DF_Percent.ToString();
                        txtVariableExpenses.Text = tsc.DV_Percent.ToString();
                        txtMargin.Text = tsc.Margin.ToString();
                        txtVariableMargin.Text = tsc.MarginVariable.ToString();
                        txtISS.Text = tsc.ISS.ToString();
                        txtIR.Text = tsc.IR.ToString();
                        txtCSLL.Text = tsc.CSLL.ToString();
                        txtMarkup.Text = tsc.Markup.ToString();
                        txtTotalMarkup.Text = tsc.TotalMarkup.ToString();
                        txtTotalSimulation.Text = tsc.TotalSimulation.ToString();
                    }
                    else
                    {
                        txtTotalSimulation.Text = unitValue.ToString();
                        double?[] vet = serviceItem.GetTaxValues(item);
                        if (vet != null && vet.Length > 0)
                        {
                            txtPis.Text = vet[0].ToString();
                            txtCofins.Text = vet[1].ToString();
                            txtIcms.Text = vet[2].ToString();
                            txtIpi.Text = vet[3].ToString();
                            txtISS.Text = vet[4].ToString();
                            txtIR.Text = vet[5].ToString();
                            txtCSLL.Text = vet[6].ToString();
                            txtComissions.Text = vet[7].ToString();
                            LoadDFDV();
                            CalculateMarkup();
                        }
                    }
                }
                else
                {
                    totalComponents = CalculateServicesMaterial(id, item);
                    unitValue = totalComponents;//(totalOperations + totalComponents);
                    var tsc = tcsService.GetAllWithParams(id, txtSelectItem.Text, item);
                    if (tsc != null)
                    {
                        ckSumIcmsIpi.IsChecked = tsc.SumIpiIcms == "1" ? true : false;
                        txtFreightValue.Text = tsc.Freight.ToString();
                        txtPis.Text = tsc.PIS.ToString();
                        txtCofins.Text = tsc.COFINS.ToString();
                        txtIcms.Text = tsc.ICMS.ToString();
                        txtIpi.Text = tsc.IPI.ToString();
                        txtComissions.Text = tsc.Comission.ToString();
                        txtFixedExpenses.Text = tsc.DF_Percent.ToString();
                        txtVariableExpenses.Text = tsc.DV_Percent.ToString();
                        txtMargin.Text = tsc.Margin.ToString();
                        txtVariableMargin.Text = tsc.MarginVariable.ToString();
                        txtISS.Text = tsc.ISS.ToString();
                        txtIR.Text = tsc.IR.ToString();
                        txtCSLL.Text = tsc.CSLL.ToString();
                        txtMarkup.Text = tsc.Markup.ToString();
                        txtTotalMarkup.Text = tsc.TotalMarkup.ToString();
                        txtTotalSimulation.Text = tsc.TotalSimulation.ToString();
                    }
                    else
                    {
                        txtTotalSimulation.Text = unitValue.ToString();
                        double?[] vet = serviceItem.GetTaxValues(item);
                        if (vet != null && vet.Length > 0)
                        {
                            txtPis.Text = vet[0].ToString();
                            txtCofins.Text = vet[1].ToString();
                            txtIcms.Text = vet[2].ToString();
                            txtIpi.Text = vet[3].ToString();
                            txtISS.Text = vet[4].ToString();
                            txtIR.Text = vet[5].ToString();
                            txtCSLL.Text = vet[6].ToString();
                            txtComissions.Text = vet[7].ToString();
                            LoadDFDV();
                            CalculateMarkup();
                        }
                    }
                }
                CalculateFieldsView(Convert.ToDouble(unitValue));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void rb100PercentServices_Checked(object sender, RoutedEventArgs e)
        {
            if (rb100PercentServices.IsChecked == true)
            {
                rbServicePlusMaterial.IsChecked = !rb100PercentServices.IsChecked;
            }
        }

        private void rbServicePlusMaterial_Checked(object sender, RoutedEventArgs e)
        {
            if (rbServicePlusMaterial.IsChecked == true)
            {
                rb100PercentServices.IsChecked = !rbServicePlusMaterial.IsChecked;
            }
        }

        private double CalculateServicesMaterial(int id, string item)
        {
            try
            {
                double rateValue = serviceCompOp.GetNCMPartyRate(id, item);
                double totalcomp = serviceCompOp.GetBOMComponentsServices(id, item);

                double result = totalcomp + ((totalcomp / 100) * rateValue);

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
