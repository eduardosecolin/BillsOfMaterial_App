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
    /// Lógica interna para LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {

        private readonly UserManagerService usService;


        public static string userLogon;
        public static string pwdLogon;

        public LoginView()
        {
            InitializeComponent();
            usService = new UserManagerService();
        }

        private bool Validate()
        {
            if (txtUser.Text == string.Empty)
            {
                MessageBox.Show("Insira o usuário!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (txtPassword.Password == string.Empty)
            {
                MessageBox.Show("Insira a senha!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void btnLogon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Access();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.InnerException.Message + "\n" + ex.StackTrace, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Access()
        {
            try
            {
                if (Validate())
                {
                    if (usService.ExistData(txtUser.Text, txtPassword.Password))
                    {
                        userLogon = txtUser.Text;
                        pwdLogon = txtPassword.Password;

                        int typeUser = usService.GetTypeUser(txtUser.Text, txtPassword.Password);
                        if (typeUser == 2057371649 || typeUser == 2057371650)
                        {
                            Menu window = new Menu();
                            window.Show();
                        }
                        else
                        {
                            ChooseDataBaseView window = new ChooseDataBaseView();
                            window.Show();
                        }

                        this.Hide();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Dados invalidos!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                        Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Clear()
        {
            txtUser.Text = "";
            txtPassword.Password = "";
        }

        private void txtPassword_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    Access();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.InnerException.Message + "\n" + ex.StackTrace, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
