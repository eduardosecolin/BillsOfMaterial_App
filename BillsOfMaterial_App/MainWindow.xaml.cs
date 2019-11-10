using BillsOfMaterial_App.Utilities;
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
using BillsOfMaterial_App.Service;
using BillsOfMaterial_App.View;
using BillsOfMaterial_App.Model;
using System.Collections.ObjectModel;
using System.Collections;

namespace BillsOfMaterial_App
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CustQuotasService custQuotasService = new CustQuotasService();
        private readonly CQBOMService cQBOMService = new CQBOMService();
        private readonly ItemsService itemService = new ItemsService();
        private readonly CustQuotaCompOpService compOpService = new CustQuotaCompOpService();

        public bool level1 = false;
        public bool level2 = false;
        public bool level3 = false;

        public int positionLine;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void LoadCbItemGrid()
        {
            try
            {
                var custQuota = custQuotasService.GetById(Convert.ToInt32(lblCustQuotaId.Content.ToString()));

                if (custQuota != null)
                {
                    cbItemGrid.ItemsSource = null;
                    List<string> items = new List<string>();
                    foreach (var item in custQuotasService.GetAll(custQuota.CustQuotaId))
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

        private void BtnComponent_Click(object sender, RoutedEventArgs e)
        {
            level1 = true;
            level2 = false;
            level3 = false;
            ItemsView window = new ItemsView(this);
            window.Show();
        }

        private void BtnOperation_Click(object sender, RoutedEventArgs e)
        {
            level1 = true;
            level2 = false;
            level3 = false;
            OperationView window = new OperationView(this);
            window.Show();
        }

        private void CbItemGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cbItemGrid.Text != string.Empty)
            {
                tvBOM.Visibility = Visibility.Visible;
                txtDrawing.Visibility = Visibility.Visible;
                txtTecConclusion.Visibility = Visibility.Visible;
                btnDrawing.Visibility = Visibility.Visible;
                btnSaveSimulation.IsEnabled = true;
                btnClear.IsEnabled = true;
            }
        }

        private void TvBOM_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var treeview = tvBOM.SelectedItem as TreeViewItem;
                if (treeview.Header.ToString() != "Componente")
                {
                    StackPanel sp = treeview.Header as StackPanel;
                    if (sp != null)
                    {
                        if (sp.Children.Count > 1)
                        {
                            foreach (var item in sp.Children)
                            {
                                var label = item as Label;
                                if (label != null)
                                {
                                    switch (label.Content.ToString())
                                    {
                                        case "Componente nv.2":
                                            level1 = false;
                                            level2 = true;
                                            level3 = false;
                                            ItemsView window = new ItemsView(this);
                                            window.Show();
                                            break;
                                        case "Componente nv.3":
                                            level1 = false;
                                            level2 = false;
                                            level3 = true;
                                            ItemsView window2 = new ItemsView(this);
                                            window2.Show();
                                            break;
                                        case "Componente nv.4":
                                            level1 = false;
                                            level2 = false;
                                            level3 = true;
                                            ItemsView window3 = new ItemsView(this);
                                            window3.Show();
                                            break;
                                        case "Operação nv.2":
                                            level1 = false;
                                            level2 = true;
                                            OperationView window4 = new OperationView(this);
                                            window4.Show();
                                            break;
                                        case "Operação nv.3":
                                            level1 = false;
                                            level2 = true;
                                            OperationView window5 = new OperationView(this);
                                            window5.Show();
                                            break;
                                    }
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception /*ex*/)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region ADD COMPONENTS LEVELS

        private bool AddComplevel1()
        {
            try
            {
                foreach (var item in tvComponent.Items)
                {
                    TreeViewItem selectedNode = (TreeViewItem)item;
                    CS_CustQuotasComponent comp = new CS_CustQuotasComponent();
                    comp.Line = compOpService.GetMaxLineComp();
                    comp.Id = int.Parse(lblCustQuotaId.Content.ToString());
                    comp.BOM = cbItemGrid.Text;
                    comp.Item = cbItemGrid.Text;
                    comp.Drawing = txtDrawing.Text;
                    comp.TecConclusion = txtTecConclusion.Text;
                    if (selectedNode.Items.Count > 1)
                    {
                        foreach (var item2 in selectedNode.Items)
                        {
                            StackPanel stackPanel = selectedNode.Header as StackPanel;
                            if (stackPanel.Children.Count == 2)
                            {
                                foreach (var child in stackPanel.Children)
                                {
                                    TextBlock text = child as TextBlock;
                                    string[] vetItem = text.Text.ToString().Split('|');
                                    if (vetItem != null)
                                    {
                                        comp.Component = vetItem[0].Trim();
                                        comp.Description = vetItem[1];

                                    }
                                    comp.UoM = itemService.GetUoMItem(comp.Component);

                                    if (vetItem.Length > 3)
                                    {
                                        foreach (var o in vetItem)
                                        {
                                            if (o.Contains("Quantidade:"))
                                            {
                                                string[] vetQty = o.Split(':');
                                                if (vetQty.Length > 1)
                                                {
                                                    comp.Qty = Convert.ToDouble(vetQty[1]);
                                                }
                                            }
                                            if (o.Contains("OBS:"))
                                            {
                                                string[] vetObs = o.Split(':');
                                                if (vetObs.Length > 1)
                                                {
                                                    comp.Obs = vetObs[1];
                                                }
                                            }
                                            if (o.Contains("Imagem:"))
                                            {
                                                string[] vetDraw = o.Split(':');
                                                if (vetDraw.Length > 1)
                                                {
                                                    comp.PathFile1 = vetDraw[1];
                                                }
                                            }
                                            if (o.Contains("Custo R1:"))
                                            {
                                                string[] vetR1 = o.Split(':');
                                                if (vetR1.Length > 1)
                                                {
                                                    string vet1 = vetR1[0];
                                                    string vet2 = vetR1[1].Trim();
                                                    if (vet2 != "")
                                                    {
                                                        comp.R1Costvalue = Convert.ToDouble(vetR1[1]);
                                                    }
                                                    else
                                                    {
                                                        comp.R1Costvalue = 0;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        comp.Obs = string.Empty;
                                    }

                                    break;

                                }
                            }

                            break;
                        }
                    }
                    else
                    {
                        string[] vetItem = selectedNode.Header.ToString().Split('|');
                        if (vetItem != null)
                        {
                            comp.Component = vetItem[0].Trim();
                            comp.Description = vetItem[1];

                        }
                        comp.UoM = itemService.GetUoMItem(comp.Component);

                        if (vetItem.Length > 0)
                        {
                            foreach (var o in vetItem)
                            {
                                if (o.Contains("Quantidade:"))
                                {
                                    string[] vetQty = o.Split(':');
                                    if (vetQty.Length > 1)
                                    {
                                        comp.Qty = Convert.ToDouble(vetQty[1]);
                                    }
                                }
                                if (o.Contains("OBS:"))
                                {
                                    string[] vetObs = o.Split(':');
                                    if (vetObs.Length > 1)
                                    {
                                        comp.Obs = vetObs[1];
                                    }
                                }
                                if (o.Contains("Imagem:"))
                                {
                                    string[] vetDraw = o.Split(':');
                                    if (vetDraw.Length > 1)
                                    {
                                        comp.PathFile1 = vetDraw[1];
                                    }
                                }
                                if (o.Contains("Custo R1:"))
                                {
                                    string[] vetR1 = o.Split(':');
                                    if (vetR1.Length > 1)
                                    {
                                        string vet1 = vetR1[0];
                                        string vet2 = vetR1[1].Trim();
                                        if (vet2 != "")
                                        {
                                            comp.R1Costvalue = Convert.ToDouble(vetR1[1]);
                                        }
                                        else
                                        {
                                            comp.R1Costvalue = 0;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            comp.Obs = string.Empty;
                        }
                    }
                    comp.TBCreated = DateTime.Now;
                    comp.TBModified = DateTime.Now;
                    comp.TBCreatedID = 1;

                    compOpService.SaveComp(comp);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private bool AddOplevel1()
        {
            try
            {
                foreach (var item in tvOperation.Items)
                {
                    TreeViewItem selectedNode = (TreeViewItem)item;
                    CS_CustQuotasOperation op = new CS_CustQuotasOperation();
                    op.Line = compOpService.GetMaxLineOp();
                    op.Id = int.Parse(lblCustQuotaId.Content.ToString());
                    op.BOM = cbItemGrid.Text;
                    op.Item = cbItemGrid.Text;
                    string[] vetItem = selectedNode.Header.ToString().Split('|');
                    if (vetItem != null)
                    {
                        op.Operation = vetItem[0].Trim();
                        op.DescriptionOperation = vetItem[1];
                    }

                    if (vetItem.Length > 0)
                    {
                        foreach (var o in vetItem)
                        {
                            if (o.Contains("OBS:"))
                            {
                                string[] vetObs = o.Split(':');
                                if (vetObs.Length > 1)
                                {
                                    op.Obs = vetObs[1];
                                }
                            }

                            if (o.Contains("Tempo de Processamento"))
                            {
                                string[] vetObs = o.Split('=');
                                if (vetObs.Length > 1)
                                {
                                    op.TimeProcess = Convert.ToDateTime(vetObs[1]);
                                }
                            }
                        }
                    }

                    op.TBCreated = DateTime.Now;
                    op.TBModified = DateTime.Now;

                    compOpService.SaveOp(op);
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private bool AddComplevel2()
        {
            try
            {
                bool onlyOneIntered = false;
                foreach (var item in tvComponent.Items)
                {
                    if (onlyOneIntered) { onlyOneIntered = false; }
                    TreeViewItem selectedNode = (TreeViewItem)item;
                    if (selectedNode.Items.Count > 0)
                    {
                        StackPanel st = selectedNode.Header as StackPanel;
                        if (st != null && st.Children.Count > 1)
                        {
                            foreach (var child in selectedNode.Items)
                            {
                                TreeViewItem compNode = (TreeViewItem)child;
                                if (compNode.Items.Count > 0)
                                {
                                    foreach (var childNode in compNode.Items)
                                    {
                                        StackPanel stp = compNode.Header as StackPanel;
                                        if (stp.Children.Count == 2)
                                        {
                                            foreach (var spanel in stp.Children)
                                            {
                                                if (spanel is Label)
                                                {
                                                    Label lbl = spanel as Label;
                                                    if (lbl.Content.ToString().Equals("Componente nv.2", StringComparison.OrdinalIgnoreCase) && onlyOneIntered == false)
                                                    {

                                                        foreach (var comps in compNode.Items)
                                                        {
                                                            int cont = compNode.Items.Count;
                                                            CS_CustQuotasComponent comp = new CS_CustQuotasComponent();
                                                            foreach (var sti in st.Children)
                                                            {
                                                                TextBlock tb = sti as TextBlock;
                                                                string[] vet = tb.Text.Split('|');
                                                                if (vet != null)
                                                                {
                                                                    comp.BOM = vet[0];
                                                                    break;
                                                                }
                                                            }                                                  
                                                            comp.Line = compOpService.GetMaxLineComp();
                                                            comp.Id = int.Parse(lblCustQuotaId.Content.ToString());
                                                            comp.Item = cbItemGrid.Text;
                                                            comp.Drawing = txtDrawing.Text;
                                                            comp.TecConclusion = txtTecConclusion.Text;

                                                            TreeViewItem components = comps as TreeViewItem;

                                                            if (components.Header is StackPanel)
                                                            {

                                                                StackPanel stackPanel = components.Header as StackPanel;

                                                                foreach (var stack in stackPanel.Children)
                                                                {
                                                                    TextBlock text = stack as TextBlock;
                                                                    string[] vetItem = text.Text.ToString().Split('|');
                                                                    if (vetItem != null)
                                                                    {
                                                                        comp.Component = vetItem[0].Trim();
                                                                        comp.Description = vetItem[1];
                                                                    }
                                                                    comp.UoM = itemService.GetUoMItem(comp.Component);

                                                                    if (vetItem.Length > 3)
                                                                    {
                                                                        foreach (var o in vetItem)
                                                                        {
                                                                            if (o.Contains("Quantidade:"))
                                                                            {
                                                                                string[] vetQty = o.Split(':');
                                                                                if (vetQty.Length > 1)
                                                                                {
                                                                                    comp.Qty = Convert.ToDouble(vetQty[1]);
                                                                                }
                                                                            }
                                                                            if (o.Contains("OBS:"))
                                                                            {
                                                                                string[] vetObs = o.Split(':');
                                                                                if (vetObs.Length > 1)
                                                                                {
                                                                                    comp.Obs = vetObs[1];
                                                                                }
                                                                            }
                                                                            if (o.Contains("Imagem:"))
                                                                            {
                                                                                string[] vetDraw = o.Split(':');
                                                                                if (vetDraw.Length > 1)
                                                                                {
                                                                                    comp.PathFile1 = vetDraw[1];
                                                                                }
                                                                            }
                                                                            if (o.Contains("Custo R1:"))
                                                                            {
                                                                                string[] vetR1 = o.Split(':');
                                                                                if (vetR1.Length > 1)
                                                                                {
                                                                                    string vet1 = vetR1[0];
                                                                                    string vet2 = vetR1[1].Trim();
                                                                                    if (vet2 != "")
                                                                                    {
                                                                                        comp.R1Costvalue = Convert.ToDouble(vetR1[1]);
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        comp.R1Costvalue = 0;
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        comp.Obs = string.Empty;
                                                                    }

                                                                    break;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                string[] vetItem = components.Header.ToString().Split('|');
                                                                if (vetItem != null)
                                                                {
                                                                    comp.Component = vetItem[0].Trim();
                                                                    comp.Description = vetItem[1];
                                                                }
                                                                comp.UoM = itemService.GetUoMItem(comp.Component);

                                                                if (vetItem.Length > 3)
                                                                {
                                                                    foreach (var o in vetItem)
                                                                    {
                                                                        if (o.Contains("Quantidade:"))
                                                                        {
                                                                            string[] vetQty = o.Split(':');
                                                                            if (vetQty.Length > 1)
                                                                            {
                                                                                comp.Qty = Convert.ToDouble(vetQty[1]);
                                                                            }
                                                                        }
                                                                        if (o.Contains("OBS:"))
                                                                        {
                                                                            string[] vetObs = o.Split(':');
                                                                            if (vetObs.Length > 1)
                                                                            {
                                                                                comp.Obs = vetObs[1];
                                                                            }
                                                                        }
                                                                        if (o.Contains("Imagem:"))
                                                                        {
                                                                            string[] vetDraw = o.Split(':');
                                                                            if (vetDraw.Length > 1)
                                                                            {
                                                                                comp.PathFile1 = vetDraw[1];
                                                                            }
                                                                        }
                                                                        if (o.Contains("Custo R1:"))
                                                                        {
                                                                            string[] vetR1 = o.Split(':');
                                                                            if (vetR1.Length > 1)
                                                                            {
                                                                                string vet1 = vetR1[0];
                                                                                string vet2 = vetR1[1].Trim();
                                                                                if (vet2 != "")
                                                                                {
                                                                                    comp.R1Costvalue = Convert.ToDouble(vetR1[1]);
                                                                                }
                                                                                else
                                                                                {
                                                                                    comp.R1Costvalue = 0;
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    comp.Obs = string.Empty;
                                                                }
                                                            }
                                                            comp.TBCreated = DateTime.Now;
                                                            comp.TBModified = DateTime.Now;
                                                            comp.TBCreatedID = 2;

                                                            compOpService.SaveComp(comp);

                                                            onlyOneIntered = true;
                                                        }


                                                    }
                                                    else if (lbl.Content.ToString().Equals("Operação nv.2", StringComparison.OrdinalIgnoreCase) && onlyOneIntered == true)
                                                    {

                                                        foreach (var ops in compNode.Items)
                                                        {
                                                            CS_CustQuotasOperation op = new CS_CustQuotasOperation();
                                                            foreach (var sti in st.Children)
                                                            {
                                                                TextBlock tb = sti as TextBlock;
                                                                string[] vet = tb.Text.Split('|');
                                                                if (vet != null)
                                                                {
                                                                    op.BOM = vet[0];
                                                                    break;
                                                                }
                                                            }

                                                            op.Line = compOpService.GetMaxLineOp();
                                                            op.Id = int.Parse(lblCustQuotaId.Content.ToString());
                                                            op.Item = cbItemGrid.Text;

                                                            TreeViewItem operations = ops as TreeViewItem;
                                                            string[] vetItem2 = operations.Header.ToString().Split('|');
                                                            if (vetItem2 != null)
                                                            {
                                                                op.Operation = vetItem2[0].Trim();
                                                                op.DescriptionOperation = vetItem2[1];
                                                            }

                                                            if (vetItem2.Length > 0)
                                                            {
                                                                foreach (var o in vetItem2)
                                                                {
                                                                    if (o.Contains("OBS:"))
                                                                    {
                                                                        string[] vetObs = o.Split(':');
                                                                        if (vetObs.Length > 1)
                                                                        {
                                                                            op.Obs = vetObs[1];
                                                                        }
                                                                    }

                                                                    if (o.Contains("Tempo de Processamento"))
                                                                    {
                                                                        string[] vetObs = o.Split('=');
                                                                        if (vetObs.Length > 1)
                                                                        {
                                                                            op.TimeProcess = Convert.ToDateTime(vetObs[1]);
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                            op.TBCreated = DateTime.Now;
                                                            op.TBModified = DateTime.Now;

                                                            compOpService.SaveOp(op);

                                                            onlyOneIntered = false;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private bool AddComplevel3()
        {
            try
            {
                List<TreeViewItem> tvLevelTwo = GetLogicalChildCollection<TreeViewItem>(tvComponent);
                foreach (var item in tvLevelTwo)
                {
                    if (item.Name == "tvLevel2")
                    {
                        if (item.Header is StackPanel)
                        {
                            StackPanel st = item.Header as StackPanel;
                            foreach (var item2 in st.Children)
                            {
                                if (item2 is TextBlock)
                                {
                                    TextBlock lbl = item2 as TextBlock;
                                    string content = lbl.Text;
                                    List<TreeViewItem> tvLevel2 = GetLogicalChildCollection<TreeViewItem>(item);
                                    foreach (var item3 in tvLevel2)
                                    {
                                        if (item3.Name == "tvLevel3")
                                        {
                                            CS_CustQuotasComponent comp = new CS_CustQuotasComponent();
                                            string[] vet = content.Split('|');
                                            if (vet != null)
                                            {
                                                comp.BOM = vet[0];
                                            }
                                            comp.Line = compOpService.GetMaxLineComp();
                                            comp.Id = int.Parse(lblCustQuotaId.Content.ToString());
                                            comp.Item = cbItemGrid.Text;
                                            comp.Drawing = txtDrawing.Text;
                                            comp.TecConclusion = txtTecConclusion.Text;

                                            if (item3.Header is StackPanel)
                                            {
                                                StackPanel st1 = item3.Header as StackPanel;
                                                foreach (var item4 in st1.Children)
                                                {
                                                    if (item4 is TextBlock)
                                                    {
                                                        TextBlock lbl2 = item4 as TextBlock;
                                                        string[] vetItem = lbl2.Text.ToString().Split('|');
                                                        if (vetItem != null)
                                                        {
                                                            comp.Component = vetItem[0].Trim();
                                                            comp.Description = vetItem[1];
                                                        }
                                                        comp.UoM = itemService.GetUoMItem(comp.Component);

                                                        if (vetItem.Length > 3)
                                                        {
                                                            foreach (var o in vetItem)
                                                            {
                                                                if (o.Contains("Quantidade:"))
                                                                {
                                                                    string[] vetQty = o.Split(':');
                                                                    if (vetQty.Length > 1)
                                                                    {
                                                                        comp.Qty = Convert.ToDouble(vetQty[1]);
                                                                    }
                                                                }
                                                                if (o.Contains("OBS:"))
                                                                {
                                                                    string[] vetObs = o.Split(':');
                                                                    if (vetObs.Length > 1)
                                                                    {
                                                                        comp.Obs = vetObs[1];
                                                                    }
                                                                }
                                                                if (o.Contains("Imagem:"))
                                                                {
                                                                    string[] vetDraw = o.Split(':');
                                                                    if (vetDraw.Length > 1)
                                                                    {
                                                                        comp.PathFile1 = vetDraw[1];
                                                                    }
                                                                }
                                                                if (o.Contains("Custo R1:"))
                                                                {
                                                                    string[] vetR1 = o.Split(':');
                                                                    if (vetR1.Length > 1)
                                                                    {
                                                                        string vet1 = vetR1[0];
                                                                        string vet2 = vetR1[1].Trim();
                                                                        if (vet2 != "")
                                                                        {
                                                                            comp.R1Costvalue = Convert.ToDouble(vetR1[1]);
                                                                        }
                                                                        else
                                                                        {
                                                                            comp.R1Costvalue = 0;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            comp.Obs = string.Empty;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                string[] vetItem = item3.Header.ToString().Split('|');
                                                if (vetItem != null)
                                                {
                                                    comp.Component = vetItem[0].Trim();
                                                    comp.Description = vetItem[1];
                                                }
                                                comp.UoM = itemService.GetUoMItem(comp.Component);

                                                if (vetItem.Length > 3)
                                                {
                                                    foreach (var o in vetItem)
                                                    {
                                                        if (o.Contains("Quantidade:"))
                                                        {
                                                            string[] vetQty = o.Split(':');
                                                            if (vetQty.Length > 1)
                                                            {
                                                                comp.Qty = Convert.ToDouble(vetQty[1]);
                                                            }
                                                        }
                                                        if (o.Contains("OBS:"))
                                                        {
                                                            string[] vetObs = o.Split(':');
                                                            if (vetObs.Length > 1)
                                                            {
                                                                comp.Obs = vetObs[1];
                                                            }
                                                        }
                                                        if (o.Contains("Imagem:"))
                                                        {
                                                            string[] vetDraw = o.Split(':');
                                                            if (vetDraw.Length > 1)
                                                            {
                                                                comp.PathFile1 = vetDraw[1];
                                                            }
                                                        }
                                                        if (o.Contains("Custo R1:"))
                                                        {
                                                            string[] vetR1 = o.Split(':');
                                                            if (vetR1.Length > 1)
                                                            {
                                                                string vet1 = vetR1[0];
                                                                string vet2 = vetR1[1].Trim();
                                                                if (vet2 != "")
                                                                {
                                                                    comp.R1Costvalue = Convert.ToDouble(vetR1[1]);
                                                                }
                                                                else
                                                                {
                                                                    comp.R1Costvalue = 0;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    comp.Obs = string.Empty;
                                                }
                                            }
                                            comp.TBCreated = DateTime.Now;
                                            comp.TBModified = DateTime.Now;
                                            comp.TBCreatedID = 3;

                                            compOpService.SaveComp(comp);
                                        }
                                    }
                                }
                            }
                        }
                    }

                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private bool AddOplevel3()
        {
            try
            {
                List<TreeViewItem> tvLevelTwo = GetLogicalChildCollection<TreeViewItem>(tvComponent);
                string content = string.Empty;
                foreach (var item in tvLevelTwo)
                {
                    if (item.Name == "tvLevel2")
                    {
                        if (item.Header is StackPanel)
                        {
                            StackPanel st = item.Header as StackPanel;
                            foreach (var item2 in st.Children)
                            {
                                if (item2 is TextBlock)
                                {
                                    TextBlock lbl = item2 as TextBlock;
                                    content = lbl.Text;
                                }
                            }
                        }
                    }
                    if (item.Name == "tvOpLabelLevel2")
                    {
                        if (item.Header is StackPanel)
                        {
                            StackPanel st = item.Header as StackPanel;
                            foreach (var item2 in st.Children)
                            {
                                if (item2 is Label)
                                {
                                    Label lbl = item2 as Label;
                                    string content2 = lbl.Content.ToString();
                                    if (content2 == "Operação nv.3")
                                    {
                                        List<TreeViewItem> tvLevel2 = GetLogicalChildCollection<TreeViewItem>(item);
                                        foreach (var item3 in tvLevel2)
                                        {
                                            if (item3.Name == "tvOpLevel2")
                                            {
                                                CS_CustQuotasOperation op = new CS_CustQuotasOperation();
                                                string[] vet = content.Split('|');
                                                if (vet != null)
                                                {
                                                    op.BOM = vet[0];
                                                }
                                                op.Line = compOpService.GetMaxLineOp();
                                                op.Id = int.Parse(lblCustQuotaId.Content.ToString());
                                                op.Item = cbItemGrid.Text;

                                                string[] vetItem = item3.Header.ToString().Split('|');
                                                if (vetItem != null)
                                                {
                                                    op.Operation = vetItem[0].Trim();
                                                    op.DescriptionOperation = vetItem[1];
                                                }

                                                if (vetItem.Length > 0)
                                                {
                                                    foreach (var o in vetItem)
                                                    {
                                                        if (o.Contains("OBS:"))
                                                        {
                                                            string[] vetObs = o.Split(':');
                                                            if (vetObs.Length > 1)
                                                            {
                                                                op.Obs = vetObs[1];
                                                            }
                                                        }

                                                        if (o.Contains("Tempo de Processamento"))
                                                        {
                                                            string[] vetObs = o.Split('=');
                                                            if (vetObs.Length > 1)
                                                            {
                                                                op.TimeProcess = Convert.ToDateTime(vetObs[1]);
                                                            }
                                                        }
                                                    }
                                                }

                                                op.TBCreated = DateTime.Now;
                                                op.TBModified = DateTime.Now;

                                                compOpService.SaveOp(op);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        #endregion

        private void CalcFormationCost()
        {

        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            tvBOM.IsEnabled = false;
            CostFormationView window = new CostFormationView();
            window.Show();
        }

        public void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            tvComponent.Items.Clear();
            tvOperation.Items.Clear();
            txtTecConclusion.Clear();
            txtDrawing.Clear();
            lblCustQuotaId.Content = string.Empty;
            lblNoCustQuota.Content = "Selecione a Oferta";
            cbItemGrid.Text = string.Empty;
            tvBOM.Visibility = Visibility.Hidden;
            txtDrawing.Visibility = Visibility.Hidden;
            txtTecConclusion.Visibility = Visibility.Hidden;
            btnNext.IsEnabled = false;
            btnClear.IsEnabled = false;
            txtDrawing.Clear();
            if(tvBOM.IsEnabled == false)
            {
                tvBOM.IsEnabled = true;
            }
        }

        private void BtnImport_Click(object sender, RoutedEventArgs e)
        {
            if (cbItemGrid.Text != string.Empty)
            {
                ImportBOMView window = new ImportBOMView(this);
                window.Show();
            }
            else
            {
                MessageBox.Show("Selecione um item para importar uma simulação de engenharia!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TvBOM_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var treeview = tvBOM.SelectedItem as TreeViewItem;
                if (treeview.Name == "treeViewLv1" || treeview.Name == "tvLevel2" || treeview.Name == "tvLevel3" ||
                    treeview.Name == "tvOpLevel1" || treeview.Name == "tvOpLevel2")
                {
                    ContextMenu contextMenu = new ContextMenu();
                    MenuItem m1 = new MenuItem()
                    {
                        Header = "Editar",
                    };
                    m1.Click += EditCurrentItemTreeView;
                    MenuItem m2 = new MenuItem()
                    {
                        Header = "Deletar"
                    };
                    m2.Click += DeleteCurrentItemTreeView;

                    contextMenu.Items.Add(m1);
                    contextMenu.Items.Add(m2);

                    treeview.ContextMenu = contextMenu;

                    treeview.ContextMenu.IsOpen = true;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Selecione um item!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void EditCurrentItemTreeView(object sender, RoutedEventArgs e)
        {
            try
            {

                var treeview = tvBOM.SelectedItem as TreeViewItem;
                if (treeview.Name == "treeViewLv1" || treeview.Name == "tvLevel2" || treeview.Name == "tvLevel3")
                {
                    ItemsView window = new ItemsView(this);
                    if (treeview.Header is StackPanel)
                    {
                        StackPanel panel = treeview.Header as StackPanel;
                        if (panel.Children.Count > 0)
                        {
                            foreach (var item in panel.Children)
                            {
                                if (item is TextBlock)
                                {
                                    TextBlock txt = item as TextBlock;
                                    string[] vet = txt.Text.Split('|');
                                    if (vet != null)
                                    {
                                        window.isEditmode = true;
                                        window.txtItem.Text = vet[0];
                                        if (vet.Length > 0)
                                        {
                                            foreach (var item2 in vet)
                                            {
                                                if (item2.Contains("Quantidade:"))
                                                {
                                                    string[] vetQty = item2.Split(':');
                                                    if (vetQty.Length > 1)
                                                    {
                                                        window.txtQty.Text = vetQty[1];
                                                    }
                                                }
                                                if (item2.Contains("OBS:"))
                                                {
                                                    string[] vetObs = item2.Split(':');
                                                    if (vetObs.Length > 1)
                                                    {
                                                        window.txtObs.Text = vetObs[1];
                                                    }
                                                }
                                                if (item2.Contains("Imagem:"))
                                                {
                                                    string[] vetDraw = item2.Split(':');
                                                    if (vetDraw.Length > 1)
                                                    {
                                                        window.txtDrawing.Text = vetDraw[1];
                                                    }
                                                }
                                                if (item2.Contains("Custo R1:"))
                                                {
                                                    string[] vetR1 = item2.Split(':');
                                                    if (vetR1.Length > 1)
                                                    {
                                                        window.txtResultValue.Text = vetR1[1];
                                                    }
                                                }

                                                window.Show();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        string[] vet = treeview.Header.ToString().Split('|');
                        if (vet != null)
                        {
                            window.isEditmode = true;
                            window.txtItem.Text = vet[0];
                            if (vet.Length > 0)
                            {
                                foreach (var item in vet)
                                {
                                    if (item.Contains("Quantidade:"))
                                    {
                                        string[] vetQty = item.Split(':');
                                        if(vetQty.Length > 1)
                                        {
                                            window.txtQty.Text = vetQty[1];
                                        }
                                    }
                                    if (item.Contains("OBS:"))
                                    {
                                        string[] vetObs= item.Split(':');
                                        if (vetObs.Length > 1)
                                        {
                                            window.txtObs.Text = vetObs[1];
                                        }
                                    }
                                    if (item.Contains("Imagem:"))
                                    {
                                        string[] vetDraw = item.Split(':');
                                        if (vetDraw.Length > 1)
                                        {
                                            window.txtDrawing.Text = vetDraw[1];
                                        }
                                    }
                                    if (item.Contains("Custo R1:"))
                                    {
                                        string[] vetR1 = item.Split(':');
                                        if (vetR1.Length > 1)
                                        {
                                            window.txtResultValue.Text = vetR1[1];
                                        }
                                    }

                                    window.Show();
                                }
                            }                                                    
                        }
                    }
                }
                else if (treeview.Name == "tvOpLevel1" || treeview.Name == "tvOpLevel2")
                {
                    OperationView window = new OperationView(this);
                    string[] vet = treeview.Header.ToString().Split('|');
                    if (vet != null)
                    {
                        window.isEditmode = true;
                        window.txtOperation.Text = vet[0];
                        foreach (var item in vet)
                        {
                            if (vet.Length > 0)
                            {
                                if (item.Contains("Tempo de Processamento ="))
                                {
                                    string[] vetP = item.Split('=');
                                    if (vetP.Length > 1)
                                    {
                                        window.dpTimeProcess.Text = vetP[1];
                                    }
                                }
                                if (item.Contains("OBS:"))
                                {
                                    string[] vetObs = item.Split(':');
                                    if (vetObs.Length > 1)
                                    {
                                        window.txtObs.Text = vetObs[1];
                                    }
                                }
                            }
                        }

                        window.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteCurrentItemTreeView(object sender, RoutedEventArgs e)
        {
            var treeview = tvBOM.SelectedItem as TreeViewItem;
            if (treeview != null)
            {
                if (treeview.Name == "treeViewLv1")
                {
                    tvComponent.Items.Remove(tvBOM.SelectedItem);

                }
                else if (treeview.Name == "tvLevel2")
                {
                    (treeview.Parent as TreeViewItem).Items.Remove(tvBOM.SelectedItem);

                }
                else if (treeview.Name == "tvLevel3")
                {
                    (treeview.Parent as TreeViewItem).Items.Remove(tvBOM.SelectedItem);
                }
                else if (treeview.Name == "tvOpLevel1")
                {
                    tvOperation.Items.Remove(tvBOM.SelectedItem);
                }
                else if (treeview.Name == "tvOpLevel2")
                {
                    (treeview.Parent as TreeViewItem).Items.Remove(tvBOM.SelectedItem);
                }
            }
        }

        #region OTHERS METHODS

        private List<T> GetLogicalChildCollection<T>(object parent) where T : DependencyObject
        {
            List<T> logicalCollection = new List<T>();
            GetLogicalChildCollection(parent as DependencyObject, logicalCollection);
            return logicalCollection;
        }

        private void GetLogicalChildCollection<T>(DependencyObject parent, List<T> logicalCollection) where T : DependencyObject
        {
            IEnumerable children = LogicalTreeHelper.GetChildren(parent);
            foreach (object child in children)
            {
                if (child is DependencyObject)
                {
                    DependencyObject depChild = child as DependencyObject;
                    if (child is T)
                    {
                        logicalCollection.Add(child as T);
                    }
                    GetLogicalChildCollection(depChild, logicalCollection);
                }
            }
        }

        #endregion

        private void BtnSearchOffer_Click(object sender, RoutedEventArgs e)
        {
            OfferSearchView window = new OfferSearchView(this);
            window.Show();
        }

        public bool SaveSimulation()
        {
            try
            {
                if(compOpService.ExistData(Convert.ToInt32(lblCustQuotaId.Content.ToString()), cbItemGrid.Text))
                {
                    MessageBoxResult resultDialog = MessageBox.Show($"Já existe simulação para a oferta nº: { lblNoCustQuota.Content.ToString() } para o item nº: { cbItemGrid.Text }" +
                        $"\n Deletar a simulaçao existente e salvar a nova?",
                        "Pergunta",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question
                        );

                    if (resultDialog == MessageBoxResult.Yes)
                    {
                        compOpService.Delete(Convert.ToInt32(lblCustQuotaId.Content.ToString()), cbItemGrid.Text);
                    }
                    else
                    {
                        return false;
                    }
                }
                //Eng Level 1
                if (AddComplevel1() && AddOplevel1())
                {
                    // Eng Level 2
                    if (AddComplevel2())
                    {
                        if (AddComplevel3() && AddOplevel3())
                        {

                            return true;
                        }
                        else
                        {
                            MessageBox.Show(
                                "Erro ao gravar engenharia de nivel 3"
                                , "Mensagem"
                                , MessageBoxButton.OK
                                , MessageBoxImage.Information
                                );

                            compOpService.Delete(Convert.ToInt32(lblCustQuotaId.Content.ToString()), cbItemGrid.Text);
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show(
                               "Erro ao gravar engenharia de nivel 2"
                               , "Mensagem"
                               , MessageBoxButton.OK
                               , MessageBoxImage.Information
                               );

                        compOpService.Delete(Convert.ToInt32(lblCustQuotaId.Content.ToString()), cbItemGrid.Text);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show(
                               "Erro ao gravar engenharia de nivel 1"
                               , "Mensagem"
                               , MessageBoxButton.OK
                               , MessageBoxImage.Information
                               );

                    compOpService.Delete(Convert.ToInt32(lblCustQuotaId.Content.ToString()), cbItemGrid.Text);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + "Reinicie o aplicativo!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
                return false;
            }
        }

        private void CbItemGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            positionLine = cbItemGrid.SelectedIndex + 1;
        }

        private void BtnDrawing_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbItemGrid.Text != string.Empty)
                {
                    DrawingView window = new DrawingView(this);
                    window.Show();
                }
                else
                {
                    MessageBox.Show("Selecione um item para simular a engenharia!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Erro ao abrir tela de Desenhos!" + "\n" + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnSaveSimulation_Click(object sender, RoutedEventArgs e)
        {
            if (SaveSimulation())
            {
                MessageBox.Show("Simulação de Engenharia de Produtos salva com sucesso!", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                BtnClear_Click(sender, e);
            }
        }

        private void BtnRegisterItems_Click_1(object sender, RoutedEventArgs e)
        {
            RegisterItem window = new RegisterItem();
            window.Show();
        }
    }
}
