using BillsOfMaterial_App.Model;
using BillsOfMaterial_App.Service;
using MaterialDesignThemes.Wpf;
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
    /// Lógica interna para ImportBOMView.xaml
    /// </summary>
    public partial class ImportBOMView : Window
    {
        MainWindow _mainWindow;
        BOMService bomService;
        ItemsService itemsService;
        OperationService opService;

        public ImportBOMView(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            bomService = new BOMService();
            itemsService = new ItemsService();
            opService = new OperationService();
            LoadGrid();
        }

        private void LoadGrid()
        {
            try
            {
                dgBOM.ItemsSource = bomService.GetBOM();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " - Erro ao consultar dados", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TxtFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtFilter.Text != string.Empty)
            {
                dgBOM.ItemsSource = null;
                dgBOM.ItemsSource = bomService.GetBOMByParams(txtFilter.Text);
            }
            else
            {
                LoadGrid();
            }
        }

        private void DgBOM_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dgBOM.SelectedItems.Count > 0)
                {
                    var bom = dgBOM.SelectedItem as MA_BillOfMaterials;
                    if (bom != null)
                    {
                        txtItem.Text = bom.BOM;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(txtItem.Text != string.Empty)
                {
                    _mainWindow.tvComponent.Items.Clear();
                    _mainWindow.tvOperation.Items.Clear();

                    // Components Level 1
                    foreach (var item in bomService.GetComponentsBOM(txtItem.Text))
                    {
                        TreeViewItem tvItem1 = FillLevel1(item.Component, item.Description.Replace(",",""), item.Qty.ToString().Replace(",","."), item.Notes);

                        #region Level 2

                        List<MA_BillOfMaterialsComp> listComp = bomService.GetComponentsBOM(item.Component);
                        List<MA_BillOfMaterialsRouting> listOp = bomService.GetOperationsBOM(item.Component);

                        if(listComp.Count > 0)
                        {
                            if (tvItem1.Name == "treeViewLv1")
                            {
                                foreach (var itemTv1 in tvItem1.Items)
                                {
                                    TreeViewItem isComp = itemTv1 as TreeViewItem;

                                    if(isComp.Header is StackPanel)
                                    {
                                        StackPanel panel1 = isComp.Header as StackPanel;
                                        if(panel1.Children.Count > 0)
                                        {
                                            foreach (var child in panel1.Children)
                                            {
                                                if(child is Label)
                                                {
                                                    Label lbl = child as Label;
                                                    if(lbl.Content.ToString().Equals("Componente nv.2"))
                                                    {
                                                        foreach (var comp2 in listComp)
                                                        {
                                                            TreeViewItem tvItem2 = FillLevel2(comp2.Component, comp2.Description.Replace(",", ""), comp2.Qty.ToString().Replace(",","."), item.Notes, isComp);

                                                            #region Level 3

                                                            List<MA_BillOfMaterialsComp> listComp2 = bomService.GetComponentsBOM(comp2.Component);
                                                            List<MA_BillOfMaterialsRouting> listOp2 = bomService.GetOperationsBOM(comp2.Component);

                                                            if(listComp2.Count > 0)
                                                            {
                                                                if(tvItem2.Name == "tvLevel2")
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
                                                                                                FillLevel3(comp3.Component, comp3.Description.Replace(",", ""), comp3.Qty.ToString().Replace(",","."), item.Notes, isComp2);
                                                                                            }                             
                                                                                        }
                                                                                        else if(lbl2.Content.ToString().Equals("Operação nv.3"))
                                                                                        {
                                                                                            if (listOp2.Count > 0)
                                                                                            {
                                                                                                foreach (var comp3 in listOp2)
                                                                                                {
                                                                                                    FillLevel2(comp3.Operation, opService.GetDescriptionOp(comp3.Operation).Replace(",", ""), comp3.ProcessingTime, item.Notes, isComp2);
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
                                                                FillLevel2(comp2.Operation, opService.GetDescriptionOp(comp2.Operation).Replace(",", ""), comp2.ProcessingTime, item.Notes, isComp);
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
                    foreach (var item in bomService.GetOperationsBOM(txtItem.Text))
                    {
                        FillLevel1(item.Operation, opService.GetDescriptionOp(item.Operation).Replace(",", ""), item.ProcessingTime, item.Notes);
                    }

                    this.Close();

                }
                else
                {
                    MessageBox.Show("Escolha um item!", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao importar engenharia!" + "\n" + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private TreeViewItem FillLevel1(string item, string description, string qty, string obs)
        {
            if (itemsService.GetNatureItem(item) == 22413314)
            {
                TreeViewItem chilItem = new TreeViewItem();
                chilItem.Name = "treeViewLv1";
                if(obs == string.Empty)
                {
                    chilItem.Header = item.Replace("-", "") + " - " + description.Replace("-", "") + " - Quantidade: " + qty;
                }
                else
                {
                    chilItem.Header = item.Replace("-", "") + " - " + description.Replace("-", "") + " - Quantidade: " + qty + " - OBS: " + obs.Replace("-", "");
                }               
                _mainWindow.tvComponent.Items.Add(chilItem);
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
                if(obs == string.Empty)
                {
                    TreeViewItem chilItem = new TreeViewItem
                    {
                        Name = "treeViewLv1",
                        IsExpanded = true,
                        Header = new StackPanel
                        {
                            Orientation = Orientation.Horizontal,
                            Children = {
                                new TextBlock { Text = item.Replace("-", "") + " - " + description.Replace("-", "") + " - Quantidade: " + qty },
                                btn
                            }
                        }

                    };

                    chilItem.Items.Add(CompControls());
                    chilItem.Items.Add(OpControls());

                    _mainWindow.tvComponent.Items.Add(chilItem);

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
                                new TextBlock { Text = item.Replace("-", "") + " - " + description.Replace("-", "") + " - Quantidade: " + qty + " - OBS: " + obs.Replace("-", "") },
                                btn
                            }
                        }

                    };

                    chilItem.Items.Add(CompControls());
                    chilItem.Items.Add(OpControls());

                    _mainWindow.tvComponent.Items.Add(chilItem);

                    return chilItem;
                }
            }

            return null;
        }

        private TreeViewItem FillLevel2(string item, string description, string qty, string obs, TreeViewItem tvParam)
        {
            if (itemsService.GetNatureItem(item) == 22413314)
            {
                TreeViewItem viewItem = tvParam;
                TreeViewItem chilItem = new TreeViewItem();
                chilItem.Name = "tvLevel2";
                if (obs == string.Empty)
                {
                    chilItem.Header = item.Replace("-", "") + " - " + description.Replace("-", "") + " - Quantidade: " + qty;
                }
                else
                {
                    chilItem.Header = item.Replace("-", "") + " - " + description.Replace("-", "") + " - Quantidade: " + qty + " - OBS: " + obs.Replace("-", "");
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
                if(obs == string.Empty)
                {
                    TreeViewItem chilItem = new TreeViewItem
                    {
                        Name = "tvLevel2",
                        IsExpanded = true,
                        Header = new StackPanel
                        {
                            Orientation = Orientation.Horizontal,
                            Children = {
                                new TextBlock { Text = item.Replace("-", "") + " - " + description.Replace("-", "") + "- Quantidade: " + qty },
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
                                new TextBlock { Text = item.Replace("-", "") + " - " + description.Replace("-", "") + "- Quantidade: " + qty + " - OBS: " + obs.Replace("-", "") },
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

            return null;
        }

        private void FillLevel3(string item, string description, string qty, string obs, TreeViewItem tvParam)
        {
            if (itemsService.GetNatureItem(item) == 22413314)
            {
                TreeViewItem viewItem = tvParam;
                TreeViewItem chilItem = new TreeViewItem();
                chilItem.Name = "tvLevel3";
                if(obs == string.Empty)
                {
                    chilItem.Header = item.Replace("-", "") + " - " + description.Replace("-", "") + "- Quantidade: " + qty;
                }
                else
                {
                    chilItem.Header = item.Replace("-", "") + " - " + description.Replace("-", "") + "- Quantidade: " + qty + " - OBS: " + obs.Replace("-", "");
                }
                viewItem.Items.Add(chilItem);
                this.Close();
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
                if(obs == string.Empty)
                {
                    TreeViewItem chilItem = new TreeViewItem
                    {
                        Name = "tvLevel3",
                        IsExpanded = true,
                        Header = new StackPanel
                        {
                            Orientation = Orientation.Horizontal,
                            Children = {
                                new TextBlock { Text = item.Replace("-", "") + " - " + description.Replace("-", "") + "- Quantidade: " + qty },
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
                                new TextBlock { Text = item.Replace("-", "") + " - " + description.Replace("-", "") + "- Quantidade: " + qty + " - OBS: " + obs.Replace("-", "") },
                                btn
                            }
                        }

                    };

                    viewItem.Items.Add(chilItem);
                }
            }
        }

        private void FillLevel1(string op, string description, int? timeProcess, string obs)
        {
            string timeString = timeProcess.ToString() + "000";
            double timeDouble = Convert.ToDouble(timeString);
            double time = TimeSpan.FromMilliseconds(timeDouble).TotalHours;
            string timeStr = TimeSpan.FromHours(time).ToString("h\\:mm");
            if(timeStr.Length == 4) { timeStr = "0" + timeStr; }
            TreeViewItem chilItem = new TreeViewItem();
            chilItem.Name = "tvOpLevel1";
            if(obs == string.Empty)
            {
                chilItem.Header = op.Replace("-", "") + " - " + description.Replace("-", "") + "- " + "Tempo de Processamento = " + timeStr;
            }
            else
            {
                chilItem.Header = op.Replace("-", "") + " - " + description.Replace("-", "") + "- " + "Tempo de Processamento = " + timeStr + " - OBS: " + obs.Replace("-", "");
            }
            
            _mainWindow.tvOperation.Items.Add(chilItem);
        }

        private void FillLevel2(string op, string description, int? timeProcess, string obs, TreeViewItem tvOp2)
        {
            string timeString = timeProcess.ToString() + "000";
            double timeDouble = Convert.ToDouble(timeString);
            double time = TimeSpan.FromMilliseconds(timeDouble).TotalHours;
            string timeStr = TimeSpan.FromHours(time).ToString("h\\:mm");
            if (timeStr.Length == 4) { timeStr = "0" + timeStr; }
            TreeViewItem viewItem = tvOp2;
            TreeViewItem chilItem = new TreeViewItem();
            chilItem.Name = "tvOpLevel2";
            if (obs == string.Empty)
            {
                chilItem.Header = op.Replace("-", "") + " - " + description.Replace("-", "") + "- " + "Tempo de Processamento = " + timeStr;
            }
            else
            {
                chilItem.Header = op.Replace("-", "") + " - " + description.Replace("-", "") + "- " + "Tempo de Processamento = " + timeStr + " - OBS: " + obs.Replace("-", "");
            }
            viewItem.Items.Add(chilItem);
        }
    }
}
