﻿using System;
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
using System.Windows.Threading;

namespace BillsOfMaterial_App
{
    /// <summary>
    /// Lógica interna para SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        private const int TEMP = 4000;

        public SplashScreen()
        {
            InitializeComponent();
        }

        private delegate void ProgressBarDelegate();

        private void CreateConstruction()
        {
            PB.IsIndeterminate = false;
            PB.Maximum = TEMP;
            PB.Value = 0;

            for (int i = 0; i < TEMP; i++)
            {
                PB.Dispatcher.Invoke(new ProgressBarDelegate(UpdateProgress), DispatcherPriority.Background);
            }
        }

        private void UpdateProgress()
        {
            PB.Value += 1;
        }

        private void LoadprogressBar()
        {
            CreateConstruction();
            try
            {
                MainWindow window = new MainWindow();
                window.Show();
                this.Hide();
                Close();
            }
            catch (Exception a)
            {
                MessageBox.Show("Erro",
                                a.Message,
                                MessageBoxButton.OK,
                                MessageBoxImage.Error
                );
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            LoadprogressBar();
        }
    }
}