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
using System.Windows.Shapes;

namespace BillsOfMaterial_App.View
{
    /// <summary>
    /// Lógica interna para ObsListMaterialsView.xaml
    /// </summary>
    public partial class ObsListMaterialsView : Window
    {
        private BOMNotesService service;
        private ItemsView _window;
        private string observation = string.Empty;

        public ObsListMaterialsView(ItemsView window)
        {
            InitializeComponent();
            service = new BOMNotesService();
            _window = window;
            LoadGrid();
        }

        private void LoadGrid()
        {
            try
            {
                dgLMObs.ItemsSource = null;
                if(txtFilter.Text != string.Empty)
                {
                    dgLMObs.ItemsSource = service.Filter(txtFilter.Text);
                }
                else
                {
                    dgLMObs.ItemsSource = service.GetAll();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao carregar grid! \n" + ex.Message,
                    "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            LoadGrid();
        }

        private void dgLMObs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(dgLMObs.SelectedItems.Count > 0)
            {
                var bomNote = dgLMObs.SelectedItem as MA_BillOfMaterialsNotes;
                if(bomNote != null)
                {
                    txtNoteCode.Text = bomNote.NoteCode;
                    observation = bomNote.Description;
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(txtNoteCode.Text != string.Empty)
                {
                    _window.txtObs.Text = observation;
                    this.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n" + ex.StackTrace,
                    "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
