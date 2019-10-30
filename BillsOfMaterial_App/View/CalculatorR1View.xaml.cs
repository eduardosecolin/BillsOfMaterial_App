using BillsOfMaterial_App.Service;
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
    /// Lógica interna para CalculatorR1View.xaml
    /// </summary>
    public partial class CalculatorR1View : Window
    {
        ItemsView _window;
        ItemsService service;

        public CalculatorR1View(ItemsView window)
        {
            InitializeComponent();
            txtD1.Focus();
            _window = window;
            service = new ItemsService();
            EnabledFields();
        }

        #region Preview text input regex

        private void TxtD1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TxtD2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TxtD3_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TxtLength_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TxtSpecificWeight_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        #endregion

        private void ClearFields()
        {
            txtCostValue.Clear();
            txtSpecificWeight.Clear();
            txtTotalWeigth.Clear();
            txtTotalValue.Clear();
        }

        private bool Validate()
        {
            if (txtFormatItem.Text == "Bloco" || txtFormatItem.Text == "Tubo Redondo" || txtFormatItem.Text == "Tubo Sextavado")
            {
                if (txtD1.Text == string.Empty)
                {
                    MessageBox.Show("Insira a dimensão 1!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ClearFields();
                    return false;
                }
                if (txtD2.Text == string.Empty)
                {
                    MessageBox.Show("Insira a dimensão 2!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ClearFields();
                    return false;
                }
                if (txtLength.Text == string.Empty)
                {
                    MessageBox.Show("Insira o comprimento!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ClearFields();
                    return false;
                }
            }
            else if (txtFormatItem.Text == "Redondo" || txtFormatItem.Text == "Sextavado")
            {
                if (txtD1.Text == string.Empty)
                {
                    MessageBox.Show("Insira a dimensão 1!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ClearFields();
                    return false;
                }
                if (txtLength.Text == string.Empty)
                {
                    MessageBox.Show("Insira o comprimento!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ClearFields();
                    return false;
                }
            }
            else if (txtFormatItem.Text == "Tubo Retangular")
            {
                if (txtD1.Text == string.Empty)
                {
                    MessageBox.Show("Insira a dimensão 1!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ClearFields();
                    return false;
                }
                if (txtD2.Text == string.Empty)
                {
                    MessageBox.Show("Insira a dimensão 2!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ClearFields();
                    return false;
                }
                if (txtD3.Text == string.Empty)
                {
                    MessageBox.Show("Insira a dimensão 3!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ClearFields();
                    return false;
                }
                if (txtLength.Text == string.Empty)
                {
                    MessageBox.Show("Insira o comprimento!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ClearFields();
                    return false;
                }
            }

            return true;
        }

        private void EnabledFields()
        {
            if (_window.cbFormatItem.Text != string.Empty)
            {
                txtFormatItem.Text = _window.cbFormatItem.Text;
                txtCostValue.Text = service.GetStandardCost(_window.txtItem.Text).ToString();
                txtSpecificWeight.Text = service.GetSpecificWeight(_window.txtItem.Text).ToString();
                switch (txtFormatItem.Text)
                {
                    case "Bloco":
                        txtD3.IsEnabled = false;
                        imgR1.Source = new BitmapImage(new Uri(@"/Formação de Custo e Simulação de E.D.P;component/Images/bloco.png", UriKind.Relative));
                        break;
                    case "Redondo":
                        txtD2.IsEnabled = false;
                        txtD3.IsEnabled = false;
                        imgR1.Source = new BitmapImage(new Uri(@"/Formação de Custo e Simulação de E.D.P;component/Images/redondo.png", UriKind.Relative));
                        break;
                    case "Sextavado":
                        txtD2.IsEnabled = false;
                        txtD3.IsEnabled = false;
                        imgR1.Source = new BitmapImage(new Uri(@"/Formação de Custo e Simulação de E.D.P;component/Images/sextavado.png", UriKind.Relative));
                        break;
                    case "Tubo Redondo":
                        txtD3.IsEnabled = false;
                        imgR1.Source = new BitmapImage(new Uri(@"/Formação de Custo e Simulação de E.D.P;component/Images/tubo redondo.png", UriKind.Relative));
                        break;
                    case "Tubo Retangular":
                        imgR1.Source = new BitmapImage(new Uri(@"/Formação de Custo e Simulação de E.D.P;component/Images/tubo retangular.png", UriKind.Relative));
                        break;
                    case "Tubo Sextavado":
                        txtD3.IsEnabled = false;
                        imgR1.Source = new BitmapImage(new Uri(@"/Formação de Custo e Simulação de E.D.P;component/Images/tubo sextavado.png", UriKind.Relative));
                        break;
                }
            }
        }

        private void Calculate()
        {
            double d1 = 0; double d2 = 0; double d3 = 0; double lenght = 0;
            double costValue = 0; double specificWeight = 0; double totalWeight = 0; double totalValue = 0;

            d1 = (txtD1.Text != string.Empty) ? Convert.ToDouble(txtD1.Text) : 0;
            d2 = (txtD2.Text != string.Empty) ? Convert.ToDouble(txtD2.Text) : 0;
            d3 = (txtD3.Text != string.Empty) ? Convert.ToDouble(txtD3.Text) : 0;
            lenght = (txtLength.Text != string.Empty) ? Convert.ToDouble(txtLength.Text) : 0;

            costValue = (txtCostValue.Text != string.Empty) ? Convert.ToDouble(txtCostValue.Text) : 0;
            specificWeight = (txtSpecificWeight.Text != string.Empty) ? Convert.ToDouble(txtSpecificWeight.Text) : 0;

            if (txtFormatItem.Text != string.Empty)
            {
                switch (txtFormatItem.Text)
                {
                    case "Bloco":
                        totalWeight = (d1 * d2 * lenght * specificWeight) / 1000000;
                        totalValue = (totalWeight * costValue);
                        break;
                    case "Redondo":
                        totalWeight = ((d1 * d1 * 0.7854) * specificWeight * lenght) / 1000000;
                        totalValue = (totalWeight * costValue);
                        break;
                    case "Sextavado":
                        totalWeight = ((d1 * d1 * 0.866) * specificWeight * lenght) / 1000000;
                        totalValue = (totalWeight * costValue);
                        break;
                    case "Tubo Redondo":
                        totalWeight = (((d2 - d1) * d1 * 3.1416) * specificWeight * lenght) / 1000000;
                        totalValue = (totalWeight * costValue);
                        break;
                    case "Tubo Retangular":
                        totalWeight = (((d3 * d2) - (((d2 - d1) * 2) * ((d3 - d1) * 2))) * (specificWeight * lenght)) / 1000000;
                        totalValue = (totalWeight * costValue);
                        break;
                    case "Tubo Sextavado":
                        totalWeight = (((d2 - d1) * d1 * 3.464) * specificWeight * lenght) / 1000000;
                        totalValue = (totalWeight * costValue);
                        break;
                }
            }

            txtTotalWeigth.Text = totalWeight.ToString();
            txtTotalValue.Text = Math.Round(totalValue, 2).ToString();

        }

        private void TxtSpecificWeight_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                Calculate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " - " + ex.StackTrace, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnApply_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Validate())
                {
                    if (txtTotalValue.Text != string.Empty)
                    {
                        _window.txtResultValue.Text = txtTotalValue.Text;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Calculo sem resultado!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " - " + ex.StackTrace, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
