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
    /// Lógica interna para RegisterItem.xaml
    /// </summary>
    public partial class RegisterItem : Window
    {
        public RegisterItem()
        {
            InitializeComponent();
            BlockFields();
        }

        private void BlockFields()
        {
            txtItem.IsEnabled = false;
            txtDescription.IsEnabled = false;
            txtNCM.IsEnabled = false;
            txtUoM.IsEnabled = false;
            txtStandardCost.IsEnabled = false;
            cbNature.IsEnabled = false;
        }

        private void EnabledFields()
        {
            txtItem.IsEnabled = true;
            txtDescription.IsEnabled = true;
            txtStandardCost.IsEnabled = true;
            cbNature.IsEnabled = true;
        }

        private void TxtStandardCost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
