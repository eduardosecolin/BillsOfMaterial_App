using BillsOfMaterial_App.AuxView;
using BillsOfMaterial_App.Model;
using BillsOfMaterial_App.Service;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Lógica interna para ItemsView.xaml
    /// </summary>
    public partial class ItemsView : Window
    {
        public SimulationEDP _mainWindow;
        ItemsService itemsService;
        private CS_CustQuotasComponent componentObj;
        private string description = string.Empty;
        public bool isEditmode = false;
        private bool isException = false;
        public string longPath = string.Empty;
        public static List<List<CS_ItemsAnalysisParameters>> itemsAnalysisParametersSuperList = new List<List<CS_ItemsAnalysisParameters>>();

        public ItemsView(SimulationEDP window)
        {
            InitializeComponent();
            componentObj = new CS_CustQuotasComponent();
            _mainWindow = window;
            itemsService = new ItemsService();
            LoadGrid();
            NotVisibleIsSpecific();
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
            finally
            {
                isEditmode = false;
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
                    description = items.Description;

                    txtStandardCost.Text = itemsService.GetStandardCost(items.Item).ToString();

                    if (itemsService.IsSpecificItem(items.Item))
                    {
                        VisibleIsSpecific();
                    }
                    else
                    {
                        NotVisibleIsSpecific();
                    }

                    btnQualityControl.IsEnabled = true;
                }
            }
        }

        private void TxtFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtFilter.Text != string.Empty)
            {
                dgItems.ItemsSource = null;
                dgItems.ItemsSource = itemsService.GetAll2(txtFilter.Text);
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
                if(txtResultValue.Text.Trim() != string.Empty && Convert.ToDouble(txtResultValue.Text.Trim()) > 0) { txtStandardCost.Text = 0.ToString(); }

                if (isEditmode)
                {
                    txtCostValue.Text = CalculateCostValue().ToString();
                    if (txtItem.Text != string.Empty)
                    {
                        GetThisComponent();
                        var treeview = _mainWindow.tvBOM.SelectedItem as TreeViewItem;
                        if (treeview != null)
                        {
                            if (itemsService.GetNatureItem(txtItem.Text) == 22413314)
                            {
                                if (txtObs.Text == string.Empty)
                                {
                                    treeview.Header = componentObj.Component.Replace("|", "") + " | " + itemsService.GetDescriptionItem(componentObj.Component).Replace("|", "") + "| Quantidade: " + componentObj.Qty + " | Valor R$: " + componentObj.Costvalue + " | Imagem= " + componentObj.PathFile1.Replace("|", "") + " | Custo R1: " + componentObj.R1Costvalue + " | Desenho: " + componentObj.DrawingComponent;
                                }
                                else
                                {
                                    treeview.Header = componentObj.Component.Replace("|", "") + " | " + itemsService.GetDescriptionItem(componentObj.Component).Replace("|", "") + " | Quantidade: " + componentObj.Qty + " | Valor R$: " + componentObj.Costvalue + " | OBS: " + componentObj.Obs.Replace("|", "") + " | Imagem= " + componentObj.PathFile1.Replace("|", "") + " | Custo R1: " + componentObj.R1Costvalue + " | Desenho: " + componentObj.DrawingComponent;
                                }
                                this.Close();
                            }
                            else if (itemsService.GetNatureItem(txtItem.Text) == 22413312)
                            {
                                Button btn = new Button
                                {
                                    Name = "btnComponent1",
                                    Cursor = Cursors.Hand,
                                    Background = null,
                                    BorderBrush = null,
                                    Content = new PackIcon { Kind = PackIconKind.Check, Foreground = Brushes.Green },
                                };

                                treeview.IsExpanded = true;
                                if (txtObs.Text == string.Empty)
                                {
                                    treeview.Header = new StackPanel
                                    {
                                        Orientation = Orientation.Horizontal,
                                        Children = {
                                            new TextBlock { Text = componentObj.Component.Replace("|", "") + " | " + itemsService.GetDescriptionItem(componentObj.Component).Replace("|", "") + " | Quantidade: " + componentObj.Qty + " | Valor R$: " + componentObj.Costvalue + " | Imagem= " + componentObj.PathFile1.Replace("|", "") + " | Custo R1: " + componentObj.R1Costvalue + " | Desenho: " + componentObj.DrawingComponent },
                                            btn
                                        }
                                    };
                                }
                                else
                                {
                                    treeview.Header = new StackPanel
                                    {
                                        Orientation = Orientation.Horizontal,
                                        Children = {
                                            new TextBlock { Text = componentObj.Component.Replace("|", "") + " | " + itemsService.GetDescriptionItem(componentObj.Component).Replace("|", "") + " | Quantidade: " + componentObj.Qty + " | Valor R$: " + componentObj.Costvalue + " | OBS: " + componentObj.Obs.Replace("|", "") + " | Imagem= " + componentObj.PathFile1.Replace("|", "") + " | Custo R1: " + componentObj.R1Costvalue + " | Desenho: " + componentObj.DrawingComponent },
                                            btn
                                        }
                                    };
                                }


                                if (treeview.Items.Count == 0)
                                {

                                    if (treeview.Name == "treeViewLv1")
                                    {
                                        treeview.Items.Add(CompControls());
                                        treeview.Items.Add(OpControls());

                                    }
                                    else if (treeview.Name == "tvLevel2")
                                    {
                                        treeview.Items.Add(CompControls2());
                                        treeview.Items.Add(OpControls2());
                                    }
                                }

                                this.Close();
                            }
                        }
                    }
                }
                else
                {
                    if (txtItem.Text != string.Empty)
                    {
                        txtCostValue.Text = CalculateCostValue().ToString();
                        GetThisComponent();
                        if (_mainWindow.level1 == true)
                        {
                            FillLevel1();
                        }
                        else if (_mainWindow.level2 == true)
                        {
                            FillLevel2();
                        }
                        else if (_mainWindow.level3 == true)
                        {
                            FillLevel3();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (isEditmode == true)
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

        private void TxtQty_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void FillLevel1()
        {
            if (itemsService.GetNatureItem(txtItem.Text) == 22413314)
            {
                TreeViewItem chilItem = new TreeViewItem();
                chilItem.Name = "treeViewLv1";
                if (txtObs.Text == string.Empty)
                {
                    chilItem.Header = componentObj.Component.Replace("|", "") + " | " + componentObj.Description.Replace("|", "") + " | Quantidade: " + componentObj.Qty + " | Valor R$: " + componentObj.Costvalue + " | Imagem= " + componentObj.PathFile1.Replace("|", "") + " | Custo R1: " + componentObj.R1Costvalue + " | Desenho: " + componentObj.DrawingComponent;
                }
                else
                {
                    chilItem.Header = componentObj.Component.Replace("|", "") + " | " + componentObj.Description.Replace("|", "") + " | Quantidade: " + componentObj.Qty + " | Valor R$: " + componentObj.Costvalue + " | OBS: " + componentObj.Obs.Replace("|", "") + " | Imagem= " + componentObj.PathFile1.Replace("|", "") + " | Custo R1: " + componentObj.R1Costvalue + " | Desenho: " + componentObj.DrawingComponent;
                }
                _mainWindow.tvComponent.Items.Add(chilItem);
                this.Close();
            }
            else if (itemsService.GetNatureItem(txtItem.Text) == 22413312)
            {
                Button btn = new Button
                {
                    Name = "btnComponent1",
                    Cursor = Cursors.Hand,
                    Background = null,
                    BorderBrush = null,
                    Content = new PackIcon { Kind = PackIconKind.Check, Foreground = Brushes.Green },
                };

                if (txtObs.Text == string.Empty)
                {
                    TreeViewItem chilItem = new TreeViewItem
                    {
                        Name = "treeViewLv1",
                        IsExpanded = true,
                        Header = new StackPanel
                        {
                            Orientation = Orientation.Horizontal,
                            Children = {
                                new TextBlock { Text = componentObj.Component.Replace("|", "") + " | " + componentObj.Description.Replace("|","") + " | Quantidade: " + componentObj.Qty + " | Valor R$: " + componentObj.Costvalue + " | Imagem= " + componentObj.PathFile1.Replace("|", "") + " | Custo R1: " + componentObj.R1Costvalue + " | Desenho: " + componentObj.DrawingComponent },
                                btn
                            }
                        }

                    };

                    chilItem.Items.Add(CompControls());
                    chilItem.Items.Add(OpControls());

                    _mainWindow.tvComponent.Items.Add(chilItem);
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
                                new TextBlock { Text = componentObj.Component.Replace("|", "") + " | " + componentObj.Description.Replace("|","") + " | Quantidade: " + componentObj.Qty + " | Valor R$: " + componentObj.Costvalue + " | OBS: " + componentObj.Obs.Replace("|", "") + " | Imagem= " + componentObj.PathFile1.Replace("|", "") + " | Custo R1: " + componentObj.R1Costvalue + " | Desenho: " + componentObj.DrawingComponent },
                                btn
                            }
                        }

                    };

                    chilItem.Items.Add(CompControls());
                    chilItem.Items.Add(OpControls());

                    _mainWindow.tvComponent.Items.Add(chilItem);
                }

                this.Close();
            }
            else
            {
                TreeViewItem chilItem = new TreeViewItem();
                chilItem.Name = "treeViewLv1";
                if (txtObs.Text == string.Empty)
                {
                    chilItem.Header = componentObj.Component.Replace("|", "") + " | " + componentObj.Description.Replace("|", "") + " | Quantidade: " + componentObj.Qty + " | Valor R$: " + componentObj.Costvalue + " | Imagem= " + componentObj.PathFile1.Replace("|", "") + " | Custo R1: " + componentObj.R1Costvalue + " | Desenho: " + componentObj.DrawingComponent;
                }
                else
                {
                    chilItem.Header = componentObj.Component.Replace("|", "") + " | " + componentObj.Description.Replace("|", "") + " | Quantidade: " + componentObj.Qty + " | Valor R$: " + componentObj.Costvalue + " | OBS: " + componentObj.Obs.Replace("|", "") + " | Imagem= " + componentObj.PathFile1.Replace("|", "") + " | Custo R1: " + componentObj.R1Costvalue + " | Desenho: " + componentObj.DrawingComponent;
                }
                _mainWindow.tvComponent.Items.Add(chilItem);
                this.Close();
            }
        }

        private void FillLevel2()
        {
            if (itemsService.GetNatureItem(txtItem.Text) == 22413314)
            {
                TreeViewItem viewItem = _mainWindow.tvBOM.SelectedItem as TreeViewItem;
                TreeViewItem chilItem = new TreeViewItem();
                chilItem.Name = "tvLevel2";
                if (txtObs.Text == string.Empty)
                {
                    chilItem.Header = componentObj.Component.Replace("|", "") + " | " + componentObj.Description.Replace("|", "") + " | Quantidade: " + componentObj.Qty + " | Valor R$: " + componentObj.Costvalue + " | Imagem= " + componentObj.PathFile1.Replace("|", "") + " | Custo R1: " + componentObj.R1Costvalue + " | Desenho: " + componentObj.DrawingComponent;
                }
                else
                {
                    chilItem.Header = componentObj.Component.Replace("|", "") + " | " + componentObj.Description.Replace("|", "") + " | Quantidade: " + componentObj.Qty + " | Valor R$: " + componentObj.Costvalue + " | OBS: " + componentObj.Obs.Replace("|", "") + " | Imagem= " + componentObj.PathFile1.Replace("|", "") + " | Custo R1: " + componentObj.R1Costvalue + " | Desenho: " + componentObj.DrawingComponent;
                }
                viewItem.Items.Add(chilItem);
                this.Close();
            }
            else if (itemsService.GetNatureItem(txtItem.Text) == 22413312)
            {
                TreeViewItem viewItem = _mainWindow.tvBOM.SelectedItem as TreeViewItem;
                Button btn = new Button
                {
                    Name = "btnComponent2",
                    Cursor = Cursors.Hand,
                    Background = null,
                    BorderBrush = null,
                    Content = new PackIcon { Kind = PackIconKind.Check, Foreground = Brushes.Green },
                };
                if (txtObs.Text == string.Empty)
                {
                    TreeViewItem chilItem = new TreeViewItem
                    {
                        Name = "tvLevel2",
                        IsExpanded = true,
                        Header = new StackPanel
                        {
                            Orientation = Orientation.Horizontal,
                            Children = {
                                new TextBlock { Text = componentObj.Component.Replace("|", "") + " | " + componentObj.Description.Replace("|","") + " | Quantidade: " + componentObj.Qty + " | Valor R$: " + componentObj.Costvalue + " | Imagem= " + componentObj.PathFile1.Replace("|", "") + " | Custo R1: " + componentObj.R1Costvalue + " | Desenho: " + componentObj.DrawingComponent },
                                btn
                            }
                        }

                    };

                    chilItem.Items.Add(CompControls2());
                    chilItem.Items.Add(OpControls2());

                    viewItem.Items.Add(chilItem);
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
                                new TextBlock { Text = componentObj.Component.Replace("|", "") + " | " + componentObj.Description.Replace("|","") + " | Quantidade: " + componentObj.Qty + " | Valor R$: " + componentObj.Costvalue + " | OBS: " + componentObj.Obs.Replace("|", "") + " | Imagem= " + componentObj.PathFile1.Replace("|", "") + " | Custo R1: " + componentObj.R1Costvalue + " | Desenho: " + componentObj.DrawingComponent },
                                btn
                            }
                        }

                    };

                    chilItem.Items.Add(CompControls2());
                    chilItem.Items.Add(OpControls2());

                    viewItem.Items.Add(chilItem);
                }

                this.Close();
            }
            else
            {
                TreeViewItem viewItem = _mainWindow.tvBOM.SelectedItem as TreeViewItem;
                TreeViewItem chilItem = new TreeViewItem();
                chilItem.Name = "tvLevel2";
                if (txtObs.Text == string.Empty)
                {
                    chilItem.Header = componentObj.Component.Replace("|", "") + " | " + componentObj.Description.Replace("|", "") + " | Quantidade: " + componentObj.Qty + " | Valor R$: " + componentObj.Costvalue + " | Imagem= " + componentObj.PathFile1.Replace("|", "") + " | Custo R1: " + componentObj.R1Costvalue + " | Desenho: " + componentObj.DrawingComponent;
                }
                else
                {
                    chilItem.Header = componentObj.Component.Replace("|", "") + " | " + componentObj.Description.Replace("|", "") + " | Quantidade: " + componentObj.Qty + " | Valor R$: " + componentObj.Costvalue + " | OBS: " + componentObj.Obs.Replace("|", "") + " | Imagem= " + componentObj.PathFile1.Replace("|", "") + " | Custo R1: " + componentObj.R1Costvalue + " | Desenho: " + componentObj.DrawingComponent;
                }
                viewItem.Items.Add(chilItem);
                this.Close();
            }
        }

        private void FillLevel3()
        {
            if (itemsService.GetNatureItem(txtItem.Text) == 22413314)
            {
                TreeViewItem viewItem = _mainWindow.tvBOM.SelectedItem as TreeViewItem;
                TreeViewItem chilItem = new TreeViewItem();
                chilItem.Name = "tvLevel3";
                if (txtObs.Text == string.Empty)
                {
                    chilItem.Header = componentObj.Component.Replace("|", "") + " | " + componentObj.Description.Replace("|", "") + " | Quantidade: " + componentObj.Qty + " | Valor R$: " + componentObj.Costvalue + " | Imagem= " + componentObj.PathFile1.Replace("|", "") + " | Custo R1: " + componentObj.R1Costvalue + " | Desenho: " + componentObj.DrawingComponent;
                }
                else
                {
                    chilItem.Header = componentObj.Component.Replace("|", "") + " | " + componentObj.Description.Replace("|", "") + " | Quantidade: " + componentObj.Qty + " | Valor R$: " + componentObj.Costvalue + " | OBS: " + componentObj.Obs.Replace("|", "") + " | Imagem= " + componentObj.PathFile1.Replace("|", "") + " | Custo R1: " + componentObj.R1Costvalue + " | Desenho: " + componentObj.DrawingComponent;
                }
                viewItem.Items.Add(chilItem);
                this.Close();
            }
            else if (itemsService.GetNatureItem(txtItem.Text) == 22413312)
            {
                TreeViewItem viewItem = _mainWindow.tvBOM.SelectedItem as TreeViewItem;
                Button btn = new Button
                {
                    Name = "btnComponent2",
                    Cursor = Cursors.Hand,
                    Background = null,
                    BorderBrush = null,
                    Content = new PackIcon { Kind = PackIconKind.Check, Foreground = Brushes.Green },
                };
                if (txtObs.Text == string.Empty)
                {
                    TreeViewItem chilItem = new TreeViewItem
                    {
                        Name = "tvLevel3",
                        IsExpanded = true,
                        Header = new StackPanel
                        {
                            Orientation = Orientation.Horizontal,
                            Children = {
                                new TextBlock { Text = componentObj.Component.Replace("|", "") + " | " + componentObj.Description.Replace("|","") + " | Quantidade: " + componentObj.Qty + " | Valor R$: " + componentObj.Costvalue + " | Imagem= " + componentObj.PathFile1.Replace("|", "") + " | Custo R1: " + componentObj.R1Costvalue + " | Desenho: " + componentObj.DrawingComponent },
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
                                new TextBlock { Text = componentObj.Component.Replace("|", "") + " | " + componentObj.Description.Replace("|","") + " | Quantidade: " + componentObj.Qty + " | Valor R$: " + componentObj.Costvalue + " | OBS: " + componentObj.Obs.Replace("|", "") + " | Imagem= " + componentObj.PathFile1.Replace("|", "") + " | Custo R1: " + componentObj.R1Costvalue + " | Desenho: " + componentObj.DrawingComponent },
                                btn
                            }
                        }

                    };

                    viewItem.Items.Add(chilItem);
                }

                this.Close();
            }
            else
            {
                TreeViewItem viewItem = _mainWindow.tvBOM.SelectedItem as TreeViewItem;
                TreeViewItem chilItem = new TreeViewItem();
                chilItem.Name = "tvLevel3";
                if (txtObs.Text == string.Empty)
                {
                    chilItem.Header = componentObj.Component.Replace("|", "") + " | " + componentObj.Description.Replace("|", "") + " | Quantidade: " + componentObj.Qty + " | Valor R$: " + componentObj.Costvalue + " | Imagem= " + componentObj.PathFile1.Replace("|", "") + " | Custo R1: " + componentObj.R1Costvalue + " | Desenho: " + componentObj.DrawingComponent;
                }
                else
                {
                    chilItem.Header = componentObj.Component.Replace("|", "") + " | " + componentObj.Description.Replace("|", "") + " | Quantidade: " + componentObj.Qty + " | Valor R$: " + componentObj.Costvalue + " | OBS: " + componentObj.Obs.Replace("|", "") + " | Imagem= " + componentObj.PathFile1.Replace("|", "") + " | Custo R1: " + componentObj.R1Costvalue + " | Desenho: " + componentObj.DrawingComponent;
                }
                viewItem.Items.Add(chilItem);
                this.Close();
            }
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

        private TreeViewItem CompControls3()
        {

            Button btn = new Button
            {
                Name = "btnComponent3",
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
                    Name = "StackPanelComp3",
                    Orientation = Orientation.Horizontal,
                    Children = {
                                new Label { Content = "-Componente nv.3", Foreground = Brushes.DarkCyan, FontWeight = FontWeights.Bold },
                                btn
                    }
                }

            };

            return chilItemComp;
        }

        private TreeViewItem OpControls3()
        {
            Button btn = new Button
            {
                Name = "btnOperation3",
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
                    Name = "StackPanelOp3",
                    Orientation = Orientation.Horizontal,
                    Children = {
                                new Label { Content = "-Operação nv.3", Width = 95, Foreground = Brushes.DarkCyan, FontWeight = FontWeights.Bold },
                                btn
                    }
                }

            };

            return chilItemOp;
        }

        private void BtnDrawing_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Todos os arquivos (*.*)|*.*";
                dialog.Title = "Selecione um arquivo";

                Nullable<bool> result = dialog.ShowDialog();
                if (result == true)
                {
                    txtDrawing.Text = System.IO.Path.GetFileName(dialog.FileName);
                    longPath = dialog.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " - " + ex.StackTrace, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void VisibleIsSpecific()
        {
            cbFormatItem.Visibility = Visibility.Visible;
            txtResultValue.Visibility = Visibility.Visible;
        }

        private void NotVisibleIsSpecific()
        {
            cbFormatItem.Visibility = Visibility.Hidden;
            txtResultValue.Visibility = Visibility.Hidden;
        }

        private void CbFormatItem_DropDownClosed(object sender, EventArgs e)
        {
            if(cbFormatItem.Text != string.Empty)
            {
                if (txtQty.Text == "")
                {
                    MessageBox.Show("Insira uma quantidade para o calculo!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                CalculatorR1View window = new CalculatorR1View(this);
                window.Show();
            }
        }

        private void BtnDrawingComponent_Click(object sender, RoutedEventArgs e)
        {
            DrawingView window = new DrawingView(this);
            window.Show();
        }

        private double CalculateCostValue()
        {
            double qtd = 0;
            double? standardcost;
            double baseUoMQty = 0;
            if (string.IsNullOrEmpty(txtQty.Text))
            {
                qtd = 0;
            }
            else
            {
                qtd = Convert.ToDouble(txtQty.Text);
            }

            if (string.IsNullOrEmpty(txtItem.Text))
            {
                standardcost = 0;
                baseUoMQty = 0;
            }
            else
            {
                standardcost = Convert.ToDouble(txtStandardCost.Text);
                baseUoMQty = itemsService.GetBaseUoMQty(txtItem.Text);
            }

            

            return Math.Round((Convert.ToDouble(standardcost) / baseUoMQty) * qtd, 2);
        }

        private void TxtStandardCost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtFilterDescription_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtFilterDescription.Text != string.Empty)
            {
                dgItems.ItemsSource = null;
                dgItems.ItemsSource = itemsService.GetAll(txtFilterDescription.Text);
            }
            else
            {
                LoadGrid();
            }
        }

        private void btnDefaultOBS_Click(object sender, RoutedEventArgs e)
        {
            ObsListMaterialsView window = new ObsListMaterialsView(this);
            window.ShowDialog();
        }

        private void btnQualityControl_Click(object sender, RoutedEventArgs e)
        {
            QualityControlView window = new QualityControlView(this);
            window.ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (componentObj != null) componentObj = null;
        }

        private void GetThisComponent()
        {
            try
            {
                componentObj.Component = txtItem.Text;
                componentObj.Description = description;
                componentObj.Qty = string.IsNullOrEmpty(txtQty.Text) ? 0 : Convert.ToDouble(txtQty.Text);
                componentObj.Costvalue = string.IsNullOrEmpty(txtCostValue.Text) ? 0 : Convert.ToDouble(txtCostValue.Text);
                componentObj.Obs = txtObs.Text;
                componentObj.DrawingComponent = txtDrawingComponent.Text;
                componentObj.PathFile1 = longPath;
                componentObj.R1Costvalue = string.IsNullOrEmpty(txtResultValue.Text) ? 0 : Convert.ToDouble(txtResultValue.Text);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
