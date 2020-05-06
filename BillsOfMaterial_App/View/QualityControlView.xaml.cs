using BillsOfMaterial_App.Model;
using BillsOfMaterial_App.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BillsOfMaterial_App.View
{
    /// <summary>
    /// Lógica interna para QualityControlView.xaml
    /// </summary>
    public partial class QualityControlView : Window
    {
        private ItemsView _window;
        private QualityControlService serviceQa;

        public QualityControlView(ItemsView window)
        {
            InitializeComponent();
            _window = window;
            serviceQa = new QualityControlService();
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-BR");
            Init();
        }

        private void Init()
        {

            List<CS_ItemsAnalysisParameters> listIAP = serviceQa.GetAll(Convert.ToInt32(_window._mainWindow.lblCustQuotaId.Content), _window.txtItem.Text.Trim());
            if (listIAP == null)
            {
                List<CS_ItemsAnalysisParameters> list = new List<CS_ItemsAnalysisParameters>
                {
                    new CS_ItemsAnalysisParameters()
                    {
                        Item = _window.txtItem.Text,
                        Parameter = "",
                        UoM = "",
                        ExpectedNumResult = 0,
                        AnalysisMethod = "",
                        AnalysisArea = 0,
                        LowerBound = "",
                        UpperBound = "",
                        DetectableBound = "",
                        Revision = 0,
                        InsertionDate = DateTime.Now,
                        IdSimulation = Convert.ToInt32(_window._mainWindow.lblCustQuotaId.Content)
                    }
                };

                dgQuality.ItemsSource = null;
                dgQuality.ItemsSource = list;
                dgQuality.SelectedIndex = 0;
            }
            else
            {
                dgQuality.ItemsSource = null;
                dgQuality.ItemsSource = listIAP;
                dgQuality.SelectedIndex = 0;
            }

            dgcParameter.ItemsSource = serviceQa.GetParametersQA();
            dgcAnalisysMethod.ItemsSource = serviceQa.GetAnalysisQA();
        }

        private void dgc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combobox = sender as ComboBox;
            var selectionItem = this.dgQuality.CurrentItem;
            string result = combobox.SelectedItem.ToString();
            GetCell(dgQuality, 1).Content = serviceQa.GetUoM(result);
        }

        private void dgc_SelectionChanged2(object sender, SelectionChangedEventArgs e)
        {
            var combobox = sender as ComboBox;
            var selectionItem = this.dgQuality.CurrentItem;
            string result = combobox.SelectedItem.ToString();
            GetCell(dgQuality, 4).Content = serviceQa.GetAnalysisDescription(result);
        }

        #region Utils

        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        public static DataGridCell GetCell(DataGrid grid, int column)
        {
            DataGridRow row = grid.ItemContainerGenerator.ContainerFromIndex(grid.SelectedIndex) as DataGridRow;

            if (row != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);

                if (presenter == null)
                {
                    grid.ScrollIntoView(row, grid.Columns[column]);
                    presenter = GetVisualChild<DataGridCellsPresenter>(row);
                }

                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                return cell;
            }
            return null;
        }

        #endregion

        private void dgQuality_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                //GetCell(dgQuality, 10).Content = DateTime.Now.ToString("dd/MM/yyyy");
                GetCell(dgQuality, 11).Content = Convert.ToInt32(_window._mainWindow.lblCustQuotaId.Content);
                GetCell(dgQuality, 12).Content = _window.txtItem.Text;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<CS_ItemsAnalysisParameters> list = dgQuality.ItemsSource as List<CS_ItemsAnalysisParameters>;
                if(list.Count > 0)
                {
                    int cont = 0;
                    int? idsimulation = 0;
                    string item = string.Empty;
                    foreach(var itemList in list)
                    {
                        itemList.UoM = serviceQa.GetUoM(itemList.Parameter);
                        if (cont == 0) 
                        {
                            idsimulation = itemList.IdSimulation;
                            item = itemList.Item;
                        }
                        if(cont > 0)
                        {
                            itemList.IdSimulation = idsimulation;
                            itemList.Item = item; 
                        }
                        cont++;
                    }
                }
                ItemsView.itemsAnalysisParametersSuperList.Add(list);
                int temp = ItemsView.itemsAnalysisParametersSuperList.Count;
                this.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
