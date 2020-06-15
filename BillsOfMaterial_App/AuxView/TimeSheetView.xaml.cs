using BillsOfMaterial_App.Model;
using BillsOfMaterial_App.Service;
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

namespace BillsOfMaterial_App.AuxView
{
    /// <summary>
    /// Interação lógica para TimeSheetView.xam
    /// </summary>
    public partial class TimeSheetView : UserControl
    {
        private readonly TimeSheetService sheetService;
        private CS_TimeSheetPROD tm = new CS_TimeSheetPROD();

        public TimeSheetView()
        {
            InitializeComponent();
            sheetService = new TimeSheetService();
            lblUserLogin.Content = Menu.usermanager.UserName;
            FillListViewODP();
            FillListViewTM();
            FillListViewOPDDetails();
            CollectionView cView = CollectionViewSource.GetDefaultView(lsODP.ItemsSource) as CollectionView;
            cView.Filter = CustomFilter;
            CollectionView cView2 = CollectionViewSource.GetDefaultView(lsODPdETAILS.ItemsSource) as CollectionView;
            cView2.Filter = CustomFilterDetails;
            dpEndDate.IsEnabled = false;
        }

        private void FillListViewODP()
        {
            lsODP.ItemsSource = null;
            lsODP.ItemsSource = sheetService.getOPS();
        }

        private void FillListViewTM()
        {
            lsTimeSheet.ItemsSource = null;
            if((rbTMOpen.IsChecked == false || rbTMOpen.IsChecked == true) && rbTMRecent.IsChecked == false && rbTMAll.IsChecked == false)
            {
                lsTimeSheet.ItemsSource = sheetService.getTimeSheetOpen(Menu.usermanager.Id);
            }
            else if(rbTMOpen.IsChecked == false && rbTMRecent.IsChecked == true && rbTMAll.IsChecked == false)
            {
                lsTimeSheet.ItemsSource = sheetService.getTimeSheetRecent(Menu.usermanager.Id);
            }
            else if(rbTMOpen.IsChecked == false && rbTMRecent.IsChecked == false && rbTMAll.IsChecked == true)
            {
                lsTimeSheet.ItemsSource = sheetService.getTimeSheetAll(Menu.usermanager.Id);
            }
            
        }

        private void FillListViewOPDDetails()
        {
            lsODPdETAILS.ItemsSource = null;
            lsODPdETAILS.ItemsSource = sheetService.GetOPSDetails();
        }

