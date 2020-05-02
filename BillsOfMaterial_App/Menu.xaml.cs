using BillsOfMaterial_App.AuxView;
using BillsOfMaterial_App.Service;
using BillsOfMaterial_App.View;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BillsOfMaterial_App
{
    /// <summary>
    /// Lógica interna para Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        bool MenuClosed = false;
        UserManagerService userService;

        public Menu()
        {
            InitializeComponent();
            InitializeUserControl();
            userService = new UserManagerService();
        }

        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            if (MenuClosed)
            {
                Storyboard openMenu = (Storyboard)btnMenu.FindResource("OpenMenu");
                openMenu.Begin();
            }
            else
            {
                Storyboard closeMenu = (Storyboard)btnMenu.FindResource("CloseMenu");
                closeMenu.Begin();
            }

            MenuClosed = !MenuClosed;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Limpando a view atual
            UserControl usc = null;
            GridMain.Children.Clear();

            //Verificando qual tela será carregada
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "Main":
                    usc = new HomeView();
                    GridMain.Children.Add(usc);
                    break;
                case "EDPSimulation":
                    usc = new SimulationEDP();
                    GridMain.Children.Add(usc);
                    break;
                case "CostFormation":
                    usc = new HomeView();
                    GridMain.Children.Add(usc);
                    CostFormationView window = new CostFormationView();
                    window.ShowDialog();
                    window.Activate();
                    break;
                case "Settings":
                    usc = new SettingsView();
                    GridMain.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }

        private void InitializeUserControl()
        {
            UserControl usc = null;
            usc = new HomeView();
            GridMain.Children.Clear();
            GridMain.Children.Add(usc);
        }

        public void BlockMenu()
        {
            try
            {
                int typeUser = userService.GetTypeUser(LoginView.userLogon, LoginView.pwdLogon);
                if(typeUser == 2057371649)
                {
                    Settings.IsEnabled = false;
                    CostFormation.IsEnabled = false;
                    SimulationEDP.hasAccess = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.InnerException.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BlockMenu();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            SimulationEDP.UpdateOnOffFalse();
            for(int i = App.Current.Windows.Count - 1; i >= 0; i--)
            {
                App.Current.Windows[i].Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (SimulationEDP.isClear)
            {
                MessageBox.Show("Linpe a simulação para sair!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                e.Cancel = true;
                return;
            }
        }
    }
}
