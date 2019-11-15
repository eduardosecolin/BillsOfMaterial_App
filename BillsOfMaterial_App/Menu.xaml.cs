using BillsOfMaterial_App.AuxView;
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

        public Menu()
        {
            InitializeComponent();
            InitializeUserControl();
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
    }
}