        private bool CustomFilter(object obj)
        {
            if (string.IsNullOrEmpty(txtOdp.Text))
            {
                return true;
            }
            else
            {
                return ((obj as GenericObject).MONo.IndexOf(txtOdp.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        private bool CustomFilterDetails(object obj)
        {
            if (string.IsNullOrEmpty(txtOdp.Text))
            {
                return true;
            }
            else
            {
                return ((obj as GenericObjectDetails).MONo.IndexOf(txtOdp.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        private void lsODP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lsODP.SelectedItems.Count > 0)
            {
                GenericObject obj = lsODP.SelectedItem as GenericObject;
                if(obj != null)
                {
                    dpEndDate.IsEnabled = false;
                    txtOdp.Text = obj.MONo;
                    txtOdp.Focus();
                }
            }
        }

        private void txtOdp_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lsODP.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(lsODPdETAILS.ItemsSource).Refresh();
            dpEndDate.IsEnabled = false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InsertData();
            }
            catch (Exception ex)
            {

                MessageBox.Show
                    (
                        "Erro: " + ex.Message,
                        "Erro",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
            }
        }

        private void InsertData()
        {
            if (Validate())
            {
                if (chFinalized.IsChecked == false)
                {
                    if (string.IsNullOrEmpty(dpEndDate.Text))
                    {
                        tm.Line = sheetService.GetMaxLine();
                        tm.StartDate = dpStartDate.SelectedTime;
                        tm.MONo = txtOdp.Text;
                        tm.Phase = txtPhase.Text;
                        tm.UserIdOP = Menu.usermanager.Id;
                        tm.StatusODP = (tm.StatusODP == 20578307) ? 20578305 : tm.StatusODP;

                        sheetService.Insert(tm);

                        sheetService.UpdateODPStausProcessing((int)tm.MOId, (int)tm.RtgStep, tm.Phase);
                        MessageBox.Show("Dados inseridos com sucesso!", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                        Environment.Exit(0);
                    }
                    else
                    {
                        sheetService.UpdateTMFinalDate(tm.Id, tm.Line, (DateTime)dpEndDate.SelectedTime);
                        MessageBox.Show("Dados atualizados com sucesso!", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                        Environment.Exit(0);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(dpEndDate.Text)) 
                    { 
                        MessageBox.Show("Insira a data final!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning); 
                        return; 
                    }
                    sheetService.UpdateTMFinalDate(tm.Id, tm.Line, (DateTime)dpEndDate.SelectedTime);
                    sheetService.UpdateTMFinalized(tm.Id, "1", 20578306, tm.Line);
                    sheetService.UpdateODPStausFinalized((int)tm.MOId, (int)tm.RtgStep, tm.Phase);
                    MessageBox.Show("Dados atualizados com sucesso!", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                    Environment.Exit(0);
                }
            }
        }

        private bool Validate()
        {
            if(txtOdp.Text == string.Empty)
            {
                MessageBox.Show("Escolha a ordem de produção!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (txtOdp.Text == string.Empty)
            {
                MessageBox.Show("Escolha a fase!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (dpStartDate.Text == string.Empty)
            {
                MessageBox.Show("Insira o horario incial!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void FillObject(GenericObjectDetails obj)
        {
            tm.Id = obj.MOId;
            tm.MOId = obj.MOId;
            tm.RtgStep = obj.RtgStep;
            tm.StatusODP = obj.MOStatus;
        }

        private void lsODPdETAILS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lsODPdETAILS.SelectedItems.Count > 0)
            {
                GenericObjectDetails obj = lsODPdETAILS.SelectedItem as GenericObjectDetails;
                if(obj != null)
                {
                    dpEndDate.IsEnabled = false;
                    txtPhase.Text = obj.Operation;
                    txtOdp.Text = obj.MONo;
                    FillObject(obj);
                    txtOdp.Focus();
                }
            }
        }

        private void lsTimeSheet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rbTMAll.IsChecked == false && rbTMRecent.IsChecked == false)
            {
                if (lsTimeSheet.SelectedItems.Count > 0)
                {
                    CS_TimeSheetPROD timesheet = lsTimeSheet.SelectedItem as CS_TimeSheetPROD;
                    if (timesheet != null)
                    {
                        dpEndDate.IsEnabled = true;
                        dpStartDate.IsEnabled = false;
                        txtOdp.Text = timesheet.MONo;
                        txtPhase.Text = timesheet.Phase;
                        dpStartDate.SelectedTime = timesheet.StartDate;

                        tm.Id = timesheet.Id;
                        tm.Line = timesheet.Line;
                        tm.MOId = timesheet.MOId;
                        tm.MONo = timesheet.MONo;
                        tm.Phase = timesheet.Phase;
                        tm.RtgStep = timesheet.RtgStep;
                        tm.StartDate = timesheet.StartDate;
                        tm.StatusODP = timesheet.StatusODP;
                        tm.UserIdOP = Menu.usermanager.Id;
                        dpEndDate.IsEnabled = true;
                    }
                }
            }
            else
            {
                dpEndDate.IsEnabled = false;
                dpStartDate.IsEnabled = true;
            }
        }

        private void rbTMOpen_Checked(object sender, RoutedEventArgs e)
        {
            rbTMAll.IsChecked = false;
            rbTMRecent.IsChecked = false;
            FillListViewTM();
        }

        private void rbTMRecent_Checked(object sender, RoutedEventArgs e)
        {
            rbTMAll.IsChecked = false;
            rbTMOpen.IsChecked = false;
            FillListViewTM();
        }

        private void rbTMAll_Checked(object sender, RoutedEventArgs e)
        {
            rbTMRecent.IsChecked = false;
            rbTMOpen.IsChecked = false;
            FillListViewTM();
        }

        private void dpStartDate_MouseEnter(object sender, MouseEventArgs e)
        {
            if (dpStartDate.IsEnabled == true)
            {
                dpStartDate.IsEnabled = false;
                dpStartDate.SelectedTime = DateTime.Now;
            }
        }

        private void dpEndDate_MouseEnter(object sender, MouseEventArgs e)
        {
            if (dpEndDate.IsEnabled == true)
            {
                dpEndDate.IsEnabled = false;
                dpEndDate.SelectedTime = DateTime.Now;
            }
        }

        private void dpStartDate_GotFocus(object sender, RoutedEventArgs e)
        {
            if (dpStartDate.IsEnabled == true)
            {
                dpStartDate.IsEnabled = false;
                dpStartDate.SelectedTime = DateTime.Now;
            }
        }

        private void dpEndDate_GotFocus(object sender, RoutedEventArgs e)
        {
            if (dpEndDate.IsEnabled == true)
            {
                dpEndDate.IsEnabled = false;
                dpEndDate.SelectedTime = DateTime.Now;
            }
        }
    }
}
