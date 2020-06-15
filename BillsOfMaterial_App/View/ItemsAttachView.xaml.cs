using BillsOfMaterial_App.AuxView;
using BillsOfMaterial_App.Model;
using BillsOfMaterial_App.Service;
using Microsoft.Win32;
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
    /// Lógica interna para ItemsAttachView.xaml
    /// </summary>
    public partial class ItemsAttachView : Window
    {
        private SimulationEDP _window;
        private List<CS_CustQuotasCompAttach> listAttach;

        public ItemsAttachView(SimulationEDP window)
        {
            InitializeComponent();
            _window = window;
            listAttach = new List<CS_CustQuotasCompAttach>();
            VerifyDictionary();

        }

        private void btnInclude_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string resultDlg = GetPathAttach();

                if (!string.IsNullOrEmpty(resultDlg))
                {
                    CS_CustQuotasCompAttach attach = new CS_CustQuotasCompAttach();
                    attach.Attatchment = resultDlg;
                    attach.CustQuotasCompId = Convert.ToInt32(_window.lblCustQuotaId.Content);
                    listAttach.Add(attach);
                    if (_window.AttachMap.Count == 0)
                    {
                        _window.AttachMap.Add(Convert.ToInt32(_window.lblCustQuotaId.Content), listAttach);
                    }
                    else
                    {
                        _window.AttachMap[Convert.ToInt32(_window.lblCustQuotaId.Content)] = listAttach;
                    }
                    FillListView();
                }
                else
                {
                    MessageBox.Show("Nenhum arquivo selecionado!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar arquivo! descrição do erro: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GetPathAttach()
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Todos os arquivos (*.*)|*.*";
                dialog.Title = "Selecione um arquivo";

                Nullable<bool> result = dialog.ShowDialog();

                if (result == true)
                {
                    return dialog.FileName;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void FillListView()
        {
            try
            {
                lvAttach.ItemsSource = null;
                lvAttach.ItemsSource = listAttach;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void VerifyDictionary()
        {
            try
            {
                if (_window.AttachMap.Count > 0)
                {
                    if (_window.AttachMap.ContainsKey(Convert.ToInt32(_window.lblCustQuotaId.Content)))
                    {
                        listAttach = _window.AttachMap[Convert.ToInt32(_window.lblCustQuotaId.Content)];
                        FillListView();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(
                    "Erro: " + ex.Message,
                    "Erro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            if (lvAttach.SelectedItems.Count > 0)
            {
                CS_CustQuotasCompAttach attach = lvAttach.SelectedItem as CS_CustQuotasCompAttach;
                if (attach != null && listAttach.Count > 0)
                {
                    listAttach.RemoveAll(x => x.Attatchment == attach.Attatchment);
                    FillListView();
                }
            }
        }

        private void lvAttach_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if(lvAttach.SelectedItems.Count > 0)
                {
                    CS_CustQuotasCompAttach attach = lvAttach.SelectedItem as CS_CustQuotasCompAttach;
                    if(attach != null)
                    {
                        System.Diagnostics.Process.Start(attach.Attatchment);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
