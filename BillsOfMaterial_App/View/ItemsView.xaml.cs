using BillsOfMaterial_App.Model;
using BillsOfMaterial_App.Service;
using MaterialDesignThemes.Wpf;
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
        MainWindow _mainWindow;
        ItemsService itemsService;
        private string description = string.Empty;
        public bool isEditmode = false;
        private bool isException = false;

        public ItemsView(MainWindow window)
        {
            InitializeComponent();
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

                    if (itemsService.IsSpecificItem(items.Item))
                    {
                        VisibleIsSpecific();
                    }
                    else
                    {
                        NotVisibleIsSpecific();
                    }
                }
            }
        }

        private void TxtFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtFilter.Text != string.Empty)
            {
                dgItems.ItemsSource = null;
                dgItems.ItemsSource = itemsService.GetAll(txtFilter.Text);
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
                if (isEditmode)
                {
                    if (txtItem.Text != string.Empty)
                    {
                        var treeview = _mainWindow.tvBOM.SelectedItem as TreeViewItem;
                        if (treeview != null)
                        {
                            if (itemsService.GetNatureItem(txtItem.Text) == 22413314)
                            {
                                if (txtObs.Text == string.Empty)
                                {
                                    treeview.Header = txtItem.Text.Replace("-", "") + " - " + itemsService.GetDescriptionItem(txtItem.Text).Replace("-", "") + "- Quantidade: " + txtQty.Text + " - Desenho: " + txtDrawing.Text.Replace("-", "") + " - Custo R1: " + txtResultValue.Text;
                                }
                                else
                                {
                                    treeview.Header = txtItem.Text.Replace("-", "") + " - " + itemsService.GetDescriptionItem(txtItem.Text).Replace("-", "") + " - Quantidade: " + txtQty.Text + " - OBS: " + txtObs.Text.Replace("-", "") + " - Desenho: " + txtDrawing.Text.Replace("-", "") + " - Custo R1: " + txtResultValue.Text;
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
                                            new TextBlock { Text = txtItem.Text.Replace("-", "") + " - " + itemsService.GetDescriptionItem(txtItem.Text).Replace("-", "") + " - Quantidade: " + txtQty.Text + " - Desenho: " + txtDrawing.Text.Replace("-", "") + " - Custo R1: " + txtResultValue.Text },
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
                                            new TextBlock { Text = txtItem.Text.Replace("-", "") + " - " + itemsService.GetDescriptionItem(txtItem.Text).Replace("-", "") + " - Quantidade: " + txtQty.Text + " - OBS: " + txtObs.Text.Replace("-", "") + " - Desenho: " + txtDrawing.Text.Replace("-", "") + " - Custo R1: " + txtResultValue.Text },
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
                    chilItem.Header = txtItem.Text.Replace("-", "") + " - " + description.Replace("-", "") + " - Quantidade: " + txtQty.Text + " - Desenho: " + txtDrawing.Text.Replace("-", "") + " - Custo R1: " + txtResultValue.Text;
                }
                else
                {
                    chilItem.Header = txtItem.Text.Replace("-", "") + " - " + description.Replace("-", "") + " - Quantidade: " + txtQty.Text + " - OBS: " + txtObs.Text.Replace("-", "") + " - Desenho: " + txtDrawing.Text.Replace("-", "") + " - Custo R1: " + txtResultValue.Text;
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
                                new TextBlock { Text = txtItem.Text.Replace("-", "") + " - " + description.Replace("-","") + " - Quantidade: " + txtQty.Text + " - Desenho: " + txtDrawing.Text.Replace("-", "") + " - Custo R1: " + txtResultValue.Text },
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
                                new TextBlock { Text = txtItem.Text.Replace("-", "") + " - " + description.Replace("-","") + " - Quantidade: " + txtQty.Text + " - OBS: " + txtObs.Text.Replace("-", "") + " - Desenho: " + txtDrawing.Text.Replace("-", "") + " - Custo R1: " + txtResultValue.Text },
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
                    chilItem.Header = txtItem.Text.Replace("-", "") + " - " + description.Replace("-", "") + " - Quantidade: " + txtQty.Text + " - Desenho: " + txtDrawing.Text.Replace("-", "") + " - Custo R1: " + txtResultValue.Text;
                }
                else
                {
                    chilItem.Header = txtItem.Text.Replace("-", "") + " - " + description.Replace("-", "") + " - Quantidade: " + txtQty.Text + " - OBS: " + txtObs.Text.Replace("-", "") + " - Desenho: " + txtDrawing.Text.Replace("-", "") + " - Custo R1: " + txtResultValue.Text;
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
                                new TextBlock { Text = txtItem.Text.Replace("-", "") + " - " + description.Replace("-","") + " - Quantidade: " + txtQty.Text + " - Desenho: " + txtDrawing.Text.Replace("-", "") + " - Custo R1: " + txtResultValue.Text },
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
                                new TextBlock { Text = txtItem.Text.Replace("-", "") + " - " + description.Replace("-","") + " - Quantidade: " + txtQty.Text + " - OBS: " + txtObs.Text.Replace("-", "") + " - Desenho: " + txtDrawing.Text.Replace("-", "") + " - Custo R1: " + txtResultValue.Text },
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
                    chilItem.Header = txtItem.Text.Replace("-", "") + " - " + description.Replace("-", "") + " - Quantidade: " + txtQty.Text + " - Desenho: " + txtDrawing.Text.Replace("-", "") + " - Custo R1: " + txtResultValue.Text;
                }
                else
                {
                    chilItem.Header = txtItem.Text.Replace("-", "") + " - " + description.Replace("-", "") + " - Quantidade: " + txtQty.Text + " - OBS: " + txtObs.Text.Replace("-", "") + " - Desenho: " + txtDrawing.Text.Replace("-", "") + " - Custo R1: " + txtResultValue.Text;
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
                                new TextBlock { Text = txtItem.Text.Replace("-", "") + " - " + description.Replace("-","") + " - Quantidade: " + txtQty.Text + " - Desenho: " + txtDrawing.Text.Replace("-", "") + " - Custo R1: " + txtResultValue.Text },
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
                                new TextBlock { Text = txtItem.Text.Replace("-", "") + " - " + description.Replace("-","") + " - Quantidade: " + txtQty.Text + " - OBS: " + txtObs.Text.Replace("-", "") + " - Desenho: " + txtDrawing.Text.Replace("-", "") + " - Custo R1: " + txtResultValue.Text },
                                btn
                            }
                        }

                    };

                    viewItem.Items.Add(chilItem);
                }

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
                DrawingView window = new DrawingView(this);
                window.Show();
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
                CalculatorR1View window = new CalculatorR1View(this);
                window.Show();
            }
        }
    }
}
