﻿using BillsOfMaterial_App.Model;
using BillsOfMaterial_App.Service;
using BillsOfMaterial_App.View;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace BillsOfMaterial_App.AuxView
{
    /// <summary>
    /// Interação lógica para SimulationEDP.xam
    /// </summary>
    public partial class SimulationEDP : UserControl
    {
        #region GLOBAL ATTRIBUTES

        private readonly CustQuotasService custQuotasService = new CustQuotasService();
        private readonly CQBOMService cQBOMService = new CQBOMService();
        private readonly ItemsService itemService = new ItemsService();
        private readonly CustQuotaCompOpService compOpService = new CustQuotaCompOpService();
        private readonly BOMService bomService = new BOMService();
        private readonly ItemsService itemsService = new ItemsService();
        private readonly OperationService opService = new OperationService();
        private readonly OnOffService onoffService = new OnOffService();
        private readonly CustQuotasCompAttachService attachService = new CustQuotasCompAttachService();
        private readonly QualityControlService serviceQA = new QualityControlService();
        public Dictionary<int, List<CS_CustQuotasCompAttach>> AttachMap = new Dictionary<int, List<CS_CustQuotasCompAttach>>();
        private string longPath;

        public bool level1 = false;
        public bool level2 = false;
        public bool level3 = false;

        public static bool hasAccess = true;
        public static bool isClear = false;
        public int positionLine;
        public static int id_Offer = 0;
        public static string offerNo = string.Empty;
        public static string itemOffer = string.Empty;

        #endregion

        public SimulationEDP()
        {
            InitializeComponent();
            rbAnalize.IsChecked = true;
            rbIsRetired.IsChecked = true;
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

        #region CREATE COMPOENENTS TREEVIEW

        private TreeViewItem FillLevel1(string item, string description, string qty, string obs, string image, string drawing, string costvalue, double? r1costvalue)
        {
            if (itemsService.GetNatureItem(item) == 22413314)
            {
                TreeViewItem chilItem = new TreeViewItem();
                chilItem.Name = "treeViewLv1";
                if (obs == string.Empty)
                {
                    chilItem.Header = item.Replace("|", "") + " | " + description.Replace("|", "") + " | Quantidade: " + qty + " | Valor R$: " + costvalue + " | Custo R1: " + r1costvalue.ToString() + " | Imagem= " + image + " | Desenho: " + drawing;
                }
                else
                {
                    chilItem.Header = item.Replace("|", "") + " | " + description.Replace("|", "") + " | Quantidade: " + qty + " | Valor R$: " + costvalue + " | Custo R1: " + r1costvalue.ToString() + " | OBS: " + obs.Replace("|", "") + " | Imagem= " + image + " | Desenho: " + drawing;
                }
                tvComponent.Items.Add(chilItem);
                return chilItem;
            }
            else if (itemsService.GetNatureItem(item) == 22413312)
            {
                Button btn = new Button
                {
                    Name = "btnComponent1",
                    Cursor = Cursors.Hand,
                    Background = null,
                    BorderBrush = null,
                    Content = new PackIcon { Kind = PackIconKind.Check, Foreground = Brushes.Green },
                };
                if (obs == string.Empty)
                {
                    TreeViewItem chilItem = new TreeViewItem
                    {
                        Name = "treeViewLv1",
                        IsExpanded = true,
                        Header = new StackPanel
                        {
                            Orientation = Orientation.Horizontal,
                            Children = {
                                new TextBlock { Text = item.Replace("|", "") + " | " + description.Replace("|", "") + " | Quantidade: " + qty + " | Valor R$: " + costvalue + " | Custo R1: " + r1costvalue.ToString() + " | Imagem= " + image + " | Desenho: " + drawing },
                                btn
                            }
                        }

                    };

                    chilItem.Items.Add(CompControls());
                    chilItem.Items.Add(OpControls());

                    tvComponent.Items.Add(chilItem);

                    return chilItem;
                }
                else
                {
                    TreeViewItem chilItem = new TreeViewItem
                    {
                        Name = "treeViewLv1",
                        IsExpanded = true,
                        Header = new StackPanel
                        {
                            Orientation = Orientation.Horizontal,
                            Children = {
                                new TextBlock { Text = item.Replace("|", "") + " | " + description.Replace("|", "") + " | Quantidade: " + qty + " | Valor R$: " + costvalue + " | Custo R1: " + r1costvalue.ToString() + " | OBS: " + obs.Replace("|", "") + " | Imagem= " + image + " | Desenho: " + drawing },
                                btn
                            }
                        }

                    };

                    chilItem.Items.Add(CompControls());
                    chilItem.Items.Add(OpControls());

                    tvComponent.Items.Add(chilItem);

                    return chilItem;
                }
            }
            else
            {
                TreeViewItem chilItem = new TreeViewItem();
                chilItem.Name = "treeViewLv1";
                if (obs == string.Empty)
                {
                    chilItem.Header = item.Replace("|", "") + " | " + description.Replace("|", "") + " | Quantidade: " + qty + " | Valor R$: " + costvalue + " | Custo R1: " + r1costvalue.ToString() + " | Imagem= " + image + " | Desenho: " + drawing;
                }
                else
                {
                    chilItem.Header = item.Replace("|", "") + " | " + description.Replace("|", "") + " | Quantidade: " + qty + " | Valor R$: " + costvalue + " | Custo R1: " + r1costvalue.ToString() + " | OBS: " + obs.Replace("|", "") + " | Imagem= " + image + " | Desenho: " + drawing;
                }
                tvComponent.Items.Add(chilItem);
                return chilItem;
            }

        }

        private TreeViewItem FillLevel2(string item, string description, string qty, string obs, TreeViewItem tvParam, string image, string drawing, string costvalue, double? r1costvalue)
        {
            if (itemsService.GetNatureItem(item) == 22413314)
            {
                TreeViewItem viewItem = tvParam;
                TreeViewItem chilItem = new TreeViewItem();
                chilItem.Name = "tvLevel2";
                if (obs == string.Empty)
                {
                    chilItem.Header = item.Replace("|", "") + " | " + description.Replace("|", "") + " | Quantidade: " + qty + " | Valor R$: " + costvalue + " | Custo R1: " + r1costvalue.ToString() + " | Imagem= " + image + " | Desenho: " + drawing;
                }
                else
                {
                    chilItem.Header = item.Replace("|", "") + " | " + description.Replace("|", "") + " | Quantidade: " + qty + " | Valor R$: " + costvalue + " | Custo R1: " + r1costvalue.ToString() + " | OBS: " + obs.Replace("|", "") + " | Imagem= " + image + " | Desenho: " + drawing;
                }
                viewItem.Items.Add(chilItem);
                return chilItem;
            }
            else if (itemsService.GetNatureItem(item) == 22413312)
            {
                TreeViewItem viewItem = tvParam;
                Button btn = new Button
                {
                    Name = "btnComponent2",
                    Cursor = Cursors.Hand,
                    Background = null,
                    BorderBrush = null,
                    Content = new PackIcon { Kind = PackIconKind.Check, Foreground = Brushes.Green },
                };
                if (obs == string.Empty)
                {
                    TreeViewItem chilItem = new TreeViewItem
                    {
                        Name = "tvLevel2",
                        IsExpanded = true,
                        Header = new StackPanel
                        {
                            Orientation = Orientation.Horizontal,
                            Children = {
                                new TextBlock { Text = item.Replace("|", "") + " | " + description.Replace("|", "") + "| Quantidade: " + qty + " | Valor R$: " + costvalue +  " | Custo R1: " + r1costvalue.ToString() + " | Imagem= " + image + " | Desenho: " + drawing },
                                btn
                            }
                        }

                    };

                    chilItem.Items.Add(CompControls2());
                    chilItem.Items.Add(OpControls2());

                    viewItem.Items.Add(chilItem);

                    return chilItem;
                }
                else
                {
                    TreeViewItem chilItem = new TreeViewItem
                    {
                        Name = "tvLevel2",
                        IsExpanded = true,
                        Header = new StackPanel
                        {
                            Orientation = Orientation.Horizontal,
                            Children = {
                                new TextBlock { Text = item.Replace("|", "") + " | " + description.Replace("|", "") + "| Quantidade: " + qty + " | Valor R$: " + costvalue +  " | Custo R1: " + r1costvalue.ToString() + " | OBS: " + obs.Replace("|", "") + " | Imagem= " + image + " | Desenho: " + drawing },
                                btn
                            }
                        }

                    };

                    chilItem.Items.Add(CompControls2());
                    chilItem.Items.Add(OpControls2());

                    viewItem.Items.Add(chilItem);

                    return chilItem;
                }
            }
            else
            {
                TreeViewItem viewItem = tvParam;
                TreeViewItem chilItem = new TreeViewItem();
                chilItem.Name = "tvLevel2";
                if (obs == string.Empty)
                {
                    chilItem.Header = item.Replace("|", "") + " | " + description.Replace("|", "") + " | Quantidade: " + qty + " | Valor R$: " + costvalue + " | Custo R1: " + r1costvalue.ToString() + " | Imagem= " + image + " | Desenho: " + drawing;
                }
                else
                {
                    chilItem.Header = item.Replace("|", "") + " | " + description.Replace("|", "") + " | Quantidade: " + qty + " | Valor R$: " + costvalue + " | Custo R1: " + r1costvalue.ToString() + " | OBS: " + obs.Replace("|", "") + " | Imagem= " + image + " | Desenho: " + drawing;
                }
                viewItem.Items.Add(chilItem);
                return chilItem;
            }
        }

        private void FillLevel3(string item, string description, string qty, string obs, TreeViewItem tvParam, string image, string drawing, string costvalue, double? r1costvalue)
        {
            if (itemsService.GetNatureItem(item) == 22413314)
            {
                TreeViewItem viewItem = tvParam;
                TreeViewItem chilItem = new TreeViewItem();
                chilItem.Name = "tvLevel3";
                if (obs == string.Empty)
                {
                    chilItem.Header = item.Replace("|", "") + " | " + description.Replace("|", "") + "| Quantidade: " + qty + " | Valor R$: " + costvalue + " | Custo R1: " + r1costvalue.ToString() + " | Imagem= " + image + " | Desenho: " + drawing;
                }
                else
                {
                    chilItem.Header = item.Replace("|", "") + " | " + description.Replace("|", "") + "| Quantidade: " + qty + " | Valor R$: " + costvalue + " | Custo R1: " + r1costvalue.ToString() + " | OBS: " + obs.Replace("|", "") + " | Imagem= " + image + " | Desenho: " + drawing;
                }
                viewItem.Items.Add(chilItem);
            }
            else if (itemsService.GetNatureItem(item) == 22413312)
            {
                TreeViewItem viewItem = tvParam;
                Button btn = new Button
                {
                    Name = "btnComponent2",
                    Cursor = Cursors.Hand,
                    Background = null,
                    BorderBrush = null,
                    Content = new PackIcon { Kind = PackIconKind.Check, Foreground = Brushes.Green },
                };
                if (obs == string.Empty)
                {
                    TreeViewItem chilItem = new TreeViewItem
                    {
                        Name = "tvLevel3",
                        IsExpanded = true,
                        Header = new StackPanel
                        {
                            Orientation = Orientation.Horizontal,
                            Children = {
                                new TextBlock { Text = item.Replace("|", "") + " | " + description.Replace("|", "") + "| Quantidade: " + qty + " | Valor R$: " + costvalue + " | Custo R1: " + r1costvalue.ToString() + " | Imagem= " + image + " | Desenho: " + drawing },
                                btn
                            }
                        }

                    };

                    viewItem.Items.Add(chilItem);
                }
                else
                {
                    TreeViewItem chilItem = new TreeViewItem
                    {
                        Name = "tvLevel3",
                        IsExpanded = true,
                        Header = new StackPanel
                        {
                            Orientation = Orientation.Horizontal,
                            Children = {
                                new TextBlock { Text = item.Replace("|", "") + " | " + description.Replace("|", "") + "| Quantidade: " + qty + " | Valor R$: " + costvalue + " | Custo R1: " + r1costvalue.ToString() + " | OBS: " + obs.Replace("|", "") + " | Imagem= " + image + " | Desenho: " + drawing },
                                btn
                            }
                        }

                    };

                    viewItem.Items.Add(chilItem);
                }
            }
            else
            {
                TreeViewItem viewItem = tvParam;
                TreeViewItem chilItem = new TreeViewItem();
                chilItem.Name = "tvLevel3";
                if (obs == string.Empty)
                {
                    chilItem.Header = item.Replace("|", "") + " | " + description.Replace("|", "") + "| Quantidade: " + qty + " | Valor R$: " + costvalue + " | Custo R1: " + r1costvalue.ToString() + " | Imagem= " + image + " | Desenho: " + drawing;
                }
                else
                {
                    chilItem.Header = item.Replace("|", "") + " | " + description.Replace("|", "") + "| Quantidade: " + qty + " | Valor R$: " + costvalue + " | Custo R1: " + r1costvalue.ToString() + " | OBS: " + obs.Replace("|", "") + " | Imagem= " + image + " | Desenho: " + drawing;
                }
                viewItem.Items.Add(chilItem);
            }
        }

        private void FillLevel2(string op, string description, int? timeProcess, string obs, TreeViewItem tvOp2, double? costoperation, string uom, double? qty)
        {
            string timeString = timeProcess.ToString() + "000";
            double timeDouble = Convert.ToDouble(timeString);
            double time = TimeSpan.FromMilliseconds(timeDouble).TotalHours;
            string timeStr = string.Empty;
            string timeTemp = TimeSpan.FromHours(time).ToString("h\\:mm");
            string[] vet2 = timeTemp.Split(':');
            if (time.ToString().Contains(","))
            {
                string[] vet = time.ToString().Split(',');
                timeStr = vet[0] + ":" + vet2[1];
            }
            else
            {
                timeStr = time.ToString() + ":" + vet2[1];
            }
            if (timeStr.Length == 4) { timeStr = "0" + timeStr; }
            TreeViewItem viewItem = tvOp2;
            TreeViewItem chilItem = new TreeViewItem();
            chilItem.Name = "tvOpLevel2";
            if (obs == string.Empty)
            {
                chilItem.Header = op.Replace("|", "") + " | " + description.Replace("|", "") + "| " + "Tempo de Processamento = " + timeStr + " | Custo: " + costoperation.ToString() + " | U.Medida: " + uom + " | Quantidade: " + qty;
            }
            else
            {
                chilItem.Header = op.Replace("|", "") + " | " + description.Replace("|", "") + "| " + "Tempo de Processamento = " + timeStr + " | Custo: " + costoperation.ToString() + " | OBS: " + obs.Replace("|", "") + " | U.Medida: " + uom + " | Quantidade: " + qty;
            }
            viewItem.Items.Add(chilItem);
        }

        private void FillLevel1(string op, string description, int? timeProcess, string obs, double? costoperation, string uom, double? qty)
        {
            string timeString = timeProcess.ToString() + "000";
            double timeDouble = Convert.ToDouble(timeString);
            double time = TimeSpan.FromMilliseconds(timeDouble).TotalHours;
            string timeStr = string.Empty;
            string timeTemp = TimeSpan.FromHours(time).ToString("h\\:mm");
            string[] vet2 = timeTemp.Split(':');
            if (time.ToString().Contains(","))
            {
                string[] vet = time.ToString().Split(',');
                timeStr = vet[0] + ":" + vet2[1];
            }
            else
            {
                timeStr = time.ToString() + ":" + vet2[1];
            }

            if (timeStr.Length == 4) { timeStr = "0" + timeStr; }
            TreeViewItem chilItem = new TreeViewItem();
            chilItem.Name = "tvOpLevel1";
            if (obs == string.Empty)
            {
                chilItem.Header = op.Replace("|", "") + " | " + description.Replace("|", "") + "| " + "Tempo de Processamento = " + timeStr + " | Custo: " + costoperation.ToString() + " | U.Medida: " + uom + " | Quantidade: " + qty;
            }
            else
            {
                chilItem.Header = op.Replace("|", "") + " | " + description.Replace("|", "") + "| " + "Tempo de Processamento = " + timeStr + " | Custo: " + costoperation.ToString() + " | OBS: " + obs.Replace("|", "") + " | U.Medida: " + uom + " | Quantidade: " + qty; ;
            }

            tvOperation.Items.Add(chilItem);
        }

        private TreeViewItem CompControls()
        {

            Button btn = new Button
            {
                Name = "btnComponent2",
                Cursor = Cursors.Arrow,
                Background = null,
                BorderBrush = null,
                Content = new PackIcon { Kind = PackIconKind.Plus, Foreground = Brushes.DarkCyan },
            };
            //btn.Click += _mainWindow.btnComponent2_Click;
            TreeViewItem chilItemComp = new TreeViewItem
            {
                IsExpanded = true,
                Header = new StackPanel
                {
                    Name = "StackPanelComp1",
                    Orientation = Orientation.Horizontal,
                    Children = {
                                new Label { Content = "Componente nv.2", Foreground = Brushes.DarkCyan, FontWeight = FontWeights.Bold },
                                btn
                    }
                }

            };

            return chilItemComp;
        }

        private TreeViewItem OpControls()
        {
            Button btn = new Button
            {
                Name = "btnOperation2",
                Cursor = Cursors.Arrow,
                Background = null,
                BorderBrush = null,
                Content = new PackIcon { Kind = PackIconKind.Plus, Foreground = Brushes.DarkCyan },
            };
            //btn.Click += _mainWindow.btnOperation2_Click;
            TreeViewItem chilItemOp = new TreeViewItem
            {
                IsExpanded = true,
                Header = new StackPanel
                {
                    Name = "StackPanelOp1",
                    Orientation = Orientation.Horizontal,
                    Children = {
                                new Label { Content = "Operação nv.2", Width = 109, Foreground = Brushes.DarkCyan, FontWeight = FontWeights.Bold },
                                btn
                    }
                }

            };

            return chilItemOp;
        }

        private TreeViewItem CompControls2()
        {

            Button btn = new Button
            {
                Name = "btnComponent2",
                Cursor = Cursors.Arrow,
                Background = null,
                BorderBrush = null,
                Content = new PackIcon { Kind = PackIconKind.Plus, Foreground = Brushes.DarkCyan },
            };
            //btn.Click += _mainWindow.btnComponent2_Click;
            TreeViewItem chilItemComp = new TreeViewItem
            {
                IsExpanded = true,
                Header = new StackPanel
                {
                    Name = "StackPanelComp2",
                    Orientation = Orientation.Horizontal,
                    Children = {
                                new Label { Content = "Componente nv.3", Foreground = Brushes.DarkCyan, FontWeight = FontWeights.Bold },
                                btn
                    }
                }

            };

            return chilItemComp;
        }

        private TreeViewItem OpControls2()
        {
            Button btn = new Button
            {
                Name = "btnOperation2",
                Cursor = Cursors.Arrow,
                Background = null,
                BorderBrush = null,
                Content = new PackIcon { Kind = PackIconKind.Plus, Foreground = Brushes.DarkCyan },
            };
            //btn.Click += _mainWindow.btnOperation2_Click;
            TreeViewItem chilItemOp = new TreeViewItem
            {
                IsExpanded = true,
                Name = "tvOpLabelLevel2",
                Header = new StackPanel
                {
                    Name = "StackPanelOp2",
                    Orientation = Orientation.Horizontal,
                    Children = {
                                new Label { Content = "Operação nv.3", Width = 109, Foreground = Brushes.DarkCyan, FontWeight = FontWeights.Bold },
                                btn
                    }
                }

            };

            return chilItemOp;
        }

        private void LoadSimulationEng()
        {

            #region LoadSimulationEng

            try
            {

                int? id = Convert.ToInt32(lblCustQuotaId.Content.ToString());
                tvComponent.Items.Clear();
                tvOperation.Items.Clear();

                if (rbAnalize.IsChecked == true)
                {

                    // Components Level 1
                    foreach (var item in bomService.GetComponentsBOMFromSimulation(cbItemGrid.Text, cbItemGrid.Text, id, true))
                    {
                        TreeViewItem tvItem1 = FillLevel1(item.Component, item.Description.Replace(",", ""), item.Qty.ToString().Replace(",", "."), item.Notes, item.Drawing, item.DrawingComp, item.ScrapQty.ToString().Replace(",", "."), item.WastePerc);
                        txtDrawing.Text = item.TempDrawing;
                        txtImage.Text = System.IO.Path.GetFileName(item.PathFile);
                        longPath = item.PathFile;
                        txtTecConclusion.Text = item.CompTecConclusion;

                        #region Level 2

                        List<MA_BillOfMaterialsComp> listComp = bomService.GetComponentsBOMFromSimulation(item.BOM, item.Component, id, true);
                        List<MA_BillOfMaterialsRouting> listOp = bomService.GetOperationsBOMFromSimulation(item.BOM, item.Component, id, true);

                        if (listComp.Count > 0)
                        {
                            if (tvItem1.Name == "treeViewLv1")
                            {
                                foreach (var itemTv1 in tvItem1.Items)
                                {
                                    TreeViewItem isComp = itemTv1 as TreeViewItem;

                                    if (isComp.Header is StackPanel)
                                    {
                                        StackPanel panel1 = isComp.Header as StackPanel;
                                        if (panel1.Children.Count > 0)
                                        {
                                            foreach (var child in panel1.Children)
                                            {
                                                if (child is Label)
                                                {
                                                    Label lbl = child as Label;
                                                    if (lbl.Content.ToString().Equals("Componente nv.2"))
                                                    {
                                                        foreach (var comp2 in listComp)
                                                        {
                                                            TreeViewItem tvItem2 = FillLevel2(comp2.Component.Trim(), comp2.Description.Replace(",", "").Trim(), comp2.Qty.ToString().Replace(",", ".").Trim(), comp2.Notes.Trim(), isComp, comp2.Drawing.Trim(), comp2.DrawingComp.Trim(), comp2.ScrapQty.ToString().Replace(",", "."), comp2.WastePerc);

                                                            #region Level 3

                                                            List<MA_BillOfMaterialsComp> listComp2 = bomService.GetComponentsBOMFromSimulation(comp2.BOM, comp2.Component, id, true);
                                                            List<MA_BillOfMaterialsRouting> listOp2 = bomService.GetOperationsBOMFromSimulation(comp2.BOM, comp2.Component, id, true);

                                                            if (listComp2.Count > 0)
                                                            {
                                                                if (tvItem2.Name == "tvLevel2")
                                                                {
                                                                    foreach (var itemTv2 in tvItem2.Items)
                                                                    {
                                                                        TreeViewItem isComp2 = itemTv2 as TreeViewItem;
                                                                        if (isComp2.Header is StackPanel)
                                                                        {
                                                                            StackPanel panel2 = isComp2.Header as StackPanel;
                                                                            if (panel2.Children.Count > 0)
                                                                            {
                                                                                foreach (var child2 in panel2.Children)
                                                                                {
                                                                                    if (child2 is Label)
                                                                                    {
                                                                                        Label lbl2 = child2 as Label;
                                                                                        if (lbl2.Content.ToString().Equals("Componente nv.3"))
                                                                                        {
                                                                                            foreach (var comp3 in listComp2)
                                                                                            {
                                                                                                FillLevel3(comp3.Component.Trim(), comp3.Description.Replace(",", "").Trim(), comp3.Qty.ToString().Replace(",", ".").Trim(), comp3.Notes, isComp2, comp3.Drawing.Trim(), comp3.Drawing.Trim(), comp3.ScrapQty.ToString().Replace(",", "."), comp3.WastePerc);
                                                                                            }
                                                                                        }
                                                                                        else if (lbl2.Content.ToString().Equals("Operação nv.3"))
                                                                                        {
                                                                                            if (listOp2.Count > 0)
                                                                                            {
                                                                                                foreach (var comp3 in listOp2)
                                                                                                {
                                                                                                    FillLevel2(comp3.Operation, opService.GetDescriptionOp(comp3.Operation).Replace(",", ""), comp3.ProcessingTime, comp3.Notes, isComp2, comp3.SetupAttendancePerc, comp3.WC, comp3.Qty);
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

                                                            #endregion
                                                        }

                                                    }
                                                    else if (lbl.Content.ToString().Equals("Operação nv.2"))
                                                    {
                                                        if (listOp.Count > 0)
                                                        {
                                                            foreach (var comp2 in listOp)
                                                            {
                                                                FillLevel2(comp2.Operation, opService.GetDescriptionOp(comp2.Operation).Replace(",", ""), comp2.ProcessingTime, comp2.Notes, isComp, comp2.SetupAttendancePerc, comp2.WC, comp2.Qty);
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

                        #endregion

                    }

                    // Operations Level 1
                    foreach (var item in bomService.GetOperationsBOMFromSimulation(cbItemGrid.Text, cbItemGrid.Text, id, true))
                    {
                        FillLevel1(item.Operation, opService.GetDescriptionOp(item.Operation).Replace(",", ""), item.ProcessingTime, item.Notes, item.SetupAttendancePerc, item.WC, item.Qty);
                    }
                }
                else
                {
                    foreach (var item in bomService.GetComponentsBOMFromSimulation(cbItemGrid.Text, cbItemGrid.Text, id, false))
                    {
                        TreeViewItem tvItem1 = FillLevel1(item.Component, item.Description.Replace(",", ""), item.Qty.ToString().Replace(",", "."), item.Notes, item.Drawing, item.DrawingComp, item.ScrapQty.ToString().Replace(",", "."), item.WastePerc);
                        txtDrawing.Text = item.TempDrawing;
                        txtImage.Text = System.IO.Path.GetFileName(item.PathFile);
                        longPath = item.PathFile;
                        txtTecConclusion.Text = item.CompTecConclusion;

                        #region Level 2

                        List<MA_BillOfMaterialsComp> listComp = bomService.GetComponentsBOMFromSimulation(item.BOM, item.Component, id, false);
                        List<MA_BillOfMaterialsRouting> listOp = bomService.GetOperationsBOMFromSimulation(item.BOM, item.Component, id, false);

                        if (listComp.Count > 0)
                        {
                            if (tvItem1.Name == "treeViewLv1")
                            {
                                foreach (var itemTv1 in tvItem1.Items)
                                {
                                    TreeViewItem isComp = itemTv1 as TreeViewItem;

                                    if (isComp.Header is StackPanel)
                                    {
                                        StackPanel panel1 = isComp.Header as StackPanel;
                                        if (panel1.Children.Count > 0)
                                        {
                                            foreach (var child in panel1.Children)
                                            {
                                                if (child is Label)
                                                {
                                                    Label lbl = child as Label;
                                                    if (lbl.Content.ToString().Equals("Componente nv.2"))
                                                    {
                                                        foreach (var comp2 in listComp)
                                                        {
                                                            TreeViewItem tvItem2 = FillLevel2(comp2.Component.Trim(), comp2.Description.Replace(",", "").Trim(), comp2.Qty.ToString().Replace(",", ".").Trim(), comp2.Notes.Trim(), isComp, comp2.Drawing.Trim(), comp2.DrawingComp.Trim(), comp2.ScrapQty.ToString().Replace(",", "."), comp2.WastePerc);

                                                            #region Level 3

                                                            List<MA_BillOfMaterialsComp> listComp2 = bomService.GetComponentsBOMFromSimulation(comp2.BOM, comp2.Component, id, false);
                                                            List<MA_BillOfMaterialsRouting> listOp2 = bomService.GetOperationsBOMFromSimulation(comp2.BOM, comp2.Component, id, false);

                                                            if (listComp2.Count > 0)
                                                            {
                                                                if (tvItem2.Name == "tvLevel2")
                                                                {
                                                                    foreach (var itemTv2 in tvItem2.Items)
                                                                    {
                                                                        TreeViewItem isComp2 = itemTv2 as TreeViewItem;
                                                                        if (isComp2.Header is StackPanel)
                                                                        {
                                                                            StackPanel panel2 = isComp2.Header as StackPanel;
                                                                            if (panel2.Children.Count > 0)
                                                                            {
                                                                                foreach (var child2 in panel2.Children)
                                                                                {
                                                                                    if (child2 is Label)
                                                                                    {
                                                                                        Label lbl2 = child2 as Label;
                                                                                        if (lbl2.Content.ToString().Equals("Componente nv.3"))
                                                                                        {
                                                                                            foreach (var comp3 in listComp2)
                                                                                            {
                                                                                                FillLevel3(comp3.Component.Trim(), comp3.Description.Replace(",", "").Trim(), comp3.Qty.ToString().Replace(",", ".").Trim(), comp3.Notes, isComp2, comp3.Drawing.Trim(), comp3.Drawing.Trim(), comp3.ScrapQty.ToString().Replace(",", "."), comp3.WastePerc);
                                                                                            }
                                                                                        }
                                                                                        else if (lbl2.Content.ToString().Equals("Operação nv.3"))
                                                                                        {
                                                                                            if (listOp2.Count > 0)
                                                                                            {
                                                                                                foreach (var comp3 in listOp2)
                                                                                                {
                                                                                                    FillLevel2(comp3.Operation, opService.GetDescriptionOp(comp3.Operation).Replace(",", ""), comp3.ProcessingTime, comp3.Notes, isComp2, comp3.SetupAttendancePerc, comp3.WC, comp3.Qty);
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

                                                            #endregion
                                                        }

                                                    }
                                                    else if (lbl.Content.ToString().Equals("Operação nv.2"))
                                                    {
                                                        if (listOp.Count > 0)
                                                        {
                                                            foreach (var comp2 in listOp)
                                                            {
                                                                FillLevel2(comp2.Operation, opService.GetDescriptionOp(comp2.Operation).Replace(",", ""), comp2.ProcessingTime, comp2.Notes, isComp, comp2.SetupAttendancePerc, comp2.WC, comp2.Qty);
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

                        #endregion

                    }

                    // Operations Level 1
                    foreach (var item in bomService.GetOperationsBOMFromSimulation(cbItemGrid.Text, cbItemGrid.Text, id, false))
                    {
                        FillLevel1(item.Operation, opService.GetDescriptionOp(item.Operation).Replace(",", ""), item.ProcessingTime, item.Notes, item.SetupAttendancePerc, item.WC, item.Qty);
                    }
                }

                LoadAttachments();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.InnerException.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            #endregion
        }

        #endregion

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
                    comp.TecConclusion2 = txtTecConclusion.Text;
                    if(rbIsNew.IsChecked == true)
                    {
                        comp.IsItemNew = "1";
                        comp.IsItemRetired = "0";
                    }
                    if(rbIsRetired.IsChecked == true)
                    {
                        comp.IsItemNew = "0";
                        comp.IsItemRetired = "1";
                    }
                    comp.PathFile2 = longPath;
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
                                                    if (vetQty[1] == "" || vetQty[1] == " " || vetQty[1] == "  ")
                                                    {
                                                        comp.Qty = 0;
                                                    }
                                                    else
                                                    {
                                                        comp.Qty = Convert.ToDouble(vetQty[1]);
                                                    }
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
                                            if (o.Contains("Imagem="))
                                            {
                                                string[] vetDraw = o.Split('=');
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
                                            if (o.Contains("Desenho:"))
                                            {
                                                string[] vetDraw = o.Split(':');
                                                if (vetDraw.Length > 1)
                                                {
                                                    comp.DrawingComponent = vetDraw[1];
                                                }
                                            }
                                            if (o.Contains("Valor R$:"))
                                            {
                                                string[] vetR1 = o.Split(':');
                                                if (vetR1.Length > 1)
                                                {
                                                    string vet1 = vetR1[0];
                                                    string vet2 = vetR1[1].Trim();
                                                    if (vet2 != "")
                                                    {
                                                        comp.Costvalue = Convert.ToDouble(vet2.Replace(".", ","));
                                                    }
                                                    else
                                                    {
                                                        comp.Costvalue = 0;
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
                                        if (vetQty[1] == "" || vetQty[1] == " " || vetQty[1] == "  ")
                                        {
                                            comp.Qty = 0;
                                        }
                                        else
                                        {
                                            comp.Qty = Convert.ToDouble(vetQty[1]);
                                        }
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
                                if (o.Contains("Imagem="))
                                {
                                    string[] vetDraw = o.Split('=');
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
                                if (o.Contains("Desenho:"))
                                {
                                    string[] vetDraw = o.Split(':');
                                    if (vetDraw.Length > 1)
                                    {
                                        comp.DrawingComponent = vetDraw[1];
                                    }
                                }
                                if (o.Contains("Valor R$:"))
                                {
                                    string[] vetR1 = o.Split(':');
                                    if (vetR1.Length > 1)
                                    {
                                        string vet1 = vetR1[0];
                                        string vet2 = vetR1[1].Trim();
                                        if (vet2 != "")
                                        {
                                            comp.Costvalue = Convert.ToDouble(vet2.Replace(".", ","));
                                        }
                                        else
                                        {
                                            comp.Costvalue = 0;
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

                    compOpService.SaveComp2(comp);
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
                                    op.TimeProcessStr = vetObs[1];
                                }
                            }

                            if (o.Contains("Custo:"))
                            {
                                string[] vetCost = o.Split(':');
                                if (vetCost.Length > 1)
                                {
                                    if (string.IsNullOrEmpty(vetCost[1].Trim()))
                                    {
                                        op.CostOperation = 0;
                                    }
                                    else
                                    {
                                        op.CostOperation = Convert.ToDouble(vetCost[1]);
                                    }
                                }
                                else
                                {
                                    op.CostOperation = 0;
                                }
                            }
                            if (o.Contains("U.Medida:"))
                            {
                                string[] vetUoM = o.Split(':');
                                if (vetUoM.Length > 1)
                                {
                                    op.UoM = vetUoM[1];
                                }
                            }
                            if (o.Contains("Quantidade:"))
                            {
                                string[] vetQ = o.Split(':');
                                if (vetQ.Length > 1)
                                {
                                    if (string.IsNullOrEmpty(vetQ[1].Trim()))
                                    {
                                        op.Qty = 0;
                                    }
                                    else
                                    {
                                        op.Qty = Convert.ToDouble(vetQ[1]);
                                    }
                                }
                                else
                                {
                                    op.Qty = 0;
                                }
                            }
                        }
                    }

                    op.TBCreated = DateTime.Now;
                    op.TBModified = DateTime.Now;

                    compOpService.SaveOp2(op);
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
                                                            comp.TecConclusion2 = txtTecConclusion.Text;
                                                            comp.PathFile2 = longPath;

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
                                                                                    if (vetQty[1] == "" || vetQty[1] == " " || vetQty[1] == "  ")
                                                                                    {
                                                                                        comp.Qty = 0;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        comp.Qty = Convert.ToDouble(vetQty[1]);
                                                                                    }
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
                                                                            if (o.Contains("Imagem="))
                                                                            {
                                                                                string[] vetDraw = o.Split('=');
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
                                                                            if (o.Contains("Desenho:"))
                                                                            {
                                                                                string[] vetDraw = o.Split(':');
                                                                                if (vetDraw.Length > 1)
                                                                                {
                                                                                    comp.DrawingComponent = vetDraw[1];
                                                                                }
                                                                            }
                                                                            if (o.Contains("Valor R$:"))
                                                                            {
                                                                                string[] vetR1 = o.Split(':');
                                                                                if (vetR1.Length > 1)
                                                                                {
                                                                                    string vet1 = vetR1[0];
                                                                                    string vet2 = vetR1[1].Trim();
                                                                                    if (vet2 != "")
                                                                                    {
                                                                                        comp.Costvalue = Convert.ToDouble(vet2.Replace(".", ","));
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        comp.Costvalue = 0;
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
                                                                                if (vetQty[1] == "" || vetQty[1] == " " || vetQty[1] == "  ")
                                                                                {
                                                                                    comp.Qty = 0;
                                                                                }
                                                                                else
                                                                                {
                                                                                    comp.Qty = Convert.ToDouble(vetQty[1]);
                                                                                }
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
                                                                        if (o.Contains("Imagem="))
                                                                        {
                                                                            string[] vetDraw = o.Split('=');
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
                                                                        if (o.Contains("Desenho:"))
                                                                        {
                                                                            string[] vetDraw = o.Split(':');
                                                                            if (vetDraw.Length > 1)
                                                                            {
                                                                                comp.DrawingComponent = vetDraw[1];
                                                                            }
                                                                        }
                                                                        if (o.Contains("Valor R$:"))
                                                                        {
                                                                            string[] vetR1 = o.Split(':');
                                                                            if (vetR1.Length > 1)
                                                                            {
                                                                                string vet1 = vetR1[0];
                                                                                string vet2 = vetR1[1].Trim();
                                                                                if (vet2 != "")
                                                                                {
                                                                                    comp.Costvalue = Convert.ToDouble(vet2.Replace(".", ","));
                                                                                }
                                                                                else
                                                                                {
                                                                                    comp.Costvalue = 0;
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

                                                            compOpService.SaveComp2(comp);

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
                                                                            op.TimeProcessStr = vetObs[1];
                                                                        }
                                                                    }

                                                                    if (o.Contains("Custo:"))
                                                                    {
                                                                        string[] vetCost = o.Split(':');
                                                                        if (vetCost.Length > 1)
                                                                        {
                                                                            if (string.IsNullOrEmpty(vetCost[1].Trim()))
                                                                            {
                                                                                op.CostOperation = 0;
                                                                            }
                                                                            else
                                                                            {
                                                                                op.CostOperation = Convert.ToDouble(vetCost[1]);
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            op.CostOperation = 0;
                                                                        }
                                                                    }
                                                                    if (o.Contains("U.Medida:"))
                                                                    {
                                                                        string[] vetUoM = o.Split(':');
                                                                        if (vetUoM.Length > 1)
                                                                        {
                                                                            op.UoM = vetUoM[1];
                                                                        }
                                                                    }
                                                                    if (o.Contains("Quantidade:"))
                                                                    {
                                                                        string[] vetQ = o.Split(':');
                                                                        if (vetQ.Length > 1)
                                                                        {
                                                                            if (string.IsNullOrEmpty(vetQ[1].Trim())){
                                                                                op.Qty = 0;
                                                                            }
                                                                            else {
                                                                                op.Qty = Convert.ToDouble(vetQ[1]);
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            op.Qty = 0;
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                            op.TBCreated = DateTime.Now;
                                                            op.TBModified = DateTime.Now;

                                                            compOpService.SaveOp2(op);

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
                                            comp.TecConclusion2 = txtTecConclusion.Text;
                                            comp.PathFile2 = longPath;

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
                                                                        if (vetQty[1] == "" || vetQty[1] == " " || vetQty[1] == "  ")
                                                                        {
                                                                            comp.Qty = 0;
                                                                        }
                                                                        else
                                                                        {
                                                                            comp.Qty = Convert.ToDouble(vetQty[1]);
                                                                        }
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
                                                                if (o.Contains("Imagem="))
                                                                {
                                                                    string[] vetDraw = o.Split('=');
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
                                                                if (o.Contains("Desenho:"))
                                                                {
                                                                    string[] vetDraw = o.Split(':');
                                                                    if (vetDraw.Length > 1)
                                                                    {
                                                                        comp.DrawingComponent = vetDraw[1];
                                                                    }
                                                                }
                                                                if (o.Contains("Valor R$:"))
                                                                {
                                                                    string[] vetR1 = o.Split(':');
                                                                    if (vetR1.Length > 1)
                                                                    {
                                                                        string vet1 = vetR1[0];
                                                                        string vet2 = vetR1[1].Trim();
                                                                        if (vet2 != "")
                                                                        {
                                                                            comp.Costvalue = Convert.ToDouble(vet2.Replace(".", ","));
                                                                        }
                                                                        else
                                                                        {
                                                                            comp.Costvalue = 0;
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
                                                                if (vetQty[1] == "" || vetQty[1] == " " || vetQty[1] == "  ")
                                                                {
                                                                    comp.Qty = 0;
                                                                }
                                                                else
                                                                {
                                                                    comp.Qty = Convert.ToDouble(vetQty[1]);
                                                                }
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
                                                        if (o.Contains("Imagem="))
                                                        {
                                                            string[] vetDraw = o.Split('=');
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
                                                        if (o.Contains("Desenho:"))
                                                        {
                                                            string[] vetDraw = o.Split(':');
                                                            if (vetDraw.Length > 1)
                                                            {
                                                                comp.DrawingComponent = vetDraw[1];
                                                            }
                                                        }
                                                        if (o.Contains("Valor R$:"))
                                                        {
                                                            string[] vetR1 = o.Split(':');
                                                            if (vetR1.Length > 1)
                                                            {
                                                                string vet1 = vetR1[0];
                                                                string vet2 = vetR1[1].Trim();
                                                                if (vet2 != "")
                                                                {
                                                                    comp.Costvalue = Convert.ToDouble(vet2.Replace(".", ","));
                                                                }
                                                                else
                                                                {
                                                                    comp.Costvalue = 0;
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

                                            compOpService.SaveComp2(comp);
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
                                                                op.TimeProcessStr = vetObs[1];
                                                            }
                                                        }

                                                        if (o.Contains("Custo:"))
                                                        {
                                                            string[] vetCost = o.Split(':');
                                                            if (vetCost.Length > 1)
                                                            {
                                                                if (string.IsNullOrEmpty(vetCost[1].Trim()))
                                                                {
                                                                    op.CostOperation = 0;
                                                                }
                                                                else
                                                                {
                                                                    op.CostOperation = Convert.ToDouble(vetCost[1]);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                op.CostOperation = 0;
                                                            }
                                                        }
                                                        if (o.Contains("U.Medida:"))
                                                        {
                                                            string[] vetUoM = o.Split(':');
                                                            if (vetUoM.Length > 1)
                                                            {
                                                                op.UoM = vetUoM[1];
                                                            }
                                                        }
                                                        if (o.Contains("Quantidade:"))
                                                        {
                                                            string[] vetQ = o.Split(':');
                                                            if (vetQ.Length > 1)
                                                            {
                                                                if (string.IsNullOrEmpty(vetQ[1].Trim()))
                                                                {
                                                                    op.Qty = 0;
                                                                }
                                                                else
                                                                {
                                                                    op.Qty = Convert.ToDouble(vetQ[1]);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                op.Qty = 0;
                                                            }
                                                        }
                                                    }
                                                }

                                                op.TBCreated = DateTime.Now;
                                                op.TBModified = DateTime.Now;

                                                compOpService.SaveOp2(op);
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

        public bool SaveSimulation()
        {
            try
            {
                if (compOpService.ExistData(Convert.ToInt32(lblCustQuotaId.Content.ToString()), cbItemGrid.Text))
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
                            serviceQA.InsertAllDataWithList();
                            SaveAttachmentsOfSimulation();
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

        private void BtnSearchOffer_Click(object sender, RoutedEventArgs e)
        {
            OfferSearchView window = new OfferSearchView(this);
            window.ShowDialog();
        }

        private void CbItemGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            positionLine = cbItemGrid.SelectedIndex + 1;
        }

        private void BtnImport_Click(object sender, RoutedEventArgs e)
        {
            if (cbItemGrid.Text != string.Empty)
            {
                ImportBOMView window = new ImportBOMView(this);
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selecione um item para importar uma simulação de engenharia!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnRegisterItems_Click_1(object sender, RoutedEventArgs e)
        {
            if (!hasAccess)
            {
                MessageBox.Show("Acesso Negado! contacte o gerente!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            RegisterItem window = new RegisterItem();
            window.ShowDialog();
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
                                        window.btnQualityControl.IsEnabled = true;
                                        window.txtStandardCost.Text = itemsService.GetStandardCost(vet[0]).ToString();
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
                                                if (item2.Contains("Imagem="))
                                                {
                                                    string[] vetDraw = item2.Split('=');
                                                    if (vetDraw.Length > 1)
                                                    {
                                                        window.txtDrawing.Text = vetDraw[1];
                                                        window.longPath = vetDraw[1];
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
                                                if (item2.Contains("Desenho:"))
                                                {
                                                    string[] vetDraw = item2.Split(':');
                                                    if (vetDraw.Length > 1)
                                                    {
                                                        window.txtDrawingComponent.Text = vetDraw[1];
                                                    }
                                                }
                                                if (item2.Contains("Valor R$:"))
                                                {
                                                    string[] vetR1 = item2.Split(':');
                                                    if (vetR1.Length > 1)
                                                    {
                                                        window.txtCostValue.Text = vetR1[1];
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
                            window.btnQualityControl.IsEnabled = true;
                            window.txtStandardCost.Text = itemsService.GetStandardCost(vet[0]).ToString();
                            if (vet.Length > 0)
                            {
                                foreach (var item in vet)
                                {
                                    if (item.Contains("Quantidade:"))
                                    {
                                        string[] vetQty = item.Split(':');
                                        if (vetQty.Length > 1)
                                        {
                                            window.txtQty.Text = vetQty[1];
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
                                    if (item.Contains("Imagem="))
                                    {
                                        string[] vetDraw = item.Split('=');
                                        if (vetDraw.Length > 1)
                                        {
                                            window.txtDrawing.Text = vetDraw[1];
                                            window.longPath = vetDraw[1];
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
                                if (item.Contains("Custo:"))
                                {
                                    string[] vetCost = item.Split(':');
                                    if (vetCost.Length > 1)
                                    {
                                        window.txtCostOperation.Text = opService.GetOpTotalCost(vet[0]).ToString();
                                    }
                                }
                                if (item.Contains("U.Medida:"))
                                {
                                    string[] vetUoM = item.Split(':');
                                    if (vetUoM.Length > 1)
                                    {
                                        window.txtUoM.Visibility = Visibility.Visible;
                                        window.txtUoM.Text = vetUoM[1];
                                    }
                                }
                                if (item.Contains("Quantidade:"))
                                {
                                    string[] vetQ = item.Split(':');
                                    if (vetQ.Length > 1)
                                    {
                                        if (vetQ[1] != "0" && vetQ[1] != "")
                                        {
                                            window.txtQuantity.Visibility = Visibility.Visible;
                                        }
                                        window.txtQuantity.Text = vetQ[1];
                                    }
                                }
                            }
                        }

                        window.Show();
                        window.FillCostValueOperation();
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

        private void BtnImg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Todos os arquivos (*.*)|*.*";
                dialog.Title = "Selecione um arquivo";

                Nullable<bool> result = dialog.ShowDialog();
                if (result == true)
                {
                    txtImage.Text = System.IO.Path.GetFileName(dialog.FileName);
                    longPath = dialog.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " - " + ex.StackTrace, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir tela de Desenhos!" + "\n" + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tvComponent.Items.Clear();
                tvOperation.Items.Clear();
                txtTecConclusion.Clear();
                txtDrawing.Clear();
                lblCustQuotaId.Content = string.Empty;
                lblNoCustQuota.Content = "Selecione a Oferta";
                cbItemGrid.Text = string.Empty;
                cbItemGrid.ItemsSource = null;
                tvBOM.Visibility = Visibility.Hidden;
                txtDrawing.Visibility = Visibility.Hidden;
                txtTecConclusion.Visibility = Visibility.Hidden;
                btnDrawing.Visibility = Visibility.Hidden;
                txtImage.Visibility = Visibility.Hidden;
                btnImg.Visibility = Visibility.Hidden;
                btnClear.IsEnabled = false;
                btnSaveSimulation.IsEnabled = false;
                btnAttatch.IsEnabled = false;
                rbAnalize.IsChecked = true;
                isClear = false;

                UpdateOnOffFalse();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
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

        public static void UpdateOnOffFalse()
        {
            try
            {
                if (itemOffer != string.Empty && id_Offer > 0 && offerNo != string.Empty)
                {
                    OnOffService.Update(id_Offer, offerNo, itemOffer, false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ClearFields()
        {
            tvComponent.Items.Clear();
            tvOperation.Items.Clear();
            txtTecConclusion.Clear();
            txtDrawing.Clear();
            lblCustQuotaId.Content = string.Empty;
            lblNoCustQuota.Content = "Selecione a Oferta";
            cbItemGrid.Text = string.Empty;
            cbItemGrid.ItemsSource = null;
            tvBOM.Visibility = Visibility.Hidden;
            txtDrawing.Visibility = Visibility.Hidden;
            txtTecConclusion.Visibility = Visibility.Hidden;
            btnDrawing.Visibility = Visibility.Hidden;
            txtImage.Visibility = Visibility.Hidden;
            btnImg.Visibility = Visibility.Hidden;
            btnClear.IsEnabled = false;
            btnSaveSimulation.IsEnabled = false;
            btnAttatch.IsEnabled = false;
            rbAnalize.IsChecked = true;
            isClear = false;
        }

        private void cbItemGrid_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if (cbItemGrid.Text != string.Empty)
                {

                    if(tvBOM.Visibility == Visibility.Visible)
                    {
                        MessageBox.Show("Linpe a simulação para carregar novamente!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    tvBOM.Visibility = Visibility.Visible;
                    txtDrawing.Visibility = Visibility.Visible;
                    txtTecConclusion.Visibility = Visibility.Visible;
                    btnDrawing.Visibility = Visibility.Visible;
                    txtImage.Visibility = Visibility.Visible;
                    btnImg.Visibility = Visibility.Visible;
                    btnSaveSimulation.IsEnabled = true;
                    btnClear.IsEnabled = true;
                    btnAttatch.IsEnabled = true;

                    #region Verify Double Intance

                    string offerno = string.Empty;
                    bool isUpdatable = false;
                    if (lblNoCustQuota.Content.ToString().Contains(":"))
                    {
                        string[] vet = lblNoCustQuota.Content.ToString().Split(':');
                        if (vet.Length > 1)
                        {
                            offerno = vet[1].Trim();
                        }
                    }
                    var OnOffObj = onoffService.GetData(Convert.ToInt32(lblCustQuotaId.Content.ToString()), offerno, cbItemGrid.Text);
                    if (OnOffObj != null)
                    {
                        if (OnOffObj.On_Off)
                        {
                            MessageBox.Show("Simulação de EDP já esta aberta em outra instância!", "Aviso",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                            ClearFields();
                            return;
                        }
                        else
                        {
                            isUpdatable = true;
                        }
                    }
                    else
                    {
                        CS_OnOffValidate on_off = new CS_OnOffValidate();
                        on_off.Id = onoffService.GetMaxId();
                        on_off.Id_Offer = Convert.ToInt32(lblCustQuotaId.Content.ToString());
                        on_off.OfferNo = offerno;
                        on_off.Item = cbItemGrid.Text;
                        on_off.On_Off = true;
                        on_off.TBCreated = DateTime.Now;
                        on_off.TBModified = DateTime.Now;
                        on_off.TBCreatedID = 1;
                        onoffService.Insert(on_off);
                    }

                    #endregion

                    id_Offer = Convert.ToInt32(lblCustQuotaId.Content.ToString());
                    offerNo = offerno;
                    itemOffer = cbItemGrid.Text;

                    if (compOpService.ExistData(Convert.ToInt32(lblCustQuotaId.Content.ToString()), cbItemGrid.Text))
                    {
                        LoadSimulationEng();
                        if (isUpdatable)
                        {
                            OnOffService.Update(Convert.ToInt32(lblCustQuotaId.Content.ToString()), offerno, cbItemGrid.Text, true);
                        }
                    }
                    else
                    {
                        tvComponent.Items.Clear();
                        tvOperation.Items.Clear();
                    }
                }

                isClear = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro" + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void rbAnalize_Checked(object sender, RoutedEventArgs e)
        {
            if (rbAnalize.IsChecked == true)
            {
                rbProduction.IsChecked = !rbAnalize.IsChecked;
            }
        }

        private void rbProduction_Checked(object sender, RoutedEventArgs e)
        {
            if (rbProduction.IsChecked == true)
            {
                rbAnalize.IsChecked = !rbProduction.IsChecked;
            }
        }

        private void btnAttatch_Click(object sender, RoutedEventArgs e)
        {
            ItemsAttachView window = new ItemsAttachView(this);
            window.ShowDialog();
        }

        private void SaveAttachmentsOfSimulation()
        {
            try
            {
                if(AttachMap.Count > 0)
                {
                    List<CS_CustQuotasCompAttach> attachs = AttachMap[Convert.ToInt32(lblCustQuotaId.Content)];
                    if(attachs.Count > 0 && attachs != null)
                    {
                        foreach (var item in attachs)
                        {
                            attachService.SaveOrUpdateAttachToSimulation(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(
                    "Erro ao salvar anexos! descrição do erro: " + ex.Message,
                    "Erro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void LoadAttachments()
        {
            try
            {
                if (attachService.ExistAttachDataToLoad(Convert.ToInt32(lblCustQuotaId.Content)))
                {
                    List<CS_CustQuotasCompAttach> attachs = attachService.GetAttchById(Convert.ToInt32(lblCustQuotaId.Content));
                    if(attachs.Count > 0 && attachs != null)
                    {
                        AttachMap = new Dictionary<int, List<CS_CustQuotasCompAttach>>();
                        AttachMap.Add(Convert.ToInt32(lblCustQuotaId.Content), attachs);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void rbIsNew_Checked(object sender, RoutedEventArgs e)
        {
            if(rbIsNew.IsChecked == true)
            {
                rbIsRetired.IsChecked = !rbIsNew.IsChecked;
            }
        }

        private void rbIsRetired_Checked(object sender, RoutedEventArgs e)
        {
            if (rbIsRetired.IsChecked == true)
            {
                rbIsNew.IsChecked = !rbIsRetired.IsChecked;
            }
        }
    }
}
